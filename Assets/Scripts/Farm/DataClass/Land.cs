using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Land : MonoBehaviour, IBind<LandData>
{
    [field: SerializeField] public SerializableGuid Id{get;set;} = SerializableGuid.NewGuid();
    [SerializeField] public LandData data;

    enum LandStatus
    {
        Default,Soil,Watered,GrownUp,Farmland
    }

    public void Bind(LandData data)
    {
        this.data = data;
        this.data.Id = Id;
        if(data.LandStatus == null)data.LandStatus = LandStatus.Default.ToString();
        if(data.LandPosition != new Vector3(0,0,0)) transform.position = data.LandPosition;
    }

    void SetMaterial()
    {
        switch (data.LandStatus)
        {
            
        }
    }

    void Start()
    {
        data.LandPosition = transform.position;
    }

    public string GetLandStatus()
    {
        return data.LandStatus;
    }
}

[Serializable]
public class LandData : ISaveable{
    [field: SerializeField] public SerializableGuid Id{get;set;}
    public string LandStatus; 
    public Vector3 LandPosition;
}
