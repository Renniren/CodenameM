using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Controller : MonoBehaviour
{
    public int Index;
    public Vector2 Joystick;
    public InputAction Up;


    void Awake()
    {
        
    }

    public void ManageJoystick(InputAction.CallbackContext context)
    {
        Joystick = context.ReadValue<Vector2>();
        Debug.Log(Joystick.x);
        Debug.Log(Joystick.y);

    }
}