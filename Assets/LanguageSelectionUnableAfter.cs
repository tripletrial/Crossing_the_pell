using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
//using UnityEditor.Presets;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class LanguageSelectionUnableAfter : MonoBehaviour
{
    // Start is called before the first frame update
    public float timeToDisable;
    public GameObject[] Next_Tutorial;
    public GameObject whiteScreen;
    [SerializeField] languageBool myLanguageBool;
    [SerializeField] InputActionProperty leftTriggerBtn;
    [SerializeField] InputActionProperty rightTriggerBtn;
    float leftTriggerValue = 0;
    float rightTriggerValue = 0;

    void Start()
    {
        
    }

    void Update(){

        leftTriggerValue = leftTriggerBtn.action.ReadValue<float>(); 
        rightTriggerValue = rightTriggerBtn.action.ReadValue<float>();
        Debug.Log("isEn: " + myLanguageBool.isEn);

        if ((leftTriggerValue > 0.75f && rightTriggerValue < 0.05f) || Input.GetKeyDown("n"))
        {
            // left trigger pressed, english, isEn
            Debug.Log("left trigger pressed, english, isEn");
            myLanguageBool.isEn = true;
            StartCoroutine(disableAfterTime());
            StartCoroutine(whiteScreenTransition());
        }
        else if ((rightTriggerValue > 0.75f && leftTriggerValue < 0.05f) || Input.GetKeyDown("m"))
        {
            // right trigger pressed, spanish
            Debug.Log("right trigger pressed, spanish");
            myLanguageBool.isEn = false;
            StartCoroutine(disableAfterTime());
             StartCoroutine(whiteScreenTransition());
        }
    }

    IEnumerator disableAfterTime()
    {
        yield return new WaitForSeconds(timeToDisable);
        unenableCurentObject();
    }

    public void unenableCurentObject()
    {
        gameObject.SetActive(false);
    }

    public void OnDisable()
    {
        //Debug.Log("Disabled");
        whiteScreenTransition();
        foreach(GameObject tutorial in Next_Tutorial){
            tutorial.SetActive(true);
        }
    }
    
    IEnumerator whiteScreenTransition(){
        yield return new WaitForSeconds(timeToDisable-0.5f);
        Animator whiteAnimator = whiteScreen.GetComponent<Animator>();
        //play white screen transition animation
        whiteAnimator.SetTrigger("WhiteTransition");
        Debug.Log("White Screen Transition");
    }
}
