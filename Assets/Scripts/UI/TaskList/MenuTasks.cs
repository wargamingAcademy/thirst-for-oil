using System.Collections.Generic;
using UnityEngine;
using Tasks;
using PoolsManagement;

public class MenuTasks : MonoBehaviour
{
#pragma warning disable CS0649
    [SerializeField]
    Transform _tasksSelector;
    [SerializeField]
    Transform _taskWidgetContainer;
#pragma warning restore CS0649

    int _selectedIndex = 0;
    BasicTaskWidget _taskWidget; // Todo: replace with interface

    Dictionary<ITask, TaskShortInfoWidget> _taskInfos = new Dictionary<ITask, TaskShortInfoWidget>();

    void OnEnable() // Todo: Replace to OnShow when menu manager will implemented
    {
        TaskManager taskManager = TaskManager.instance;
        taskManager.OnTaskChangedSubscribe(OnTaskChanged);

        List<ITask> tasks = taskManager.GetTaskList();
        ClearTasks();
        foreach (var task in tasks)
            _taskInfos.Add(task, CreateTaskWidget(_taskInfos.Count, task));

        ShowTask(_selectedIndex);
    }

    void OnDisable() // Todo: Replace to OnHide when menu manager will implemented
    {
        if (TaskManager.instance != null)
            TaskManager.instance.OnTaskChangedUnscribe(OnTaskChanged);
    }

    void OnTaskChanged(TaskEventType eventType, ITask task)
    {
        TaskShortInfoWidget taskInfo;
        switch (eventType)
        {
            case TaskEventType.Added:
                taskInfo = CreateTaskWidget(_taskInfos.Count, task);
                _taskInfos.Add(task, taskInfo);
                if (_taskWidget == null)
                    ShowTask(taskInfo.index);
                break;
            case TaskEventType.Changed:
                taskInfo = UpdateTaskWidget(task);
                if (_taskWidget != null && taskInfo.index == _selectedIndex)
                    _taskWidget.TaskUpdate(task);
                break;
            case TaskEventType.Removed:
                taskInfo = RemoveTaskWidget(task);
                _taskInfos.Remove(task);
                if (taskInfo.index == _selectedIndex)
                        ShowTask(_selectedIndex + ((_selectedIndex > 0) ? -1 : 1));
                break;
        }
    }

    public void ShowTask(int index)
    {
        if (index >= 0 && _taskInfos.Count > 0 && index < _taskInfos.Count)
        {
            foreach (KeyValuePair<ITask, TaskShortInfoWidget> kv in _taskInfos)
            {
                if (kv.Value.index != index)
                    continue;
                ShowTask(kv.Key);
                return;
            }
        }
        _selectedIndex = -1;
        HideTask();
    }

    public void ShowTask(ITask task)
    {
        HideTask();

        if (_taskInfos.TryGetValue(task, out TaskShortInfoWidget shortWidget))
            _selectedIndex = shortWidget.index;

        PoolSettingsSO taskWidgetPoolSet = task.GetTaskWidgetPoolSet();
        _taskWidget = taskWidgetPoolSet.GetNewObject(_taskWidgetContainer) as BasicTaskWidget;
        _taskWidget.TaskUpdate(task);

        _taskWidget.gameObject.SetActive(true); // Todo: run widget show animation
    }

    public void HideTask()
    {
        if (_taskWidget != null)
            _taskWidget.ReturnToPool();
    }

    void ClearTasks()
    {
        foreach (TaskShortInfoWidget taskInfo in _taskInfos.Values)
            taskInfo.ReturnToPool();
        _taskInfos.Clear();
    }

    TaskShortInfoWidget CreateTaskWidget(int index, ITask task)
    {
        PoolSettingsSO taskInfoWidgetPoolSet = task.GetShortInfoPoolSet();
        var taskShortInfoWidget = taskInfoWidgetPoolSet.GetNewObject(_tasksSelector) as TaskShortInfoWidget;

        taskShortInfoWidget.Initialize(index, task);
        taskShortInfoWidget.OnClickSubscribe(ShowTask);

        taskShortInfoWidget.gameObject.name = taskInfoWidgetPoolSet.prefab.name + "_" + index;
        taskShortInfoWidget.gameObject.SetActive(true); // Todo: run widget show animation
        return taskShortInfoWidget;
    }

    TaskShortInfoWidget UpdateTaskWidget(ITask task)
    {
        if (_taskInfos.TryGetValue(task, out TaskShortInfoWidget taskShortInfoWidget))
        {
            // Reinitialize widget in same place if it's objects key changed
            PoolSettingsSO taskInfoWidgetPoolSet = task.GetShortInfoPoolSet();
            if (taskInfoWidgetPoolSet.KeyHash != taskShortInfoWidget.GetPoolSet().KeyHash)
            {
                int childIndex = taskShortInfoWidget.transform.GetSiblingIndex();
                taskShortInfoWidget.ReturnToPool();
                taskShortInfoWidget = CreateTaskWidget(childIndex, task);
                taskShortInfoWidget.transform.SetSiblingIndex(childIndex);
                _taskInfos[task] = taskShortInfoWidget;
            }
            taskShortInfoWidget.InfoUpdate(task);
        }
        else
        {
            taskShortInfoWidget = CreateTaskWidget(_taskInfos.Count,task);
            _taskInfos.Add(task, taskShortInfoWidget);
        }
        return taskShortInfoWidget;
    }

    TaskShortInfoWidget RemoveTaskWidget(ITask task)
    {
        if (_taskInfos.TryGetValue(task, out TaskShortInfoWidget taskShortInfoWidget))
            taskShortInfoWidget.ReturnToPool();
        return taskShortInfoWidget;
    }
}
