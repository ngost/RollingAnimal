using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoScript : MonoBehaviour
{
    public float tornado_speed = 2f;
    public bool vertical = false;


    // Start is called before the first frame update
    
    void Update()
    {
            if (!vertical)
            {
                transform.position = new Vector3(Mathf.PingPong(Time.time * tornado_speed, 4) - 2, transform.position.y, transform.position.z);
            }
            else
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, Mathf.PingPong(Time.time * tornado_speed, 4) + 2);
            }

    }

}
