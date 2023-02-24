using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using UnityEngine.UI;

public class diable_gif_on_disable : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject gif;
void OnDisable()
{
    gif.GetComponent<Image>().enabled = false;
}
}
