using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class PlayerControler : MonoBehaviour
{
    public float rotationPeriod = 0.3f; // 옆으로 이동하는 데 걸리는 시간
    public float sideLength = 1f; // Cube의 변의 길이

    bool isRotate = false; // Cube가 회전하고 있는지 여부를 감지하는 플래그
    float directionX = 0; // 회전 방향 플래그
    float directionZ = 0; // 회전 방향 플래그

    Vector3 startPos; // 회전 이전 Cube 위치
    float rotationTime = 0; // 회전하는 시간 경과
    float radius; // 중심의 궤도 반경
    Quaternion fromRotation; // 회전 이전 Cube의 쿠ォ타니온
    Quaternion toRotation; // 회전 후 Cube의 쿠ォ타니온

    //// Start is called before the first frame update
    PlayerActionControler control;

    float comboCoolTime;

    GameObject CountDownObj;
    GameObject m_canvas;

    GameObject clear_percent;
    GameObject combo_obj;
    GameObject ControlBackground;

    GameObject timer_obj;
    Image ControlBackgroundImg;

    RectTransform display_transform;

    public bool anykeyTouched;
    public bool stopflag = false;
    float ScreenWidth;
    float ScreenLeft;
    float ScreenRight;
    float ScreenHeight;
    float ScreenHeightLine;

    float endPositionZ;
    ClearCheckManager ccm;
    Text percent_text;
    Text comboText;
    Text timerText;

    public int percent;
    public int maxCombo;
    int currentCombo;
    int maxFontSize;

    // Use this for initialization
    void Start()
    {
        m_canvas = GameObject.Find("Canvas");
        CountDownObj = m_canvas.transform.Find("countDown").gameObject;
        // 중심의 회전 궤도 반경을 계산
        radius = sideLength * Mathf.Sqrt(2f) / 2f;

        maxCombo = StaticInfoManager.maxCombo;
        maxFontSize = 5;
        comboCoolTime = 1.5f;
        ScreenWidth = Screen.width;
        ScreenLeft = Screen.width * 0.5f;
        ScreenHeight = Screen.height;
        ScreenHeightLine = ScreenHeight * 0.2f;
        control = (PlayerActionControler)gameObject.GetComponent(typeof(PlayerActionControler));
        ccm = (ClearCheckManager)GameObject.Find("ClearChecker").GetComponent(typeof(ClearCheckManager));
        endPositionZ = ccm.clearZ;
        endPositionZ = 1f / endPositionZ;


        clear_percent = m_canvas.transform.Find("ClearPercent").gameObject;
        combo_obj = m_canvas.transform.Find("Combo").gameObject;
        timer_obj = m_canvas.transform.Find("Timer").gameObject;

        comboText = combo_obj.GetComponent<Text>();
        percent_text = clear_percent.GetComponent<Text>();
        timerText = timer_obj.GetComponent<Text>();

        clear_percent.GetComponent<RectTransform>().localPosition = new Vector3(Screen.width * 0.38f, Screen.height * 0.47f, 0);
        timer_obj.GetComponent<RectTransform>().localPosition = new Vector3(-Screen.width * 0.3f, Screen.height * 0.47f, 0);
        combo_obj.GetComponent<RectTransform>().localPosition = new Vector3(0, Screen.height * 0.25f, 0);
        ControlBackground = GameObject.Find("ControlBackground");
        ControlBackgroundImg = ControlBackground.GetComponent<Image>();
        ControlBackground.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width,Screen.height*0.4f);
        ControlBackground.GetComponent<RectTransform>().localPosition = new Vector3(0f, -Screen.height * 0.5f + Screen.height * 0.2f, 0f);

        comboText.fontSize = (int)Mathf.Round(Screen.width * 0.11f);
        percent_text.fontSize = (int)Mathf.Round(Screen.width * 0.1f);
        percent_text.text = "0" + " %";

        timerText.fontSize = (int)Mathf.Round(Screen.width * 0.1f);
        timerText.text = "00 : 00";

        StartCoroutine("StartCountDown");

    }

    // Update is called once per frame
    void Update()
    {

        float x = 0;
        float y = 0;

        if (!anykeyTouched || stopflag)
        {
            return;
        }

        if(comboCoolTime > 0f)
        {
            comboCoolTime -= Time.deltaTime;
//            combo_obj.transform.localScale = combo_obj.transform.localScale * 0.95f;
            CompareToMaxCombo();
        }
        else
        {
            EndCombo();
            comboCoolTime = 1.5f;
        }



        // 키 입력을 선택하십시오.
        x = Input.GetAxisRaw("Vertical");
        if (x == 0f)
        {
            y = Input.GetAxisRaw("Horizontal");
        }
       
      
       //added temp
       y = -y;

        // x 1 front
        // x -1 back
        // y 1 left
        // y -1 right
        //left

        if (Input.touchCount > 0)
        {
            float regionX = Input.GetTouch(0).position.x;
            float regionY = Input.GetTouch(0).position.y;
            //일단 좌우로 나눔
            if (regionX < ScreenLeft)
            {
                //왼쪽
                if(regionY > ScreenHeightLine)
                {
                    //위 [왼쪽]
                    x = 0;
                    y = 1;
                }
                else
                {
                    //아래 [후진]
                    x = -1;
                    y = 0;
                }
            }
            else
            {
                //오른쪽
                if(regionY > ScreenHeightLine)
                {
                    //위 [전진]
                    x = 1;
                    y = 0;
                }
                else
                {
                    //아래
                    x = 0;
                    y = -1;
                }
            }
        }



        if (control.isClear)
        {
            x = 1;
            y = 0;
        }
        // 키 입력이하고 Cube가 회전 중이 아닌 경우, Cube를 회전한다.
        if ((x != 0f || y != 0f) && !isRotate)
        {
            if (!control.isClear)
            {
                RefreshCombo();
                percent = (int)Mathf.Round(((transform.position.z * endPositionZ * 100)));
//                percent_text.text = percent.ToString() + " %";
                percent_text.text = string.Format("{0} %", percent);
            }
            control.FootPrint();


            directionX = y; // 회전 방향 세트 (x, y 중 하나는 반드시 0)
            directionZ = x; // 회전 방향 세트 (x, y 중 하나는 반드시 0)
            startPos = transform.position; // 회전 전 좌표를 유지
            fromRotation = transform.rotation; // 회전 이전 쿠ォ타니온을 유지
            transform.Rotate(directionZ * 90, 0, directionX * 90, Space.World); // 회전 방향으로 90도 회전
            toRotation = transform.rotation; // 회전 후 쿠ォ타니온을 유지
            transform.rotation = fromRotation; // Cube의 Rotation을 회전 전에 돌린다. (transform의 샤로 코피 라든지 수없는 걸까 ...)
            rotationTime = 0; // 회전 중의 경과 시간을 0으로.
            isRotate = true; // 회전 중 플래그를 세운다.
        }
    }

    void FixedUpdate()
    {

        if (isRotate)
        {
            rotationTime += Time.fixedDeltaTime; // 경과 시간을 증가
            float ratio = Mathf.Lerp(0, 1, rotationTime / rotationPeriod); // 회전 시간에 대한 지금의 경과 시간의 비율

            // 이동
            float thetaRad = Mathf.Lerp(0, Mathf.PI * 0.5f, ratio); // 회전 각도를 라디안으로.
            float distanceX = -directionX * radius * (Mathf.Cos(45f * Mathf.Deg2Rad) - Mathf.Cos(45f * Mathf.Deg2Rad + thetaRad)); // X 축 이동 거리. - 부호는 키와 이동 방향을 맞추기 위해.
            float distanceY = radius * (Mathf.Sin(45f * Mathf.Deg2Rad + thetaRad) - Mathf.Sin(45f * Mathf.Deg2Rad)); // Y 축 이동 거리
            float distanceZ = directionZ * radius * (Mathf.Cos(45f * Mathf.Deg2Rad) - Mathf.Cos(45f * Mathf.Deg2Rad + thetaRad)); // Z 축 이동 거리
            transform.position = new Vector3(startPos.x + distanceX, startPos.y + distanceY, startPos.z + distanceZ); // 현재 위치를 세트

            // 회전
            transform.rotation = Quaternion.Lerp(fromRotation, toRotation, ratio); // Quaternion.Lerp에서 현재의 회전 각도를 세트 (다니 편리한 함수)

            // 이동 ​​· 회전 종료시에 각 매개 변수를 초기화합니다. isRotate 플래그를 내린다.
            if (ratio == 1)
            {
                isRotate = false;
                directionX = 0;
                directionZ = 0;
                rotationTime = 0;
            }
        }
    }

    private void RefreshCombo()
    {
        comboText.enabled = true;
        currentCombo++;
        if (currentCombo>=StaticInfoManager.needForFever && (currentCombo % StaticInfoManager.needForFever).Equals(0))
        {
            if (!control.isFevering)
            {
                StartCoroutine(control.FeverActivate());
            }
        }

        comboCoolTime = 1.5f;
//        comboText.text = currentCombo + " Combo";
        comboText.text = string.Format("{0} Combo", currentCombo);
        combo_obj.transform.localScale = new Vector3(1f, 1f, 1f);

        //플레이어의 현재 위치 저장
        StaticInfoManager.current_player_position = transform.position.z;
    }

    private void EndCombo()
    {
        currentCombo = 0;
        comboText.enabled = false;
    }

    private void CompareToMaxCombo()
    {
        if (maxCombo < currentCombo)
        {
            maxCombo = currentCombo;
        }
    }

    IEnumerator StartCountDown()
    {
        yield return new WaitForSeconds(1.5f);
        ControlBackgroundImg.enabled = true;
        yield return new WaitForSeconds(0.5f);
        anykeyTouched = true;


        yield return new WaitForSeconds(0.5f);
        CountDownObj.SetActive(false);
    }
}










