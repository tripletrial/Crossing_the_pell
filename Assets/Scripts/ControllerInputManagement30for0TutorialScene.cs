using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
public class ControllerInputManagement30for0TutorialScene : MonoBehaviour
{
    #region field
    #region SerializeField
    [SerializeField]
    InputActionProperty leftTriggerBtn;

    [SerializeField]
    InputActionProperty rightTriggerBtn;

    [SerializeField]
    InputActionProperty btnA;
    List<GameObject> mainMenuBtnLst;

    // string[] mainMenuBtnNames;
    List<GameObject> sceneImgLst;

    // string[] sceneImgNames;
    [SerializeField]
    languageBool myLanguageBool;

    [SerializeField]
    List<GameObject> projectList;

    [SerializeField]
    GameObject allBridge;

    [SerializeField]
    GameObject[] bridgeList;
    //[SerializeField] TextMeshProUGUI tuturialMsgInfo;
    //[SerializeField] TextMeshProUGUI loadingMsgBox;
    #endregion
    #region variable
    Mover myMover; //GameObject.FindObjectOfType<Mover>();
    List<string> sceneNameLst = new List<string>
    {
        "1_Inhabited_Bridge_Bike",
        "2_Conductivity_Bike",
        "3_All_The_Worlds_a_Stage",
        "4_The_Net_bike"
    };
    GameObject flexibleCube; //= GameObject.FindGameObjectWithTag("testCube");
    GameObject mainMenu; //= GameObject.FindGameObjectWithTag("menu_main");
    GameObject subMenu_01; //= GameObject.FindGameObjectWithTag("menu_sub_01");

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

    // flags
    bool hasSceneMenuShowed = false;
    bool hasRightTriggerClicken = false;
    bool hasLeftTriggerClicken = false;
    bool hasSubSceneMenuShowed = false;
    bool hasMenuClosed = false;
    #endregion
    #endregion
    #region property
    // outside envt can only get the scale, and cannot set the value
    public float ProgressBarScale { get; }
    public bool HasSceneMenuShowed
    {
        get => hasSceneMenuShowed;
    }
    public bool HasRightTriggerClicken
    {
        get => hasRightTriggerClicken;
    }
    public bool HasLeftTriggerClicken
    {
        get => hasLeftTriggerClicken;
    }
    public bool HasSubSceneMenuShowed
    {
        get => hasSubSceneMenuShowed;
    }
    public bool HasMenuClosed
    {
        get => hasMenuClosed;
    }
    bool stoppedLooping = false;
    public GameObject[] SpeedoMeters;

