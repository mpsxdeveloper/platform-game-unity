using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour
{

    public Rigidbody2D rb;
    public float speed;
    public bool moveUp;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector2(rb.velocity.x, speed);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.layer == 8) {
            moveUp = true;               
        }
        if(collision.gameObject.tag == "VerticalCollider_Tag") {
            moveUp = false;
        }
        speed *= -1;
    }

}