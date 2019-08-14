using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornShellScript : MonoBehaviour
{

    public GameObject particleObj;
    AudioSource audioSource;
    public AudioClip crash_clip;
    private MeshRenderer renderer;
    private SphereCollider collider;
    private Rigidbody rb;
      
    // Start is called before the first frame update
    void Start()
    {
        renderer = gameObject.GetComponent<MeshRenderer>();
        collider = gameObject.GetComponent<SphereCollider>();
        rb = gameObject.GetComponent<Rigidbody>();
        audioSource = gameObject.GetComponent<AudioSource>();
        audioSource.Play();
        StartCoroutine(FakeDestroy());
    }
    private void OnTriggerEnter(Collider other)
    {
        if(gameObject != null)
        {
            rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
            audioSource.PlayOneShot(crash_clip);
//            Debug.Log("play crash");
            Instantiate(particleObj, transform);
            Destroy(gameObject, 1f);
        }
    }

    IEnumerator FakeDestroy()
    {
        yield return new WaitForSeconds(0.2f);
        collider.enabled = false;
        renderer.enabled = false;
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;
    }
}
