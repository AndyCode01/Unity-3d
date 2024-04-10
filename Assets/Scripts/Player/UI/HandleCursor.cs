using System;
using UnityEngine;

public class HandleCursor : MonoBehaviour
{
    public GameObject player;
    public IconInput icon;
    public SelectLand selectLand;
    string ObjectAtPoint;

    Ray rayo;
    RaycastHit hit;
    Vector3 distance;


    void Update()
    {
        rayo = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        Debug.DrawRay(rayo.origin, rayo.direction * 100, Color.red);
        IsPointing();
    }

    public bool IsPointing()
    {
        if (Physics.Raycast(rayo, out hit))
        {
            distance = hit.transform.position - player.transform.position;
            if (hit.transform.CompareTag("HandItem") && distance.magnitude <= 2f) ObjectAtPoint= hit.transform.tag;
            if (hit.transform.CompareTag("Land") && distance.magnitude <= 4f)
            {
                ObjectAtPoint= hit.transform.tag;
                selectLand.IsPointingAtLand(hit);
                return false;
            }
            
        }
        else{
            ObjectAtPoint=null;
        }
        selectLand.UnselectLand();
        return false;
    }    

    public string GetObjectAtPoint()
    {
        return ObjectAtPoint;
    }

    public RaycastHit GetHit()
    {
        return hit;
    }
}
