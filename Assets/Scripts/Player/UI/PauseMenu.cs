using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HandlerMenu : MonoBehaviour
{
    public PlayerInput _playerInput;
    public GameObject UI_PauseMenu;

    void Update()
    {
        if(_playerInput.actions["Pause"].WasPressedThisFrame())
        {
            UI_PauseMenu.SetActive(!UI_PauseMenu.activeSelf);
        }
    }
}
