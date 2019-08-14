using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MonsterActionControler : MonoBehaviour
{
    GameObject[] footObjs;
    int footObjsIndex = 0;

    //public Shader disappearShader;
    public AudioClip breakClip;
    public AudioClip superJumpclip;
    public AudioClip bombClip;
    public AudioClip disappearClip;
    float maxDistance = 1.5f;
    RaycastHit hit;

    public AudioClip walkingSound;
    float AudioPauseCoolTime = 0f;
    public float AudioCool;
    public GameObject foot_particle;

    bool isHit_f, isHit_b, isHit_l, isHit_r, isHit_u, isHit_d;
    AudioSource audioSource;
    MonsterControler controler;
    public String _AnimDeathMotionTrigger;
    public float maxSpeed;

    public GameObject footPrintObject;
    private Rigidbody m_rb;
    [SerializeField] private float m_jumpForce;
    [SerializeField] private float m_moveForce;

    public float m_pushForce;
    public float gravityScale = 0.8f;
    public float tempGravityScale;
    public static float globalGravity = -9.81f;
    public AudioClip impact;
    public float limit_move_speed;
    public float targetTime = 5f;
    public float jumpCoolTime = 0f;
    public bool hasCollider = false;
    public bool cameraTriggerSwich = true;
    private bool isAlive = true;

    GameObject bombParticle;
    ParticleSystem[] bombparticles;
    public float BombCoolTime;


    //direction explanation 0 is no direction, 1 is forward, 2 is back, 3 is left, 4 is right. done.
    private int m_direction = 0;
    public const int Direction_None = 0;
    public const int Direction_Foward = 1;
    public const int Direction_Back = 2;
    public const int Direction_Left = 3;
    public const int Direction_Right = 4;
    Vector3 gravity;
    SimpleCameraControl cameraScript;
    Renderer actor_render;
    public bool isClear = false;
    BoxCollider boxCollider;
    bool sound_mute;
    Vector3 origin_position;
    GameObject footParticleObj;
    ParticleSystem footParticle;

    void Start()
    {
        footObjs = new GameObject[3];
        for (int i = 0; i < 3; i++)
        {
            footObjs[i] = Instantiate(footPrintObject, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 0.4f, gameObject.transform.position.z), Quaternion.Euler(90, 0, 0));
        }

        footParticleObj = Instantiate(foot_particle, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 0.4f, gameObject.transform.position.z), Quaternion.Euler(0, 0, 0));
        footParticle = footParticleObj.GetComponent<ParticleSystem>();

        origin_position = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y,gameObject.transform.position.z);

        controler = (MonsterControler)transform.parent.GetComponentInChildren(typeof(MonsterControler));
        audioSource = gameObject.GetComponent<AudioSource>();
        actor_render = gameObject.GetComponent<Renderer>();
        //        gravityScale = 1f;
        tempGravityScale = gravityScale;
        m_rb = GetComponent<Rigidbody>();

        gravity = globalGravity * gravityScale * Vector3.up;

        if (!DataLoadAndSave.LoadSoundData("effect_sound"))
        {
            sound_mute = true;
        }

    }

    void JumpTimer()
    {
        jumpCoolTime -= Time.deltaTime;
    }

    void BombTimer()
    {
        BombCoolTime -= Time.deltaTime;
    }

    IEnumerator MonsterDestroy()
    {
        //Debug.Log("Destroy Monster!!");
        gameObject.transform.parent.GetComponentInChildren<Animator>().SetTrigger(_AnimDeathMotionTrigger);
        yield return new WaitForSeconds(2f);


                //죽이기 대신 부활
        //Destroy(transform.parent.gameObject);
        gameObject.transform.position = origin_position;
        controler.stopflag = false;
        controler.anykeyTouched = true;
        m_rb.constraints = RigidbodyConstraints.None;
        isAlive = true;
        m_rb.useGravity = false;
        gameObject.GetComponent<BoxCollider>().enabled = true;
    }

    public void AllStop()
    {
        gameObject.GetComponent<BoxCollider>().enabled = false;
        controler.stopflag = true;
        controler.anykeyTouched = false;
        //m_rb.useGravity = true;
        m_rb.velocity = Vector3.zero;
        m_rb.angularVelocity = Vector3.zero;
        m_rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
        jumpCoolTime = 10000f;
    }

    //충돌시, 죽어야하는 상태인지 체크 후, 죽임
    void Dead()
    {
        if (isAlive)
        {
            AllStop();
            //            m_rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
           
            isAlive = false;
            //Debug.Log("Monster will die!");
            StartCoroutine("MonsterDestroy");
        }
    }



    void Update()
    {
        AudioPauseCoolTime += Time.deltaTime;
        RayCheck();
    }

    public void FootPrint()
    {
        try
        {
            if (!sound_mute)
            {
                audioSource.PlayOneShot(walkingSound);
            }
            AudioPauseCoolTime = 0;

            if (footObjsIndex == footObjs.Length)
                footObjsIndex = 0;

            footObjs[footObjsIndex].transform.position = new Vector3(gameObject.transform.position.x, footObjs[footObjsIndex].transform.position.y, gameObject.transform.position.z);
            footObjsIndex++;

            footParticleObj.transform.position = new Vector3(gameObject.transform.position.x, footParticleObj.transform.position.y, gameObject.transform.position.z);
            footParticle.Play();
            //            GameObject printer = Instantiate(footPrintObject, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 0.4f, gameObject.transform.position.z), Quaternion.Euler(90, 0, 0));

            //Destroy(printer, 1f);
        }
        catch(NullReferenceException e)
        {
        }

    }


    void RayCheck()
    {
        if (!controler.anykeyTouched)
            return;

        // Physics.Raycast (레이저를 발사할 위치, 발사 방향, 충돌 결과, 최대 거리)
        isHit_f = Physics.Raycast(transform.position, transform.forward, out hit, maxDistance);

        isHit_b = Physics.Raycast(transform.position, transform.forward * -1f, out hit, maxDistance);

        isHit_r = Physics.Raycast(transform.position, transform.right, out hit, maxDistance);

        isHit_l = Physics.Raycast(transform.position, transform.right * -1f, out hit, maxDistance);

        isHit_u = Physics.Raycast(transform.position, transform.up, out hit, maxDistance);

        isHit_d = Physics.Raycast(transform.position, transform.up * -1f, out hit, maxDistance);

//        Debug.Log("rayCheck");
        if (!isHit_f && !isHit_b && !isHit_r && !isHit_l && !isHit_u && !isHit_d)
        {
            //            Debug.Log("exactly died.");
            //            m_rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
            if (m_rb != null)
            {
               //Debug.Log("monster will be dead");
                m_rb.useGravity = true;
                controler.stopflag = true;
            }

            //            boxCollider.enabled = false;
        }
    }

    void setInputDirection(int direction)
    {
        //set direction property
        this.m_direction = direction;
    }

    int getInputDirection()
    {
        //return last direction
        return this.m_direction;
    }


    void LateUpdate()
    {
        //충돌이 일어나지 않았을 경우, false
        this.hasCollider = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
//        Debug.Log(collision.gameObject.name);
//        Debug.Log("Monster collision enter");
        BombCoolTime = 0f;
//        Debug.Log(collision.gameObject.name);
        Dead();
    }

    void OnEnable()
    {
        if (m_rb != null)
            m_rb.useGravity = false;
    }
}