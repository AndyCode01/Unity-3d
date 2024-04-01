using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleMaterials : MonoBehaviour
{
    public enum LandStatus
    {
        Soil,Watered,Farmland
    }
    public LandStatus landStatus;
    public Material Dirt, WetDirt, Grass;
    new Renderer renderer;
    void Start()
    {
        renderer = GetComponent<Renderer>();
        SwitchLandStatus(LandStatus.Soil);
    }
    
    public void SwitchLandStatus(LandStatus statusToSwitch)
    {
        landStatus = statusToSwitch;
        Material materialToSwitch = Dirt;
        switch (statusToSwitch)
        {
            case LandStatus.Soil:
                materialToSwitch = Dirt;
            break;
            case LandStatus.Watered:
                materialToSwitch = WetDirt;
            break;
            case LandStatus.Farmland:
                materialToSwitch = Grass;
            break;
        }

        renderer.material = materialToSwitch;
    }
}
