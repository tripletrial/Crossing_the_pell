using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;    

public static class FadeAudioSource {
    public static IEnumerator StartFade(AudioSource audioSource, float duration, float targetVolume)
    {
        float currentTime = 0;
        float start = audioSource.volume;
        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(start, targetVolume, currentTime / duration);
            yield return null;
        }
        yield break;
    }
}

public class Indoor_Outdoor_Transition : MonoBehaviour
{

    public AudioMixer audiomixer;

    void OnTriggerEnter(Collider indoor_trigger){
        Debug.Log(indoor_trigger.transform.parent.name + " has entered the trigger");
        if (indoor_trigger.transform.parent.name == "Indoors_colliders"){
            ChangeAudioToIndoor();
        }
        if (indoor_trigger.transform.parent.name== "Outdoors_colliders"){
            ChangeAudioToOutdoor();
        }
    }
    void ChangeAudioToIndoor(){
        
        StartCoroutine(FadeMixerGroup.StartFade(audiomixer, "Outdoor",0.5f, 0.5f));
        Debug.Log("Indoor Sounds Start");

    
    }

    void ChangeAudioToOutdoor(){
        StartCoroutine(FadeMixerGroup.StartFade(audiomixer, "Outdoor",0.5f, 1f));
       
    }
}
