using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class showTotalDistance : MonoBehaviour
{
    // Start is called before the first frame update
    public PlayerData playerData;
    public GameObject totalDistancetxt;


    // Update is called once per frame
    void Update()
    {
        totalDistancetxt.GetComponent<TextMeshPro>().text = (playerData.DistanceTraveled*0.00062137f).ToString("f2");
    }
}
