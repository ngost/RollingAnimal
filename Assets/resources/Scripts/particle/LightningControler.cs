using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.LightningBolt;

public class LightningControler : MonoBehaviour
{
    LineRenderer lightningRender;
    ParticleSystem groundLightParticle;
    bool delay_onetime;
    float coolTime;

    public float settingCoolTime;
    public float startDelay;

    public GameObject player;
    PlayerActionControler playerControler;
    AudioSource audioSource;
    float maxDistance;
    RaycastHit hit;
    LightningBoltScript lightningBoltScript;
    // Start is called before the first frame update
    void Start()
    {
        maxDistance = 2f;
        delay_onetime = false;
        lightningRender = gameObject.GetComponentInChildren<LineRenderer>();
        groundLightParticle = gameObject.GetComponentInChildren<ParticleSystem>();
        playerControler = (PlayerActionControler)player.GetComponent(typeof(PlayerActionControler));
        audioSource = gameObject.GetComponent<AudioSource>();
        lightningBoltScript = (LightningBoltScript)gameObject.GetComponentInChildren(typeof(LightningBoltScript));
        //if (!DataLoadAndSave.LoadSoundData("effect_sound"))
        //{
        //    audioSource.enabled = false;
        //}
        //else
        //{
        //    audioSource.enabled = true;
        //}

    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(gameObject.transform.position.z - StaticInfoManager.current_player_position) < 10)
        {
            lightningBoltScript.enabled = true;
        }
        coolTime += Time.deltaTime;

        //startDelay
        if (!delay_onetime)
        {
            if (startDelay <= coolTime)
            {
                delay_onetime = true;
                coolTime = 0f;
            }
            else
            {
                return;
            }
        }

        if (coolTime >= settingCoolTime)
        {
            //lightning sound play oneshot.
            if (audioSource != null)
            {
                if (audioSource.isPlaying)
                {
                    audioSource.Stop();
                    audioSource.Play();
                }
                else
                {
                    audioSource.Play();
                }
            }


            //땅 전기 파티클 처리
            if (groundLightParticle.isPlaying)
            {
                groundLightParticle.Stop();
                groundLightParticle.Play();
            }
            else
            {
                groundLightParticle.Play();
            }

            StartCoroutine("LightningOneTime");
            coolTime = 0f;



            // Physics.BoxCast (레이저를 발사할 위치, 사각형의 각 좌표의 절판 크기, 발사 방향, 충돌 결과, 회전 각도, 최대 거리)
            Vector3 castStartPos = new Vector3(transform.position.x, transform.position.y + 2, transform.position.z);
            bool isHit = Physics.Raycast(castStartPos, -transform.up, out hit, maxDistance);

            if (isHit)
            {
                if (hit.transform.gameObject.name.Equals("actor"))
                {
                    playerControler.Dead();
                }
            }
            else
            {
//                Debug.Log("not hit!");
            }
        }

    }
    IEnumerator LightningOneTime()
    {
        lightningRender.enabled = true;
        yield return new WaitForSeconds(0.2f);
        lightningRender.enabled = false;
    }
}
