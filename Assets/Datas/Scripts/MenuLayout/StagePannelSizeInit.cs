using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StagePannelSizeInit : MonoBehaviour
{
    RectTransform rectTransform;
    float marginY,marginX;
    int lastStage;
    void Start()
    {
        //size init

        lastStage = DataLoadAndSave.LoadTopClearStage(StaticInfoManager.level);
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        //get device display size, get margin , i did axis of width
        marginY = Screen.height * 0.25f;
        //width, height
        //        margin = 0;
        float realPictureHeight = Screen.height - marginY;
        float realPictureWidth = realPictureHeight * 0.5f;
        marginX = (Screen.width - realPictureWidth) * 0.5f;

        //float realPictureWidth = realPictureHeight / 2;
        //        float realPictureHeight = Screen.height - margin;

        rectTransform.sizeDelta = new Vector2(realPictureWidth,realPictureHeight);

        //Debug.Log(margin);
        //Debug.Log(realPictureWidth);

        //position init
//        RectTransform panel_transform = gameObject.transform.parent.gameObject.GetComponent<RectTransform>();
        //float startX = panel_transform.sizeDelta.x * -0.41f;
        //float realX = realPictureWidth*0.1f*screen_rate + (Screen.width * int.Parse(gameObject.name));
        //        float realX = realPictureWidth * 0.1f * screen_rate
        float realX = marginX + (Screen.width * int.Parse(gameObject.name));
//        float realX = margin * 0.25f + (Screen.width * int.Parse(gameObject.name));
        rectTransform.localPosition = new Vector3(realX , 0f, 0f);
        //        rectTransform.position = new Vector3(-2500, 0f, 0f);

        //클리어하지 못한 스테이지 이후의 스테이지에 백그라운드 불투명도 적용
        SetBackgroundAlpha();
    }

    public void SetBackgroundAlpha()
    {
//        Debug.Log(StaticInfoManager.level);
        lastStage = DataLoadAndSave.LoadTopClearStage(StaticInfoManager.level);
        if (int.Parse(gameObject.name) > lastStage)
        {
            Image image = gameObject.GetComponent<Image>();
            Color tempColor = image.color;
            tempColor.a = 0.6f;
            image.color = tempColor;
        }
        else
        {
            Image image = gameObject.GetComponent<Image>();
            Color tempColor = image.color;
            tempColor.a = 1f;
            image.color = tempColor;
        }
    }
}
