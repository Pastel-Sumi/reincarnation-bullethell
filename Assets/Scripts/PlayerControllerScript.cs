using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerControllerScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10;
    public AudioSource clip;

    private Vector2 moveDirection;
    private float screenWidth;

    public bool canMove = true;
    [SerializeField] private Vector2 reboundSpeed;
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            clip.Play();
            Shoot();
        }
    }

    private void FixedUpdate()
    {
        //Physics Calculations
        if (canMove)
        {
            Move();
            TeleportIfNeeded();
        }
        
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
    void Shoot()
    {
        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = bulletSpawnPoint.up * bulletSpeed;
    }

    public void Rebound(Vector2 strikePoint)
    {
        rb.velocity = new Vector2(reboundSpeed.x, -reboundSpeed.y * strikePoint.y);
    }

}
