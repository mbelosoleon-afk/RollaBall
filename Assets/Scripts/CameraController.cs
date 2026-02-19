using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
 // Referencia al objeto de juego (GameObject) del jugador.
 public GameObject player;

 // La distancia (desfase) entre la cámara y el jugador.
 private Vector3 offset;

 // Start se llama antes de la actualización del primer frame.
 void Start()
    {
 // Calcula la distancia inicial entre la posición de la cámara y la posición del jugador.
        offset = transform.position - player.transform.position; 
    }

 // LateUpdate se llama una vez por frame después de que todas las funciones Update se hayan completado.
 void LateUpdate()
    {
 // Mantiene la misma distancia entre la cámara y el jugador durante todo el juego.
        transform.position = player.transform.position + offset;  
    }
}
