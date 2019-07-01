using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        SimpleSceneFader.ChangeSceneWithFade("StoryMapScene");
    }
}
