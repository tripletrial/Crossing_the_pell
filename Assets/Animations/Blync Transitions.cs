using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlyncTransitions : MonoBehaviour
{
    // Start is called before the first frame update

    private Animator blync_gif_animator;
    void Start()
    {
        blync_gif_animator= GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (blync_gif_animator != null){
            if(Input.GetKeyDown(KeyCode.I)){
                blync_gif_animator.SetTrigger("BlyncIn");
            }else if(Input.GetKeyDown(KeyCode.O)){
                blync_gif_animator.SetTrigger("BlyncOut");
            }
        }
    }
}
