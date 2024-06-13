using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLogic : MonoBehaviour
{
    [SerializeField] private float life = 3f; // Vida del enemigo
    [SerializeField] private float damage = 1f; // Da�o que inflige al jugador
    [SerializeField] private float moveSpeed = 2f; // Velocidad de movimiento hacia arriba
    [SerializeField] private float fireRate = 1f;
    private bool movingRight = true; // Variable para controlar la direcci�n de movimiento
    private float changeDirectionInterval = 2f; // Intervalo para cambiar la direcci�n de movimiento
    private float currentDirectionChangeTime = 0f; // Tiempo actual hasta el pr�ximo cambio de direcci�n
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab; // Prefab de la bala a disparar
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
        // Iniciar el disparo en intervalos regulares
        InvokeRepeating("FireAtPlayer", 0f, 1f / fireRate);
    }

    private void Update()
    {
        MoveUpwards();
        MoveSideways();
        UpdateDirectionChangeTimer();
    }

    // Moverse hacia arriba a una velocidad constante
    private void MoveUpwards()
    {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
    }
    // M�todo para moverse de manera lateral (izquierda y derecha)
    private void MoveSideways()
    {
        float direction = movingRight ? 1f : -1f;
        transform.Translate(Vector3.right * direction * moveSpeed * Time.deltaTime);
    }
    // M�todo para disparar hacia la posici�n del jugador
    private void FireAtPlayer()
    {
        if (bulletPrefab != null && bulletSpawnPoint != null && player != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
            Vector2 direction = (player.position - bullet.transform.position).normalized;
            bullet.GetComponent<Rigidbody2D>().velocity = direction * 10f; //
        }
        
    }

    // M�todo para recibir da�o
    public void TakeDamage(float damage)
    {
        life -= damage;

        if (life <= 0)
        {
            
            Debug.Log("Enemy is dead and has been destroyed");
            spriteRenderer.sprite = naveDestruidaSprite;
            Destroy(gameObject, 0.5f);
        }
        else
        {
            StartCoroutine(DamageEffect());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Infligir da�o al jugador
            collision.GetComponent<FightLogic>().takeDamage(damage);

            //Destruir al enemigo despu�s de atacar al jugador
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

    // M�todo para intentar esquivar las balas
    private void UpdateDirectionChangeTimer()
    {
        currentDirectionChangeTime -= Time.deltaTime;
        if (currentDirectionChangeTime <= 0f)
        {
            // Cambiar la direcci�n lateral aleatoriamente
            movingRight = !movingRight;

            // Reiniciar el temporizador de cambio de direcci�n
            currentDirectionChangeTime = changeDirectionInterval;
        }
    }
}