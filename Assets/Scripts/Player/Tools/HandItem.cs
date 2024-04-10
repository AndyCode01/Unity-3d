using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HandItem : MonoBehaviour
{
    public Tools_Equipment tools_Equipment;
    public IconInput icon;
    public GameObject player;
    HandleCursor handleCursor;
    float massHandleItem=0f;
    RaycastHit hitHandItem;
    
    void Start()
    {
        handleCursor = GetComponent<HandleCursor>();
    }

    void Update()
    {
        if(handleCursor.GetObjectAtPoint() == "HandItem")
        {
            if(hitHandItem.collider== null)hitHandItem = handleCursor.GetHit();
        } 
    }

    public float HandleHandItem(float defaultSpeed)
    {
        float speedWithItem =  massHandleItem/2.5f;
        if(massHandleItem>12) speedWithItem= defaultSpeed;
        return speedWithItem;
    } 

    public void IsPointingAtHandItem()
    {
        if(hitHandItem.collider != null)
        {

            hitHandItem.rigidbody.isKinematic = true;
            hitHandItem.transform.position = player.transform.position + player.transform.forward ;
            hitHandItem.transform.parent = player.transform;
            massHandleItem=hitHandItem.rigidbody.mass;
            icon.SwitchItemInHand();
        }
    }

    public void DropAtHandObject()
    {
        hitHandItem.transform.rotation= Quaternion.Euler(0,0,0);
        hitHandItem.transform.parent = null;
        hitHandItem.transform.position =  new Vector3(player.transform.position.x, 1, player.transform.position.z) + player.transform.forward ;
        hitHandItem.rigidbody.isKinematic = false;
        massHandleItem=0f;
        hitHandItem = new RaycastHit();
        icon.SwitchItemInHand();
    }
}
