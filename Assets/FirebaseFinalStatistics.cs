using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;

public class FirebaseFinalStatistics : MonoBehaviour
{
    [SerializeField]
    private PlayerData Player;

    float tTime = 0;
    float tempDistance = 0;
    float tempMinutes;
    float tempSeconds;
    float unvisitedCount;
    private string firestoreUrlPrefix =
        "https://firestore.googleapis.com/v1/projects/pellbridgestatistics/databases/(default)/documents/gameStatistics/";
    private string patchUrl =
        "https://firestore.googleapis.com/v1beta1/projects/pellbridgestatistics/databases/(default)/documents/gameStatistics/";
    string urlBikeType;
    public BikeType bikeType;
    int bikeTypeIndex;
    string subfix = "?updateMask.fieldPaths=";
    string largeKidBike = "LARGE%20KID%20BIKE";
    string smallKidBike = "SMALL%20KID%20BIKE";
    string tricycle = "TRICYCLE";
    string adultBike = "ADULT%20BIKE";
    string TotalBikeStatistics = "TotalBikeStatistics";
    string urlSubfix;

    // set up variables for current bike
    [System.Serializable]
    public class currentBike
    {
        public string bikeType;
        public double totalBikedDistanceCurrent;
        public int bikeTimesCurrent;
        public double averageDistanceCurrent;
        public string totalBikedDurationCurrent;
        public string averageTimeCurrent;
    }

    currentBike currentBikeData = new currentBike();
    currentBike totalBikeData = new currentBike();

    [System.Serializable]
    public class GameStatistics
    {
        public string name;
        public GameStatisticsFields fields;
        public string createTime;
        public string updateTime;
    }

    [System.Serializable]
    public class GameStatisticsFields
    {
        public DoubleValue totalBikedDistance;
        public IntegerValue bikeTimes;
        public DoubleValue averageDistance;
        public StringValue totalBikedDuration;
        public StringValue averageTime;
    }

    [System.Serializable]
    public class DoubleValue
    {
        public double doubleValue;
    }

    [System.Serializable]
    public class IntegerValue
    {
        public int integerValue;
    }

    [System.Serializable]
    public class StringValue
    {
        public string stringValue;
    }

    private void Start()
    {
        //sum up all the distance traveled in each project from scene data
        Player.DistanceTraveled =
            Player.Scene_1.distanceTraveled
            + Player.Scene_2.distanceTraveled
            + Player.Scene_3.distanceTraveled
            + Player.Scene_4.distanceTraveled;
        tTime =
            Player.Scene_1.timeSpent
            + Player.Scene_2.timeSpent
            + Player.Scene_3.timeSpent
            + Player.Scene_4.timeSpent;
        tempMinutes = Mathf.Floor(tTime / 60);
        tempSeconds = Mathf.RoundToInt(tTime % 60);

        if (bikeType.kidsBike)
        {
            if (bikeType.Blue)
            {
                urlBikeType = "LARGE%20KID%20BIKE";
                currentBikeData.bikeType = "LARGE KID BIKE";
                urlSubfix = largeKidBike + subfix;
            }
            else
            {
                urlBikeType = "SMALL%20KID%20BIKE";
                currentBikeData.bikeType = "SMALL KID BIKE";
                urlSubfix = smallKidBike + subfix;
            }
        }
        else
        {
            if (bikeType.Blue)
            {
                urlBikeType = "TRICYCLE";
                currentBikeData.bikeType = "TRICYCLE";
                urlSubfix = tricycle + subfix;
            }
            else
            {
                urlBikeType = "ADULT%20BIKE";
                currentBikeData.bikeType = "ADULT BIKE";
                urlSubfix = adultBike + subfix;
            }
        }
        StartCoroutine(GetData());
        StartCoroutine(GetDataTotalStatistics());
    }

