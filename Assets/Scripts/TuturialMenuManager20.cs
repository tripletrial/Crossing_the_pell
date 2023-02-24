using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine.SceneManagement;

public class TuturialMenuManager20 : MonoBehaviour
{
    #region field
    [SerializeField] ControllerInputManagement30 controllerInputManager;
    [SerializeField] TextMeshProUGUI tuturialMsgInfo;
    [SerializeField] languageBool myLB;

    Dictionary<string, string> messageInfoDict = new Dictionary<string, string>();
    Dictionary<string, string> messageInfoDictEN = new Dictionary<string, string>();
    Dictionary<string, string> messageInfoDictSP = new Dictionary<string, string>();

    bool isEn;
    
    int highlightFontSize = 36;

    bool hasSceneMenuShowed;
    bool hasRightTriggerClicken;
    bool hasLeftTriggerClicken;
    bool hasMenuClosed;

    string leftTriggerColor = "#dc143c";
    string rightTriggerColor = "#add8e6";

    // bool hasSubSceneMenuShowed;
    #endregion
    #region method
    // Start is called before the first frame update
    void Start()
    {
        isEn = myLB.isEn;
        #region checkNull
        if (controllerInputManager != null)
        {
            Debug.Log($"---success: controllerInputManager loaded---");
        }
        else
        {
            Debug.Log($"---Fail: controllerInputManager not loaded---");
        }
        #endregion
        if (SceneManager.GetActiveScene().name == "0_StartingScene 1") 
        {
            if (isEn)
            {
                StartCoroutine(UpdateTuturialMsgBox($"Hello, welcome to the menu tutorial. \nPlease press <b><color=red><size={highlightFontSize}>LEFT</size></color></b> triggers to call menu"));
            }
            else
            {
                StartCoroutine(UpdateTuturialMsgBox($"Hola, bienvenido al menú tutorial\npresione los disparadores <b><color={leftTriggerColor}><size={highlightFontSize}>IZQUIERDOS</size></color></b> para acceder al menú <b>LLAMAR</b>"));
            }
            
        }
        else {
            // in other scenes, there is no need to showup the tutorial texts
            tuturialMsgInfo.text = "";
        }
        #region implementDict
        messageInfoDictEN.Add("teach to show the menu",
            $"Hi \nPlease press <b><color={leftTriggerColor}><size={highlightFontSize}>LEFT</size></color></b> triggers to \n<b>call</b> menu");
        messageInfoDictEN.Add("teach to switch the button", 
            $"Great! \nPress <b><color={rightTriggerColor}><size={highlightFontSize}>RIGHT</size></color></b> trigger to \n<b>switch</b> the selected button");
        messageInfoDictEN.Add("teach to confirm and click the button", 
            $"Awesome!\nPlease press <b><color={leftTriggerColor}><size={highlightFontSize}>LEFT</size></color></b> triggers to \n<b>load</b> the selected button");
        messageInfoDictEN.Add("teach in the sub menu scene",
            $"Awesome!\nPlease press <b><color={leftTriggerColor}><size={highlightFontSize}>LEFT</size></color></b> triggers to \n<b>load</b> the selected button \n Press <b><color=#add8e6><size={highlightFontSize}>RIGHT</size></color></b> trigger to \n<b>switch</b> the selected button");

        // TO DO: implement the spanish menu texts
        messageInfoDictSP.Add("teach to show the menu",
            $"Hola \npresione los disparadores <b><color={leftTriggerColor}><size={highlightFontSize}>IZQUIERDOS</size></color></b> para acceder al menú <b>LLAMAR</b>");
        messageInfoDictSP.Add("teach to switch the button",
            $"¡Excelente! \nPresione el disparador <b><color={rightTriggerColor}><size={highlightFontSize}>RIGHT</size></color></b> para \n<b>cambiar</b> el botón seleccionado");
        messageInfoDictSP.Add("teach to confirm and click the button",
            $"¡Impresionante!\nPresione <b><color={leftTriggerColor}><size{highlightFontSize}>LEFT</size></color></b> disparadores para \n<b>cargar</b> el botón seleccionado");
        messageInfoDictSP.Add("teach in the sub menu scene",
            $"¡Impresionante!\nPresione <b><color={leftTriggerColor}><size={highlightFontSize}>LEFT</size></color></b> disparadores para \n<b>cargar</b> el botón seleccionado \n Presione el disparador <b><color=#add8e6><size={highlightFontSize}>RIGHT</size></color></b> para \n<b>cambiar</b> el botón seleccionado");
        #endregion
        messageInfoDict = isEn ? messageInfoDictEN : messageInfoDictSP;
    }

    // Update is called once per frame
    void Update()
    {
        if (controllerInputManager != null)
        {
            hasSceneMenuShowed = controllerInputManager.HasSceneMenuShowed;
            hasRightTriggerClicken = controllerInputManager.HasRightTriggerClicken;
            hasLeftTriggerClicken = controllerInputManager.HasLeftTriggerClicken;
            hasMenuClosed = controllerInputManager.HasMenuClosed;
        }

        if (!hasSceneMenuShowed)
        {
            tuturialMsgInfo.text = messageInfoDict["teach to show the menu"];
        }
        else if(hasSceneMenuShowed && !hasRightTriggerClicken && !hasLeftTriggerClicken)
        {
            tuturialMsgInfo.text = messageInfoDict["teach to switch the button"];
        }
        else if(hasSceneMenuShowed && hasRightTriggerClicken && !hasLeftTriggerClicken)
        {
            tuturialMsgInfo.text = messageInfoDict["teach to confirm and click the button"];
        }
        else if(hasSceneMenuShowed && hasRightTriggerClicken && hasLeftTriggerClicken && !hasMenuClosed)
        {
            tuturialMsgInfo.text = messageInfoDict["teach in the sub menu scene"];
        }
        else
        {
            tuturialMsgInfo.text = "";
        }
    }

    IEnumerator UpdateTuturialMsgBox(string msg)
    {
        tuturialMsgInfo.text = "";
        
        for (int i= 0; i < msg.ToCharArray().Length; i++)
        {
            yield return new WaitForSeconds(0.5f);
            tuturialMsgInfo.text += msg.ToCharArray()[i];
        }
    }
    #endregion
}
