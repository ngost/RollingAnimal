using UnityEngine;
using System.Collections;

public class Destroyor : MonoBehaviour
{

    float maxDistance = 1f;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, SimpleCameraControl.destroyLine);
        RaycastHit hit;
        
        // Physics.BoxCast (레이저를 발사할 위치, 사각형의 각 좌표의 절판 크기, 발사 방향, 충돌 결과, 회전 각도, 최대 거리)
        Vector3 castStartPos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        bool isHit = Physics.BoxCast(castStartPos, transform.lossyScale / 2, -transform.forward, out hit, transform.rotation, maxDistance);

        if (isHit)
        {
            //                Debug.Log("hit!");
            Destroy(hit.transform.gameObject);
        }
    }

}
