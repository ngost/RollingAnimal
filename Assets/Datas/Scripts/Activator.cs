using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour
{
    List<GameObject> grounds,items,hurdles;
    public GameObject player;
    bool onetimeFlag = true;
    float positionZ;
    bool[] onetimeDelay;
    // Start is called before the first frame update
    void Start()
    {
        grounds = new List<GameObject>();
        items = new List<GameObject>();
        hurdles = new List<GameObject>();
        onetimeDelay = new bool[3];
        onetimeDelay[0] = true;
        onetimeDelay[1] = true;
        onetimeDelay[2] = true;

        GameObject[] objs = GameObject.FindGameObjectsWithTag("ground");
        GameObject[] obj_items = GameObject.FindGameObjectsWithTag("items");
        GameObject[] obj_hurdles = GameObject.FindGameObjectsWithTag("hurdles");
        for (int i = 0; i < objs.Length; i++)
        {
            grounds.Add(objs[i]);
        }

        for (int i = 0; i < obj_items.Length; i++)
        {
            items.Add(obj_items[i]);
        }
        for (int i = 0; i < obj_hurdles.Length; i++)
        {
            hurdles.Add(obj_hurdles[i]);
        }

        StartCoroutine(CheckingGround());
        StartCoroutine(CheckingItems());
        StartCoroutine(CheckingHurdles());
    }


    IEnumerator CheckingHurdles()
    {
        if (onetimeDelay[1])
        {
            onetimeDelay[1] = false;
            yield return new WaitForSeconds(0.2f);
        }
        for (int i = 0; i < hurdles.Count; i++)
        {
            if (hurdles[i] == null)
                continue;

            if (hurdles[i].transform.position.z > player.transform.position.z + 20 || hurdles[i].transform.position.z < player.transform.position.z - 15)
            {
                hurdles[i].SetActive(false);
            }
            else
            {
                hurdles[i].SetActive(true);
            }
        }
        yield return new WaitForSeconds(0.5f);

        StartCoroutine(CheckingHurdles());
    }

    IEnumerator CheckingItems()
    {
        if (onetimeDelay[2])
        {
            onetimeDelay[2] = false;
            yield return new WaitForSeconds(0.3f);
        }

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i] == null)
                continue;
            if (items[i].transform.position.z > player.transform.position.z + 20 || items[i].transform.position.z < player.transform.position.z - 8)
            {
                items[i].SetActive(false);
            }
            else
            {
                items[i].SetActive(true);
            }
        }
        yield return new WaitForSeconds(0.5f);

        StartCoroutine(CheckingItems());
    }
    IEnumerator CheckingGround()
    {
        if (onetimeDelay[0])
        {
            onetimeDelay[0] = false;
            yield return new WaitForSeconds(0.1f);
        }
//        Debug.Log(player.transform.position.z);
        for (int i = 0; i < grounds.Count; i++)
        {

            //if (grounds[i].transform.position.z < player.transform.position.z-20)
            //{
            //    grounds[i].SetActive(false);
            //    //grounds.RemoveAt(i);
            //}

            if (grounds[i] == null)
                continue;

            //else
            if (grounds[i].transform.position.z > player.transform.position.z+20 || grounds[i].transform.position.z < player.transform.position.z - 8)
            {
                grounds[i].SetActive(false);
            }
            else
            {
                grounds[i].SetActive(true);
            }
        }
        positionZ = 0f;
        yield return new WaitForSeconds(0.5f);

        StartCoroutine(CheckingGround());
    }
    
}
