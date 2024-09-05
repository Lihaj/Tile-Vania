using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovment : MonoBehaviour
{

    [SerializeField] float RunSpeed=10f;
    [SerializeField] float jumpSpeed=5f;
    [SerializeField] float climbSpeed=3f;

    [SerializeField] Vector2 dethKick=new Vector2(0f,15f);
    [SerializeField]GameObject bullet;
    [SerializeField]Transform gun;
    Vector2 MoveInput;
    Rigidbody2D myRigidbody;

    Animator myanimator;
    CapsuleCollider2D myBodyCollider;

    BoxCollider2D myFeetCollider;
    float StartingGravity;
    bool isAlive=true;
    void Start()
    {
        myRigidbody=GetComponent<Rigidbody2D>();
        myanimator=GetComponent<Animator>();
        myBodyCollider=GetComponent<CapsuleCollider2D>();
        myFeetCollider=GetComponent<BoxCollider2D>();
        StartingGravity=myRigidbody.gravityScale;
    }

    void Update()
    {
        if(!isAlive){return;}
        Run();
        flipSprite();
       ClimbLadder();
       die();
    }

    void OnMove(InputValue value){
        if(!isAlive){return;}
        MoveInput=value.Get<Vector2>();

    }
    void OnJump(InputValue value){
        if(!isAlive){return;}
        if(myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground"))){
          if(value.isPressed){
            myRigidbody.velocity +=new Vector2(0f,jumpSpeed);
            }
        }
       
    }

    void OnFire(InputValue value){
         if(!isAlive){return;}
         Instantiate(bullet,gun.position,transform.rotation);
         

    }

    void Run(){
        Vector2 MoveVelocity= new Vector2(MoveInput.x*RunSpeed,myRigidbody.velocity.y);
        myRigidbody.velocity=MoveVelocity;
       
        bool playerHasHorizontalSpeed=Mathf.Abs(myRigidbody.velocity.x)>Mathf.Epsilon; //epsilon is ~ to 0
        myanimator.SetBool("IsRunning",playerHasHorizontalSpeed);
    }

    void flipSprite(){
        bool playerHasHorizontalSpeed=Mathf.Abs(myRigidbody.velocity.x)>Mathf.Epsilon; //epsilon is ~ to 0

        if(playerHasHorizontalSpeed==true){
             transform.localScale= new Vector2(Mathf.Sign(myRigidbody.velocity.x),1f);
        }
       
    }

    void ClimbLadder(){
       
        if(myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing"))){
            myRigidbody.gravityScale=0f;

            bool playerHasVerticalSpeed=Mathf.Abs(myRigidbody.velocity.y)>Mathf.Epsilon; //epsilon is ~ to 0
            myanimator.SetBool("IsClimbing",playerHasVerticalSpeed);

            Vector2 Climblveocity= new Vector2(myRigidbody.velocity.x,MoveInput.y*climbSpeed);
            myRigidbody.velocity=Climblveocity;
        }else{
            myRigidbody.gravityScale=StartingGravity;
            myanimator.SetBool("IsClimbing",false);
        }

    }
    void die(){
        if(myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemies","Hazard"))){
            isAlive=false;
            myanimator.SetTrigger("dying");
            myRigidbody.velocity=dethKick;
            FindAnyObjectByType<GameSession>().ProcessPlayerDeath();
        }
    }


}
