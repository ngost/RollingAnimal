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

        if (DataLoadAndSave.LoadRewardItemIsUsing().Equals(1))
        {
            GameObject.Find("RewardParticle").GetComponent<ParticleSystem>().Play();
        }
        else
        {
            GameObject.Find("RewardParticle").GetComponent<ParticleSystem>().Stop();
        }

        if (DataLoadAndSave.LoadShieldItemIsUsing().Equals(1))
        {
            GameObject.Find("ShieldParticle").GetComponent<ParticleSystem>().Play();
        }
        else
        {
            GameObject.Find("ShieldParticle").GetComponent<ParticleSystem>().Stop();
        }

        if (DataLoadAndSave.LoadFeverItemIsUsing().Equals(1))
        {
            GameObject.Find("FeverParticle").GetComponent<ParticleSystem>().Play();
        }
        else
        {
            GameObject.Find("FeverParticle").GetComponent<ParticleSystem>().Stop();
        }
    }
}
