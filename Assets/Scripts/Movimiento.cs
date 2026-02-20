using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    //Componenteque permite que el objeto tenga gravedad y colisiones físicas
    private Rigidbody rb;
    
    //Cuenta de pnutuación
    private int count;
    
    // movement values
    private float movementX;
    private float movementY;
    //Velocidad y fuerza de salto
    public float speed = 10.0f;
    public float jumpForce = 5.0f;
    //Texto para la cuenta
    public TextMeshProUGUI countText;
    //Detecta si el jugador está tocando el suelopara evitar que salte infinitamente
    private bool isGrounded;
    
    //Texto de victoria
    public GameObject winTextObject;
    /**
    * Start se llama antes del primer frame de actualización
    * solo una vez en el juego
    */ 
    void Start()
    {
        count = 0; 
        // Obtener el componente RigidBody
        rb = GetComponent <Rigidbody>();
        // Mensaje de depuración
        Debug.Log("Hello, I'am a message in Start");
        SetCountText();
        winTextObject.SetActive(false);
    }
    /**
    * Update se llama una vez por frame
    * se llama en cada frame
    */
    void Update()
    {
        // mensaje de depuración, puedes verlo en la consola
        // Advertencia: este mensaje se llama en cada frame
        // Debug.Log("Hola, soy un mensaje en Update");
    }   

    /**
    * Método para mover el player, captura esos valore en Vector2 (eje X,Y)
     * y los guarda en las variables movementX y movementY
    */
    void OnMove (InputValue movementValue)
    {
        // tomar valores del InputSystem
        Vector2 movementVector = movementValue.Get<Vector2>();
        // actualizar los valores de fuerza/movimiento
        movementX = movementVector.x; 
        movementY = movementVector.y; 
        
    }
    
    void SetCountText() 
    {
        countText.text =  "Count: " + count.ToString();
        // Comprueba cuantos puntos tienes
        if (count >= 1)
        {
            // Display the win text.
            winTextObject.SetActive(true);
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        }
    }

    /**
     * Salta con la barra espaciadora o el click del ratón
     */
    void OnFire(){
        // Mensaje de depuración
        Debug.Log("Hello!, I'm OnFire");

        // Confirma si estás tocando el suelo para no saltar infinitamente
        if (isGrounded) // SOLO si está en el suelo
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false; // Al saltar, ya no está en el suelo
            Debug.Log("Saltando...");
        }
    }
    
    /**
    * FixedUpdate se llama una vez por frame de física
    * se llama en cada frame
    * Diferencia entre Update y FixedUpdate:
    * https://learn.unity.com/tutorial/update-and-fixedupdate
    */
    private void FixedUpdate() 
    {
        // saltar con la barra espaciadora
        // este método es diferente al del InputSystem
        // Es el Input original de Unity, más simple
        if (Input.GetKeyDown(KeyCode.Space)) {
            OnFire();
        }

        // crear un vector con los valores de movimiento
        // No es realmente un movimiento de traslación, es una fuerza
        // El efecto es como en un juego de billar
        // El jugador es la bola y la fuerza es el taco
        // El jugador se mueve en la dirección de la fuerza
        // El jugador se detiene cuando la fuerza es cero
        // El jugador se mueve más rápido cuando la fuerza es mayor
        // La velocidad (speed) es el valor de la fuerza
        Vector3 movement = new Vector3 (speed*movementX, 0.0f , speed*movementY);

        // depurar valores de fuerza
        // Advertencia: este mensaje se llama en cada frame
        // Debug.Log("X: " + movementX + " Y: " + movementY + " Z: 0");
        
        // aplicar la fuerza al jugador
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

    //Recolección de objetos
 void OnTriggerEnter(Collider other) 
    {
        // Comprobar si el objeto con el que colisionó el jugador tiene la etiqueta "PickUp".
 if (other.gameObject.CompareTag("PickUp")) 
        {
 // Desactivar el objeto colisionado (haciéndolo desaparecer).
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Destroy the current object
            Destroy(gameObject); 
            // Update the winText to display "You Lose!"
            winTextObject.gameObject.SetActive(true);
            winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
        }
    }

}
