using System.ComponentModel;

namespace PubSubWebUi.Services;

public class ProjectContext : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private readonly IList<string> _availableProjects;

    public ProjectContext(IConfiguration configuration)
    {
        var projectIds = configuration.GetValue<string>("GCP_PROJECT_IDS");
        _availableProjects = projectIds?.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
            .ToList() ?? ["test-project"];

        CurrentProject = _availableProjects[0];
    }

    public string CurrentProject
    {
        get;
        set
        {
            if (field != value)
            {
                field = value;
                PropertyChanged?.Invoke(this, new(nameof(CurrentProject)));
            }
        }
    }

    public IReadOnlyCollection<string> AvailableProjects => _availableProjects.ToList().AsReadOnly();

    public void AddProject(string projectId)
    {
        if (!_availableProjects.Contains(projectId))
        {
            _availableProjects.Add(projectId);
        }
    }
}