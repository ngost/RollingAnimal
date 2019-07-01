using UnityEngine;
using System.Collections;

/*
* Created by EasyJin.
*/

public class QualitySetter : MonoBehaviour
{
    void Start()
    {
        /*
         InputString gives the string representation of the input button that is being pressed.
         Hence within the Cases each having " ".
        */
        Application.targetFrameRate = 60;

        switch (DataLoadAndSave.LoadGraphicQuality())
        {
            /*
             When we load a new QualitySetting, we log to the console as visual feedback so
             the developer knows that they just changed the setting and to which one.
             */

            /*
             The true corresponds to expensive settings also getting update.
             e.g. Anti-Aliasing
             */
            case 0:
                QualitySettings.SetQualityLevel(0, true);
                Debug.Log("Quality settings set to 'low'");
                break;
            case 1:
                QualitySettings.SetQualityLevel(1, true);
                Debug.Log("Quality settings set to 'medium'");
                break;
            case 2:
                QualitySettings.SetQualityLevel(2, true);
                Debug.Log("Quality settings set to 'high'");
                break;
            /*
             A default value as a just-in-case.
             If it doesnt match the values above it may use this.
             It will simply post a log saying that the pressed button does not correspond
             to a quality setting.

             In my testing it did not use the default case when I pressed a different button
             No idea why but it does not cause any issues.
            */
            default:
                Debug.Log("Button does not change the quality settings!");
                break;
        }
    }
}