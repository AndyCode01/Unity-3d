using System;
using UnityEngine;

public class HandleCursor : MonoBehaviour
{
    public GameObject player;
    HandItem handItem;
    IconInput iconInput;
    ForEach forEach;
    string ObjectAtPoint;
    Ray rayo;
    RaycastHit hit;
    GameObject previousInteractiveObject;
    float pointHitY;

    void Start()
    {
        iconInput = GetComponent<IconInput>();
        forEach = GetComponent<ForEach>();
        handItem = GetComponent<HandItem>();
    }

    void Update()
    {
        rayo = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        // Debug.DrawRay(rayo.origin, rayo.direction * 100, Color.red);
        ObjectAtPoint = IsPointing();
    }

    public float GetHitPointY()
    {
        return pointHitY;
    }

    public string IsPointing()
    {
        if (Physics.Raycast(rayo, out hit))
        {
            pointHitY=hit.point.y;
            Vector3 distance = hit.transform.position - player.transform.position;
            if (hit.transform.CompareTag("HandItem") && distance.magnitude <= 2f && !handItem.GetItemInHand())
            {
                IsPointingAtInteractiveObject(hit.transform.gameObject,"Hand");
                return hit.transform.tag;
            }
            // if (hit.transform.CompareTag("Land") && distance.magnitude <= 3f)
            // {
            //     IsPointingAtInteractiveObject(hit.transform.gameObject,"Mini_shovel");
            //     return hit.transform.tag;
            // }
            if (hit.transform.CompareTag("Land") && distance.magnitude <= 3f)
            {
                Transform parent  = hit.transform.parent;
                while (!parent.name.Contains("Land"))
                {
                    parent = parent.parent;
                } 
                IsPointingAtInteractiveObject(parent.gameObject,"");
                return hit.transform.tag;
            }
            
        }
        UnselectInteractiveObject();

        return null;
    }  

    void ExistSelectedArea(GameObject interactiveObject, bool condition)
    {
        if (interactiveObject.transform.Find("Selected"))
        {
            interactiveObject.transform.Find("Selected").gameObject.SetActive(condition);
        }
    }

    void IsPointingAtInteractiveObject(GameObject interactiveObject, string toolNecessary)
    {
        if(previousInteractiveObject != null)
        {
            if (previousInteractiveObject.transform != interactiveObject.transform)
            {
                forEach.SetActivationByGroup(previousInteractiveObject.transform.Find("Icon").gameObject,"null");
                ExistSelectedArea(previousInteractiveObject.transform.gameObject,false);
                ExistSelectedArea(interactiveObject,true);
                if(toolNecessary!="")forEach.SetActivationByGroup(interactiveObject.transform.Find("Icon").gameObject,toolNecessary);
            }
        }
        else
        {
            iconInput.GetIcon(interactiveObject.transform.gameObject).SetActive(true);
            ExistSelectedArea(interactiveObject,true);
        }
        previousInteractiveObject = interactiveObject;
        
    }

    void UnselectInteractiveObject()
    {
        if (previousInteractiveObject != null)
        {
            forEach.SetActivationByGroup(previousInteractiveObject.transform.Find("Icon").gameObject,"null");
            ExistSelectedArea(previousInteractiveObject,false);
            previousInteractiveObject = null;
        }  
    }

    public string GetObjectAtPoint()
    {
        return ObjectAtPoint;
    }

    public GameObject GetInteractiveObject()
    {
        return previousInteractiveObject;
    }
}
