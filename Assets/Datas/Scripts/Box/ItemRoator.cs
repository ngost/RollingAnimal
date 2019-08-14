using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRoator : MonoBehaviour
{
    public string rotateAxis = "z";

    float accelx, accely, accelz = 0;

    void Start()
    {
        accelx = 2500f;
        accely = 2500f;
        accelz = 2500f;
    }


    void Update()
    {
        if (rotateAxis.Equals("z"))
        {
            if (accelz > 0)
            {
                accelz = accelz - 55f;
            }

            transform.Rotate(0, 0, accelz * Time.deltaTime);
        }
        else if (rotateAxis.Equals("y"))
        {
            if (accely > 0)
            {
                accely = accely - 55f;
            }

            transform.Rotate(0, -accely * Time.deltaTime, 0);
        }
        else if (rotateAxis.Equals("x"))
        {
            if (accelx > 0)
            {
                accelx = accelx - 55f;
            }

            transform.Rotate(accelx * Time.deltaTime, 0, 0);
        }
    }
}
