using UnityEngine;
using System.Collections.Generic;
using PoolsManagement;
using Tasks;
using TMPro;

public class BasicTaskWidget : PoolableObject
{
    [SerializeField]
    Transform _answersContainer;
    [SerializeField]
    TMP_Text _taskName;
    [SerializeField]
    TMP_Text _taskDescription;

    BasicTask _task;
    List<TaskAnswerOption> _answerOptions = new List<TaskAnswerOption>();

    TaskAnswerOption CreateAnswerOption(int index, AnswerOptionSet answerOptionSet)
    {
        TaskAnswerOption answerOption = answerOptionSet.poolSet.GetNewObject(_answersContainer) as TaskAnswerOption;
        answerOption.SetValues(index, answerOptionSet.text);
        answerOption.OnClickSubscribe(OptionSelected);

        answerOption.gameObject.name = answerOptionSet.poolSet.prefab.name + "_" + index;
        answerOption.transform.SetSiblingIndex(index);
        answerOption.gameObject.SetActive(true);
        return answerOption;
    }

    void OptionSelected(int index)
    {
        _task.PlayerAnwer(index);
    }

    public void TaskUpdate(ITask task)
    {
        _task = task as BasicTask;

        _taskName.text = _task.GetTaskName();
        _taskDescription.text = _task.GetTaskDescription();

        AnswerOptionSet[] answerOptions = _task.GetAnswerOptions();

        int deltaCount = _answerOptions.Count - answerOptions.Length;
        if (deltaCount > 0)
        {
            for (int i = _answerOptions.Count - 1; i >= answerOptions.Length; i--)
                _answerOptions[i].ReturnToPool();
            _answerOptions.RemoveRange(answerOptions.Length, deltaCount);
        }
            

        for (int i = 0; i < answerOptions.Length; i++)
        {
            TaskAnswerOption answerOption;
            if (i < _answerOptions.Count)
            {
                answerOption = _answerOptions[i];
                if (answerOption.GetPoolSet().KeyHash != answerOptions[i].poolSet.KeyHash)
                {
                    answerOption.ReturnToPool();

                    answerOption = CreateAnswerOption(i, answerOptions[i]);
                    _answerOptions[i] = answerOption;
                }
                else
                {
                    answerOption.SetValues(i, answerOptions[i].text);
                }
            }
            else
            {
                answerOption = CreateAnswerOption(i, answerOptions[i]);
                _answerOptions.Add(answerOption);
            }
        }
    }

    public override void ReturnToPool()
    {
        foreach (TaskAnswerOption answerOptions in _answerOptions)
        {
            answerOptions.ReturnToPool();
        }
        _answerOptions.Clear();
        base.ReturnToPool();
    }
}
