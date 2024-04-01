using UnityEngine.InputSystem;
using UnityEngine;
using UnityEngine.UI;

public class ControllerInput : MonoBehaviour
{
    public GameObject toolsPanel;
    public GameObject HandItem;
    public PlayerInput _playerInput;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        Image UI_handItem_gamepad = HandItem.transform.Find("Gamepad").GetComponent<Image>();
        Image UI_handItem_keyboard =HandItem.transform.Find("Keyboard").GetComponent<Image>();
        Image UI_tools_gamepad =toolsPanel.transform.Find("Gamepad").GetComponent<Image>();
        Image UI_tools_keyboard =toolsPanel.transform.Find("Keyboard").GetComponent<Image>();

        if (_playerInput.currentControlScheme == "Gamepad")
        {
            UI_tools_gamepad.canvasRenderer.SetAlpha(1.0f);
            UI_tools_keyboard.canvasRenderer.SetAlpha(0.0f);
            UI_handItem_gamepad.canvasRenderer.SetAlpha(1.0f);
            UI_handItem_keyboard.canvasRenderer.SetAlpha(0.0f);
        }
        else
        {
            UI_tools_gamepad.canvasRenderer.SetAlpha(0.0f);
            UI_tools_keyboard.canvasRenderer.SetAlpha(1.0f);
            UI_handItem_gamepad.canvasRenderer.SetAlpha(0.0f);
            UI_handItem_keyboard.canvasRenderer.SetAlpha(1.0f);
        } 
    }
}
