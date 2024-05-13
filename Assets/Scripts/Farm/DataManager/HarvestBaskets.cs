using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestBaskets : MonoBehaviour
{
    public GameObject basketPrefab;
    public float radio = 0.8f;
    SaveLoadSystem saveLoadSystem;

    void Awake()
    {
        saveLoadSystem = GetComponent<SaveLoadSystem>();
    }

    public List<BasketData> newBasket(Vector3 positionGroup, int quantityBasket)
    {
        List<BasketData> BasketDatas = new List<BasketData>();
        float angleStep = 360f / quantityBasket;
        for (int i = 0; i < quantityBasket; i++)
        {
            float angle = angleStep * i;
            Vector3 positionBasket = positionGroup + new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), positionGroup.y, Mathf.Sin(angle * Mathf.Deg2Rad)) * radio;
            GameObject BasketObject = Instantiate(basketPrefab,positionBasket,Quaternion.identity);
            BasketObject.GetComponent<Basket>().Id = SerializableGuid.NewGuid();
            BasketDatas.Add(BasketObject.GetComponent<Basket>().data);
        }
        return BasketDatas;
    }

    public void loadBasket(List<BasketData> BasketDatas)
    {

        foreach (BasketData Basket in BasketDatas)
        {
            GameObject BasketObject = Instantiate(basketPrefab,Basket.BasketPosition,Basket.BasketRotation);
            BasketObject.GetComponent<Basket>().Id = Basket.Id;
        }
    }

    public void SwitchBasketStatus(SerializableGuid idBasket, string newStatus)
    {
        List<BasketData> listBasket = saveLoadSystem.gameData.BasketDatas;
        BasketData BasketAtChange =listBasket.Find(Basket => Basket.Id == idBasket);
        BasketAtChange.BasketStatus = newStatus;
    }

    public void SetCountGrain(SerializableGuid idBasket, int countGrain)
    {
        List<BasketData> listBasket = saveLoadSystem.gameData.BasketDatas;
        BasketData BasketAtChange =listBasket.Find(Basket => Basket.Id == idBasket);
        BasketAtChange.CountGrain = countGrain;
    }

    public int GetCountGrain()
    {
        SerializableGuid  id = saveLoadSystem.gameData.PlayerData.HandItem;
        List<BasketData> listBasket = saveLoadSystem.gameData.BasketDatas;
        BasketData basket =listBasket.Find(Basket => Basket.Id == id);
        return basket.CountGrain;
    }
}
