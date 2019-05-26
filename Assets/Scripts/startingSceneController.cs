using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class startingSceneController : MonoBehaviour
{
    public GameObject[] texts;
    public float spaceRate = 0.1F;
    public float nextTextTime = 0.5F;
    public int currentText = 0;
    public string nextSceneName;

    void Update()
    {
        if ((Input.GetButton("Jump")||Input.GetMouseButton(0)) && Time.time > nextTextTime)
        {
            nextTextTime = Time.time + spaceRate;
            if (currentText < texts.Length)
                texts[currentText].SetActive(true);
            else
                SceneManager.LoadScene(nextSceneName);
            currentText++;
        }
    }
}
