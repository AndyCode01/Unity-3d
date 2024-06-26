using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MouseMovement : MonoBehaviour
{
   public float mouseSensitivity = 15f;
   public float gamepadSensitivity = 150f;
   public HandItem handItem;
   public float defaultLockUpLimit=70f;
   public float defaultLockDownLimit=70f;
   private PlayerInput _playerInput;
   float xRotation = 0f;
   float YRotation = 0f;
 
    void Start()
    {
      //Locking the cursor to the middle of the screen and making it invisible
      Cursor.lockState = CursorLockMode.Locked;
      _playerInput = GetComponent<PlayerInput>();
    }
 
    void Update()
    {
      float sensitivity;
      if (_playerInput.currentControlScheme == "Gamepad")
      {
         sensitivity = gamepadSensitivity;
      }
      else
      {
         sensitivity = mouseSensitivity;
      }   
      Vector2 cameraInput = _playerInput.actions["Look"].ReadValue<Vector2>() * sensitivity * Time.deltaTime;
      float lockDownLimit=0, lockUpLimit=0;
      if(handItem.FlagHaveItem())
      {
         lockDownLimit = lockUpLimit=40f;
      }
      else{
          lockUpLimit = lockDownLimit= defaultLockDownLimit;
      }
      //control rotation around x axis (Look up and down)
      xRotation -= cameraInput.y;

      //we clamp the rotation so we cant Over-rotate (like in real life)
      xRotation = Mathf.Clamp(xRotation, (lockUpLimit * -1), lockDownLimit);

      //control rotation around y axis (Look up and down)
      YRotation += cameraInput.x;

      //applying both rotations
      transform.localRotation = Quaternion.Euler(xRotation, YRotation, 0f);

    }
}
