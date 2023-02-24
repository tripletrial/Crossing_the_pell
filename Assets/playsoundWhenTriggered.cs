using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playsoundWhenTriggered : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] audioSources;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            foreach (GameObject audioSource in audioSources)
            {
                audioSource.SetActive(true);
                audioSource.GetComponent<AudioSource>().Play();
            }
            Debug.Log("Playing the sound");
        }
    }
}
