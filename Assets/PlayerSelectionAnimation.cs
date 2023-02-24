using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerSelectionAnimation : MonoBehaviour
{
    public GameObject gif;
    private Animator tutorial_gif_animator;
    public bool Skip_Tutorial = false;
    public TutorialData tutorialData;
    public GameObject DarkScreen;
    private bool outroPlayed = false;

    [SerializeField] InputActionProperty leftTriggerBtn;
    [SerializeField] InputActionProperty rightTriggerBtn;
    float leftTriggerValue;
    float rightTriggerValue;
    // Update is called once per frame
    // Start is called before the first frame update
    void Start()
    {
        tutorial_gif_animator = gif.GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        leftTriggerValue = leftTriggerBtn.action.ReadValue<float>(); 
        rightTriggerValue = rightTriggerBtn.action.ReadValue<float>();

        if((leftTriggerValue > 0.75f && rightTriggerValue < 0.05f) || Input.GetKeyDown(KeyCode.A)){
            // left trigger pressed
            tutorial_gif_animator.SetTrigger("SkipTutorial");
            Skip_Tutorial = true;
            tutorialData.Skip_Tutorial = true;
            if(outroPlayed == false){
                StartCoroutine(darkScreenTransition());
                StartCoroutine( loadScene());
                outroPlayed = true;
            } 
        else if((rightTriggerValue > 0.75f && leftTriggerValue < 0.05f) || Input.GetKeyDown(KeyCode.D)){
            // right trigger pressed
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
}
