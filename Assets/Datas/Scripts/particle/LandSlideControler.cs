using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DigitalRuby.LightningBolt;

public class LandSlideControler : MonoBehaviour
{
    bool delay_onetime;
    float coolTime;

    public float settingCoolTime;
    public float startDelay;

    public GameObject rock;
    AudioSource audioSource;
    float maxDistance;
    // Start is called before the first frame update
    void Start()
    {
        maxDistance = 2f;
        delay_onetime = false;
        audioSource = gameObject.GetComponent<AudioSource>();
        if (!DataLoadAndSave.LoadSoundData("effect_sound"))
        {
            audioSource.enabled = false;
        }
        else
        {
            audioSource.enabled = true;
        }

    }

    // Update is called once per frame
    void Update()
    {
        //if (Mathf.Abs(gameObject.transform.position.z - StaticInfoManager.current_player_position) < 10)
        //{
        //    lightningBoltScript.enabled = true;
        //}
        coolTime += Time.deltaTime;
        //rock.transform.rotation = Quaternion.Euler(0, 0, coolTime);

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
            Instantiate(rock,transform);
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
            coolTime = 0f;
        }

    }
}
