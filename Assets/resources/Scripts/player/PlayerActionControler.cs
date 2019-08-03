using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerActionControler : MonoBehaviour
{
    //public Shader disappearShader;
    public AudioClip breakClip;
    public AudioClip superJumpclip;

    public AudioClip disappearClip;
    float maxDistance = 1.5f;
    RaycastHit hit;

    public GameObject foot_particle;
    public AudioClip walkingSound;
    float AudioPauseCoolTime = 0f;
    public float AudioCool;

    bool isHit_f, isHit_b, isHit_l, isHit_r, isHit_u, isHit_d;
    AudioSource audioSource;
    PlayerControler controler;
    public String _AnimDeathMotionTrigger;
    public float maxSpeed;

    public GameObject footPrintObject;
    public GameObject deadEffect;
    GameObject deadEffectObj;
    public AudioClip deadClip;

    private Rigidbody m_rb;
    //[SerializeField] private float m_jumpForce;

    public float m_pushForce;
    public float gravityScale = 0.8f;
    public float tempGravityScale;
    public static float globalGravity = -9.81f;
    private GameObject cameraObj;
    private AudioSource m_audio;
    public AudioClip impact;
    public AudioClip fever_audio;
    public AudioClip shield_audio;
    public float limit_move_speed;
    public float targetTime = 5f;
    public float jumpCoolTime = 0f;
    public bool hasCollider = false;
    public bool cameraTriggerSwich = true;
    private bool isAlive = true;
    private ParticleSystem feverParticle;
    float disovleTime = -50f;

    GameObject bombParticle;
    ParticleSystem[] bombparticles;
    public float BombCoolTime;

    private AudioClip stopClip;
    //CubeInitor cube_script;
    float shakeX = 0.1f, shakeY = 1f;

    //direction explanation 0 is no direction, 1 is forward, 2 is back, 3 is left, 4 is right. done.
    private int m_direction = 0;
    //public const int Direction_None = 0;
    //public const int Direction_Foward = 1;
    //public const int Direction_Back = 2;
    //public const int Direction_Left = 3;
    //public const int Direction_Right = 4;

    Vector3 gravity;
    SimpleCameraControl cameraScript;
    bool isTransfering;
    Renderer actor_render;
    public bool isClear = false;
    BoxCollider boxCollider;
    public bool isFevering = false;

    void Start()
    { 
        controler = (PlayerControler)gameObject.GetComponent(typeof(PlayerControler));
        audioSource = (AudioSource)gameObject.GetComponent<AudioSource>();
        //disappearShader = Resources.Load<Shader>("prefabs/etc/actor_disappear");
        audioSource = gameObject.GetComponent<AudioSource>();
        //breakClip = Resources.Load<AudioClip>("music/wood_break");
        //superJumpclip = Resources.Load<AudioClip>("music/bigjump");
        deadClip = Resources.Load<AudioClip>("music/death");
        //disappearClip = Resources.Load<AudioClip>("music/disappear_bgm");
        //boxCollider = gameObject.GetComponent<BoxCollider>().enabled;
        actor_render = gameObject.GetComponent<Renderer>();
//        gravityScale = 1f;
        isTransfering = false;
        tempGravityScale = gravityScale;
        m_rb = GetComponent<Rigidbody>();
        cameraObj = GameObject.Find("Main Camera");
        m_audio = gameObject.GetComponent<AudioSource>();
        feverParticle = gameObject.GetComponentInChildren<ParticleSystem>();
        //        stopClip = Resources.Load<AudioClip>("music/stop");

        //must set Joystick type correctly..
        //joystick = FindObjectOfType<DynamicJoystick>();

        //bombParticle = (GameObject)gameObject.transform.Find("BombParticle").gameObject;
        //bombparticles = bombParticle.GetComponentsInChildren<ParticleSystem>();
        gravity = globalGravity * gravityScale * Vector3.up;
        cameraScript = (SimpleCameraControl)cameraObj.GetComponent(typeof(SimpleCameraControl));
        
        if (!DataLoadAndSave.LoadSoundData("effect_sound"))
        {
            Debug.Log("okok");
            audioSource.enabled = false;
        }

    }
    public IEnumerator FeverActivate()
	{
        float calibrationVal = controler.rotationPeriod * 0.2f;
        isFevering = true;
        audioSource.PlayOneShot(fever_audio, 1f);
        feverParticle.Play();
        controler.rotationPeriod -= calibrationVal;
        yield return new WaitForSeconds(StaticInfoManager.feverTime);
        feverParticle.Stop();
        controler.rotationPeriod += calibrationVal;
        isFevering = false;

    }

    void JumpTimer()
    {
        jumpCoolTime -= Time.deltaTime;
    }

    void BombTimer()
    {
        BombCoolTime -= Time.deltaTime;
    }

    IEnumerator ActorDestroy()
    {
        Debug.Log("Destroy Actor!!");
        //        gameObject.GetComponent<MeshRenderer>().enabled = false;
        gameObject.transform.GetComponentInChildren<Animator>().SetTrigger(_AnimDeathMotionTrigger);
        StaticInfoManager.maxCombo = controler.maxCombo;
        if (StaticInfoManager.clearPercent < controler.percent)
        {
            StaticInfoManager.clearPercent = controler.percent;
        }

        yield return new WaitForSeconds(1f);
        StaticInfoManager.life--;

        if (StaticInfoManager.life < 0)
        {
            GameObject canvas = Resources.Load("prefabs/etc/ResponDialog") as GameObject;
            Instantiate(canvas);
            //DialogManager dialogManager = (DialogManager) GameObject.Find("DialogManager").GetComponent(typeof(DialogManager));
            //dialogManager.nameText = new Text().name = "hi";
            //dialogManager.dialogText = "Response..?";
            //GameObject canvas = GameObject.Find("Canvas");
            //canvas.GetComponent<Canvas>().enabled = true;
            GameObject trigger = GameObject.Find("Trigger");
            DialogTrigger dialogTrigger = (DialogTrigger)trigger.GetComponent(typeof(DialogTrigger));
            dialogTrigger.TriggerDialog();
        }
        else
        {
            //just alive, life num -- 
            Scene scene = SceneManager.GetActiveScene();
            //          Debug.Log(scene.name);
            StaticInfoManager.current_player_position = 0f;
            SceneManager.LoadScene(scene.name);
//            SimpleSceneFader.ChangeSceneWithFade("Stage_1");
        }

    }
    //public void StopControl()
    //{
    //    controler.stopflag = true;
    //}

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
    public void saveCheckPoint()
    {
        //
    }
    //충돌시, 죽어야하는 상태인지 체크 후, 죽임
    public void Dead()
    {
        if (isAlive)
        {
            AllStop();
//            m_rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
            playDeadsound();
            DeadEffect();
//            cube_script.BombSound(gameObject.GetComponent<AudioSource>());
            //foreach (ParticleSystem p in bombparticles)
            //{
            //    p.Play();
            //}
            isAlive = false;
            Debug.Log("you will die!");
            StartCoroutine("ActorDestroy");
        }
    }



    void Update()
    {
        AudioPauseCoolTime += Time.deltaTime;
        RayCheck();
        if (deadEffectObj != null)
        {
            deadEffectObj.transform.position = Vector3.Lerp(deadEffectObj.transform.position, new Vector3(deadEffectObj.transform.position.x + shakeX, deadEffectObj.transform.position.y+shakeY, deadEffectObj.transform.position.z), 2f * Time.deltaTime);
        }

    }

    public void FootPrint()
    {
        audioSource.PlayOneShot(walkingSound,2f);
        AudioPauseCoolTime = 0;

        GameObject printer = Instantiate(footPrintObject, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 0.4f, gameObject.transform.position.z), Quaternion.Euler(90, 0, 0));
        Instantiate(foot_particle, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y - 0.4f, gameObject.transform.position.z), Quaternion.Euler(0, 0, 0));
        Destroy(printer, 1f);
    }

    public void DeadEffect()
    {
        deadEffectObj = Instantiate(deadEffect, new Vector3(gameObject.transform.position.x-0.3f, gameObject.transform.position.y + 1f, gameObject.transform.position.z), Quaternion.Euler(60, 0, 0));
        Destroy(deadEffectObj, 1f);

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


        if (!isHit_f && !isHit_b && !isHit_r && !isHit_l && !isHit_u && !isHit_d)
        {
//            Debug.Log("exactly died.");
//            m_rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
           if(m_rb != null)
            {
//                Debug.Log("?");
                m_rb.useGravity = true;
                controler.stopflag = true;
            }
            
        }

    }

    void FixedUpdate()
    {
           
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


    private void PlayStopSound()
    {
        m_audio.PlayOneShot(stopClip, 1f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.Equals("CheckPoint"))
        {
            return;
        }
        BombCoolTime = 0f;
//        Debug.Log(collision.gameObject.name);
        Dead();
    }


//    private void OnCollisionStay(Collision collision)
//    {
////        Debug.Log("collision stay");
    //    //충돌이 다른 오브젝트에서 일어났을 때, 하나만 처리 되게끔 처리.
    //    if (jumpCoolTime > 0)
    //        return;
    //    if (this.hasCollider == true) { return; }
    //    this.hasCollider = true;

    //    ContactPoint[] contactPoints = collision.contacts;
    //    GameObject cube = collision.gameObject;

    //    //cube_script = (CubeInitor)cube.GetComponent(typeof(CubeInitor));


    //}


    void OnEnable()
    {
        if (m_rb != null)
            m_rb.useGravity = false;
    }


    void applyAlaphCameraY(float val)
    {

        cameraScript.alpha_y = val + cameraScript.camera_optima_y;
    }

    void playCollisionSound()
    {
        m_audio.PlayOneShot(impact, 1f);
    }

    void playDeadsound()
    {
        m_audio.PlayOneShot(deadClip, 1f);
    }

    void BreakBoxSound(AudioSource audio)
    {
        audio.PlayOneShot(breakClip, 1f);

    }

    void SuperJumpSound(AudioSource audio)
    {
        audio.PlayOneShot(superJumpclip, 1f);
    }

    public void BombSound(AudioSource audio)
    {
//        audio.PlayOneShot(bombClip, 1f);
    }

    void DisappearSound(AudioSource audio)
    {
        audio.pitch = 3f;
        //        audio.

        audio.PlayOneShot(disappearClip, 1f);
    }
}
