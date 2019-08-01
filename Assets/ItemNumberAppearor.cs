using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ItemNumberAppearor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameDataLoader loader = new GameDataLoader();
        InventoryClass inventory = loader.LoadData();
        GameObject shield = GameObject.Find("ShieldText");
        GameObject reward = GameObject.Find("RewardText");
        GameObject fever = GameObject.Find("FeverText");
        shield.GetComponent<Text>().text = inventory.shieldItem.ToString();
        reward.GetComponent<Text>().text = inventory.rewardItem.ToString();
        fever.GetComponent<Text>().text = inventory.feverItem.ToString();
    }
}
