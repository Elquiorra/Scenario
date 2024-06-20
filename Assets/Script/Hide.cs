using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hide : MonoBehaviour
{
    public Animator animator;
    void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.CompareTag("Background"))
        {
            animator.SetBool("onScreen", true);
        }
    }

    // void OnTriggerStay2D(Collider2D col){
    //     if (col.gameObject.CompareTag("Background"))
    //     {
    //         animator.SetBool("onScreen", true);
    //     }
    //     else{
    //         animator.SetBool("onScreen", false);
    //     }
    // }

    void OnTriggerExit2D(Collider2D col){
        if(col.gameObject.CompareTag("Background"))
        {
            animator.SetBool("onScreen", false);
        }
    }
}
