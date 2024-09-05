using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed=15f;
   Rigidbody2D myRigidbody;

   GameObject Player;
   float xSpeed;
    void Start()
    {
        myRigidbody=GetComponent<Rigidbody2D>();
        Player=GameObject.FindWithTag("Player");
        xSpeed=Player.transform.localScale.x*bulletSpeed;
    }

  
    void Update()
    {
        gameObject.transform.localScale=new Vector2(Player.transform.localScale.x,Player.transform.localScale.y);
        myRigidbody.velocity=new Vector2(xSpeed,0f);
    }

 void OnTriggerEnter2D(Collider2D other) {
        if(other.tag=="Enemies"){
            Destroy(other.gameObject);
        }
        Destroy(gameObject);
    }
void  OnCollisionEnter2D(Collision2D other) {
       Destroy(gameObject);
}
}
