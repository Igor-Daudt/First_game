using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour{

    private const string HORIZONTAL = "horizontal";
    private const string VERTICAL = "vertical";
    private Animator animator;
    [SerializeField] private Player player;

    private void Awake(){
        animator = GetComponent<Animator>();
    }

    private void Update(){
        animator.SetFloat(HORIZONTAL, player.GetXMovement());
        animator.SetFloat(VERTICAL, player.GetYMovement());
    }
}