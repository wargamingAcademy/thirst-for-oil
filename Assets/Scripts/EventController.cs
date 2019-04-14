using UnityEngine;
using UnityEditor;

public class EventController
{
    public delegate void MethodContainer();
    public static event MethodContainer TurnEndEvent;

    void ddd()
    {
        TurnEndEvent();
    }
}