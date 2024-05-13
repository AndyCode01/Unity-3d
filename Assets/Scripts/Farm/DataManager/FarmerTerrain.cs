using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerTerrain : MonoBehaviour
{
    public GameObject landPrefab;
    public float distance = 8f;

    public List<LandData> newLand(Vector3 startPosition,int quantityLand)
    {
        List<LandData> landDatas = new List<LandData>();
        int rowSize = (int)Mathf.Sqrt(quantityLand);
        for (int i = 0; i < quantityLand; i++)
        {
            float x = startPosition.x;
            float z = startPosition.z - (landPrefab.transform.lossyScale.z*i);
            Vector3 position = new Vector3(x, startPosition.y, z); 
            GameObject landObject = Instantiate(landPrefab, position, Quaternion.identity);
            landObject.GetComponent<Land>().Id = SerializableGuid.NewGuid();
            landDatas.Add(landObject.GetComponent<Land>().data);
        }
        return landDatas;
    }

    public void loadLand(List<LandData> landDatas)
    {
        foreach (LandData land in landDatas)
        {
            GameObject landObject = Instantiate(landPrefab, land.LandPosition,Quaternion.identity);
            landObject.GetComponent<Land>().Id = land.Id;
        }
    }
}
