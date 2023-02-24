using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bridgeLooping : MonoBehaviour
{
    // Start is called before the first frame update
    public float waitTime;
    public bool isLooping = true;
    private GameObject Inhabited_Bridge;
    private GameObject Conductivity;
    private GameObject Stage;
    private GameObject Net;
    private Dissolver_Stage Ib_dissolve;
    private Dissolver_Stage C_dissolve;
    private Dissolver_Stage S_dissolve;
    private Dissolver_Stage N_dissolve;

    
    void Start()
    {
        

        StartCoroutine(show_IB());
    }
    // Update is called once per frame
    IEnumerator show_IB(){
        GameObject Inhabited_Bridge = transform.Find("Sofia Scene Objects").gameObject;
         GameObject Conductivity = transform.Find("Demi Scene Elements").gameObject;
         GameObject Stage = transform.Find("Saira Scene Elements").gameObject;
         GameObject Net = transform.Find("The Net Scene Elements").gameObject;

        Dissolver_Stage Ib_dissolve = Inhabited_Bridge.GetComponent<Dissolver_Stage>();
        Dissolver_Stage C_dissolve = Conductivity.GetComponent<Dissolver_Stage>();
        Dissolver_Stage S_dissolve = Stage.GetComponent<Dissolver_Stage>();
        Dissolver_Stage N_dissolve = Net.GetComponent<Dissolver_Stage>();

        //Debug.Log(Ib_dissolve);
        C_dissolve.show = false;
        S_dissolve.show = false;
        N_dissolve.show = false;

        StartCoroutine(show(Ib_dissolve));
        yield return new WaitForSeconds(waitTime);
        if (isLooping == true){
            StartCoroutine(show_C());
        }
        
    }
    IEnumerator show_C(){
        GameObject Inhabited_Bridge = transform.Find("Sofia Scene Objects").gameObject;
         GameObject Conductivity = transform.Find("Demi Scene Elements").gameObject;
         GameObject Stage = transform.Find("Saira Scene Elements").gameObject;
         GameObject Net = transform.Find("The Net Scene Elements").gameObject;

        Dissolver_Stage Ib_dissolve = Inhabited_Bridge.GetComponent<Dissolver_Stage>();
        Dissolver_Stage C_dissolve = Conductivity.GetComponent<Dissolver_Stage>();
        Dissolver_Stage S_dissolve = Stage.GetComponent<Dissolver_Stage>();
        Dissolver_Stage N_dissolve = Net.GetComponent<Dissolver_Stage>();
        Ib_dissolve.show = false;
        //C_dissolve.show = true;
        S_dissolve.show = false;
        N_dissolve.show = false;
        StartCoroutine(show(C_dissolve));
        yield return new WaitForSeconds(waitTime);
        if (isLooping == true){
            StartCoroutine(show_S());
        }

    }   
    IEnumerator show_S(){
        //yield return new WaitForSeconds(waitTime);
        GameObject Inhabited_Bridge = transform.Find("Sofia Scene Objects").gameObject;
         GameObject Conductivity = transform.Find("Demi Scene Elements").gameObject;
         GameObject Stage = transform.Find("Saira Scene Elements").gameObject;
         GameObject Net = transform.Find("The Net Scene Elements").gameObject;

        Dissolver_Stage Ib_dissolve = Inhabited_Bridge.GetComponent<Dissolver_Stage>();
        Dissolver_Stage C_dissolve = Conductivity.GetComponent<Dissolver_Stage>();
        Dissolver_Stage S_dissolve = Stage.GetComponent<Dissolver_Stage>();
        Dissolver_Stage N_dissolve = Net.GetComponent<Dissolver_Stage>();
        Ib_dissolve.show = false;
        C_dissolve.show = false;

        N_dissolve.show = false;
        StartCoroutine(show(S_dissolve));
        yield return new WaitForSeconds(waitTime);
        if (isLooping == true){
            StartCoroutine(show_N());
        }

    }
    IEnumerator show_N(){
        //yield return new WaitForSeconds(waitTime);
        GameObject Inhabited_Bridge = transform.Find("Sofia Scene Objects").gameObject;
         GameObject Conductivity = transform.Find("Demi Scene Elements").gameObject;
         GameObject Stage = transform.Find("Saira Scene Elements").gameObject;
         GameObject Net = transform.Find("The Net Scene Elements").gameObject;

        Dissolver_Stage Ib_dissolve = Inhabited_Bridge.GetComponent<Dissolver_Stage>();
        Dissolver_Stage C_dissolve = Conductivity.GetComponent<Dissolver_Stage>();
        Dissolver_Stage S_dissolve = Stage.GetComponent<Dissolver_Stage>();
        Dissolver_Stage N_dissolve = Net.GetComponent<Dissolver_Stage>();

        Ib_dissolve.show = false;
        C_dissolve.show = false;
        S_dissolve.show = false;
        StartCoroutine(show(N_dissolve));
        yield return new WaitForSeconds(waitTime);
        if (isLooping == true){
            StartCoroutine(show_IB());
        }
    }
    IEnumerator show(Dissolver_Stage desolve){
        yield return new WaitForSeconds(4);
        desolve.show = true;
    }

}
