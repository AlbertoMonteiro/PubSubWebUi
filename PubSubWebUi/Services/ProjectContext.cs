using System;

namespace PubSubWebUi.Services;

public class ProjectContext
{
    public event Action? OnProjectChange;
    
    private string _currentProject = "test-project";
    
    public string CurrentProject
    {
        get => _currentProject;
        set
        {
            if (_currentProject != value)
            {
                _currentProject = value;
                OnProjectChange?.Invoke();
            }
        }
    }
}