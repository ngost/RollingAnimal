using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterResponer : MonoBehaviour
{
    //float coolTime;
    //public float respon_cool_time;
    public int moving_direction;
    public float monster_speed;
    public GameObject respon_object;
    bool isNull = true;
    bool stoped = false;
    GameObject monster;
    MonsterControler controler;
    bool onetime = true;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (monster == null)
            isNull = true;
   

        if ((isNull || stoped))
        {
            response();
        }
    }

    void response()
    {
        if (stoped)
        {
//            Debug.Log("원위치");
            ParticleSystem particle = gameObject.GetComponentInChildren<ParticleSystem>();
            if (particle != null)
                particle.Play();
            isNull = false;
        }
        else
        {
            //Debug.Log("생산");
            monster = Instantiate(respon_object, transform);

            ParticleSystem particle = gameObject.GetComponentInChildren<ParticleSystem>();
            if (particle != null)
                particle.Play();
            isNull = false;

            controler = (MonsterControler)gameObject.GetComponentInChildren(typeof(MonsterControler));
            controler.step = monster_speed;
            controler.MonsterMoveDirection = moving_direction;
        }
    }
}
