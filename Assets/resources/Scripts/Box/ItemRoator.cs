using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRoator : MonoBehaviour
{
    float accelx, accely, accelz = 0;

    void Start()
    {
        accelx = 0;
        accely = 0;
        accelz = 2500f;
    }


    void Update()
    {
        if (accelz > 0)
        {
            accelz = accelz - 55f;
        }
        
        transform.Rotate(0, 0, accelz * Time.deltaTime);
    }
}
