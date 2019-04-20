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

        public void PlayerAnwer(int index) // Todo: resolve player answers
        {
            string anwerKey = _settings.progress[_progress].answerOptions[index].key;
            _progress++;
            if (_progress < _settings.progress.Length)
            {
                Debug.Log($"Option {index} with kew {anwerKey} answered to {GetTaskName()}. New Progress is {_progress}");
                TaskManager.instance.TaskUpdated(this);
            }
            else
            {
                Debug.Log($"Option {index} answered to {GetTaskName()}. Task {GetTaskName()} Done!");
                TaskManager.instance.RemoveTask(this);
            }
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