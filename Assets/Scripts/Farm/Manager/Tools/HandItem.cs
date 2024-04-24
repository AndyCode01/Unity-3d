using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandItem : MonoBehaviour
{
    public GameObject player;
    IconInput iconInput;
    HandleCursor handleCursor;
    ForEach forEach;
    float massHandleItem=0f;
    bool ItemInHand= false;
    GameObject hitHandItem;
    Vector3 distance;
    
    void Start()
    {
        handleCursor = GetComponent<HandleCursor>();
        forEach = GetComponent<ForEach>();
        iconInput = GetComponent<IconInput>();
    }

    public float HandleHandItem(float defaultSpeed)
    {
        float speedWithItem =  massHandleItem/2.5f;
        if(massHandleItem>12) speedWithItem= defaultSpeed;
        return speedWithItem;
    } 

    public bool GetItemInHand()
    {
        return ItemInHand;
    }

    public void SetActiveIcon(GameObject icon)
    {
        if (distance.magnitude <= 2f && !ItemInHand) icon.SetActive(true);
        else icon.SetActive(false);
    }

    public void SetHandItem()
    {
        hitHandItem  = handleCursor.GetInteractiveObject();
        if(hitHandItem != null && !ItemInHand && (hitHandItem.transform.tag == "HandItem" ||hitHandItem.transform.tag == "Pot") )
        {
            if (distance.magnitude <= 2f)
            {
                forEach.SetActivationByGroup(hitHandItem.transform.Find("Icon").gameObject,"null");
                hitHandItem.transform.tag= "Untagged";
                hitHandItem.GetComponent<Rigidbody>().isKinematic = true;
                hitHandItem.transform.position = player.transform.position + player.transform.forward ;
                hitHandItem.transform.parent = player.transform;
                massHandleItem=hitHandItem.GetComponent<Rigidbody>().mass;
                ItemInHand= true;
            }
        }
    }

    public void DropAtHandObject()
    {
        if(ItemInHand)
        {
            hitHandItem.transform.tag= "HandItem";
            hitHandItem.transform.rotation= Quaternion.Euler(0,0,0);
            hitHandItem.transform.parent = null;
            hitHandItem.transform.position =  new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z) + player.transform.forward ;
            hitHandItem.GetComponent<Rigidbody>().isKinematic = false;
            massHandleItem=0f;
            hitHandItem = null;
            ItemInHand= false;
        }
    }
}
