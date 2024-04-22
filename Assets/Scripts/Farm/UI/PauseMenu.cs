using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class HandlerMenu : MonoBehaviour
{
    public PlayerInput _playerInput;
    public GameObject UI_PauseMenu;
    private bool isGamePaused = false;

    void Update()
    {
        if(_playerInput.actions["Pause"].WasPressedThisFrame())
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        isGamePaused = !isGamePaused;
        Time.timeScale = isGamePaused ? 0 : 1;
        UI_PauseMenu.SetActive(isGamePaused);
    }
}
