using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTpParticle : MonoBehaviour
{
    // Start is called before the first frame update
    public ParticleSystem[] particles;



    void OnTriggerEnter(Collider other){
        if(other.gameObject.tag == "Player"){
            foreach(ParticleSystem particle in particles){
                particle.Play();
            }
        }
}
}