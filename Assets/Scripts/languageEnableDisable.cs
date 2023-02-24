using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class languageEnableDisable : MonoBehaviour
{
    public List<GameObject> englishObjectsList;
    public List<GameObject> spanishObjectsList;
    public languageBool myLB;

    void Update()
    {
        UpdateLang(myLB.isEn);
    }

    private void UpdateLang(bool isEn)
    {
        if (isEn)
        {
            foreach (GameObject obj in englishObjectsList)
            {
                obj.SetActive(true);
            }
            foreach (GameObject obj in spanishObjectsList)
            {
                obj.SetActive(false);
            }
        }
        else
        {
            foreach (GameObject obj in englishObjectsList)
            {
                obj.SetActive(false);
            }
            foreach (GameObject obj in spanishObjectsList)
            {
                obj.SetActive(true);
            }
        }
    }
}
