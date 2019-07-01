using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowBallScript : MonoBehaviour
{
    public GameObject particleObj;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        float scale = Random.Range(0.5f, 1f);
        transform.localScale = new Vector3(scale, scale, scale);
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (transform.position.y <= -1f)
        {
            Destroy(gameObject);
        }
        //Transform instance_tr = transform;
        //instance_tr.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);
        audioSource.Play();
        Instantiate(particleObj,transform);
    }
}
