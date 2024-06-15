using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class PlayerControllerScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Transform bulletSpawnPoint;
    public Transform bulletSpawnPoint1;
    public Transform bulletSpawnPoint2;
    public Transform bulletSpawnPoint3;
    public Transform bulletSpawnPoint4;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10;
    public int upgrade = 0;
    public AudioSource clip;
    public AudioSource clipDestroy;
    public SpriteRenderer spriteRenderer;
    public Sprite nave1;
    public Sprite nave2;
    public Sprite nave3;

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
        if (Input.GetButtonDown("Fire1"))
        {
            clip.Play();
            Shoot();
        }
        if (Input.GetButtonDown("Cancel"))
        {
            SceneManager.LoadScene("Main_Menu");
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

    public void Upgrade(int num)
    {
        if (upgrade < 4)
        {
            upgrade += num;
            if(upgrade == 1)
            {
                spriteRenderer.sprite = nave1;
            }
            else if(upgrade == 2)
            {
                spriteRenderer.sprite = nave2;
            }
            else if(upgrade == 3)
            {
                spriteRenderer.sprite = nave3;
            }
            
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

        // Actualizar la posición de la nave si ha cruzado los lúŠites
        if (hasTeleported)
        {
            rb.position = position;
        }
    }
    void Shoot()
    {
        if(upgrade == 0)
        {
            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody2D>().velocity = bulletSpawnPoint.up * bulletSpeed;
        }
        else if(upgrade == 1)
        {
            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint1.position, bulletSpawnPoint1.rotation);
            bullet.GetComponent<Rigidbody2D>().velocity = bulletSpawnPoint1.up * bulletSpeed;

            var bullet2 = Instantiate(bulletPrefab, bulletSpawnPoint2.position, bulletSpawnPoint2.rotation);
            bullet2.GetComponent<Rigidbody2D>().velocity = bulletSpawnPoint2.up * bulletSpeed;
        }
        else if(upgrade == 2)
        {
            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            bullet.GetComponent<Rigidbody2D>().velocity = bulletSpawnPoint.up * bulletSpeed;

            var bullet1 = Instantiate(bulletPrefab, bulletSpawnPoint3.position, bulletSpawnPoint3.rotation);
            bullet1.GetComponent<Rigidbody2D>().velocity = bulletSpawnPoint3.up * bulletSpeed;

            var bullet2 = Instantiate(bulletPrefab, bulletSpawnPoint4.position, bulletSpawnPoint4.rotation);
            bullet2.GetComponent<Rigidbody2D>().velocity = bulletSpawnPoint4.up * bulletSpeed;
        }
        else
        {
            var bullet = Instantiate(bulletPrefab, bulletSpawnPoint1.position, bulletSpawnPoint1.rotation);
            bullet.GetComponent<Rigidbody2D>().velocity = bulletSpawnPoint1.up * bulletSpeed;

            var bullet2 = Instantiate(bulletPrefab, bulletSpawnPoint2.position, bulletSpawnPoint2.rotation);
            bullet2.GetComponent<Rigidbody2D>().velocity = bulletSpawnPoint2.up * bulletSpeed;

            var bullet3 = Instantiate(bulletPrefab, bulletSpawnPoint3.position, bulletSpawnPoint3.rotation);
            bullet3.GetComponent<Rigidbody2D>().velocity = bulletSpawnPoint3.up * bulletSpeed;

            var bullet4 = Instantiate(bulletPrefab, bulletSpawnPoint4.position, bulletSpawnPoint4.rotation);
            bullet4.GetComponent<Rigidbody2D>().velocity = bulletSpawnPoint4.up * bulletSpeed;
        }
        
    }

    public void Rebound(Vector2 strikePoint)
    {
        rb.velocity = new Vector2(reboundSpeed.x, -reboundSpeed.y * strikePoint.y);
    }

}
