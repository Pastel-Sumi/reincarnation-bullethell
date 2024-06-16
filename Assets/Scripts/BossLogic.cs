using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLogic : MonoBehaviour
{
    [SerializeField] private float life = 3f; // Vida del enemigo
    
    //[SerializeField] private float damage = 1f; // Daño que inflige al jugador
    [SerializeField] private float moveSpeed = 2f; // Velocidad de movimiento hacia arriba
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float waitTimeBeforeMoveUpwards = 5f; // Tiempo de espera antes de moverse hacia arriba y disparar
    private bool canMoveUpwards = false;
    private bool movingRight = true; // Variable para controlar la dirección de movimiento
    private float changeDirectionInterval = 2f; // Intervalo para cambiar la dirección de movimiento
    private float currentDirectionChangeTime = 0f; // Tiempo actual hasta el próximo cambio de dirección
    public Transform bulletSpawnPoint;
    public Transform bulletSpawnPoint1;
    public Transform bulletSpawnPoint2;
    public Transform lootPoint;
    public GameObject bulletPrefab; // Prefab de la bala a disparar
    public GameObject laser;
    public GameObject loot;
    private Transform player; // Referencia al jugador
    private SpriteRenderer spriteRenderer;
    public Color damageColor = Color.red;
    public float damageEffectDuration = 0.1f;
    private Color originalColor;
    public Transform laserSpawnPoint;

    [SerializeField] private float lateralDistance = 3f; // Distancia lateral para moverse antes de cambiar de dirección

    private Vector3 initialPosition;
    private Vector3 targetPosition;

    private void Start()
    {
        currentDirectionChangeTime = changeDirectionInterval;
        player = GameObject.FindGameObjectWithTag("Player").transform; // Encuentra al jugador por etiqueta
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;

        initialPosition = transform.position;
        targetPosition = initialPosition + Vector3.right * lateralDistance;
        // Iniciar la corrutina de espera
        StartCoroutine(WaitBeforeMoveUpwards());
    }

    private void Update()
    {
        if (canMoveUpwards)
        {
            MoveUpwards();
        }

        MoveSideways();
        CheckDirectionChange();
    }
    private void MoveUpwards()
    {
        transform.Translate(Vector3.up * 2 * Time.deltaTime);
    }

    private IEnumerator WaitBeforeMoveUpwards()
    {
        yield return new WaitForSeconds(waitTimeBeforeMoveUpwards);
        canMoveUpwards = true;

        // Iniciar el disparo en intervalos regulares
        InvokeRepeating("FireAtPlayer", 0f, 1f / fireRate);
    }

    

    // Método para moverse de manera lateral (izquierda y derecha)
    private void MoveSideways()
    {
        float direction = movingRight ? 1f : -1f;
        transform.Translate(Vector3.right * direction * moveSpeed * Time.deltaTime);
    }

    // Método para disparar hacia la posición del jugador
    private void FireAtPlayer()
    {
        if (bulletPrefab != null && bulletSpawnPoint != null && player != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
            bullet.tag = "EnemyBullet";
            Vector2 direction = (player.position - bullet.transform.position).normalized;
            bullet.GetComponent<Rigidbody2D>().velocity = direction * 15f; //

            GameObject bullet1 = Instantiate(bulletPrefab, bulletSpawnPoint1.position, Quaternion.identity);
            bullet1.tag = "EnemyBullet";
            Vector2 direction1 = (player.position - bullet1.transform.position).normalized;
            bullet1.GetComponent<Rigidbody2D>().velocity = direction1 * 15f;

            GameObject bullet2 = Instantiate(bulletPrefab, bulletSpawnPoint2.position, Quaternion.identity);
            bullet2.tag = "EnemyBullet";
            Vector2 direction2 = (player.position - bullet2.transform.position).normalized;
            bullet2.GetComponent<Rigidbody2D>().velocity = direction2 * 10f;
        }
    }

    // Método para recibir daño
    public void TakeDamage(float damage)
    {
        if (canMoveUpwards)
        {
            life -= damage;

            if (life <= 0)
            {
                Destroy(gameObject);
                Instantiate(loot, lootPoint.position, Quaternion.identity);
            }

            {
                StartCoroutine(DamageEffect());
            }
        }
        
    }

    private IEnumerator DamageEffect()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = damageColor;
            yield return new WaitForSeconds(damageEffectDuration);
            spriteRenderer.color = originalColor;
        }
        else
        {
            Debug.LogError("SpriteRenderer is null");
        }
    }

    private void CheckDirectionChange()
    {
        if (movingRight && transform.position.x >= targetPosition.x)
        {
            movingRight = false;
            targetPosition = initialPosition + Vector3.left * lateralDistance;
        }
        else if (!movingRight && transform.position.x <= targetPosition.x)
        {
            movingRight = true;
            targetPosition = initialPosition + Vector3.right * lateralDistance;
        }
    }
}

