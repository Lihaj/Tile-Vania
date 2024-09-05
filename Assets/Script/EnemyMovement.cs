using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float movespeed=1f;
    Rigidbody2D myRigidbody;
    
        void Start()
    {
        myRigidbody=GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        myRigidbody.velocity=new Vector2(movespeed,0f);
    }

     void OnTriggerExit2D(Collider2D other) {
        movespeed=-movespeed;
        flipEnemy();
    }
    void flipEnemy(){
        transform.localScale= new Vector2(-(Mathf.Sign(myRigidbody.velocity.x)),1f);
    }
}
