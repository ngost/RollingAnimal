using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BoxTypeInitor : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Bronze_Box, Silver_Box, Gold_Box;
    GameObject mainCam;
    Camera cam;
    void Start()
    {
        mainCam = GameObject.Find("Main Camera");
        cam = mainCam.GetComponent<Camera>();
        switch (StaticInfoManager.boxType)
        {
            case 0:
                Bronze_Box.SetActive(true);
                cam.backgroundColor = new Color(0.6196079f, 0.6235294f, 0.5843138f, 1f);
                break;
            case 1:
                Silver_Box.SetActive(true);
                cam.backgroundColor = new Color(0.6078432f, 0.8431373f, 1f, 1f);
                break;
            case 2:
                Gold_Box.SetActive(true);
                cam.backgroundColor = new Color(1f, 0.8941177f, 0.3647059f, 1f);
                break;
            default:
                StaticInfoManager.boxType = 0;
                SceneManager.LoadScene("ShopScene");
                break;
        }
    }
}
