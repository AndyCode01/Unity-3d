using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour, IBind<PotData>
{
   [field: SerializeField] public SerializableGuid Id{get;set;}
   [SerializeField] public PotData data;

    public void Bind(PotData data)
    {
        this.data = data;
        this.data.Id = Id;
        if(data.PotPosition != new Vector3(0,0,0)) transform.SetPositionAndRotation(data.PotPosition, data.PotRotation);
    }

    public SerializableGuid GetId()
    {
        return Id;
    }

    void Update()
    {
        data.PotPosition = transform.position;
        data.PotRotation = transform.rotation;
    }

    public delegate void ObjectDestroyedAction();
    public static event ObjectDestroyedAction OnObjectDestroyed;

    void OnDestroy()
    {
        GameObject dataObject =  GameObject.Find("Data");
        if(dataObject != null) dataObject.GetComponent<Crops>().removePod(Id);
    }
}

[Serializable]
public class PotData : ISaveable{
    [field: SerializeField] public SerializableGuid Id{get;set;}
    public Vector3 PotPosition; 
    public Quaternion PotRotation;
}
