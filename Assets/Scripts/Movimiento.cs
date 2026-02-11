using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // player's component rigidbody
    private Rigidbody rb;
    
    private int count;
    
    // movement values
    private float movementX;
    private float movementY;
    // this is the speed of the player
    // you can change it in the Unity Editor
    public float speed = 10.0f;
    public float jumpForce = 5.0f;
    public TextMeshProUGUI countText;
    private bool isGrounded;
    /**
    * Start is called before the first frame update
    * only once in the game
    */ 
    void Start()
    {
        count = 0; 
        // get the rigidbody component
        rb = GetComponent <Rigidbody>();
        // debug message, you can see it in the console
        Debug.Log("Hello, I'am a message in Start");
        SetCountText();
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
    
    void SetCountText() 
    {
        countText.text =  "Count: " + count.ToString();
    }

    /**
     * Jump with space bar
     */
    void OnFire(){
        // debug message, you can see it in the console
        Debug.Log("Hello!, I'm OnFire");

        // apply a vertical force to the player
        if (isGrounded) // SOLO si está en el suelo
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false; // Al saltar, ya no está en el suelo
            Debug.Log("Saltando...");
        }
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
    private void OnCollisionStay(Collision collision)
    {
        // Revisamos todos los puntos de contacto de la colisión
        foreach (ContactPoint contact in collision.contacts)
        {
            // El valor contact.normal.y nos dice hacia dónde apunta la cara
            // Si el valor es cercano a 1, es una superficie plana (suelo)
            // Si es cercano a 0, es una pared vertical
            if (contact.normal.y > 0.6f) 
            {
                isGrounded = true;
                return; // Salimos del bucle si ya encontramos suelo
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // Cuando dejamos de tocar el suelo
        isGrounded = false;
    }

 void OnTriggerEnter(Collider other) 
    {
 // Check if the object the player collided with has the "PickUp" tag.
 if (other.gameObject.CompareTag("PickUp")) 
        {
 // Deactivate the collided object (making it disappear).
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
    }


}
