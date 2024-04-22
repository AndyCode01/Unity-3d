using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.UI;

public class ControllerInput : MonoBehaviour
{
    public PlayerInput _playerInput;
    public HandItem handItem;
    GameObject controlScheme, keyboardScheme, currentScheme;

    void Start()
    {
        controlScheme = transform.Find("Gamepad").gameObject;
        keyboardScheme = transform.Find("Keyboard").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        GetUIbyDevice();
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
        if (handItem.GetItemInHand())
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
}
