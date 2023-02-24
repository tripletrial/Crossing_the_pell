using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playWhenEnabledList : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] audioSource;

    // Update is called once per frame
void OnEnable()
    {
        foreach (GameObject t in audioSource)
        {
            t.GetComponent<AudioSource>().Play();
        }
    }
}
