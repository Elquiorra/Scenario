using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAnim : MonoBehaviour
{
    Animator animator;

    void Awake(){
        Cursor.visible = false;
        animator = GetComponent<Animator>();
    }

    void OnTriggerEnter2D(Collider2D col){
        animator.SetBool("isNear", false);
    }

    void OnTriggerStay2D(Collider2D col){
        animator.SetBool("isNear", false);
    }

    void OnTriggerExit2D(Collider2D col){
        animator.SetBool("isNear", true);
    }
}
