using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallScript : MonoBehaviour
{
    public GameObject particleObj;
    AudioSource audioSource;
    MeshRenderer renderer;
    Collider m_col;
    Rigidbody m_rb;
    // Start is called before the first frame update
    void Start()
    {
        m_col = gameObject.GetComponent<SphereCollider>();
        audioSource = gameObject.GetComponent<AudioSource>();
        renderer = gameObject.GetComponent<MeshRenderer>();
        m_rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("fireball Collision");
        m_col.enabled = false;
        renderer.enabled = false;
        m_rb.constraints = RigidbodyConstraints.FreezeAll;

        audioSource.Play();
        Instantiate(particleObj, transform);
        Destroy(gameObject, 2f);

        //Transform instance_tr = transform;
        //instance_tr.position = new Vector3(transform.position.x, transform.position.y - 0.5f, transform.position.z);

    }
}
