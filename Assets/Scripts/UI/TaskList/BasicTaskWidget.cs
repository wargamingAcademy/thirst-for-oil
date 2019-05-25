using UnityEngine;
using System.Collections.Generic;
using PoolsManagement;
using Tasks;
using TMPro;

public class BasicTaskWidget : PoolableObject
{
#pragma warning disable CS0649
    [SerializeField]
    Transform _answersContainer;
    [SerializeField]
    TMP_Text _taskName;
    [SerializeField]
    TMP_Text _taskDescription;
#pragma warning restore CS0649

    BasicTask _task;
    List<TaskAnswerOption> _answerOptions = new List<TaskAnswerOption>();

    TaskAnswerOption CreateAnswerOption(int index, AnswerOptionSet answerOptionSet)
    {
        var answerOption = answerOptionSet.poolSet.GetNewObject(_answersContainer) as TaskAnswerOption;
        answerOption.SetValues(index, _task.GetAnswerText(index));
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

        AnswerOptionSet[] answerOptionsSets = _task.GetAnswerOptionsSets();

        int deltaCount = _answerOptions.Count - answerOptionsSets.Length;
        if (deltaCount > 0)
        {
            for (int i = _answerOptions.Count - 1; i >= answerOptionsSets.Length; i--)
                _answerOptions[i].ReturnToPool();
            _answerOptions.RemoveRange(answerOptionsSets.Length, deltaCount);
        }
            

        for (int i = 0; i < answerOptionsSets.Length; i++)
        {
            TaskAnswerOption answerOption;
            if (i < _answerOptions.Count)
            {
                answerOption = _answerOptions[i];
                if (answerOption.GetPoolSet().KeyHash != answerOptionsSets[i].poolSet.KeyHash)
                {
                    answerOption.ReturnToPool();

                    answerOption = CreateAnswerOption(i, answerOptionsSets[i]);
                    _answerOptions[i] = answerOption;
                }
                else
                {
                    answerOption.SetValues(i, _task.GetAnswerText(i));
                }
            }
            else
            {
                answerOption = CreateAnswerOption(i, answerOptionsSets[i]);
                _answerOptions.Add(answerOption);
            }
            answerOption.SetAllowed(_task.IsAnswerOptionAllowed(i));
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
