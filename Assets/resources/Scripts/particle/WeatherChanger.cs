using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherChanger : MonoBehaviour
{
    public GameObject weatherPrefab;
    public GameObject player;
    public float changeDistanceZ;
    public GameObject light_obj;
    Light light;
    bool onetime = true;
    public string goalWeather;
    bool changeDone;
    GameObject instantiedObj;
    ParticleSystem particle;
    AudioSource audio;
    // Start is called before the first frame update
    void Start()
    {
        light = light_obj.GetComponent<Light>();
        changeDone = false;
        if (goalWeather.Equals("rainy"))
        {
            instantiedObj = Instantiate(weatherPrefab, gameObject.transform);
            particle = instantiedObj.GetComponent<ParticleSystem>();
            audio = instantiedObj.GetComponent<AudioSource>();
            particle.Pause();
            audio.enabled = false;
        }

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, player.transform.position.z);
        if (!changeDone)
        {
            if (player.transform.position.z >= changeDistanceZ)
            {

                if (goalWeather.Equals("rainy"))
                {
                    if (onetime)
                    {
                        GameObject flowDance = GameObject.Find("FlowerDance");
                        if(flowDance != null)
                        {
                            Destroy(flowDance);
                        }
                        particle.Play();
                        audio.enabled = true;
                        onetime = false;
                        StartCoroutine("Done");
                    }
                    if (light.intensity > 0.8f)
                    {
                        light.intensity = light.intensity - 0.01f;
                    }
                }

                if (goalWeather.Equals("sunny"))
                {
                    if (onetime)
                    {
                        Destroy(GameObject.Find("Rain_Wind(Clone)"));
                        onetime = false;
                        StartCoroutine("Done");
                    }
                    if (light.intensity < 1.5f)
                    {
                        light.intensity = light.intensity + 0.01f;
                    }
                }

                if (goalWeather.Equals("snow"))
                {
                    if (onetime)
                    {
                        GameObject snowObject = Instantiate(weatherPrefab, gameObject.transform);
                        Destroy(snowObject, 5f);
                        onetime = false;
                        StartCoroutine("Iterator");
                    }
                }
            }
        }
    }

    IEnumerator Iterator()
    {
        yield return new WaitForSeconds(2f);
        onetime = true;
    }

    IEnumerator Done()
    {
        yield return new WaitForSeconds(3f);
        changeDone = true;
    }
}
