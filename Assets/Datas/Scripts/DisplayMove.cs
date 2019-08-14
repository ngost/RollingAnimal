using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayMove : MonoBehaviour
{
    private float x_rate;
    private float y_rate;
    public float rate;

    // Start is called before the first frame update
    void Start()
    {
        x_rate = rate;
        y_rate = x_rate*4;
    }

    // Update is called once per frame
    void Update()
    {
        //before moving coordinate
        float before_x, before_y, before_z;
        before_x = transform.position.x;
        before_y = transform.position.y;
        before_z = transform.position.z;

        //after moving coordinate
        transform.position = new Vector3(before_x - x_rate, before_y - y_rate, before_z);
    }
}
