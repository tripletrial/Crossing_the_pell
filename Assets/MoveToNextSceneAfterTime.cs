using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveToNextSceneAfterTime : MonoBehaviour
{
    // Start is called before the first frame update
    public float timeToDisable;
    string nextSceneName = "0_StartingScene 1";
    
    void Start()
    {
        StartCoroutine(disableAfterTime());
    }

    IEnumerator disableAfterTime()
    {
        yield return new WaitForSeconds(timeToDisable);
        unenableCurentObject();
    }

    public void unenableCurentObject()
    {
        gameObject.SetActive(false);
        //Load next scene
        SceneManager.LoadScene(nextSceneName);
    }
}
