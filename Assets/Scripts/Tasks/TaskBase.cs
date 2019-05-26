using PoolsManagement;
using UnityEngine;

namespace Tasks
{
    public abstract class TaskBase : ITask
    {
        protected int _progress;
        protected int _randomSeed;
        protected TaskSettingsBase _settings;

        public int GetProgress()
        {
            return _progress;
        }

        public virtual string GetTaskName()
        {
            return _settings.taskName;
        }

        public PoolSettingsSO GetShortInfoPoolSet()
        {
            return _settings.descriptionPoolSet;
        }

        public virtual PoolSettingsSO GetTaskWidgetPoolSet()
        {
            return _settings.taskWidgetPoolSet;
        }

        public virtual void Initialize(TaskSettingsBase settings)
        {
            _settings = settings;
            _randomSeed = System.Environment.TickCount;
            // Todo: Load actual values from file 
            // save progress, seed, setting key when it will implemented
        }

        public abstract bool UpdateTask();
        public abstract string GetTaskShortInfo();
        public abstract string GetTaskDescription();
    }
}

