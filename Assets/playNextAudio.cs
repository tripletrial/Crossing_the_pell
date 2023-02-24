using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playNextAudio : MonoBehaviour
{
    // Start is called before the first frame update
    public float waitTime;
    public GameObject nextAudioSource;
    void Start()
    {
        StartCoroutine(playNextAudioSource());
    }

    IEnumerator playNextAudioSource()
    {
        yield return new WaitForSeconds(waitTime);
        nextAudioSource.SetActive(true);
        if (nextAudioSource.GetComponent<AudioSource>().playOnAwake != true)
        {
            nextAudioSource.GetComponent<AudioSource>().Play();
        }
    }
}
