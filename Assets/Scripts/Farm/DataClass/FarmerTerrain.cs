using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmerTerrain : MonoBehaviour
{
    public GameObject landPrefab;
    public float distance = 1f;

    public List<LandData> newLand(Vector3 startPosition,int quantityLand)
    {
        List<LandData> landDatas = new List<LandData>();
        int rowSize = (int)Mathf.Sqrt(quantityLand);
        for (int i = 0; i < quantityLand; i++)
        {
            float x = startPosition.x + (i % rowSize) * distance;
            float z = startPosition.z - (i / rowSize) * distance;
            Vector3 position = new Vector3(x, 0, z); 
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