//
//
//
//
//

//private void Update()
//{
//    if(comboCoolTime > 0f)
//    {
//        comboCoolTime -= Time.deltaTime;
//        combo_obj.transform.localScale = combo_obj.transform.localScale * 0.95f;
//        CompareToMaxCombo();
//    }
//    else
//    {
//        EndCombo();
//        comboCoolTime = 1.5f;
//    }



//    if (!anykeyTouched || stopflag)
//    {
//        return;
//    }
//    if (input)
//    {

//        keyboardInput = false;
//        if (control.isClear)
//        {
//            StaticInfoManager.maxCombo = maxCombo;
//            keyboardInput = true;
//            StartCoroutine("moveUp");
//            input = false;
//            return;
//            //keyboardInput = true;
//        }

//        //if (player_transform.position.z > speed_change_sections[step_index])
//        //    ControlSpeedUp();

//        //if (player_transform.position.z < speed_change_sections[step_index])
//        //ControlSpeedDown();

//        if (Input.GetKey(KeyCode.DownArrow))
//        {
//            RefreshCombo();
//            keyboardInput = true;
//            keyboardInput = !keyboardInput;
//            StartCoroutine("moveDown");
//            input = false;
//            //keyboardInput = true;
//            Debug.Log(player_transform.position.z);
//            Debug.Log(endPositionZ);
//            percent = (int)Mathf.Round(((player_transform.position.z * endPositionZ * 100)));
//            percent_text.text = percent.ToString() + " %";
//        }
//        else if (Input.GetKey(KeyCode.LeftArrow))
//        {
//            RefreshCombo();
//            keyboardInput = true;
//            StartCoroutine("moveLeft");
//            input = false;
//            //keyboardInput = true;
//            percent = (int)Mathf.Round(((player_transform.position.z * endPositionZ * 100)));
//            percent_text.text = percent.ToString() + " %";
//        }
//        else if (Input.GetKey(KeyCode.RightArrow))
//        {
//            RefreshCombo();
//            keyboardInput = true;
//            StartCoroutine("moveRight");
//            input = false;
//            //keyboardInput = true;
//            percent = (int)Mathf.Round(((player_transform.position.z * endPositionZ * 100)));
//            percent_text.text = percent.ToString() + " %";
//        }
//        else if(Input.GetKey(KeyCode.UpArrow))
//        {
//            RefreshCombo();
//            keyboardInput = true;
//            StartCoroutine("moveUp");
//            input = false;
//            //keyboardInput = true;
//            percent = (int)Mathf.Round(((player_transform.position.z * endPositionZ * 100)));
//            percent_text.text = percent.ToString() + " %";
//        }

