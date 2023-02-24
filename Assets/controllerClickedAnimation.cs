using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controllerClickedAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    public bool click = false;
    //public bool clicked = false;


    // Update is called once per frame
    void Update()
    {
        
        if(click == true ){
            Animator animator = GetComponent<Animator>();
            animator.SetTrigger("Clicked");
            click = false;
        } 
    }
}
