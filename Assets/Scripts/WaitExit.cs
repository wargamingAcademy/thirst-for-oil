using Gamekit2D;
using UnityEngine;
using UnityEditor;

public class WaitExit : MonoBehaviour
{
    void Update()
    {
        if (PlayerInput.Instance.Exit.Down)
        {
            Application.Quit();
        }
    }
}