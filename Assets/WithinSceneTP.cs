using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WithinSceneTP : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject tp_endpt;
    public GameObject bike;
    //public GameObject whiteScreen;
    public GameObject transitionCanvas;
    public bool teleported = false;
    public AudioSource[] tpSounds;
    //collider for teleporting
    void OnTriggerEnter(Collider other){
        //wait for 2 seconds
        //Debug.Log("Collided into"+ other.GetComponent<Collider>().name);
        //enable whihite screen
       
        
        if (other.tag == "Player" && teleported == false){
             Animator whiteAnimator = transitionCanvas.GetComponent<Animator>();
            ///whiteScreen.SetActive(true);
            //play white screen transition animation
            whiteAnimator.SetTrigger("WhiteTransition");
            StartCoroutine(Wait());
            teleported = true;
            //play teleport sound
            foreach (AudioSource sound in tpSounds){
                sound.Play();
            }
        }
        //StartCoroutine(Wait());

        IEnumerator Wait(){
            yield return new WaitForSeconds(1.5f);
            //Rotate the bike to face the direction of the teleporter
            bike.transform.rotation = tp_endpt.transform.rotation;
            //teleport to the end point
            other.transform.position = tp_endpt.transform.position;
            bike.GetComponent<Mover>().enabled = false;
             StartCoroutine(Wait2());
            
        }
        IEnumerator Wait2(){
                yield return new WaitForSeconds(1);
                bike.GetComponent<Mover>().enabled = true;
            }
    }

//     public IEnumerator Wait(Collider oth){
//         yield return new WaitForSeconds(2);
//         //teleport
//         transform.position = tp_endpt.transform.position;
//     }
// }
}
