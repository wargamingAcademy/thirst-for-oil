using UnityEngine;
using Tasks;

public class TaskTEster : MonoBehaviour
{
    public BasicTaskSettings taskToAdd;


    TaskManager taskManager;

    private void Awake()
    {
       // gameObject.AddComponent<TaskManager>().Initialize();
    }

    [ContextMenu("AddTask")]
    public void AddTask()
    {
        TaskManager.instance.AddTask(taskToAdd);
    }
}
