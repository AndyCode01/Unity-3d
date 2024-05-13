using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ManagerTimesUp : MonoBehaviour
{
    public SpriteManager spriteManager;
    void Start()
    {
        transform.Find("Panel").gameObject.SetActive(false);
        StartCoroutine(TimesUp());
    }

    void Update()
    {
        if((GameObject.Find("Data").GetComponent<HarvestBaskets>().GetCountGrain())+(spriteManager.grains.Sum()*6/400) >=4.7f)FinishMinigame();
    }

    void FinishMinigame()
    {
        if(spriteManager.HarvestQuality == null)spriteManager.HarvestQuality = "Mala calidad";
        transform.Find("Panel").transform.Find("TotalGrain").GetComponent<TextMeshProUGUI>().text = $"Han recolectado un total de {spriteManager.grains.Sum()} granos";
        if(spriteManager.HarvestQuality == null)spriteManager.HarvestQuality = "Mala calidad";
        transform.Find("Panel").transform.Find("Quality").GetComponent<TextMeshProUGUI>().text = $"Calidad cosecha: {spriteManager.HarvestQuality}";
        transform.Find("Panel").gameObject.SetActive(true);
        Camera.main.GetComponent<AudioSource>().Stop();
        transform.parent.Find("OptionHarvest").gameObject.SetActive(false);
        StartCoroutine(ExitLevel());
    }

    IEnumerator TimesUp()
    {
        yield return new WaitForSeconds(20);
        FinishMinigame();
    }
    

    IEnumerator ExitLevel()
    {
        yield return new WaitForSeconds(10);
        GameObject data = GameObject.Find("Data");
        data.GetComponent<HarvestBaskets>().SwitchBasketStatus(data.GetComponent<SaveLoadSystem>().gameData.PlayerData.HandItem,spriteManager.status);
        data.GetComponent<HarvestBaskets>().SetCountGrain(data.GetComponent<SaveLoadSystem>().gameData.PlayerData.HandItem,spriteManager.grains.Sum());
        data.GetComponent<SaveLoadSystem>().SaveGame();
        data.GetComponent<SaveLoadSystem>().LoadGame(data.
        GetComponent<SaveLoadSystem>().gameData.Name);
    }
}
