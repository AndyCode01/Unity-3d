using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.UI;

public class ControllerInput : MonoBehaviour
{
    public PlayerInput _playerInput;
    public HandItem handItem;
    GameObject controlScheme, keyboardScheme, currentScheme;

    void Awake()
    {
        controlScheme = transform.Find("Gamepad").gameObject;
        keyboardScheme = transform.Find("Keyboard").gameObject;
    }

    void Update()
    {
        GetUIbyDevice();
        SetHelps();
        GetHelps();
    }

    void GetUIbyDevice()
    {
        if (_playerInput.currentControlScheme == "Gamepad")
        {
            controlScheme.SetActive(true);
            keyboardScheme.SetActive(false);
            currentScheme= controlScheme;
        }
        else
        {
            controlScheme.SetActive(false);
            keyboardScheme.SetActive(true);
            currentScheme= keyboardScheme;
        }
        ActiveSpriteHandItem();
    }

    void ActiveSpriteHandItem()
    {
        if (handItem.FlagHaveItem())
        {
            currentScheme.transform.Find("HandItem").gameObject.SetActive(true);
        }
        else
        {
            currentScheme.transform.Find("HandItem").gameObject.SetActive(false);
        }
    }

    public void SetSpriteToolSelected(string DpadSelect)
    {
        if (currentScheme != null)
        {
            GameObject toolPanel = currentScheme.transform.Find("ToolsPanel").gameObject;
            foreach (Transform child in toolPanel.transform.Find("Select"))
            {
                if(child.name == DpadSelect)
                {
                    child.gameObject.SetActive(true);
                }
                else
                {
                    child.gameObject.SetActive(false);
                }
            }
        } 
    }

    void SetHelps()
    {
        if(_playerInput.actions["Helps"].WasPressedThisFrame())
        {
            currentScheme.transform.Find("Helps").gameObject.SetActive(currentScheme.transform.Find("Helps").gameObject.activeSelf ? false : true);
        }
    }

    void GetHelps()
    {
        if(_playerInput.currentActionMap.name != "Pause")
        {
            foreach (Transform item in currentScheme.transform.Find("Helps"))
            {
                if(item.name != _playerInput.currentActionMap.name && item.name != "Panel" && item.name != "SetHelp") item.gameObject.SetActive(false);
                else item.gameObject.SetActive(true);
            }
        }
        
    }
}
