using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class Basket : MonoBehaviour, IBind<BasketData>
{
    [field: SerializeField] public SerializableGuid Id{get;set;}
    [SerializeField] public BasketData data;

    public void Bind(BasketData data)
    {
        this.data = data;
        this.data.Id = Id;
        GetComponent<Rigidbody>().mass +=(data.CountGrain*6/400);
        if(data.BasketPosition != new Vector3(0,0,0)) transform.SetPositionAndRotation(data.BasketPosition, data.BasketRotation);
        GetBaskedStatus();
    }

    public SerializableGuid GetId()
    {
        return Id;
    }

    void Update()
    {
        data.BasketPosition = transform.position;
        data.BasketRotation = transform.rotation;
    }

    void GetBaskedStatus()
    {    
        if(data.BasketStatus == null)data.BasketStatus="Basket";
        foreach (Transform child in transform.Find("Phases"))
        {
            if(child.name == data.BasketStatus )
            {
                child.gameObject.SetActive(true);
            }
            else if(child.name != data.BasketStatus)
            {
                child.gameObject.SetActive(false);
            }
        } 
    }
}

[Serializable]
public class BasketData: ISaveable{
    [field: SerializeField] public SerializableGuid Id{get;set;}
    public Vector3 BasketPosition; 
    public Quaternion BasketRotation;
    public string BasketStatus;
    public int CountGrain;
}