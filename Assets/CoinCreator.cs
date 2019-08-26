using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CoinCreator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject CoinObj = (GameObject)Resources.Load("prefabs/Item/10000Coin");

        CoinObj.transform.localScale = new Vector3(2f, CoinObj.transform.localScale.y, 2f);
        Instantiate(CoinObj);
        DataLoadAndSave.CoinsRewarded(10000);

        StartCoroutine("BackToShop");
    }

    IEnumerator BackToShop()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene("ShopScene");
    }

}