    private IEnumerator GetDataTotalStatistics()
    {
        var request = new UnityWebRequest(firestoreUrlPrefix + "TotalBikeStatistics", "GET");
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();
        // Check for errors
        if (
            request.result == UnityWebRequest.Result.ConnectionError
            || request.result == UnityWebRequest.Result.ProtocolError
        )
        {
            Debug.LogError(request.error);
            Debug.Log(request.downloadHandler.text);
        }
        else
        {
            Debug.Log(request.downloadHandler.text);
            GameStatistics gameStatistics = JsonUtility.FromJson<GameStatistics>(
                request.downloadHandler.text
            );
            // fill in values for totalBikeData
            totalBikeData.totalBikedDistanceCurrent = gameStatistics
                .fields
                .totalBikedDistance
                .doubleValue;
            totalBikeData.bikeTimesCurrent = gameStatistics.fields.bikeTimes.integerValue;
            totalBikeData.averageDistanceCurrent = gameStatistics
                .fields
                .averageDistance
                .doubleValue;
            totalBikeData.totalBikedDurationCurrent = gameStatistics
                .fields
                .totalBikedDuration
                .stringValue;
            totalBikeData.averageTimeCurrent = gameStatistics.fields.averageTime.stringValue;
            // update values for totalBikeData
            totalBikeData.totalBikedDistanceCurrent += Player.DistanceTraveled;
            totalBikeData.bikeTimesCurrent += 1;

            // round the average distance to 2 decimal places
            totalBikeData.averageDistanceCurrent =
                Mathf.Round(
                    (float)totalBikeData.totalBikedDistanceCurrent / totalBikeData.bikeTimesCurrent * 100
                ) / 100;
            // the totalBikedDuration is formatted as "hours:minutes:seconds"
            // so we need to split it up and add the new time to it
            string[] totalBikedDurationArray = totalBikeData.totalBikedDurationCurrent.Split(':');
            int hours = int.Parse(totalBikedDurationArray[0]);
            int minutes = int.Parse(totalBikedDurationArray[1]);
            int seconds = int.Parse(totalBikedDurationArray[2]);
            seconds += Mathf.RoundToInt(tTime);
            minutes += Mathf.FloorToInt(seconds / 60);
            seconds = Mathf.RoundToInt(seconds % 60);
            hours += Mathf.FloorToInt(minutes / 60);
            minutes = Mathf.RoundToInt(minutes % 60);
            totalBikeData.totalBikedDurationCurrent = hours + ":" + minutes + ":" + seconds;
            // the averageTime is formatted as "minutes:seconds"
            // so we need to split it up and add the new time to it
            string[] averageTimeArray = totalBikeData.averageTimeCurrent.Split(':');
            minutes = int.Parse(averageTimeArray[0]);
            seconds = int.Parse(averageTimeArray[1]);
            seconds += Mathf.RoundToInt(tTime);
            minutes += Mathf.FloorToInt(seconds / 60);
            seconds = Mathf.RoundToInt(seconds % 60);
            totalBikeData.averageTimeCurrent = minutes + ":" + seconds;
            // update the database
            PatchDataTotalStatistics();
        }
    }

