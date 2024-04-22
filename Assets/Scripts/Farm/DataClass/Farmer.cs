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
        transform.SetPositionAndRotation(data.PlayerPosition, data.PlayerRotation);
        GameObject[] todosLosObjetos = FindObjectsOfType<GameObject>();
        tools_Equipment.StringToEnumTool(data.ToolEquipted);
    }

    void Update()
    {
        data.PlayerPosition = transform.position;
        data.PlayerRotation = transform.rotation;
        // if(handItem.GetItemInHand())
        // {
        //     data.HandItem = GameObject.FindGameObjectWithTag("HandItem").GetComponent<Pot>().GetId();
        // }
        data.ToolEquipted = tools_Equipment.GetToolEquipment();
    }
}

[Serializable]
public class PlayerData : ISaveable {
    [field: SerializeField] public SerializableGuid Id{get;set;}
    public Vector3 PlayerPosition;
    public Quaternion PlayerRotation;
    public string ToolEquipted;
    public SerializableGuid HandItem{get;set;}
}
