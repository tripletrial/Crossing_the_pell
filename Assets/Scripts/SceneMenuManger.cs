using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
/// <summary>
/// SceneMenuManger:
/// to implement the function of the progress bar in the menu. the progress bar would set itself as soon as the both trigger being pressed for a while.
/// For this script, three varables need to be bound. 
/// They are menu, progressBar, and controllerInputManagement20. 
/// The previous two can be bound by GameObject.FindGameObjectWithTag(), and the last one shall be bound by GameObject.FindObjectOfType<>()
/// </summary>
public class SceneMenuManger : MonoBehaviour
{
    public InputActionProperty showButton;
    GameObject progressBar;
    float progressBarScale; // can be fetched by other scripts
    float progressBarWidth = 0.59f; // the max is 0.7f, within the menu border
    ControllerInputManagement30 controllerInputManagement30;

    private void Start()
    {
        progressBar = GameObject.FindGameObjectWithTag("progressiveBar");
        controllerInputManagement30 = GameObject.FindObjectOfType<ControllerInputManagement30>();
    }
    // Update is called once per frame
    void Update()
    {
        if(controllerInputManagement30 != null)
        {
            progressBarScale = controllerInputManagement30.ProgressBarScale;
        }
        UpdateProgressBar();
    }

    void UpdateProgressBar()
    {
        if (progressBar != null)
        {
            progressBar.transform.localScale = new Vector3(progressBarScale * progressBarWidth, 0.1f, 1f);
        }
    }
}
