using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    private float xMovement = 0;
    private float yMovement = 0;

    // Update is called once per frame
    void Update(){
        Vector2 inputVector = new Vector2(0,0);

        if(Input.GetKey(KeyCode.W)){
            inputVector.y = +1;
        }
        if(Input.GetKey(KeyCode.S)){
            inputVector.y = -1;
        }
        if(Input.GetKey(KeyCode.A)){
            inputVector.x = -1;
        }
        if(Input.GetKey(KeyCode.D)){
            inputVector.x = +1;
        }

        inputVector = inputVector.normalized;
        Vector3 moveDir = new Vector3(inputVector.x, inputVector.y, 0f );

        //Send info to animation
        xMovement = inputVector.x;
        yMovement = inputVector.y;

        transform.position += (Vector3)moveDir * Time.deltaTime * moveSpeed; 
    }

    public float GetXMovement(){
       return xMovement;
   }

   public float GetYMovement(){
       return yMovement;
   }
}
