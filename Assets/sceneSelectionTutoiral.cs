using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class sceneSelectionTutoiral : MonoBehaviour
{
    // Start is called before the first frame update
    public bool Skip_Tutorial = false;
    public bool load_scene = false;
    public TutorialData tutorialData;
    // Update is called once per frame
    void Update()
    {
        if (load_scene == true){
            if(Skip_Tutorial == true){
                tutorialData.Skip_Tutorial = true;
                SceneManager.LoadScene("0_TutorialScene");
            } else{
                tutorialData.Skip_Tutorial = false;
                SceneManager.LoadScene("0_TutorialScene");
            }
        }
    }
}
