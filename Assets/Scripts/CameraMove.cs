using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    public float moveSpeed = 2.0f; // Velocidad de movimiento de la c�mara

    void Update()
    {
        // Mover la c�mara hacia arriba continuamente
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;
    }
}
