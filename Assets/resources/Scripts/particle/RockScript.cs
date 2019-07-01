using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockScript : MonoBehaviour
{
    public GameObject particle;
    MeshRenderer renderer;
    ParticleSystem particleSystem;
    float rotateVal;
    bool isCollded = false;
    int random;
    SphereCollider collider;
    // Start is called before the first frame update
    void Start()
    {
        particleSystem = particle.GetComponent<ParticleSystem>();
        renderer = gameObject.GetComponent<MeshRenderer>();
        collider = gameObject.GetComponent<SphereCollider>();
        random = Random.Range(0, 3);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCollded)
        {
            if(random == 0)
            {
                rotateVal += Time.deltaTime * 300f;
                transform.rotation = Quaternion.Euler(0, 0, rotateVal);
            }else if (random == 1)
            {
                rotateVal += Time.deltaTime * 300f;
                transform.rotation = Quaternion.Euler(0, rotateVal, 0);
            }
            else
            {
                rotateVal += Time.deltaTime * 300f;
                transform.rotation = Quaternion.Euler(rotateVal, 0, 0);
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        collider.enabled = false;
        isCollded = true;
        particleSystem.Play();
        renderer.enabled = false;
        Destroy(gameObject,0.3f);
    }
}
