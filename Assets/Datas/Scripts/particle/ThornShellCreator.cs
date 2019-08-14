using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornShellCreator : MonoBehaviour
{
    public GameObject thornShellPrefab;
    float coolTime;
    bool onetime;
    public float startDelay;
    public int createType = 0;
    public float interval = 2f;
    // Start is called before the first frame update
    void Start()
    {
        coolTime = 0f;
        onetime = false;
    }

    // Update is called once per frame
    void Update()
    {
        coolTime += Time.deltaTime;

        if (!onetime)
        {
            if (coolTime < startDelay)
            {
                return;
            }
            else
            {
                coolTime = 0f;
                onetime = true;
            }
        }

        switch (createType)
        {
            //foward
            case 1:
                if (coolTime > interval)
                {
                    GameObject thornShell = Instantiate(thornShellPrefab, transform);
                    coolTime = 0f;
                    Rigidbody shell_rigi = thornShell.GetComponent<Rigidbody>();
                    shell_rigi.AddForce(new Vector3(0f, 0f, 500f));
                }
                break;
            case 2:
                if (coolTime > interval)
                {
                    GameObject thornShell = Instantiate(thornShellPrefab, transform);
                    coolTime = 0f;
                    Rigidbody shell_rigi = thornShell.GetComponent<Rigidbody>();
                    shell_rigi.AddForce(new Vector3(0f, 0f, -500f));
                }
                break;
            case 3:
                if (coolTime > interval)
                {
                    GameObject thornShell = Instantiate(thornShellPrefab, transform);
                    coolTime = 0f;
                    Rigidbody shell_rigi = thornShell.GetComponent<Rigidbody>();
                    shell_rigi.AddForce(new Vector3(-500f, 0f, 0f));
                }
                break;
            case 4:
                if (coolTime > interval)
                {
                    GameObject thornShell = Instantiate(thornShellPrefab, transform);
                    coolTime = 0f;
                    Rigidbody shell_rigi = thornShell.GetComponent<Rigidbody>();
                    shell_rigi.AddForce(new Vector3(500f, 0f, 0f));
                }
                break;
            default:
                break;
        }
    }
}
