using UnityEngine;
using UnityEngine.InputSystem;

public class Tools_Equipment : MonoBehaviour
{
    public PlayerInput _playerInput;
    public HandleCursor handleCursor;

    RaycastHit handItem = new RaycastHit();
    string Tool_active="";
   
    void Update()
    {
        if(handItem.collider == null)
        {
            SwitchTools();
            UseTool();
        }

        if(_playerInput.actions["BackButton"].WasPerformedThisFrame() && handItem.collider != null)
        {
            handleCursor.DropAtHandObject(handItem);
            handItem=new RaycastHit();
        }
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
            Tool_active="";
            Equipping(Tool_active);
        }
    }

    void UseTool()
    {
        if (_playerInput.actions["UseTool"].WasPressedThisFrame())
        {
            if(Tool_active != "")
            {
                Debug.Log("Estas usando la " + Tool_active);
            }
            else{
                handItem = handleCursor.IsPointingAtHandObject();
            }    
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

    public string GetToolActive()
    {
        return Tool_active;
    }

}
