using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterParticleControler : MonoBehaviour
{
    AudioSource audioSource;
    public AudioClip waterClip;
    public float activeCoolTime = 1.0f;
    private float coolTime = 0f;
    ParticleSystem particle;
    BoxCollider box_collider;
    public float start_delay;
    bool delay_onetime = false;
    Vector3 originPos;
    public GameObject player;
    //PlayerActionControler playerControler;
    //bool stopFlag = true;
    // Start is called before the first frame update
    void Start()
    {
        particle = gameObject.GetComponent<ParticleSystem>();
        box_collider = gameObject.GetComponent<BoxCollider>();
        //box_collider.enabled = false;
        originPos = box_collider.center;
        //playerControler = (PlayerActionControler)player.GetComponent(typeof(PlayerActionControler));
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        coolTime += Time.deltaTime;

        if (!delay_onetime)
        {
            if (start_delay < coolTime)
            {
                delay_onetime = true;
                coolTime = activeCoolTime;
            }
            else
            {
                return;
            }
        }

        if (activeCoolTime < coolTime)
        {
            //            box_collider.center = originPos;
            //box_collider.enabled = true;
            particle.Play();
            audioSource.PlayOneShot(waterClip);
            coolTime = 0f;
            float maxDistance = 2f;
            RaycastHit hit;
            // Physics.BoxCast (레이저를 발사할 위치, 사각형의 각 좌표의 절판 크기, 발사 방향, 충돌 결과, 회전 각도, 최대 거리)
            Vector3 castStartPos = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
            bool isHit = Physics.Raycast(castStartPos, transform.forward, out hit, maxDistance);
            //            Debug.Log(isHit);
            if (isHit)
            {
                //                Debug.Log(hit.transform.gameObject.name);
                if (hit.transform.gameObject.name.Equals("actor"))
                {
                    if (StaticInfoManager.shieldEnable)
                    {
                        hit.collider.SendMessageUpwards("ShieldActivate", null, SendMessageOptions.DontRequireReceiver);
                        //Debug.Log(hit.collider.gameObject.name+ "is hit!");
                        //playerControler.ShieldActivate();
                    }
                    else
                    {
                        hit.collider.SendMessageUpwards("Dead", null, SendMessageOptions.DontRequireReceiver);
                        //Debug.Log(hit.collider.gameObject.name + "is hit!");
                        //playerControler.Dead();
                    }

                    // Debug.Log("water splash hit!");
                    //Gizmos.DrawRay(castStartPos, transform.forward * hit.distance);
                    //Gizmos.DrawWireCube(castStartPos + transform.forward * hit.distance, transform.lossyScale);
                }
            }

        }
    }


}