    public GameObject transitionCanvas;
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
            flexibleCube = GameObject.FindGameObjectWithTag("testCube");
            mainMenu = GameObject.FindGameObjectWithTag("menu_main");
            subMenu_01 = GameObject.FindGameObjectWithTag("menu_sub_01");
            mainMenuBtnLst = GameObject.FindGameObjectsWithTag("mainBtn").ToList(); // get gameObjects
            mainMenuBtnLst = mainMenuBtnLst.OrderBy(element => element.name).ToList(); // sort by name
            sceneImgLst = GameObject.FindGameObjectsWithTag("subSceneBtn").ToList(); // get gameObjects
            sceneImgLst = sceneImgLst.OrderBy(element => element.name).ToList(); // sort
        }
        catch (NullReferenceException err)
        {
            Debug.Log("---Fail: sth. NOT loaded---");
            Debug.Log(err.Message);
        }

        //Use the constructor: new List<object>(myArray), to convert an array to a list
        if (sceneImgLst != null)
        {
            Debug.Log($"---success: sceneImgLst loaded---");
        }
        else
        {
            Debug.Log($"---Fail: sceneImgLst not loaded---");
        }
        if (mainMenu != null)
        {
            Debug.Log($"---success: mainMenu loaded---");
        }
        else
        {
            Debug.Log($"---Fail: mainMenu not loaded---");
        }
        if (subMenu_01 != null)
        {
            Debug.Log($"---success: subMenu_01 loaded---");
        }
        else
        {
            Debug.Log($"---Fail: subMenu_01 not loaded---");
        }
        #endregion

        mainMenu.SetActive(false);
        subMenu_01.SetActive(false);

        progressBarScale = 0f;
        currentSelectedIndex = 0;
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
        flexibleCube.transform.localScale = new Vector3(
            rightTriggerValue * 0.1f,
            leftTriggerValue * 0.1f,
            1f
        );
        CheckTriggersFrozenTime();
        #region menuInput
        // safety, if not in the tutorialScene, but no needed since the script is unique
        if (SceneManager.GetActiveScene().name != "00_PreStartingScene")
        {
            // in this case, All menu are OFF (hidden)
            if (!mainMenu.activeSelf && !subMenu_01.activeSelf && isTriggerActive)
            {
                // true means that the left trigger is pressed CALL MENU
                if (leftTriggerValue > 0.75f || Input.GetKeyUp(KeyCode.Escape))
                {
                    SpeedoMeters = GameObject.FindGameObjectsWithTag("speedoMeter");
                    foreach (GameObject speedoMeter in SpeedoMeters)
                    {
                        speedoMeter.GetComponent<speedometerAnimation>().open = true;
                    }
                    mainMenu.SetActive(true);
                    hasSceneMenuShowed = true;
                    FreezeTriggers();
                    RenderSelectedImg(currentSelectedIndex, mainMenuBtnLst);
                } // SHOW the menu
            }
            // Mainmenu is currently ON (shown), and the SubSceneMenu is OFF
            else if (mainMenu.activeSelf && !subMenu_01.activeSelf && isTriggerActive)
            {
                // left trigger, confirm the selected btn CONFIRM => show the SubSceneMenu
                if (
                    (leftTriggerValue > 0.75f && rightTriggerValue < 0.05f)
                    || Input.GetKeyUp(KeyCode.Escape)
                )
                {
                    hasLeftTriggerClicken = true;
                    FreezeTriggers();

                    // MainMenu btn click
                    switch (currentSelectedIndex)
                    {
                        case 0: // show the sub scene menu, and hide the main menu
                            mainMenu.SetActive(false); // hide main menu
                            subMenu_01.SetActive(true); // show sub menu
                            currentSelectedIndex = 0; // refresh/update index to 0
                            RenderSubMenuSelectedImg(currentSelectedIndex, sceneImgLst); // highlight the selected img
                            RenderSelectedProject(currentSelectedIndex, projectList); // update the background
                            hasSubSceneMenuShowed = true;
                            break;
                        case 1: // Recenter
                            myMover.recenter();
                            currentSelectedIndex = 1;
                            RenderSelectedImg(currentSelectedIndex, mainMenuBtnLst);
                            break;
                        case 2: // End the Journey
                            //load the ending scene
                            SceneManager.LoadScene("5_Ending");
                            currentSelectedIndex = 2;
                            RenderSelectedImg(currentSelectedIndex, mainMenuBtnLst);
                            break;
                        case 3: // close menu
                            mainMenu.SetActive(false);
                            subMenu_01.SetActive(false);
                            hasMenuClosed = true;
                            currentSelectedIndex = 0;
                            foreach (GameObject speedoMeter in SpeedoMeters)
                            {
                                speedoMeter.GetComponent<speedometerAnimation>().open = false;
                            }
                            break;
                        default:
                            break;
                    }
                    FreezeTriggers();
                }
                // true means that the right trigger is pressed SWITCH
                if (
                    (rightTriggerValue > 0.75f && leftTriggerValue < 0.05f && isTriggerActive)
                    || Input.GetKeyDown("n")
                )
                {
                    // if (tuturialStep == 1) tuturialStep = 2;
                    currentSelectedIndex++;
                    currentSelectedIndex = CheckListIndex(
                        currentSelectedIndex,
                        mainMenuBtnLst.Count
                    );
                    RenderSelectedImg(currentSelectedIndex, mainMenuBtnLst);
                    FreezeTriggers();
                    hasRightTriggerClicken = true;
                }
            }
            // Submenu is currently ON (shown)
            else if (!mainMenu.activeSelf && subMenu_01.activeSelf && isTriggerActive)
            {
                // looping
                allBridge.GetComponent<bridgeLooping>().isLooping = false; // stop the looping
                if (stoppedLooping != true)
                {
                    foreach (GameObject bridge in bridgeList)
                    {
                        bridge.GetComponent<Dissolver_Stage>().show = false;
                        bridge.GetComponent<Dissolver_Stage>().Duration = 1f;
                    }
                    stoppedLooping = true;
                }

                // true means that the RIGHT trigger is pressed --- SWITCH
                if (
                    (rightTriggerValue > 0.75f && leftTriggerValue < 0.05f && isTriggerActive)
                    || Input.GetKeyDown("n")
                )
                {
                    currentSelectedIndex++;
                    currentSelectedIndex = CheckListIndex(currentSelectedIndex, sceneImgLst.Count);
                    RenderSubMenuSelectedImg(currentSelectedIndex, sceneImgLst);
                    if (currentSelectedIndex < projectList.Count)
                    {
                        RenderSelectedProject(currentSelectedIndex, projectList); // update the background;
                    }
                    else
                    {
                        Debug.Log("Cursor on Back Btn");
                    }
                    // careful: sceneImgLst has one more element than projectList, the BACK
                    hasRightTriggerClicken = true;
                    FreezeTriggers();
                }
                // true means that the LEFT trigger is pressed --- CONFIRM
                if (
                    (leftTriggerValue > 0.75f && rightTriggerValue < 0.05f)
                    || Input.GetKeyUp(KeyCode.Escape)
                )
                {
                    hasLeftTriggerClicken = true;
                    // MainMenu btn click
                    string currentSceneName = SceneManager.GetActiveScene().name;
                    /*if (currentSceneName == "0_StartingScene 1")
                    {
                       // loadingMsgBox.text = "Great, you made it ;)";
                    }
                    else
                    {*/
                    //string currentSelectedSceneName = sceneNameLst[currentSelectedIndex];
                    if (
                        currentSelectedIndex < sceneImgLst.Count - 1
                        && currentSceneName != "0_StartingScene 1"
                    )
                    {
                        SceneManager.LoadScene(sceneNameLst[currentSelectedIndex]);
                    }
                    else
                    {
                        // the 5th(last) one is clicked, meaning return
                        mainMenu.SetActive(true);
                        subMenu_01.SetActive(false);
                        currentSelectedIndex = 0;
                    }
                    // }
                    FreezeTriggers();
                }
            }
        }
        else
        {
            if ((leftTriggerValue > 0.75f && isTriggerActive) || Input.GetKeyUp("n"))
            {
                // TO DO: english
                myLanguageBool.isEn = true;
            }
            else if ((rightTriggerValue > 0.75f && isTriggerActive) || Input.GetKeyUp("m"))
            {
                // TO DO: spanish
                myLanguageBool.isEn = false;
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
    int CheckListIndex(int index, int upperBoundary)
    {
        if (index < 0)
        {
            index += upperBoundary;
        }

        if (index >= upperBoundary)
        {
            index -= upperBoundary;
        }

        if (index >= 0 && index < upperBoundary)
        {
            return index; // safe, return index - Base Case
        }
        else
        {
            CheckListIndex(index, upperBoundary); // recursion
        }

        return 0; // in case of death loop
    }

    void RenderSelectedImg(int index, List<GameObject> imgList)
    {
        // if(sceneImgLst.Count != 0)
        // {
        //     // first, reset all img to (1f,1f,1f)
        //     foreach (GameObject obj in imgList)
        //     {
        //         obj.transform.localScale = new Vector3(1f, 1f, 1f);
        //     }
        //     // then
        //     GameObject selectedSceneImg = imgList[index];
        //     selectedSceneImg.transform.localScale = new Vector3(SELECTEDIMGSCALERATIO, SELECTEDIMGSCALERATIO, SELECTEDIMGSCALERATIO);
        // }

        if (sceneImgLst.Count != 0)
        {
            // first, reset all img to (1f,1f,1f)
            foreach (GameObject obj in imgList)
            {
                obj.GetComponent<UnityEngine.UI.Image>().enabled = true;
                obj.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = false;
            }
            // then

            GameObject selectedSceneImg = imgList[index];
            selectedSceneImg.GetComponent<UnityEngine.UI.Image>().enabled = false;
            selectedSceneImg.transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = true;
            //selectedSceneImg.transform.localScale = new Vector3(SELECTEDIMGSCALERATIO, SELECTEDIMGSCALERATIO, SELECTEDIMGSCALERATIO);
        }
    }

    void RenderSubMenuSelectedImg(int index, List<GameObject> imgList)
    {
        if (sceneImgLst.Count != 0)
        {
            // first, reset all img false
            foreach (GameObject obj in imgList)
            {
                obj.GetComponent<UnityEngine.UI.Image>().enabled = false;
            }
            // then
            GameObject selectedSceneImg = imgList[index];
            selectedSceneImg.GetComponent<UnityEngine.UI.Image>().enabled = true; // render highlight
            //selectedSceneImg.transform.localScale = new Vector3(SELECTEDIMGSCALERATIO, SELECTEDIMGSCALERATIO, SELECTEDIMGSCALERATIO);
        }
    }

    void RenderSelectedProject(int index, List<GameObject> projectList)
    {
        GameObject selectedProject = projectList[index]; // careful: sceneImgLst has one more element than projectList, the BACK
        if (selectedProject != null)
        {
            foreach (GameObject project in projectList)
            {
                project.GetComponent<Dissolver_Stage>().show = false;
            }
            selectedProject.GetComponent<Dissolver_Stage>().show = true;
        }
    }

    void FreezeTriggers()
    {
        triggersFreezeStartTime = currentTime;
        // freeze both the triggers
        isTriggerActive = false;
    }

    void CheckTriggersFrozenTime()
    {
        if (
            triggersFreezeStartTime != 0
            && ((currentTime - triggersFreezeStartTime) > TRIGGER_FREEZETIME)
        )
        {
            isTriggerActive = true;
        }
    }
    #endregion
}
