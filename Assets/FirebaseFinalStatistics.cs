using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
public class FirebaseFinalStatistics : MonoBehaviour
{
    [SerializeField]
    private PlayerData Player;

    float tempTime = 0;
    float tempDistance = 0;
    float tempMinutes;
    float tempSeconds;
    float unvisitedCount;
    private string firestoreUrlPrefix = "https://firestore.googleapis.com/v1/projects/pellbridgestatistics/databases/(default)/documents/APITesting/";
    string urlBikeType ;
    public BikeType bikeType;
    private void Start()
    {
         //sum up all the distance traveled in each project from scene data
        Player.DistanceTraveled = Player.Scene_1.distanceTraveled + Player.Scene_2.distanceTraveled + Player.Scene_3.distanceTraveled + Player.Scene_4.distanceTraveled;
        tempTime = Player.Scene_1.timeSpent + Player.Scene_2.timeSpent + Player.Scene_3.timeSpent + Player.Scene_4.timeSpent;
        tempMinutes = Mathf.Floor(tempTime / 60);
        tempSeconds = Mathf.RoundToInt(tempTime % 60);

        if (bikeType.kidsBike)
        {
            if (bikeType.Blue)
            {
                urlBikeType = "KidsBikeBlue/Instances/";
            }
            else
            {
                urlBikeType = "KidsBikeRed/Instances/";
            }
        }
        else
        {
            if (bikeType.Blue)
            {
                urlBikeType = "AdultBikeBlue/Instances/";
            }
            else
            {
                urlBikeType = "AdultBikeRed/Instances/";
            }
        }
        firestoreUrlPrefix += urlBikeType;
        StartCoroutine(PostData());
    }

    private IEnumerator PostData()
    {
        // Create the request body
        var request = new UnityWebRequest(firestoreUrlPrefix, "POST");
        string requestBody =  @"{
            'fields': {
                'DistanceTraveled': {
                    'doubleValue': " + Player.DistanceTraveled + @"
                },
                'TimeSpent': {
                    'doubleValue': " + tempTime + @"
                }
            }
        }";
        byte[] bodyRaw = Encoding.UTF8.GetBytes(requestBody);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        // Check for errors
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(request.error);
            Debug.Log(request.downloadHandler.text);
        }
        else
        {
            Debug.Log(request.downloadHandler.text);
        }
    }
}
