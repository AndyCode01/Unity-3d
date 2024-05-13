using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using DialogueEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class ConversationStart : MonoBehaviour
{
    public PlayerInput _playerInput;
    bool flag=false;
    string conversation=null;

    public void InstanceConversation(NPCConversation nPCConversation)
    { 
        if(_playerInput.actions["UseTool"].WasPerformedThisFrame()) ConversationManager.Instance.StartConversation(nPCConversation);
        if (ConversationManager.Instance.IsConversationActive)
        {
            flag=true;
            conversation= nPCConversation.name;
            _playerInput.SwitchCurrentActionMap("Dialog");
            if(_playerInput.actions["NextOption"].WasPerformedThisFrame())ConversationManager.Instance.SelectNextOption();
            if(_playerInput.actions["PreviousOption"].WasPerformedThisFrame())ConversationManager.Instance.SelectPreviousOption();
            if(_playerInput.actions["Select"].WasPerformedThisFrame())ConversationManager.Instance.PressSelectedOption(); 
        }
        else
        {
            if(flag)
            {
                flag=false;
                // if(conversation)
                UnityEngine.Debug.Log(conversation);
                _playerInput.SwitchCurrentActionMap("Player");
            }
        } 
    }
}
