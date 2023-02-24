using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlyncSessionListener : MonoBehaviour
{
    // Start is called before the first frame update
    
    public BlyncControllerData sensorData;
    public GameObject connected_img;
    public float timeToDisable;
    public GameObject[] comfirmationIcons;
    public GameObject[] Next_Tutorial;
    private Animator blync_gif_animator;
    private bool outroPlayed = false;
    private bool transitionPlayed = false;
    public GameObject whiteScreen;

    void Start(){
        blync_gif_animator = connected_img.GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
        //Debug.Log(sensorData.sessionStarted);
        if(sensorData.sessionStarted == true){
            //connected_img.SetActive(true);
            
            if(outroPlayed == false){
                StartCoroutine(bikeOutAfterTime());
                
                outroPlayed = true;
            }
            //blync_gif_animator.SetTrigger("BlyncOut");
            foreach(GameObject icon in comfirmationIcons){
                icon.SetActive(true);
            }
            StartCoroutine(disableAfterTime());
            if(transitionPlayed == false){
                StartCoroutine(whiteScreenTransition());
                transitionPlayed = true;
            }
        }
        else{
            //connected_img.SetActive(false);
        }
    }

    IEnumerator bikeOutAfterTime(){
        yield return new WaitForSeconds(5.5f);
        blync_gif_animator.SetTrigger("BlyncOut");
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
