using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCameraControl : MonoBehaviour
{
    public static float destroyLine;

    public GameObject actor;
    Transform transform_act;
    public float camera_optima_x;
    public float camera_optima_y;
    public float camera_optima_z;
    public float alpha_y;
    private float yVelocity = 0.3f;
    public float smoothTime = 1F;
    float originSmoothTime;
    float cameraPosition;
    PlayerActionControler action_control;
    PlayerControler control;
    public Vector3 startMarker;
    public Vector3 endMarker;
    public bool stop;

    // Movement speed in units/sec.
    public float speed = 1.0F;

    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;


    // Use this for initialization
    void Start()
    {
        // Keep a note of the time the movement started.
        startTime = Time.time;


        actor = GameObject.Find("actor");
        //        transform.position = Vector3.Lerp(transform.position, new Vector3(actor.transform.position.x + camera_optima_x, actor.transform.position.y + alpha_y, actor.transform.position.z - camera_optima_z), 1f * Time.deltaTime);
        transform_act = actor.transform;
        cameraPosition = 0f;
        action_control = (PlayerActionControler)actor.GetComponent(typeof(PlayerActionControler));
        control = (PlayerControler)actor.GetComponent(typeof(PlayerControler));
        //        originSmoothTime = smoothTime;
        //초기 카메라 position.y값 셋팅
        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + StaticInfoManager.last_checkpoint.z);
        //alpha_y = actor.transform.position.y + 5;

        //init destroy line
        destroyLine = transform.position.z;

        startMarker = new Vector3(camera_optima_x, camera_optima_y, -camera_optima_z+StaticInfoManager.last_checkpoint.z);
        endMarker = new Vector3(camera_optima_x, camera_optima_y, -camera_optima_z + 200f);

        // Calculate the journey length.
        journeyLength = Vector3.Distance(startMarker, endMarker);
    }


    // Update is called once per frame
    private void LateUpdate()
    {
        if (!control.anykeyTouched || stop)
        {
            startTime = Time.time;
            return;
        }
        if ((actor.transform.position.z - camera_optima_z) > transform.position.z)
        {
            transform.position = Vector3.Lerp(transform.position, new Vector3(camera_optima_x, camera_optima_y, actor.transform.position.z+2 - camera_optima_z), Time.deltaTime);
            startMarker = new Vector3(camera_optima_x, camera_optima_y, transform.position.z);
            journeyLength = Vector3.Distance(startMarker, endMarker);
            startTime = Time.time;
        }
        else
        {
            // Distance moved = time * speed.
            float distCovered = (Time.time - startTime) * speed;

            // Fraction of journey completed = current distance divided by total distance.
            float fracJourney = distCovered / journeyLength;

            // Set our position as a fraction of the distance between the markers.
            transform.position = Vector3.Lerp(startMarker, endMarker, fracJourney);


            cameraPosition += Time.deltaTime * smoothTime;
            if (actor != null)
            {
                destroyLine = transform.position.z - 1;
                //플레이어의 위치보다 카메라의 위치가 더 멀리 있으면 플레이어 죽음
                if (actor.transform.position.z < transform.position.z+6f)
                {
                    action_control.Dead();
                    cameraPosition = 0f;
                }
            }
        } 
    }
}
