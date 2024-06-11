using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;

    private Vector2 moveDirection;
    private float screenWidth;
    // Start is called before the first frame update
    void Start()
    {
        // Calcular el ancho de la pantalla en unidades del mundo
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float cameraHeight = Camera.main.orthographicSize * 2;
        screenWidth = cameraHeight * screenAspect;
    }

    // Update is called once per frame
    void Update()
    {
        //Processing Inputs
        ProcessInputs();
    }

    private void FixedUpdate()
    {
        //Physics Calculations
        Move();
        TeleportIfNeeded();
    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;

    }

    void Move()
    {
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }

    void TeleportIfNeeded()
    {
        // Obtener la posición actual de la nave
        Vector2 position = rb.position;
        bool hasTeleported = false;

        // Chequear si la nave ha cruzado el borde izquierdo
        if (position.x < -screenWidth / 2)
        {
            position.x = screenWidth / 2;
            hasTeleported = true;
        }
        // Chequear si la nave ha cruzado el borde derecho
        else if (position.x > screenWidth / 2)
        {
            position.x = -screenWidth / 2;
            hasTeleported = true;
        }

        // Actualizar la posición de la nave si ha cruzado los límites
        if (hasTeleported)
        {
            rb.position = position;
        }
    }
}
