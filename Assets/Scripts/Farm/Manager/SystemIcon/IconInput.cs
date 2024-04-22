using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Newtonsoft.Json;

public class IconInput : MonoBehaviour
{
    public Tools_Equipment tools_Equipment;
    public GameObject player;
    GameObject icon;
    PlayerInput _playerInput;
    HandleCursor handleCursor;
    HandleMaterials handleMaterials;
    ForEach forEach;
    enum InputDevices {XboxGamepad, KeyboardMouse, PsGamepad};
    enum Actions {Path, UseTool, Pause, Sprint, Jump, BackButton};
    Dictionary<InputDevices, Dictionary<Actions, string>> spriteByAction = new Dictionary<InputDevices, Dictionary<Actions,string>>();


    void Start()
    {
        handleMaterials = GetComponent<HandleMaterials>();
        handleCursor = GetComponent<HandleCursor>();
       _playerInput = player.GetComponent<PlayerInput>();
       forEach = GetComponent<ForEach>();
       ReadJsonOnSpriteByAction();
    }
    void Update()
    {
        if(handleCursor.GetObjectAtPoint() == "HandItem" || handleCursor.GetObjectAtPoint() == "Land")
        {
            icon = GetIcon(handleCursor.GetInteractiveObject());
            if(icon.name == "PrincipalIcon") SwitchIconInput();
            SpriteAtForwardPlayer();
        }      
    }

    void SwitchIconInput()
    {
        string pathSprite = GetPathSprite();
        Sprite spriteIcon = Resources.Load<Sprite>(pathSprite);
        Texture2D texture = SpriteToTexture2D(spriteIcon);
        icon.GetComponent<Renderer>().material.mainTexture = texture;  
    }

    void SpriteAtForwardPlayer()
    {
        // float offset = 0.1f;
        // Vector3 positionIcon = new Vector3(icon.transform.position.x, handleCursor.GetHitPointY()+offset, icon.transform.position.z - offset);
        icon.transform.LookAt(icon.transform.position + Camera.main.transform.rotation * Vector3.forward, Camera.main.transform.rotation * Vector3.up);
        // icon.transform.position = positionIcon;
    }

    string GetPathSprite()
    {
        if (_playerInput.currentControlScheme == "Gamepad")
            {
                return spriteByAction[InputDevices.XboxGamepad][Actions.Path] + "\\" + spriteByAction[InputDevices.XboxGamepad][Actions.UseTool];
            }
            else
            {
                return spriteByAction[InputDevices.KeyboardMouse][Actions.Path] + "\\" + spriteByAction[InputDevices.KeyboardMouse][Actions.UseTool];
            }      
    }

    public GameObject GetIcon(GameObject interactiveObject)
    {
        GameObject GroupIcon = interactiveObject.transform.Find("Icon").gameObject;
        if (handleCursor.GetObjectAtPoint() == "HandItem")
        {
            if(tools_Equipment.GetToolEquipment() != "Hand")
            {
                forEach.SetActivationByGroup(GroupIcon,"Hand");
                return interactiveObject.transform.Find("Icon").transform.Find("Hand").gameObject;
            }
        }
        if (handleCursor.GetObjectAtPoint() == "Land")
        {
            string toolLand = handleMaterials.StatusLand();
            if(tools_Equipment.GetToolEquipment() != toolLand)
            {
                forEach.SetActivationByGroup(GroupIcon,toolLand);
                return interactiveObject.transform.Find("Icon").transform.Find(toolLand).gameObject;
            }
        }
        forEach.SetActivationByGroup(GroupIcon,"PrincipalIcon");
        return interactiveObject.transform.Find("Icon").gameObject.transform.Find("PrincipalIcon").gameObject;
    }
 
    void ReadJsonOnSpriteByAction()
    {
        // Carga el archivo JSON como un TextAsset
        TextAsset jsonTextFile = Resources.Load<TextAsset>("Json\\PathSpriteInputs");
        string jsonString = jsonTextFile.text;
        // Deserializa el JSON a tu diccionario
        spriteByAction = JsonConvert.DeserializeObject<Dictionary<InputDevices, Dictionary<Actions, string>>>(jsonString);

    }

    Texture2D SpriteToTexture2D(Sprite sprite)
    {
        if(sprite.rect.width != sprite.texture.width)
        {
            Texture2D newText = new Texture2D((int)sprite.rect.width, (int)sprite.rect.height);
            Color[] newColors = sprite.texture.GetPixels((int)sprite.textureRect.x, 
                (int)sprite.textureRect.y, 
                (int)sprite.textureRect.width, 
                (int)sprite.textureRect.height );
            newText.SetPixels(newColors);
            newText.Apply();
            return newText;
        }
        else
            return sprite.texture;
    }
}
