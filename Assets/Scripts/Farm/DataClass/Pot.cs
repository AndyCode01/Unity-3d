using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour, IBind<PotData>
{
   [field: SerializeField] public SerializableGuid Id{get;set;} = SerializableGuid.NewGuid();
   [SerializeField] PotData data;

    public void Bind(PotData data)
    {
        this.data = data;
        this.data.Id = Id;
        transform.SetPositionAndRotation(data.PotPosition, data.PotRotation);
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

}

[Serializable]
public class PotData : ISaveable{
    [field: SerializeField] public SerializableGuid Id{get;set;}
    public Vector3 PotPosition; 
    public Quaternion PotRotation;
}
