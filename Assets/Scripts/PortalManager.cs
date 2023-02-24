using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PortalManager : MonoBehaviour
{
    Dictionary<GameObject, GameObject> dictPortalINOUT = new Dictionary<GameObject, GameObject>();

    [SerializeField] List<GameObject> portalINlst = new List<GameObject>();
    [SerializeField] List<GameObject> portalOUTlst = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        dictPortalINOUT.Clear();
        int portallstLen = Mathf.Min(portalINlst.Count, portalOUTlst.Count);
        // implement the Dictionary<GameObject, GameObject> dictPortalINOUT
        for (int i = 0; i < portallstLen; i++) dictPortalINOUT.Add(portalINlst[i], portalOUTlst[i]);
    }

    public GameObject lookUpPortalOut(GameObject portalIn)
    {
        GameObject portalOUT;
        portalOUT = dictPortalINOUT[portalIn];
        return portalOUT;
    }
}
