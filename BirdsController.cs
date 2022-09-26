using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class BirdsController : MonoBehaviour
{
    public Vector3 initial_Position;
    public int bird_speed;
    public string Scene_Name;
    public bool isBirdFired;
    public float BirdWaitingTime;

    [HideInInspector]
    public SpriteRenderer spriteRenderer;
    [HideInInspector]
    public Rigidbody2D rigidBody2D;
    
    public void Awake()
    {
        Debug.Log("Bird Awake");
        initial_Position = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        
    }

    public void OnMouseDown()
    {
        spriteRenderer.color = Color.red;
        
    }

    public void OnMouseDrag()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = mousePosition;
    }

    public void OnMouseUp()
    {
        spriteRenderer.color = Color.white;
        rigidBody2D.gravityScale = 1;

        Vector3 force = initial_Position - transform.position;
        Vector3 total_force = bird_speed * force; 
        rigidBody2D.AddForce(total_force);
        isBirdFired = true;
    }

    public void Update(){
        if(transform.position.y > 6 || transform.position.y < -6  ||
            transform.position.x > 13 || transform.position.x < -13
        )
        {
            ResetBird();
        }

        if(isBirdFired && rigidBody2D.velocity.magnitude < 0.3)
        {
            BirdWaitingTime += Time.deltaTime;
        }

        if(BirdWaitingTime > 3)
        {
            ResetBird();
        }
    }

    public void ResetBird()
    {
        transform.position = initial_Position;
        rigidBody2D.velocity = new Vector2(0,0);
        rigidBody2D.angularVelocity = 0;
        rigidBody2D.gravityScale = 0;
        rigidBody2D.transform.rotation = Quaternion.identity;

        BirdWaitingTime = 0;
        isBirdFired = false;
    }
}