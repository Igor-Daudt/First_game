using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private GameInput gameInput;
    private float xMovement = 0;
    private float yMovement = 0;
    private Vector3 lastInteractDir;
    
    // Update is called once per frame
    void Update(){
        HandleMovement();
        //HandleInteractions();
    }

    private void HandleInteractions(){
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, inputVector.y);

        if(moveDir != Vector3.zero){
            lastInteractDir = moveDir;
        }

        float interactionDistance = 2f;
        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, lastInteractDir, interactionDistance);
        if(raycastHit.collider != null){
            if(raycastHit.transform.TryGetComponent(out BoxSeed boxSeed)){
                boxSeed.Interact();
            }
            else{
                Debug.Log(raycastHit.collider);
            }
        }
    }

    private void HandleMovement(){
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, inputVector.y, 0f);

        float moveDistance = moveSpeed * Time.deltaTime;
        Vector3 currentDirection = (moveDir).normalized;
        RaycastHit2D raycastHit =  Physics2D.BoxCast(transform.position, new Vector2(0.502807f, 0.9423415f), 0f, currentDirection, distance: moveDistance);
        
        if(raycastHit.collider != null){
            //Attemp only X movement
            Vector3 moveDirectionX = new Vector3(moveDir.x, 0, 0);
            raycastHit =  Physics2D.BoxCast(transform.position, new Vector2(0.502807f, 0.9423415f), 0f, moveDirectionX, distance: moveDistance);
            
            if(raycastHit.collider == null){
                //Can only move on x axis
                moveDir = moveDirectionX;
            }
            else{
                //Cannot move only on x

                //Attempt only y movement
                Vector3 moveDirectionY = new Vector3(0, moveDir.y, 0);
                raycastHit =  Physics2D.BoxCast(transform.position, new Vector2(0.502807f, 0.9423415f), 0f, moveDirectionY, distance: moveDistance);

                if(raycastHit.collider == null){
                    //Can only move on y axis
                    moveDir = moveDirectionY;
                }
                else{
                    //Cannot move anywhere
                }
            }
        }

//public static RaycastHit2D BoxCast(Vector2 origin, Vector2 size, float angle, Vector2 direction, float distance = Mathf.Infinity, int layerMask = Physics2D.AllLayers, float minDepth = -Mathf.Infinity, float maxDepth = Mathf.Infinity);
        
        //Send info to animation
        xMovement = inputVector.x;
        yMovement = inputVector.y;

        if(raycastHit.collider == null){
            transform.position += moveDir * moveDistance;
        }
        lastInteractDir = moveDir;
    }

    public float GetXMovement(){
       return xMovement;
   }

   public float GetYMovement(){
       return yMovement;
   }
}