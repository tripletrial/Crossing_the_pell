using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
//using UnityEditor.Presets;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

/// <summary>
/// this script is created to play with the trigger value
/// managing all the controller input and the keyboard input
/// the keyboard is the backup controll plan
/// Functions:
///     01) show and hide the menu 
///     02) select scene and load
///     03) skip the tuturial
///         => confirm and load the scene
/// </summary>
public class ControllerInputManagement30for00Prestart : MonoBehaviour {
    #region field
    #region SerializeField
    [SerializeField] InputActionProperty leftTriggerBtn;
    [SerializeField] InputActionProperty rightTriggerBtn;
    [SerializeField] InputActionProperty btnA;
    //[SerializeField] List<GameObject> mainMenuBtnLst;
    //[SerializeField] List<GameObject> sceneImgLst;
    [SerializeField] languageBool myLanguageBool;
    [SerializeField] GameObject languageSelectionMenu;
    [SerializeField] GameObject tuturialOrPlayMenu;
    //[SerializeField] GameObject englishTitle;
    //[SerializeField] GameObject spanishTitle;
    [SerializeField]Animator tutorial_gif_animator;
    public bool Skip_Tutorial = false;
    public TutorialData tutorialData;
    public GameObject DarkScreen;
    private bool outroPlayed = false;
    #endregion
    #region variable
    Mover myMover;  //GameObject.FindObjectOfType<Mover>();
    // hold for HOLDTIME2, and confirm the scene
    // const float HOLDTIME_CONFIRM = 3.0f;
    // float bothTriggerPressStartTime;
    // bool isBothTriggerPressedInPreviousFrame = false;
    const float SELECTEDIMGSCALERATIO = 1.15f;
    const float TRIGGER_FREEZETIME = 0.3f;
    float triggersFreezeStartTime;
    bool isTriggerActive;

    int currentSelectedIndex; // pointer position
    float currentTime;
    float progressBarScale;
    float leftTriggerValue;
    float rightTriggerValue;
    #endregion
    #endregion
    #region property
    // outside envt can only get the scale, and cannot set the value
    #endregion
    #region method
    void Start()
    {
        #region checkNull
        // hide the menu and set the progressBarScale to ZERO
        //tuturialMsgInfo.text = "";
        try
        {
            myMover = GameObject.FindObjectOfType<Mover>();
        }
        catch (NullReferenceException err)
        {
            Debug.Log("---Fail: sth. NOT loaded---");
            Debug.Log(err.Message);
        }
        #endregion
        //tutorial_gif_animator = gif.GetComponent<Animator>();
        //mainBtnListLength = mainMenuBtnLst.Count;
        //imgListLength = sceneNameLst.Count;
        isTriggerActive = true;
    }

    void Update()
    {
        currentTime = Time.time;
        leftTriggerValue = leftTriggerBtn.action.ReadValue<float>(); 
        rightTriggerValue = rightTriggerBtn.action.ReadValue<float>();
        // Testing for both the triggers' input
        CheckTriggersFrozenTime();
        #region menuInput
        if (languageSelectionMenu.activeSelf) //TO DO: Implement to check if it is the time to select language
        {
            if ((leftTriggerValue > 0.75f && isTriggerActive) || Input.GetKeyUp("n"))
            {
                myLanguageBool.isEn = true;
                //englishTitle.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                //spanishTitle.transform.localScale = Vector3.one;
                FreezeTriggers();
            }
            else if ((rightTriggerValue > 0.75f && isTriggerActive) || Input.GetKeyUp("m"))
            {
                myLanguageBool.isEn = false;
                //englishTitle.transform.localScale = Vector3.one;
                //spanishTitle.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                FreezeTriggers();
            }
        }
        else if(tuturialOrPlayMenu.activeSelf) 
        {
            if ((leftTriggerValue > 0.75f && isTriggerActive) || Input.GetKeyUp("n"))
            {
                // TO DO: TUTURIAL
                tutorial_gif_animator.SetTrigger("SkipTutorial");
                Skip_Tutorial = true;
                tutorialData.Skip_Tutorial = true;
                if(outroPlayed == false){
                    StartCoroutine(darkScreenTransition());
                    StartCoroutine(loadScene());
                    outroPlayed = true;
                } 
            }
            else if ((rightTriggerValue > 0.75f && isTriggerActive) || Input.GetKeyUp("m"))
            {
                // TO DO: START RIDING
                tutorial_gif_animator.SetTrigger("PlayTutorial");
                Skip_Tutorial = false;
                tutorialData.Skip_Tutorial = false;
                if(outroPlayed == false){
                    StartCoroutine(darkScreenTransition());
                    StartCoroutine( loadScene());
                    outroPlayed = true;
                }
            }
        }
        
        #endregion
    }
    /// <summary>
    /// Helper Method
    /// </summary>
    /// <param name="index">two intergers. the index and the upperboundary</param>
    /// <param name="upperBoundary">one interger, within the [0, upperboundary)</param>
    /// <returns>int</returns>

    void FreezeTriggers()
    {
        triggersFreezeStartTime = currentTime;
        // freeze both the triggers
        isTriggerActive = false;

    }
    void CheckTriggersFrozenTime()
    {

        if(triggersFreezeStartTime != 0 && ((currentTime - triggersFreezeStartTime) > TRIGGER_FREEZETIME))
        {
            isTriggerActive = true;
        }

    }

    IEnumerator darkScreenTransition()
    {
        Animator darkscreen = DarkScreen.GetComponent<Animator>();
        yield return new WaitForSeconds(2f);
        darkscreen.SetTrigger("WhiteTransition");
    }

    IEnumerator loadScene(){
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("0_TutorialScene");
    }
    #endregion
}
