using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandItem : MonoBehaviour
{
    public GameObject player;
    HandleCursor handleCursor;
    ForEach forEach;
    float massHandleItem=0f;
    bool ItemInHand= false;
    GameObject hitHandItem;
    Vector3 distance;
    
    void Awake()
    {
        handleCursor = GetComponent<HandleCursor>();
        forEach = GetComponent<ForEach>();
    }

    public float HandleHandItem(float defaultSpeed)
    {
        float speedWithItem =  massHandleItem/2.5f;
        if(massHandleItem>12) speedWithItem= defaultSpeed;
        return speedWithItem;
    } 

    public bool FlagHaveItem()
    {
        return ItemInHand;
    }

    public GameObject GetItemInHand()
    {
        return hitHandItem;
    }

    public void SetActiveIcon(GameObject icon)
    {
        if (distance.magnitude <= 2f && !ItemInHand) icon.SetActive(true);
        else icon.SetActive(false);
    }

    public void SetHandItem()
    {
        if(!ItemInHand)
        {
            hitHandItem  = handleCursor.GetInteractiveObject();
            if(hitHandItem != null && (hitHandItem.transform.tag == "HandItem") )
            {
                ItemOnHand(hitHandItem);
            }
        }   
    }

    public void ItemOnHand(GameObject item)
    {
        if(hitHandItem == null || hitHandItem != item)hitHandItem = item;
        forEach.SetActivationByGroup(hitHandItem.transform.Find("Icon").gameObject,"null");
        hitHandItem.transform.tag= "ItemOnHand";
        ChangeLayerRecursively(hitHandItem.transform, LayerMask.NameToLayer("Tools"));
        hitHandItem.GetComponent<Rigidbody>().isKinematic = true;
        hitHandItem.transform.position = player.transform.position + player.transform.forward ;
        hitHandItem.transform.parent = player.transform;
        massHandleItem=hitHandItem.GetComponent<Rigidbody>().mass;
        ItemInHand= true;
    }

    void ChangeLayerRecursively(Transform trans, int layer)
    {
        trans.gameObject.layer = layer;
        foreach (Transform child in trans)
        {
            ChangeLayerRecursively(child, layer);
        }
    }


    public void DropAtHandObject(bool destroyObject=false)
    {
        if(ItemInHand)
        {
            hitHandItem.transform.tag= "HandItem";
            ChangeLayerRecursively(hitHandItem.transform, LayerMask.NameToLayer("Default"));
            hitHandItem.transform.rotation= Quaternion.Euler(0,0,0);
            hitHandItem.transform.parent = null;
            hitHandItem.transform.position =  new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z) + player.transform.forward ;
            hitHandItem.GetComponent<Rigidbody>().isKinematic = false;
            massHandleItem=0f;
            if(destroyObject == true)Destroy(hitHandItem);
            hitHandItem = null;
            ItemInHand= false;
        }
    }
}
