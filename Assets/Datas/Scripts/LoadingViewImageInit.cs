using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class LoadingViewImageInit : MonoBehaviour
{

    public List<Sprite> images;
    Image img;
    // Start is called before the first frame update
    void Start()
    {
        int index =Random.Range(0, images.Count);
        img = gameObject.GetComponent<Image>();
        img.sprite = images[index];

        Text loadingText = GameObject.Find("LoadingText").GetComponent<Text>();
        Text loadingViewText = GameObject.Find("LoadingViewText").GetComponent<Text>();

        loadingText.text = StaticInfoManager.lang.getString("Loading");
        loadingText.alignment = TextAnchor.MiddleCenter;
        loadingViewText.text = StaticInfoManager.lang.getString("LoadingViewText"+(index+1));
        loadingViewText.alignment = TextAnchor.MiddleCenter;
    }

}
