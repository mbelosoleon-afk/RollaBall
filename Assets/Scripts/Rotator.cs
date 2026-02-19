using UnityEngine;

public class Rotator : MonoBehaviour
{

 // Update se llama una vez por cada Frame
 void Update()
    {
 // Rota el objeto en el eje X, Y, y Z en cantidades específicadas, ajustados según la tasa de fotogramas.
        transform.Rotate (new Vector3 (15, 30, 45) * Time.deltaTime);
    }
 
}
