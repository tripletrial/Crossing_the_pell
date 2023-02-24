using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class speedometerAnimation : MonoBehaviour
{
    // Start is called before the first frame update
    public bool open;
    public languageBool languageBool;
    private bool opened;
    private  List<Renderer> Matrixes;
    private  List<Renderer> MenuTexts;
    private List<Renderer> Variables;
    SpriteRenderer MenuImage;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(languageBool.isEn){

        if (open && !opened)
        {
            Matrixes = new List<Renderer>(transform.Find("Text(English)").transform.Find("Matrices").GetComponentsInChildren<Renderer>());
            MenuTexts = new List<Renderer>(transform.Find("Text(English)").transform.Find("Menu").GetComponentsInChildren<Renderer>());
            Variables = new List<Renderer>(transform.Find("Text").GetComponentsInChildren<Renderer>());
            MenuImage = transform.Find("MenuImage").transform.Find("Image").transform.Find("Speedometer-06").GetComponent<SpriteRenderer>();
            foreach (Renderer t in MenuTexts)
            {
                t.enabled = true;
            }
            foreach (Renderer t in Matrixes)
            {
                t.enabled = false;
            }
            foreach (Renderer t in Variables)
            {
                t.enabled = false;
            }
            MenuImage.enabled = true;
            Animator speedometerAnimator = GetComponent<Animator>();
            speedometerAnimator.SetTrigger("SpeedoMeterOpen");
            opened = true;
        } else if(!open && opened){
            Matrixes = new List<Renderer>(transform.Find("Text(English)").transform.Find("Matrices").GetComponentsInChildren<Renderer>());
            MenuTexts = new List<Renderer>(transform.Find("Text(English)").transform.Find("Menu").GetComponentsInChildren<Renderer>());
            foreach (Renderer t in MenuTexts)
            {
                t.enabled = false;
            }
            foreach (Renderer t in Matrixes)
            {
                t.enabled = true;
            }
            foreach (Renderer t in Variables)
            {
                t.enabled = true;
            }

            MenuImage.enabled = false;
            Animator speedometerAnimator = GetComponent<Animator>();
            speedometerAnimator.SetTrigger("SpeedoMeterClose");
            opened = false;
        }
        }
        else{
            if (open && !opened)
        {
            Matrixes = new List<Renderer>(transform.Find("Text(Spanish)").transform.Find("Matrices").GetComponentsInChildren<Renderer>());
            MenuTexts = new List<Renderer>(transform.Find("Text(Spanish)").transform.Find("Menu").GetComponentsInChildren<Renderer>());
            foreach (Renderer t in MenuTexts)
            {
                t.enabled = true;
            }
            foreach (Renderer t in Matrixes)
            {
                t.enabled = false;
            }
            foreach (Renderer t in Variables)
            {
                t.enabled = false;
            }
            MenuImage.enabled = true;
            
            Animator speedometerAnimator = GetComponent<Animator>();
            speedometerAnimator.SetTrigger("SpeedoMeterOpen");
            opened = true;
        } else if(!open && opened){
            Matrixes = new List<Renderer>(transform.Find("Text(Spanish)").transform.Find("Matrices").GetComponentsInChildren<Renderer>());
            MenuTexts = new List<Renderer>(transform.Find("Text(Spanish)").transform.Find("Menu").GetComponentsInChildren<Renderer>());
            foreach (Renderer t in MenuTexts)
            {
                t.enabled = false;
            }
            foreach (Renderer t in Matrixes)
            {
                t.enabled = true;
            }
            foreach (Renderer t in Variables)
            {
                t.enabled = true;
            }
            MenuImage.enabled = false;

            Animator speedometerAnimator = GetComponent<Animator>();
            speedometerAnimator.SetTrigger("SpeedoMeterClose");
            opened = false;
        }   
        }
    }
}
