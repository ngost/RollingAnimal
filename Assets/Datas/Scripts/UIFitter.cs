using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFitter : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        //get device display size, get margin , i did axis of width
        //width, height
        rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
    }
}
