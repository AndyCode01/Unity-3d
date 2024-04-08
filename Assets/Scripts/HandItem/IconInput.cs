using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Newtonsoft.Json;

public class IconInput : MonoBehaviour
{
    public Tools_Equipment tools_Equipment;
    public GameObject player;
    GameObject icon;
    public PlayerInput _playerInput;
    bool ItemInHand=false;
    Sprite spriteIcon;
    Renderer  iconRenderer;
    enum InputDevices {XboxGamepad, KeyboardMouse, PsGamepad};
    enum Actions {Path, UseTool, Pause, Sprint, Jump, BackButton};
    Dictionary<InputDevices, Dictionary<Actions, string>> spriteByAction = new Dictionary<InputDevices, Dictionary<Actions,string>>();


    void Start()
    {
       icon = transform.Find("Icon").gameObject;
       iconRenderer= icon.GetComponent<Renderer>();
       ReadJsonOnSpriteByAction();
    }
    void Update()
    {
        Vector3 distance = icon.transform.position - player.transform.position;
        
        icon.transform.LookAt(icon.transform.position + Camera.main.transform.rotation * Vector3.forward,Camera.main.transform.rotation * Vector3.up);
        if (distance.magnitude <= 1.5 && !ItemInHand)
        {
            icon.SetActive(true);
        }
        else{
            icon.SetActive(false);
        }
        string pathSprite;
        if (transform.CompareTag("HandItem"))
        {
            if (_playerInput.currentControlScheme == "Gamepad")
            {
                pathSprite = spriteByAction[InputDevices.XboxGamepad][Actions.Path] + "\\" + spriteByAction[InputDevices.XboxGamepad][Actions.UseTool];
            }
            else
            {
                pathSprite = spriteByAction[InputDevices.KeyboardMouse][Actions.Path] + "\\" + spriteByAction[InputDevices.KeyboardMouse][Actions.UseTool];
            }
            spriteIcon = Resources.Load<Sprite>(pathSprite);
            Texture2D texture = SpriteToTexture2D(spriteIcon);
            iconRenderer.material.mainTexture = texture;

            if(tools_Equipment.GetToolActive() != "") icon.SetActive(false);
            
        }
    }

    void ReadJsonOnSpriteByAction()
    {
        // Carga el archivo JSON como un TextAsset
        TextAsset jsonTextFile = Resources.Load<TextAsset>("Json\\PathSpriteInputs");
        string jsonString = jsonTextFile.text;

        // Deserializa el JSON a tu diccionario
        spriteByAction = JsonConvert.DeserializeObject<Dictionary<InputDevices, Dictionary<Actions, string>>>(jsonString);

    }

    public void SwitchItemInHand()
    {
        ItemInHand = !ItemInHand;
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