    // wrie an IEnumerator to get data from the database
    private IEnumerator GetData()
    {
        // this method is used to get data from the database
        // Create the request body
        var request = new UnityWebRequest(firestoreUrlPrefix + urlBikeType, "GET");
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();
        // Check for errors
        if (
            request.result == UnityWebRequest.Result.ConnectionError
            || request.result == UnityWebRequest.Result.ProtocolError
        )
        {
            Debug.LogError(request.error);
            Debug.Log(request.downloadHandler.text);
        }
        else
        {
            Debug.Log(request.downloadHandler.text);
            GameStatistics gameStatistics = JsonUtility.FromJson<GameStatistics>(
                request.downloadHandler.text
            );
            // fill in values for current bike
            currentBikeData.totalBikedDistanceCurrent = gameStatistics
                .fields
                .totalBikedDistance
                .doubleValue;
            currentBikeData.bikeTimesCurrent = gameStatistics.fields.bikeTimes.integerValue;
            currentBikeData.averageDistanceCurrent = gameStatistics
                .fields
                .averageDistance
                .doubleValue;
            currentBikeData.totalBikedDurationCurrent = gameStatistics
                .fields
                .totalBikedDuration
                .stringValue;
            currentBikeData.averageTimeCurrent = gameStatistics.fields.averageTime.stringValue;
            // update values for current bike
            currentBikeData.totalBikedDistanceCurrent += Player.DistanceTraveled;
            currentBikeData.bikeTimesCurrent += 1;
            // round the average distance to 2 decimal places
            currentBikeData.averageDistanceCurrent =
                Mathf.Round(
                    (float)currentBikeData.totalBikedDistanceCurrent
                        / currentBikeData.bikeTimesCurrent
                        * 100f
                ) / 100f;
            // the totalBikedDuration is formatted as "hours:minutes:seconds"
            // so we need to split it up and add the new time to it
            string[] tempTime = currentBikeData.totalBikedDurationCurrent.Split(':');
            int tempHours = int.Parse(tempTime[0]);
            int tempMinutes = int.Parse(tempTime[1]);
            int tempSeconds = int.Parse(tempTime[2]);
            tempSeconds += Mathf.RoundToInt(tTime % 60);
            if (tempSeconds >= 60)
            {
                tempMinutes += 1;
                tempSeconds -= 60;
            }
            tempMinutes += Mathf.FloorToInt(tTime / 60);
            if (tempMinutes >= 60)
            {
                tempHours += 1;
                tempMinutes -= 60;
            }
            currentBikeData.totalBikedDurationCurrent =
                tempHours.ToString() + ":" + tempMinutes.ToString() + ":" + tempSeconds.ToString();
            // the averageTime is formatted as "minutes:seconds"
            // so we calculate the average time and then format it
            var tempTimeTotal = tempHours * 3600 + tempMinutes * 60 + tempSeconds;
            var tempTimeAverage = Mathf.RoundToInt(
                tempTimeTotal / currentBikeData.bikeTimesCurrent
            );
            tempMinutes = Mathf.FloorToInt(tempTimeAverage / 60);
            tempSeconds = Mathf.RoundToInt(tempTimeAverage % 60);
            currentBikeData.averageTimeCurrent =
                tempMinutes.ToString() + ":" + tempSeconds.ToString();

            // update values for database
            Debug.Log(currentBikeData.bikeTimesCurrent);
            PatchDataFor();
        }
    }

    public void PatchDataTotalStatistics()
    {
        StartCoroutine(PatchDataBikeTime(TotalBikeStatistics+subfix, totalBikeData));
        StartCoroutine(PatchDataTotalBikedDistance(TotalBikeStatistics+subfix, totalBikeData));
        StartCoroutine(PatchDataAverageDistance(TotalBikeStatistics+subfix, totalBikeData));
        StartCoroutine(PatchDataTotalBikedDuration(TotalBikeStatistics+subfix, totalBikeData));
        StartCoroutine(PatchDataAverageTime(TotalBikeStatistics+subfix, totalBikeData));
        
    }

    public void PatchDataFor()
    {
        StartCoroutine(PatchDataBikeTime(urlSubfix,currentBikeData));
        StartCoroutine(PatchDataTotalBikedDistance(urlSubfix,currentBikeData));
        StartCoroutine(PatchDataAverageDistance(urlSubfix,currentBikeData));
        StartCoroutine(PatchDataTotalBikedDuration(urlSubfix,currentBikeData));
        StartCoroutine(PatchDataAverageTime(urlSubfix,currentBikeData));
        // patch the data to the totalBikedDistance
    }

    // write an IEnumerator to patch data to the database
    private IEnumerator PatchDataBikeTime(string urlsubfixAndBikeType, currentBike bikeData)
    {
        var tempPatchUrl = patchUrl + urlsubfixAndBikeType + "bikeTimes";
        // Create the request body
        var request = new UnityWebRequest(tempPatchUrl, "PATCH");
        // write the request body in JSON format make sure to use double quotes only do biketimes
        string requestBody =
            @"{
            'fields': {
                'bikeTimes': {
                    'integerValue': "
            + bikeData.bikeTimesCurrent
            + @"
                }
            }
        }";
        Debug.Log(requestBody);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(requestBody);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        // Check for errors

