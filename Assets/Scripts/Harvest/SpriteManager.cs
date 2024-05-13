using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpriteManager : MonoBehaviour
{
    public Sprite BitGreenGrain,BitRedGrain,GreenGrain,HalfGrain,RedGrain;
    public int[] grains= new int[5];
    public string HarvestQuality=null, status=null;


    public Sprite SelectSprite(int index)
    {
        switch (index)
        {
           case 1:
           return GreenGrain;
           case 2:
           return BitRedGrain;
           case 3:
           return HalfGrain;
           case 4:
           return BitGreenGrain;
           case 5:
           return RedGrain;
        }
        return null;
    }

    public void SetQuality()
    {
        float PercentageBitGreen = (grains[0]*100/grains.Sum())*(0.75f);
        float PercentageBitRed= (grains[1]*100/grains.Sum()) *(0.25f);
        float PercentageGreen = (grains[2]*100/grains.Sum()) * 0;
        float PercentageHalf = (grains[3]*100/grains.Sum()) *0.5f;
        float PercentageRed = (grains[4]*100/grains.Sum()) * 1;
        
        float balance = (PercentageBitGreen + PercentageRed + PercentageHalf) +(PercentageBitRed + PercentageGreen);
        if(balance<50)
        {
            HarvestQuality = "Pesima calidad";
            status = "VeryBadQuality";
        }
        else if(balance>50 && balance<60)
        {
            HarvestQuality = "Mala Calidad";
            status = "BadQuality";
        }
        else if(balance>60 && balance<70)
        {
            HarvestQuality = "Calidad Decente";
            status = "DecentQuality";
        }
        else if(balance>70&& balance <80)
        {
            HarvestQuality = "Buena calidad";
            status = "GoodQuality";
        }
        else if(balance>90)
        {
            HarvestQuality = "Excelente calidad";
            status = "ExcelentQuality";
        }
    }
}
