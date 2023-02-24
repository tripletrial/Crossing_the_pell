using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class unableAfterTime : MonoBehaviour
{
    // Start is called before the first frame update
    public float timeToDisable;
    public GameObject[] Next_Tutorial;
    public GameObject whiteScreen;
    void Start()
    {
        StartCoroutine(disableAfterTime());
        StartCoroutine(whiteScreenTransition());
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