        if (
            request.result == UnityWebRequest.Result.ConnectionError
            || request.result == UnityWebRequest.Result.ProtocolError
        )
        {
            Debug.LogError(request.error);
            Debug.Log(request.downloadHandler.text);
        }
        else
        {
            Debug.Log(request.downloadHandler.text);
        }
    }

    private IEnumerator PatchDataTotalBikedDistance(string urlsubfixAndBikeType, currentBike bikeData)
    {
        var tempPatchUrl = patchUrl + urlsubfixAndBikeType + "totalBikedDistance";
        // Create the request body
        var request = new UnityWebRequest(tempPatchUrl, "PATCH");
        // write the request body in JSON format make sure to use double quotes only do biketimes
        string requestBody =
            @"{
            'fields': {
                'totalBikedDistance': {
                    'doubleValue': "
            + bikeData.totalBikedDistanceCurrent
            + @"
                }
            }
        }";
        Debug.Log(requestBody);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(requestBody);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        // Check for errors

        if (
            request.result == UnityWebRequest.Result.ConnectionError
            || request.result == UnityWebRequest.Result.ProtocolError
        )
        {
            Debug.LogError(request.error);
            Debug.Log(request.downloadHandler.text);
        }
        else
        {
            Debug.Log(request.downloadHandler.text);
        }
    }

    private IEnumerator PatchDataAverageDistance(string urlsubfixAndBikeType, currentBike bikeData)
    {
        var tempPatchUrl = patchUrl+ urlsubfixAndBikeType + "averageDistance";
        // Create the request body
        var request = new UnityWebRequest(tempPatchUrl, "PATCH");
        // write the request body in JSON format make sure to use double quotes only do biketimes
        string requestBody =
            @"{
            'fields': {
                'averageDistance': {
                    'doubleValue': "
            + bikeData.averageDistanceCurrent
            + @"
                }
            }
        }";
        Debug.Log(requestBody);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(requestBody);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        // Check for errors

        if (
            request.result == UnityWebRequest.Result.ConnectionError
            || request.result == UnityWebRequest.Result.ProtocolError
        )
        {
            Debug.LogError(request.error);
            Debug.Log(request.downloadHandler.text);
        }
        else
        {
            Debug.Log(request.downloadHandler.text);
        }
    }

    private IEnumerator PatchDataTotalBikedDuration(string urlsubfixAndBikeType, currentBike bikeData)
    {
        var tempPatchUrl = patchUrl+ urlsubfixAndBikeType + "totalBikedDuration";
        // Create the request body
        var request = new UnityWebRequest(tempPatchUrl, "PATCH");
        // write the request body in JSON format make sure to use double quotes only do biketimes
        string requestBody =
            @"{
            'fields': {
                'totalBikedDuration': {
                    'stringValue': '"
            + bikeData.totalBikedDurationCurrent
            + @"'
                }
            }
        }";
        Debug.Log(requestBody);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(requestBody);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        // Check for errors

        if (
            request.result == UnityWebRequest.Result.ConnectionError
            || request.result == UnityWebRequest.Result.ProtocolError
        )
        {
            Debug.LogError(request.error);
            Debug.Log(request.downloadHandler.text);
        }
        else
        {
            Debug.Log(request.downloadHandler.text);
        }
    }

    private IEnumerator PatchDataAverageTime(string urlsubfixAndBikeType, currentBike bikeData)
    {
        var tempPatchUrl = patchUrl+ urlsubfixAndBikeType + "averageTime";
        // Create the request body
        var request = new UnityWebRequest(tempPatchUrl, "PATCH");
        // write the request body in JSON format make sure to use double quotes only do biketimes
        string requestBody =
            @"{
            'fields': {
                'averageTime': {
                    'stringValue': '"
            + bikeData.averageTimeCurrent
            + @"'
                }
            }
        }";
        Debug.Log(requestBody);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(requestBody);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        // Check for errors

        if (
            request.result == UnityWebRequest.Result.ConnectionError
            || request.result == UnityWebRequest.Result.ProtocolError
        )
        {
            Debug.LogError(request.error);
            Debug.Log(request.downloadHandler.text);
        }
        else
        {
            Debug.Log(request.downloadHandler.text);
        }
    }

    private IEnumerator PostData()
    {
        // Create the request body
        var request = new UnityWebRequest(firestoreUrlPrefix, "POST");
        string requestBody =
            @"{
            'fields': {
                'DistanceTraveled': {
                    'doubleValue': "
            + Player.DistanceTraveled
            + @"
                },
                'TimeSpent': {
                    'doubleValue': "
            + tTime
            + @"
                }
            }
        }";
        byte[] bodyRaw = Encoding.UTF8.GetBytes(requestBody);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        // Check for errors
        if (
            request.result == UnityWebRequest.Result.ConnectionError
            || request.result == UnityWebRequest.Result.ProtocolError
        )
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
