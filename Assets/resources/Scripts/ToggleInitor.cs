using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleInitor : MonoBehaviour
{
    public string Toggle_Obj_Name;
    // Start is called before the first frame update
    Toggle obj_toggle;
    bool isEnable;
    void Start()
    {
        obj_toggle = gameObject.GetComponent<Toggle>();
        switch (Toggle_Obj_Name)
        {
            case "back_sound":
                StaticInfoManager.background_sound_enable = DataLoadAndSave.LoadSoundData("back_sound");
                obj_toggle.isOn = StaticInfoManager.background_sound_enable;
                break;
            case "effect_sound":
                StaticInfoManager.effect_sound_enable = DataLoadAndSave.LoadSoundData("effect_sound");
                obj_toggle.isOn = StaticInfoManager.effect_sound_enable;
                break;
            case "low":
                if(DataLoadAndSave.LoadGraphicQuality()==0)
                {
                    obj_toggle.isOn = true;
                    Debug.Log("loaded_low");
                }
                break;
            case "medium":
                if (DataLoadAndSave.LoadGraphicQuality()==1)
                {
                    obj_toggle.isOn = true;
                    Debug.Log("loaded_me");
                }
                break;
            case "high":
                if (DataLoadAndSave.LoadGraphicQuality()==2)
                {
                    obj_toggle.isOn = true;
                    Debug.Log("loaded_high");
                }
                break;
        }
    }

}