//        //                Debug.Log("touched anyway..");
//        try
//        {
//            if (Input.GetTouch(0).position.x < ScreenLeft)//left
//            {
//                StartCoroutine("moveLeft");
//                input = false;
//                RefreshCombo();
//                percent = (int)Mathf.Round(((player_transform.position.z * endPositionZ*100)));
//                percent_text.text = percent.ToString() + " %";
//            }
//            else if (Input.GetTouch(0).position.x > ScreenRight)//right
//            {
//                StartCoroutine("moveRight");
//                input = false;
//                RefreshCombo();
//                Debug.Log("touched right..");
//                percent = (int)Mathf.Round(((player_transform.position.z * endPositionZ * 100)));
//                percent_text.text = percent.ToString() + " %";
//            }
//            else if(Input.GetTouch(0).position.x >= ScreenLeft && Input.GetTouch(0).position.x <= ScreenRight)
//            {              //center
//                StartCoroutine("moveUp");
//                input = false;
//                RefreshCombo();
//                Debug.Log("touched back..");
//                percent = (int)Mathf.Round(((player_transform.position.z * endPositionZ * 100)));
//                percent_text.text = percent.ToString() + " %";
//            }

//        }
//        catch (ArgumentException e) 
//        { 

//        }

//    }



//    //if (input == true)
//    //{
//    //    if (Input.GetKey(KeyCode.DownArrow))
//    //    {
//    //        StartCoroutine("moveDown");
//    //        input = false;
//    //    }
//    //    else if (Input.GetKey(KeyCode.LeftArrow))
//    //    {
//    //        StartCoroutine("moveLeft");
//    //        input = false;
//    //    }
//    //    else if (Input.GetKey(KeyCode.RightArrow))
//    //    {
//    //        StartCoroutine("moveRight");
//    //        input = false;
//    //    }
//    //    //else
//    //    //{
//    //    //    StartCoroutine("moveUp");
//    //    //    input = false;
//    //    //}

