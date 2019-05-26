using System;
using UnityEngine;
using System.Collections.Generic;

namespace Tasks
{
    public class TaskManager : MonoBehaviour
    {
        public static TaskManager instance => _instance;

        static TaskManager _instance;
        List<ITask> _taskList = new List<ITask>();
        List<ITask> _tasksForRemove = new List<ITask>();

        Action<TaskEventType,ITask> _onTaskChanged;

        public void Awake()
        {
            if (_instance != null)
            {
                Debug.LogError("Multiple TaskManagers");
                Destroy(this);
                return;
            }
            _instance = this;
            Initialize();
        }

        public void Initialize() // Todo: load tasks
        {
            _taskList = new List<ITask>();
            TurnController.TurnEndEvent += UpdateTasks;
        }

        void OnDestroy()
        {
            if (_instance == this)
                _instance = null;
        }

        public void OnTaskChangedSubscribe(Action<TaskEventType, ITask> callback)
        {
            _onTaskChanged += callback;
        }

        public void OnTaskChangedUnscribe(Action<TaskEventType, ITask> callback)
        {
            _onTaskChanged -= callback;
        }

        public List<ITask> GetTaskList()
        {
            return _taskList;
        }

        public ITask AddTask(TaskSettingsBase taskSettings)
        {
            ITask newTask = taskSettings.CreateNewTask();
            _taskList.Add(newTask);
            newTask.Initialize(taskSettings);
            _onTaskChanged?.Invoke(TaskEventType.Added, newTask);
            return newTask;
        }

        public void RemoveTask(ITask task)
        {
            _tasksForRemove.Add(task);
            _onTaskChanged?.Invoke(TaskEventType.Removed, task);
        }

        public void TaskUpdated(ITask task)
        {
            _onTaskChanged?.Invoke(TaskEventType.Changed, task);
        }

        public void TaskSuspended(ITask task)
        {
            _onTaskChanged?.Invoke(TaskEventType.Supended, task);
        }

        void UpdateTasks() // update every turn
        {
            foreach (var task in _tasksForRemove)
            {
                _taskList.Remove(task);
            }
            _tasksForRemove.Clear();
            foreach (ITask task in _taskList)
            {
                bool taskChanged = task.UpdateTask();
                if (taskChanged)
                    _onTaskChanged?.Invoke(TaskEventType.Changed, task);
            }
        }
    }

    public enum TaskEventType
    {
        Added,
        Changed,
        Supended,
        Removed
    }
}