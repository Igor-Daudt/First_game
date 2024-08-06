using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Player : MonoBehaviour
{
    private const bool COLLIDER_FOUND = true;
    [SerializeField] private float moveSpeed = 1f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private TilemapController tilemapController;
    [SerializeField] private MarkerController marker;
    private TilemapController tilemap;	
    private float xMovement = 0;
    private float yMovement = 0;
    private Vector3 lastInteractDir;
    
    private void Start(){
        gameInput.OnInteractAction += GameInput_OnInteractAction;
        tilemap = FindObjectOfType<TilemapController>();
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e){
         
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, inputVector.y);

        if(moveDir != Vector3.zero){
            lastInteractDir = moveDir;
        }

        float interactionDistance = 2f;
        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, lastInteractDir, interactionDistance);
        if(raycastHit.collider != null){
            if(raycastHit.transform.TryGetComponent(out BoxSeed boxSeed)){
                marker.markedCellPosition = tilemap.GetGridPosition(gameInput.GetMouseCoordinates(), TilemapController.SCREEN_POSITION);
                //Debug.Log(tilemap.GetTileBase(tilemap.GetGridPosition(gameInput.GetMouseCoordinates(), TilemapController.SCREEN_POSITION) ));
            }
        }
    }

    // Update is called once per frame
    void Update(){
        HandleMovement();
        HandleInteractions();
        marker.markedCellPosition = tilemap.GetGridPosition(gameInput.GetMouseCoordinates(), TilemapController.SCREEN_POSITION);
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
            }
        }
    }

    private void HandleMovement(){
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, inputVector.y, 0f);

        float moveDistance = moveSpeed * Time.deltaTime;
        Vector3 currentDirection = (moveDir).normalized;
        RaycastHit2D raycastHit =  Physics2D.BoxCast(transform.position, new Vector2(0.502807f, 0.9423415f), 0f, currentDirection, distance: moveDistance);
        
        if(raycastHit.collider == COLLIDER_FOUND){
            //Attemp only X movement
            Vector3 moveDirectionX = new Vector3(moveDir.x, 0, 0);
            raycastHit =  Physics2D.BoxCast(transform.position, new Vector2(0.502807f, 0.9423415f), 0f, moveDirectionX, distance: moveDistance);
            
            if(raycastHit.collider != COLLIDER_FOUND){
                //Can only move on x axis
                moveDir = moveDirectionX;
            }
            else{
                //Cannot move only on x

                //Attempt only y movement
                Vector3 moveDirectionY = new Vector3(0, moveDir.y, 0);
                raycastHit =  Physics2D.BoxCast(transform.position, new Vector2(0.502807f, 0.9423415f), 0f, moveDirectionY, distance: moveDistance);

                if(raycastHit.collider != COLLIDER_FOUND){
                    //Can only move on y axis
                    moveDir = moveDirectionY;
                }
                else{
                    //Cannot move anywhere
                }
            }
        }
        
        //Send info to animation
        xMovement = inputVector.x;
        yMovement = inputVector.y;

        if(raycastHit.collider != COLLIDER_FOUND){
            transform.position += moveDir * moveDistance;
        }
    }

    public float GetXMovement(){
       return xMovement;
   }

   public float GetYMovement(){
       return yMovement;
   }
}