using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BundleUnloader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if(LoadingSceneManager.bundle != null)
        {
            LoadingSceneManager.bundle.Unload(false);
        }
    }
}
