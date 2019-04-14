using UnityEngine;
using PoolsManagement;

namespace Tasks
{
    public interface ITask
    {
        void Initialize(TaskSettingsBase settings);
        bool UpdateTask(); // parameters?
        int GetProgress();
        string GetTaskShortInfo();
        string GetTaskName();
        string GetTaskDescription();
        PoolSettingsSO GetShortInfoPoolSet();
        PoolSettingsSO GetTaskWidgetPoolSet();
    }
}

