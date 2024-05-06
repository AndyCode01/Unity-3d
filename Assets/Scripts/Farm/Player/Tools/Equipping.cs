using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tools_Equipment : MonoBehaviour
{
    public PlayerInput _playerInput;
    public HandItem handItem;
    public HandleMaterials handleMaterials;
    public ControllerInput controllerInput;
    enum Tools {Mini_shovel, Water_can, Hand};
    enum DpadSelect {SelectUp, SelectDown, SelectLeft,SelectRight};
    Dictionary<Tools,DpadSelect> DpadSelectTools = new Dictionary<Tools,DpadSelect>();
    Tools toolSelected;
    Tools previousTool;

    
    void Awake()
    {
        DpadSelectTools.Add(Tools.Mini_shovel, DpadSelect.SelectUp);
        DpadSelectTools.Add(Tools.Water_can, DpadSelect.SelectLeft);
        DpadSelectTools.Add(Tools.Hand, DpadSelect.SelectDown);
    }

    void Update()
    {
        if(handItem.HandleHandItem(0f)==0)
        {
            SwitchTools();
        }

        if(_playerInput.actions["BackButton"].WasPerformedThisFrame())
        {
            handItem.DropAtHandObject();
        }

        UseTool();
    }

    void SwitchTools()
    {
        Vector2 tools = _playerInput.actions["SwitchTools"].ReadValue<Vector2>();
        if(tools.y == 1) toolSelected = Tools.Mini_shovel;
        if(tools.x == -1) toolSelected = Tools.Water_can;
        if(tools.y == -1) toolSelected = Tools.Hand;
        if(previousTool!=toolSelected)Equipping(toolSelected);
    }

    void Equipping(Tools tool)
    {
        previousTool = tool;
        foreach (Transform child in transform)
        {
            if(child.name == tool.ToString())
            {
                child.gameObject.SetActive(true);
            }
            else
            {
                child.gameObject.SetActive(false);
            }
        }
        controllerInput.SetSpriteToolSelected(DpadSelectTools[tool].ToString());
    }


    public void StringToEnumTool(string tool)
    {
        toolSelected=Tools.Hand;
        switch (tool)
        {
           case "Mini_shovel":
           toolSelected = Tools.Mini_shovel;
           break;
           case "Water_can":
           toolSelected = Tools.Water_can;
           break;
        }
        Equipping(toolSelected);
    }

    
    void UseTool()
    {
        if (_playerInput.actions["UseTool"].WasPressedThisFrame()  )
        {
            switch (toolSelected)
            {
                case Tools.Hand:
                handItem.SetHandItem();
                handleMaterials.Plant();
                break;
                case Tools.Mini_shovel:
                handleMaterials.DrillHole();
                break;
                case Tools.Water_can:
                handleMaterials.WaterPlant();
                break;

            }  
        }

    }


    public string GetToolEquipment()
    {
        return toolSelected.ToString();
    }

}
