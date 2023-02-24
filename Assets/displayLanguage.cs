using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class displayLanguage : MonoBehaviour
{
    
    public GameObject English;
    public GameObject Spanish;
    public languageBool mylanguage; 

    void Start()
    {
        if(mylanguage.isEn == true)
        {
            English.SetActive(true);
            Spanish.SetActive(false);
        }
        else
        {
            English.SetActive(false);
            Spanish.SetActive(true);
        }
    }


}
