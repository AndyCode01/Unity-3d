using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HandleMaterials : MonoBehaviour
{
    HandleCursor handleCursor;
    HandItem handItem;
    ForEach forEach;
    public Tools_Equipment tools_Equipment;
    public int WaitForGrownUpPlant = 5;
    enum LandStatus
    {
        Default,Soil,Watered,GrownUp,Farmland
    }
    LandStatus landStatus;
    public Material Dirt, WetDirt, Grass;

    void Start()
    {
        forEach = GetComponent<ForEach>();
        handleCursor = GetComponent<HandleCursor>();
        handItem = GetComponent<HandItem>();
    }

    public string StatusLand()
    {
        landStatus = SetLandStatus();
        switch (landStatus)
        {
            case LandStatus.Soil:
                return "Pot";
            case LandStatus.Watered:
                return "Water_can";
            case LandStatus.GrownUp:
                return "Clock";
            case LandStatus.Farmland:
                return "Basket";
        }
        return "Mini_shovel";
    }

    LandStatus SetLandStatus()
    {
        GameObject Land = handleCursor.GetInteractiveObject();
        landStatus = SearchStatus(Land.transform.Find("Phases").gameObject);
        Land.GetComponent<Land>().data.LandStatus = landStatus.ToString();
        return landStatus;
    }

    LandStatus SearchStatus(GameObject Land)
    {
        foreach (Transform child in Land.transform)
        {
            if(child.gameObject.activeSelf)
            {
                switch (child.name)
                {
                    case "LandPhase2":
                    return LandStatus.Soil;
                    case "CoffeePhase1":
                    return LandStatus.Watered;
                    case "CoffeePhase2":
                    return LandStatus.GrownUp;
                    case "CoffeePhase3":
                    return LandStatus.Farmland;
                }
            } 
        }
        return LandStatus.Default;
    }

    public void DrillHole()
    {
        if (handleCursor.GetInteractiveObject() != null)
        {
            if(handleCursor.GetInteractiveObject().transform.tag == "Land")
            {
                GameObject Land = handleCursor.GetInteractiveObject().transform.Find("Phases").gameObject;
                if(Land.transform.Find("LandPhase1").gameObject.activeSelf)forEach.SetActivationByGroup(Land,"LandPhase2");
            }
        }
    } 

    public void Plant()
    {
        if (handleCursor.GetInteractiveObject() != null)
        {
            if(handleCursor.GetInteractiveObject().transform.tag == "Land" && handleCursor.GetInteractiveObject().GetComponent<Land>().data.LandStatus == "Soil" && handItem.FlagHaveItem())
            {
                tools_Equipment.StringToEnumTool("Hand");
                handItem.DropAtHandObject(true);
                GameObject Land = handleCursor.GetInteractiveObject().transform.Find("Phases").gameObject;
                if(Land.transform.Find("LandPhase2").gameObject.activeSelf)forEach.SetActivationByGroup(Land,"CoffeePhase1");
            }
        }
    }

    public void WaterPlant()
    {
        if (handleCursor.GetInteractiveObject() != null)
        {
            if(handleCursor.GetInteractiveObject().transform.tag == "Land")
            {
                GameObject Land = handleCursor.GetInteractiveObject().transform.Find("Phases").gameObject;
                if(Land.transform.Find("CoffeePhase1").gameObject.activeSelf)forEach.SetActivationByGroup(Land,"CoffeePhase2");
                StartCoroutine(GrownUpPlant(Land));
            }
        }    
    }

    IEnumerator  GrownUpPlant(GameObject Land)
    {
        yield return new WaitForSeconds(WaitForGrownUpPlant);
        if(Land.transform.Find("CoffeePhase2").gameObject.activeSelf)forEach.SetActivationByGroup(Land,"CoffeePhase3");
    }

    public void Harvest()
    {
        if (handleCursor.GetInteractiveObject() != null)
        {
            if(handleCursor.GetInteractiveObject().transform.tag == "Land" && handleCursor.GetInteractiveObject().GetComponent<Land>().data.LandStatus == "Farmland" && handItem.GetItemInHand().GetComponent<Rigidbody>().mass <10)
            {
                GameObject Phases = handleCursor.GetInteractiveObject().transform.Find("Phases").gameObject;
                forEach.SetActivationByGroup(Phases,"CoffeePhase2");
                handleCursor.GetInteractiveObject().GetComponent<Land>().data.LandStatus= "GrownUp";
                handleCursor.GetInteractiveObject().GetComponent<Land>().data.UseInHarvest= true;
                GameObject.Find("Data").GetComponent<SaveLoadSystem>().SaveGame();
                SceneManager.LoadScene("Harvest");
            }  
        }
    }
}
