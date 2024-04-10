using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectLand : MonoBehaviour
{
    RaycastHit previousLand = new RaycastHit();
    // Start is called before the first frame update
    public void IsPointingAtLand(RaycastHit hit)
    {
        if (previousLand.transform != null)
        {
            if(previousLand.transform != hit.transform)
            {
                previousLand.transform.Find("Selected").gameObject.SetActive(false);
                hit.transform.Find("Selected").gameObject.SetActive(true);
            }  
        }
        else
        {
            hit.transform.Find("Selected").gameObject.SetActive(true); 
        }
        previousLand = hit;
    }

    // Update is called once per frame
    public void UnselectLand()
    {
        if (previousLand.transform != null)
        {
            previousLand.transform.Find("Selected").gameObject.SetActive(false);
            previousLand = new RaycastHit();
        }  
    }
}
