using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    [SerializeField] private float life = 3f; // Vida del enemigo
    [SerializeField] private float damage = 1f; // Daño que inflige al jugador
    [SerializeField] private float moveSpeed = 2f; // Velocidad de movimiento hacia arriba
    [SerializeField] private float fireRate = 1f;
    [SerializeField] private float waitTimeBeforeMoveUpwards = 3f; // Tiempo de espera antes de moverse hacia arriba y disparar
    private bool canMoveUpwards = false;
    private bool movingRight = true; // Variable para controlar la dirección de movimiento
    private float changeDirectionInterval = 2f; // Intervalo para cambiar la dirección de movimiento
    private float currentDirectionChangeTime = 0f; // Tiempo actual hasta el próximo cambio de dirección
    public Transform bulletSpawnPoint;
    public Transform lootPoint;
    public GameObject bulletPrefab; // Prefab de la bala a disparar
    public GameObject loot;
    public GameObject heal;
    private Transform player; // Referencia al jugador
    private SpriteRenderer spriteRenderer;
    public Color damageColor = Color.red;
    public float damageEffectDuration = 0.5f;
    private Color originalColor;
    public Sprite naveDestruidaSprite;

    private void Start()
    {
        currentDirectionChangeTime = changeDirectionInterval;
        player = GameObject.FindGameObjectWithTag("Player").transform; // Encuentra al jugador por etiqueta
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;

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
        UpdateDirectionChangeTimer();
    }

    private IEnumerator WaitBeforeMoveUpwards()
    {
        yield return new WaitForSeconds(waitTimeBeforeMoveUpwards);
        canMoveUpwards = true;

        // Iniciar el disparo en intervalos regulares
        InvokeRepeating("FireAtPlayer", 0f, 1f / fireRate);
    }

    // Moverse hacia arriba a una velocidad constante
    private void MoveUpwards()
    {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
    }

    // Método para moverse de manera lateral (izquierda y derecha)
    private void MoveSideways()
    {
        float direction = movingRight ? 2f : -2f;
        transform.Translate(Vector3.right * direction * moveSpeed * Time.deltaTime);
    }

    // Método para disparar hacia la posición del jugador
    private void FireAtPlayer()
    {
        if (bulletPrefab != null && bulletSpawnPoint != null && player != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
            Vector2 direction = (player.position - bullet.transform.position).normalized;
            bullet.GetComponent<Rigidbody2D>().velocity = direction * 10f; //
        }
    }

    // Método para recibir daño
    public void TakeDamage(float damage)
    {
        life -= damage;

        if (life <= 0)
        {
            Debug.Log("Enemy is dead and has been destroyed");
            spriteRenderer.sprite = naveDestruidaSprite;
            Destroy(gameObject, 0.5f);
            float probability = 0.3f; // 30% de probabilidad
            if (Random.value < probability)
            {
                int randomLoot = Random.Range(0, 2);
                switch(randomLoot)
                {
                    case 0:
                        Instantiate(loot, lootPoint.position, Quaternion.identity);
                        break;
                    case 1:
                        Instantiate(heal, lootPoint.position, Quaternion.identity);
                        break;
                }
            }
        }
        
        {
            StartCoroutine(DamageEffect());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Infligir daño al jugador
            collision.GetComponent<FightLogic>().takeDamage(damage);

            //Destruir al enemigo después de atacar al jugador
            Destroy(gameObject);
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

    private void UpdateDirectionChangeTimer()
    {
        currentDirectionChangeTime -= Time.deltaTime;
        if (currentDirectionChangeTime <= 0f)
        {
            // Cambiar la dirección lateral aleatoriamente
            movingRight = !movingRight;

            // Reiniciar el temporizador de cambio de dirección
            currentDirectionChangeTime = changeDirectionInterval;
        }
    }
}
