using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crops : MonoBehaviour
{

    public GameObject potPrefab;
    public float radio = 0.5f;


    public List<PotData> newPot(Vector3 positionGroup, int quantityPot)
    {
        List<PotData> potDatas = new List<PotData>();
        float angleStep = 360f / quantityPot;
        for (int i = 0; i < quantityPot; i++)
        {
            float angle = angleStep * i;
            Vector3 positionPot = positionGroup + new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), 0, Mathf.Sin(angle * Mathf.Deg2Rad)) * radio;
            GameObject potObject = Instantiate(potPrefab,positionPot,Quaternion.identity);
            potObject.GetComponent<Pot>().Id = SerializableGuid.NewGuid();
            potDatas.Add(potObject.GetComponent<Pot>().data);

        }
        return potDatas;
    }

    public void loadPot(List<PotData> potDatas)
    {

        foreach (PotData pot in potDatas)
        {
            GameObject potObject = Instantiate(potPrefab,pot.PotPosition,pot.PotRotation);
            potObject.GetComponent<Pot>().Id = pot.Id;
        }
    }
}
