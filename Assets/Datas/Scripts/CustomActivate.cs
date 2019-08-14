using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomActivate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        int randInt = Random.Range(0, 4);
        Debug.Log(randInt);
        switch (randInt)
        {
            case 0:
                gameObject.GetComponent<Rigidbody>().AddForce(Vector3.right * 1, ForceMode.VelocityChange);
                break;
            case 1:
                gameObject.GetComponent<Rigidbody>().AddForce(Vector3.left * 0.7f, ForceMode.VelocityChange);
                break;
            case 2:
                gameObject.GetComponent<Rigidbody>().AddForce(Vector3.right * 0.7f, ForceMode.VelocityChange);
                break;
            case 3:
                gameObject.GetComponent<Rigidbody>().AddForce(Vector3.left * 1, ForceMode.VelocityChange);
                break;
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
