using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Farmer : MonoBehaviour, IBind<PlayerData>
{
    [field: SerializeField] public SerializableGuid Id{get;set;} = SerializableGuid.NewGuid();
    [SerializeField] PlayerData data;

    public HandItem handItem;
    public Tools_Equipment tools_Equipment;

    public void Bind(PlayerData data)
    {
        this.data = data;
        this.data.Id = Id;
        if(data.PlayerPosition != new Vector3(0,0,0)) transform.SetPositionAndRotation(data.PlayerPosition, data.PlayerRotation);
        GameObject[] Objects = GameObject.FindGameObjectsWithTag("HandItem");
        foreach (GameObject Object in Objects)
        {
            if(Object.GetComponent<Pot>())
            {
                if(Object.GetComponent<Pot>().GetId() == data.HandItem)handItem.ItemOnHand(Object);
            }
            else if(Object.GetComponent<Basket>())
            {
                if(Object.GetComponent<Basket>().GetId() == data.HandItem)handItem.ItemOnHand(Object);
            }
        }
        tools_Equipment.StringToEnumTool(data.ToolEquipted);
    }

    void Update()
    {
        data.PlayerPosition = transform.position;
        data.PlayerRotation = transform.rotation;
        if(handItem.FlagHaveItem())
        {
            foreach (Transform child in transform)
            {
                if(child.CompareTag("ItemOnHand"))
                {
                    if(child.GetComponent<Pot>())data.HandItem= child.GetComponent<Pot>().GetId();
                    else if(child.GetComponent<Basket>())data.HandItem= child.GetComponent<Basket>().GetId();
                }
            }
        }
        data.ToolEquipted = tools_Equipment.GetToolEquipment();
    }
}

[Serializable]
public class PlayerData : ISaveable {
    [field: SerializeField] public SerializableGuid Id{get;set;}
    public Vector3 PlayerPosition;
    public Quaternion PlayerRotation;
    public string ToolEquipted;
    public SerializableGuid HandItem;
}
