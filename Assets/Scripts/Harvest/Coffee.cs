using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using System.Linq;
using TMPro;
using UnityEngine.EventSystems;

public class Coffee : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    SpriteManager spriteManager;
    Image imagen;
    int countGrain=1;
    

    void Start()
    {
        spriteManager = transform.parent.GetComponent<SpriteManager>();
        imagen = GetComponent<Image>();
        InvokeRepeating("GenerateRandomNumber",0f,Random.Range(0.5f,1f) * 2f);
    }

    public void OnSelect(BaseEventData eventData)
    {
        transform.Find("SelectGrain").gameObject.SetActive(true);
    }

    public void OnDeselect(BaseEventData eventData)
    {
        transform.Find("SelectGrain").gameObject.SetActive(false);
    }

    void GenerateRandomNumber()
    {
        int statusCoffe = Random.Range(1, 6);
        RandomValues();
        imagen.sprite = spriteManager.SelectSprite(statusCoffe);
    }

    void RandomValues()
    {
        countGrain = Random.Range(1, 4);
        switch (countGrain)
        {
            case 1:
            countGrain=10;
            break;
            case 2:
            countGrain=15;
            break;
            case 3:
            countGrain=20;
            break;
        }
        transform.Find("Text (TMP)").GetComponent<TextMeshProUGUI>().text = $"{countGrain}";
    }

    

    public void SelectGrain()
    {
        switch (imagen.sprite.name)
        {
            case "BitGreenGrain":
            spriteManager.grains[0] += countGrain;
            break;
            case "BitRedGrain":
            spriteManager.grains[1] += countGrain;
            break;
            case "GreenGrain":
            spriteManager.grains[2] += countGrain;
            break;
            case "HalfGrain":
            spriteManager.grains[3] += countGrain;
            break;
            case "RedGrain":
            spriteManager.grains[4] += countGrain;
            break;
        }
        spriteManager.SetQuality();
        RandomValues();
        GenerateRandomNumber();
    }
}
