﻿using UnityEngine;
using System.Collections;

public class MoveTest : MonoBehaviour
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

    // Use this for initialization
    void Start()
    {

        // 중심의 회전 궤도 반경을 계산
        radius = sideLength * Mathf.Sqrt(2f) / 2f;

    }

    // Update is called once per frame
    void Update()
    {

        float x = 0;
        float y = 0;

        // 키 입력을 선택하십시오.
        x = Input.GetAxisRaw("Horizontal");
        if (x == 0f)
        {
            y = Input.GetAxisRaw("Vertical");
        }


        // 키 입력이하고 Cube가 회전 중이 아닌 경우, Cube를 회전한다.
        if ( (x != 0f || y != 0f) && !isRotate) {
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
            float thetaRad = Mathf.Lerp(0, Mathf.PI / 2f, ratio); // 회전 각도를 라디안으로.
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
}