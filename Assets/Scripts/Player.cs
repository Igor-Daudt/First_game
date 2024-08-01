using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    private float xMovement = 0;
    private float yMovement = 0;
    private Rigidbody2D rigidBody;

    private void Start(){
        rigidBody = GetComponent<Rigidbody2D>();
    }
    
    // Update is called once per frame
    void Update(){
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector2 moveDir = new Vector2(inputVector.x * moveSpeed, inputVector.y * moveSpeed);
        
        //Send info to animation
        xMovement = inputVector.x;
        yMovement = inputVector.y;

        rigidBody.velocity = moveDir;
    }

    public float GetXMovement(){
       return xMovement;
   }

   public float GetYMovement(){
       return yMovement;
   }
}