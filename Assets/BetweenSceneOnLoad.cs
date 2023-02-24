using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BetweenSceneOnLoad : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Animator whiteAnimator = GetComponent<Animator>();
        //play white screen transition animation
        whiteAnimator.SetTrigger("WhiteTransitionOnlyOut");
        //Debug.Log("White Screen Transitioning");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
