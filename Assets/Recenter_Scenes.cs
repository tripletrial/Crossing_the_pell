using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recenter_Scenes : MonoBehaviour
{
   [SerializeField] Transform resetTransform;
    [SerializeField] GameObject player;
    [SerializeField] Camera playerHead;

    public AudioSource connectedSound;
    // // Start is called before the first frame update
    // void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    // {
    //     Debug.Log("OnSceneLoaded: " + scene.name);
    //     Debug.Log(mode);
        
    // }
    void Start()
    {
        StartCoroutine(waiter_recenter());
        Debug.Log("Recentering Code is being run");
    }
    IEnumerator waiter_recenter()
    {
        yield return new WaitForSeconds(1);
        //Debug.Log("REcenter");
        recenter();
        //Debug.Log("REcentered");
        connectedSound.Play();
    }
    public void recenter()
    {
        
        var rotationAngley = playerHead.transform.rotation.eulerAngles.y - resetTransform.rotation.eulerAngles.y;
        player.transform.Rotate(0, -rotationAngley, 0, Space.Self);

        var distanceDiff = resetTransform.position- playerHead.transform.position;
        player.transform.position += distanceDiff;
        //Debug.Log("REcenter");
    }
}
