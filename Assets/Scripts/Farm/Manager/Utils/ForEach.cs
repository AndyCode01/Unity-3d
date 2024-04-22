using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForEach : MonoBehaviour
{
    public void SetActivationByGroup(GameObject group, string nameObject)
    {
        foreach (Transform child in group.transform)
        {
            if(child.name == nameObject )
            {
                child.gameObject.SetActive(true);
            }
            else if(child.name != nameObject)
            {
                child.gameObject.SetActive(false);
            }
        }
    }
}
