using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CustomGrid : MonoBehaviour
{

    public GameObject target;
    public GameObject structure;
    private Vector3 truePos;
    public float gridSize;

    void LateUpdate()
    {
        truePos.x = Mathf.Floor(target.transform.position.x / gridSize) * gridSize;
        truePos.y = Mathf.Floor(target.transform.position.y / gridSize) * gridSize;
        truePos.z = Mathf.Floor(target.transform.position.z / gridSize) * gridSize;

        structure.transform.position = truePos;

    }
}
