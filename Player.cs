using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{


    public float Speed;
    private Rigidbody2D rig;
    private Animator anim;
    public float JumpForce;
    private bool isJumping;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Jump();        
    }

    void Move() {
        
        bool canMove = true;       
        if(canMove) {
            float movement = Input.GetAxis("Horizontal");            
            rig.velocity = new Vector2(movement * Speed, rig.velocity.y);
            if (movement > 0f) {
                transform.eulerAngles = new Vector3(0f, 0f, 0f);
                anim.SetBool("walk", true);
            }
            if (movement < 0f) {               
               transform.eulerAngles = new Vector3(0f, 180f, 0f);
               anim.SetBool("walk", true);
            }
            if (movement == 0f) {
                anim.SetBool("walk", false);
            }
            if(GameObject.Find("Player").transform.position.y < -10) {                
                GameController.instance.LostLive();
            }            
        }

    }

    void Jump() {

        bool isAlive = true;
        if(isAlive) {
            if(Input.GetButtonDown("Jump") && !isJumping) {            
                anim.SetBool("jump", true);
                rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
            }
        }
        
    }

    void OnCollisionEnter2D(Collision2D collision) {
        
        if(collision.gameObject.layer == 8) {
            isJumping = false;
            anim.SetBool("jump", false);
        }        

    }

    void OnCollisionExit2D(Collision2D collision) {
        if(collision.gameObject.layer == 8) {
            isJumping = true;
        }        
    }   

}