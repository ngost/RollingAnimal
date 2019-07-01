using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimControler : MonoBehaviour
{
    public string _AnimMotionTriggerName;
    Animator animator;
    GameObject ValueRewardObj;
    VisibleTimer VisibleTimerOfRewardObj;
    bool isPlayed = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        ValueRewardObj = GameObject.Find("ValueReward");
        VisibleTimerOfRewardObj = (VisibleTimer)ValueRewardObj.GetComponent(typeof(VisibleTimer));
        animator.SetTrigger("Walk");

    }

    // Update is called once per frame
    void Update()
    {
        if (VisibleTimerOfRewardObj.times < 0 && !isPlayed)
        { 
            PlayOneShot();
        }

    }

    public void PlayOneShot()
    {
        animator.SetTrigger(_AnimMotionTriggerName);
        isPlayed = true;
    }
}