//    //}
//    //else
//    //{
//    //    //if (!audioSource.isPlaying)
//    //    //{
//    //    //    audioSource.Play();
//    //    //    AudioPauseCoolTime = 0f;
//    //    //}
//    //}

//    //if (audioSource.isPlaying && AudioPauseCoolTime > AudioCool)
//    //{
//    //    audioSource.Pause();
//    //}


//    //if (Input.GetKey(KeyCode.UpArrow))
//    //{
//    //    StartCoroutine("moveUp");
//    //    input = false;
//    //}

//    //        StartCoroutine("Delayer");
//}

//private void ControlSpeedUp()
//{
//    ++step_index;
//    if (step_index <= steps.Length)
//    {
//        step = steps[step_index];
//    }
//    else
//    {
//        --step_index;
//    }
//}

//private void ControlSpeedDown()
//{
//    --step_index;
//    if (step_index >= 0)
//    {
//        step = steps[step_index];
//    }
//    else
//    {
//        ++step_index;
//    }
//}

//public void ControlStop()
//{
//    anykeyTouched = false;
//    stopflag = true;
//}




//IEnumerator moveUp()
//{
//    control.FootPrint();
//    for (int i = 0; i < (90 / step); i++)
//    {
//        player.transform.RotateAround(up.transform.position, Vector3.right, step);
//        yield return new WaitForSeconds(speed);
//    }
//    center.transform.position = player.transform.position;
//    input = true;
//}

//IEnumerator moveDown()
//{
//    control.FootPrint();
//    for (int i = 0; i < (90 / step); i++)
//    {
//        player.transform.RotateAround(down.transform.position, Vector3.left, step);
//        yield return new WaitForSeconds(speed);
//    }
//    center.transform.position = player.transform.position;
//    input = true;
//}
//IEnumerator moveLeft()
//{
//    control.FootPrint();
//    for (int i = 0; i < (90 / step); i++)
//    {
//        player.transform.RotateAround(left.transform.position, Vector3.forward, step);
//        yield return new WaitForSeconds(speed);
//    }
//    center.transform.position = player.transform.position;
//    input = true;
//}
//IEnumerator moveRight()
//{
//    control.FootPrint();
//    for (int i = 0; i < (90 / step); i++)
//    {
//        player.transform.RotateAround(right.transform.position, Vector3.back, step);
//        yield return new WaitForSeconds(speed);
//    }
//    center.transform.position = player.transform.position;
//    input = true;
//}