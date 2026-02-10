using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // player's component rigidbody
    private Rigidbody rb;
    // movement values
    private float movementX;
    private float movementY;
    // this is the speed of the player
    // you can change it in the Unity Editor
    public float speed = 10.0f;
    /**
    * Start is called before the first frame update
    * only once in the game
    */ 
    void Start()
    {
        // get the rigidbody component
        rb = GetComponent <Rigidbody>();
        // debug message, you can see it in the console
        Debug.Log("Hello, I'am a message in Start");
    }
    /**
    * Update is called once per frame
    * it's called every frame
    */
    void Update()
    {
        // debug message, you can see it in the console
        // Warning: this message is called every frame
        // Debug.Log("Hello, I'am a message in Update");
    }   

    /**
    * OnMove is called when the player moves
    */
    void OnMove (InputValue movementValue)
    {
        // take values of the InputSystem
        Vector2 movementVector = movementValue.Get<Vector2>();
        // update the force/movement values 
        movementX = movementVector.x; 
        movementY = movementVector.y; 
        
    }

    /**
     * Jump with space bar
     */
    void OnFire(){
        // debug message, you can see it in the console
        Debug.Log("Hello!, I'm OnFire");

        // apply a vertical force to the player
        rb.AddForce(Vector3.up * 5.0f, ForceMode.Impulse); 
    }
    
    /**
    * FixedUpdate is called once per frame
    * it's called every frame
    * Difference between Update and FixedUpdate
    * https://learn.unity.com/tutorial/update-and-fixedupdate
    */
    private void FixedUpdate() 
    {
        // jump with space bar
        // this method is different from the InputSystem
        // It's original Unity Input, more simple
        if (Input.GetKeyDown(KeyCode.Space)) {
            OnFire();
        }

        // create a vector with the movement values
        // It's not really a movement, it's a force
        // The effect is like a pool game
        // The player is the ball and the force is the stick
        // The player moves in the direction of the force
        // The player stops when the force is zero
        // The player moves faster when the force is higher
        // The speed is the force value
        Vector3 movement = new Vector3 (speed*movementX, 0.0f , speed*movementY);

        // debug force values
        // Warning: this message is called every frame
        // Debug.Log("X: " + movementX + " Y: " + movementY + " Z: 0");
        
        // apply the force to the player
        rb.AddForce(movement); 
    }

 void OnTriggerEnter(Collider other) 
    {
 // Check if the object the player collided with has the "PickUp" tag.
 if (other.gameObject.CompareTag("PickUp")) 
        {
 // Deactivate the collided object (making it disappear).
            other.gameObject.SetActive(false);
        }
    }


}
