using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReviewChecker : MonoBehaviour
{
    public GameObject dialog;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ReviewChecking());
        
    }

    IEnumerator ReviewChecking()
    {
        yield return new WaitForSeconds(1.5f);
        if (DataLoadAndSave.LoadCoin() > 10000)
        {
            if (DataLoadAndSave.LoadReviewingStatus().Equals(0))
            {
                if (dialog != null)
                {
                    Instantiate(dialog);
                }

            }
            else
            {
                //nothing
            }
        }
    }

}
