using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCameraControl : MonoBehaviour {
    public static float destroyLine;

	public GameObject actor;
	Transform transform_act;
	public float camera_optima_x = 0f;
	public float camera_optima_y = 45f;
	public float camera_optima_z = 35f;
	public float alpha_y;
	private float yVelocity = 0.3f;
	public float smoothTime = 1F;
    float triggerX, triggerY, triggerZ;

    float autoCameraMoveForce;
    PlayerActionControler control;

    // Use this for initialization
    void Start () {
		actor = GameObject.Find ("actor");
        transform.position = Vector3.Lerp(transform.position, new Vector3(actor.transform.position.x + camera_optima_x, actor.transform.position.y + alpha_y, actor.transform.position.z - camera_optima_z), 3f * Time.deltaTime);
        transform_act = actor.transform;
        autoCameraMoveForce = 7f;
        control = (PlayerActionControler)actor.GetComponent(typeof(PlayerActionControler));

        //초기 카메라 position.y값 셋팅
        //alpha_y = actor.transform.position.y + 5;

        //init destroy line
        destroyLine = transform.position.z;
    }


    // Update is called once per frame
    private void LateUpdate() {
   
        autoCameraMoveForce += Time.deltaTime*2.5f;
        if(actor != null)
        {
            destroyLine = transform.position.z-1;
            if (actor.transform.position.z<transform.position.z)
            {
                control.Dead();
                autoCameraMoveForce = 0f;
            }

            triggerX = transform_act.position.x;
            triggerY = transform_act.position.y;
            triggerZ = transform_act.position.z;
            transform.position = Vector3.Lerp(transform.position, new Vector3(camera_optima_x+ triggerX, triggerY + camera_optima_y, transform.position.z+Time.deltaTime* autoCameraMoveForce/*triggerZ - camera_optima_z*/), 1f * Time.deltaTime);
            if ((actor.transform.position.z - camera_optima_z) > transform.position.z)
            {
                transform.position = Vector3.Lerp(transform.position, new Vector3(camera_optima_x+ triggerX, triggerY + camera_optima_y, triggerZ - camera_optima_z), smoothTime * Time.deltaTime);
            }
        }

    }


	public void CameraMoveToY(float y){

        transform.position = Vector3.Lerp(transform.position, new Vector3(0, triggerY + y, transform.position.z), 0.5f * Time.deltaTime);
    }
}
