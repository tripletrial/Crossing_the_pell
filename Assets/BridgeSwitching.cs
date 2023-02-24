using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeSwitching : MonoBehaviour
{
        //  GameObject Inhabited_Bridge = transform.Find("Sofia Scene Objects").gameObject;
        //  GameObject Conductivity = transform.Find("Demi Scene Elements").gameObject;
        //  GameObject Stage = transform.Find("Saira Scene Elements").gameObject;
        //  GameObject Net = transform.Find("The Net Scene Elements").gameObject;
    // Update is called once per frame
    void Start()
    {
       
    }

    void Update(){
        GameObject Inhabited_Bridge = transform.Find("Sofia Scene Objects").gameObject;
         GameObject Conductivity = transform.Find("Demi Scene Elements").gameObject;
         GameObject Stage = transform.Find("Saira Scene Elements").gameObject;
         GameObject Net = transform.Find("The Net Scene Elements").gameObject;

        Dissolver_Stage Ib_dissolve = Inhabited_Bridge.GetComponent<Dissolver_Stage>();
        Dissolver_Stage C_dissolve = Conductivity.GetComponent<Dissolver_Stage>();
        Dissolver_Stage S_dissolve = Stage.GetComponent<Dissolver_Stage>();
        Dissolver_Stage N_dissolve = Net.GetComponent<Dissolver_Stage>();
        if(Input.GetKey(KeyCode.Alpha1)){
            
            C_dissolve.show = false;
            S_dissolve.show = false;
            N_dissolve.show = false;
            //Ib_dissolve.show = true;

            StartCoroutine(show(Ib_dissolve));
        } else if (Input.GetKey(KeyCode.Alpha2)){
            Ib_dissolve.show = false;
            
            S_dissolve.show = false;
            N_dissolve.show = false;
           // C_dissolve.show = true;
            StartCoroutine(show(C_dissolve));
        } else if (Input.GetKey(KeyCode.Alpha3)){
            Ib_dissolve.show = false;
            C_dissolve.show = false;
            
            N_dissolve.show = false;
           // S_dissolve.show = true;
               
            StartCoroutine(show(S_dissolve));
        } else if (Input.GetKey(KeyCode.Alpha4)){
            Ib_dissolve.show = false;
            C_dissolve.show = false;
            S_dissolve.show = false;
           // N_dissolve.show = true;]
            StartCoroutine(show(N_dissolve));
        } 
    }
    IEnumerator show(Dissolver_Stage desolve){
        yield return new WaitForSeconds(1);
        desolve.show = true;
    }
    // }
    // IEnumerator show_C(){
    //     yield return new WaitForSeconds(1);
    //     C_dissolve.show = true;
    // }
    // IEnumerator show_S(){
    //     yield return new WaitForSeconds(1);
    //     S_dissolve.show = true;
    // }
    // IEnumerator show_N(){
    //     yield return new WaitForSeconds(1);
    //     N_dissolve.show = true;
    // }
}
