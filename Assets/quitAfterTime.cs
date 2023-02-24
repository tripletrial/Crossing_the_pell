using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quitAfterTime : MonoBehaviour
{
    // Start is called before the first frame update
    public float timeToQuit = 30f;
    void Start()
    {
        StartCoroutine(QuitAfterTime());
    }

    IEnumerator QuitAfterTime()
    {
        yield return new WaitForSeconds(timeToQuit);
        Application.Quit();
        Debug.Log("Quit");
    }
}
