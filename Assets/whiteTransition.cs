using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class whiteTransition : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W)){
            Animator whiteAnimator = GetComponent<Animator>();
            whiteAnimator.SetTrigger("WhiteTransition");
            Debug.Log("W is pressed");
        }
    }
    void whiteScreenTransition(){
        //get self animator
        Animator whiteAnimator = GetComponent<Animator>();
        //play white screen transition animation
        whiteAnimator.SetTrigger("WhiteTransition");
        Debug.Log("White Screen Transitioning");
    }
}
