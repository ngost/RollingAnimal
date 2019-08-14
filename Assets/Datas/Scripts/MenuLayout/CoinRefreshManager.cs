using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinRefreshManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("coinText").GetComponent<Text>().text = DataLoadAndSave.LoadCoin().ToString();

    }
}
