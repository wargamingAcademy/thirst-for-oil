using UnityEngine;
using PoolsManagement;

namespace Tasks
{
    public abstract class TaskSettingsBase : ScriptableObject
    {
        public string taskName;
        public PoolSettingsSO descriptionPoolSet;
        public PoolSettingsSO taskWidgetPoolSet;

        public abstract ITask CreateNewTask();
    }
}

