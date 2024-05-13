using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Land : MonoBehaviour, IBind<LandData>
{
    [field: SerializeField] public SerializableGuid Id{get;set;} = SerializableGuid.NewGuid();
    [SerializeField] public LandData data;
    int WaitForGrownUpPlant=5;

    enum LandStatus
    {
        Default,Soil,Watered,GrownUp,Farmland
    }

    public void Bind(LandData data)
    {
        this.data = data;
        this.data.Id = Id;
        SetMaterial();
        if(data.LandPosition != new Vector3(0,0,0)) transform.position = data.LandPosition;
    }

    void SetMaterial()
    {
        string namePhase = null;
        switch (data.LandStatus)
        {
            case "Soil":
            namePhase= "LandPhase2";
            break;
            case "Watered":
            namePhase=  "CoffeePhase1";
            break;
            case "GrownUp":
            namePhase= "CoffeePhase2";
            break;
            case "Farmland":
            namePhase= "CoffeePhase3";
            break;
            default:
            namePhase = "LandPhase1";
            break;
        }
        foreach (Transform phase in transform.Find("Phases"))
        {
            if(phase.name == namePhase) phase.gameObject.SetActive(true);
            else phase.gameObject.SetActive(false);
        }
        
        if(namePhase=="CoffeePhase2")
        {
            if(data.UseInHarvest)
            {
                WaitForGrownUpPlant=30;
                StartCoroutine(GrownUpPlant());
                data.UseInHarvest = false;
            }
            else StartCoroutine(GrownUpPlant());
        }
    }

    IEnumerator  GrownUpPlant()
    {
        yield return new WaitForSeconds(WaitForGrownUpPlant);
        foreach (Transform phase in transform.Find("Phases"))
        {
            if(phase.name == "CoffeePhase3") phase.gameObject.SetActive(true);
            else phase.gameObject.SetActive(false);
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
    public bool UseInHarvest;
}
