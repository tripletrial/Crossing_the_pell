using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class recenter_on_load : MonoBehaviour
{
    
    [SerializeField] Transform resetTransform;
    [SerializeField] GameObject player;
    [SerializeField] Camera playerHead;
    
    public AudioSource Tutorial_Voice;
    public AudioSource connectedSound;
    // // Start is called before the first frame update
    // void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    // {
    //     Debug.Log("OnSceneLoaded: " + scene.name);
    //     Debug.Log(mode);
        
    // }
    void Start()
    {
        Tutorial_Voice.Play();
        StartCoroutine(waiter());
    }
    IEnumerator waiter()
    {
        yield return new WaitForSeconds(4);
        recenter();
        connectedSound.Play();
    }
    public void recenter()
    {
        
        var rotationAngley = playerHead.transform.rotation.eulerAngles.y - resetTransform.rotation.eulerAngles.y;
        player.transform.Rotate(0, -rotationAngley, 0, Space.Self);

        var distanceDiff = resetTransform.position- playerHead.transform.position;
        player.transform.position += distanceDiff;
        Debug.Log("REcenter");
    }
}
