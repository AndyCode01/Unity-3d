using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MouseMovement : MonoBehaviour
{
   public float mouseSensitivity = 15f;
   public float gamepadSensitivity = 150f;
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

      //control rotation around x axis (Look up and down)
      xRotation -= cameraInput.y;

      //we clamp the rotation so we cant Over-rotate (like in real life)
      xRotation = Mathf.Clamp(xRotation, -70f, 70f);

      //control rotation around y axis (Look up and down)
      YRotation += cameraInput.x;

      //applying both rotations
      transform.localRotation = Quaternion.Euler(xRotation, YRotation, 0f);

    }
}
