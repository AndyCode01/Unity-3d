using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Tools_Equipment : MonoBehaviour
{
    public PlayerInput _playerInput;
    
    public HandItem handItem;

    string Tool_active="Hand";
   
    void Update()
    {
        if(handItem.HandleHandItem(0f)==0)
        {
            SwitchTools();
        }

        if(_playerInput.actions["BackButton"].WasPerformedThisFrame() && handItem.HandleHandItem(0f)!=0)
        {
            handItem.DropAtHandObject();
        }

        UseTool();
    }

    void SwitchTools()
    {
        Vector2 tools = _playerInput.actions["SwitchTools"].ReadValue<Vector2>();
        if(tools.y == 1)
        {
            Tool_active="Mini_shovel";
            Equipping(Tool_active);
        }
        if(tools.x == -1)
        {
            Tool_active="Water_can";
            Equipping(Tool_active);
        }
        if(tools.y == -1)
        {
            Tool_active="Hand";
            Equipping(Tool_active);
        }
    }

    void Equipping(string toolName)
    {
        foreach (Transform child in transform)
        {
            if(child.name == toolName)
            {
                child.gameObject.SetActive(true);
            }
            else
            {
                child.gameObject.SetActive(false);
            }
        }
    }

    
    void UseTool()
    {
        if (_playerInput.actions["UseTool"].WasPressedThisFrame())
        {
            switch (Tool_active)
            {
                case "Hand":
                handItem.IsPointingAtHandItem();
                break;
                

            }
            // if(Tool_active != "")
            // {
            //     Debug.Log("Estas usando la " + Tool_active);
            // }
            // else{
            //     handItem = handItem.IsPointingAtHandObject();
            // }    
        }

    }

    public string GetToolEquipment()
    {
        return Tool_active;
    }

}
