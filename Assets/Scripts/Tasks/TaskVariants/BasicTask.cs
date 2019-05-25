using PoolsManagement;
using UnityEngine;

namespace Tasks
{
    public class BasicTask : TaskBase
    {
        new protected BasicTaskSettings _settings;

        public override void Initialize(TaskSettingsBase settings)
        {
            base.Initialize(settings);
            _settings = (BasicTaskSettings)settings;
        }

        public virtual void PlayerAnwer(int index)
        {
            string anwerKey = _settings.progress[_progress].answerOptions[index].key;
            _progress++;
            if (_progress < _settings.progress.Length)
                TaskManager.instance.TaskUpdated(this);
            else
                TaskManager.instance.RemoveTask(this);
        }

        public bool IsAnswerOptionAllowed(int index)
        {
            return true; // Todo: check conditions
        }

        public AnswerOptionSet[] GetAnswerOptions()
        {
            return _settings.progress[_progress].answerOptions;
        }

        public override string GetTaskDescription()
        {
            return _settings.progress[_progress].description;
        }

        public override string GetTaskShortInfo()
        {
            return _settings.progress[_progress].shortDescription;
        }

        public override bool UpdateTask()
        {
            return true; // Todo: What exact do this task
        }
    }
}