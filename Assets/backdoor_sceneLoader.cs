using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class backdoor_sceneLoader : MonoBehaviour
{
    // Start is called before the first frame update

    
    //public GameObject BlackScreen;
    public GameObject transitionCanvas;
    // Update is called once per frame

    
    void Update()
    {
        // if the player press 1, 2, 3, or 4, load the corresponding scene
        Animator blackAnimator = transitionCanvas.GetComponent<Animator>();
        GameObject IB_sprite = transitionCanvas.transform.Find("Loading Screens").transform.Find("Inhabited Bridge").gameObject;
        GameObject C_sprite = transitionCanvas.transform.Find("Loading Screens").transform.Find("Conductivity").gameObject;
        GameObject S_sprite = transitionCanvas.transform.Find("Loading Screens").transform.Find("Stage").gameObject;
        GameObject N_sprite = transitionCanvas.transform.Find("Loading Screens").transform.Find("The Net").gameObject;
        //BlackScreen.SetActive(true);
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //SceneManager.LoadScene("1_Inhabited_Bridge_Bike");
            
        blackAnimator.SetTrigger("WhiteTransition");
            StartCoroutine(loadScene("1_Inhabited_Bridge_Bike"));

            //only enable IB_sprite
            IB_sprite.SetActive(true);
            C_sprite.SetActive(false);
            S_sprite.SetActive(false);
            N_sprite.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {

            blackAnimator.SetTrigger("WhiteTransition");
            StartCoroutine(loadScene("2_Conductivity_Bike"));
            //only enable C_sprite
            IB_sprite.SetActive(false);
            C_sprite.SetActive(true);
            S_sprite.SetActive(false);
            N_sprite.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)){
        
            blackAnimator.SetTrigger("WhiteTransition");
            StartCoroutine(loadScene("3_All_The_Worlds_a_Stage"));
            //only enable S_sprite
            IB_sprite.SetActive(false);
            C_sprite.SetActive(false);
            S_sprite.SetActive(true);
            N_sprite.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            blackAnimator.SetTrigger("WhiteTransition");
            StartCoroutine(loadScene("4_The_Net_bike"));
            //only enable N_sprite
            IB_sprite.SetActive(false);
            C_sprite.SetActive(false);
            S_sprite.SetActive(false);
            N_sprite.SetActive(true);
        }
        if(Input.GetKeyDown(KeyCode.Alpha5)){
            blackAnimator.SetTrigger("WhiteTransition");
            StartCoroutine(loadScene("5_Ending"));
        }
    }
    IEnumerator loadScene(string bike_scene){
        //play black screen transition animation
        
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(bike_scene);
    }
}
