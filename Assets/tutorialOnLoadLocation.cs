using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class tutorialOnLoadLocation : MonoBehaviour
{
    // Start is called before the first frame update

    public TutorialData tutorialData;
    public Transform Tutorial;
    public Transform NoTutorial;
    public GameObject darkScreenTransition;
    void Start()
    {   
        Scene scene = SceneManager.GetActiveScene();
       
           
        Animator darkscreen = darkScreenTransition.GetComponent<Animator>();
        darkscreen.SetTrigger("WhiteTransitionOnlyOut");
         if (scene.name == "0_TutorialScene"){
        if(tutorialData.Skip_Tutorial == true){
            //transorm rotation 
            transform.rotation = NoTutorial.rotation;
            transform.position = NoTutorial.position;
            Debug.Log("No Tutorial");
            StartCoroutine(startMover());
           // GetComponent<Mover>().enabled = true;
        }
        else{
            transform.rotation = Tutorial.rotation;
            transform.position = Tutorial.position;
            Debug.Log("Tutorial");
            StartCoroutine(startMover());
            //GetComponent<Mover>().enabled = true;
        }
         } else{
            GetComponent<Mover>().enabled = true;
            //disable this script
            this.enabled = false;
         }
        //Debug.Log("Tutorial On Load Location");
    }

    // Update is called once per frame
    IEnumerator startMover(){
        yield return new WaitForSeconds(1f);
        GetComponent<Mover>().enabled = true;
    }

}
