using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour{

    public event EventHandler OnInteractAction;
    public event EventHandler OnToolSelectedAction;
    private PlayerInputActions playerInputActions;
    private void Awake(){
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.Interact.performed += Interact_performed;
        playerInputActions.Player.SelectTool.performed += Tool_selected;
    }

    private void Interact_performed(InputAction.CallbackContext obj){
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

    private void Tool_selected(InputAction.CallbackContext obj){
        OnToolSelectedAction?.Invoke(this, EventArgs.Empty);
    }

    public Vector2 GetMouseCoordinates(){
        return Mouse.current.position.ReadValue();
    }

    public int GetKeyboardNumberPressed(){
        return (int)playerInputActions.Player.SelectTool.ReadValue<float>();
    }

    
    public Vector2 GetMovementVectorNormalized(){
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;

        return inputVector;
    }
}
