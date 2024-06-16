using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FightLogic : MonoBehaviour
{
    [SerializeField] private float life;
    private PlayerControllerScript playerController;
    [SerializeField] private float lostControlTime;
    public float bulletDamage;
    // Variables para la barra de vida
    public Image lifeBar;
    public float maxLife;
    // Variables para el efecto de da�o
    private SpriteRenderer spriteRenderer;
    public Color damageColor = Color.red;
    public float damageEffectDuration = 10f;
    private Color originalColor;
    public Sprite naveDestruidaSprite;
    public GameObject gameOverScreen;
    public Text gameOverText;

    public Camera mainCamera;
    public float damageInterval = 1f; // Intervalo de tiempo entre cada da�o
    private float lastDamageTime;



    private void Start()
    {
        playerController = GetComponent<PlayerControllerScript>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Inicializar la vida y la barra de vida
        maxLife = life;
        if (lifeBar != null)
        {
            UpdateLifeBar(life);
        }
        originalColor = spriteRenderer.color;
        gameOverScreen.SetActive(false);
    }
    private void Update()
    {
        CheckPlayerOutOfBounds();
    }
    public void takeDamage(float damage)
    {
        life -= damage;
        if (lifeBar != null)
        {
            UpdateLifeBar(life);
        }

        if (life <= 0)
        {


            spriteRenderer.sprite = naveDestruidaSprite;
            spriteRenderer.sprite = null;
            StartCoroutine(GameOverSequence());
            //Destroy(gameObject, 0.5f);
            

        }
        else
        {
            // Iniciar el efecto de da�o
            StartCoroutine(DamageEffect());
        }
        
    }
    private void CheckPlayerOutOfBounds()
    {
        
        Vector3 playerPosition = transform.position;

        // Calcular los l�mites visibles de la c�mara
        Vector3 screenBottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        

        // Verificar si el jugador est� fuera de los l�mites en la direcci�n Y (abajo de la c�mara)
        if (playerPosition.y < screenBottomLeft.y)
        {
            // Aplicar da�o al jugador si ha pasado el intervalo de da�o
            if (Time.time - lastDamageTime > damageInterval)
            {
                takeDamage(20); // Ajusta la cantidad de da�o seg�n sea necesario
                lastDamageTime = Time.time;
            }
        }
    }
    public void Heal(float heal)
    {
        if ((life + heal) > maxLife)
        {
            life = maxLife;
        }
        else
        {
            life += heal;
        }
        UpdateLifeBar(life);
    }

    public void takeDamage(float damage, Vector2 position)
    {
        takeDamage(damage);
        StartCoroutine(LostControl());
        playerController.Rebound(position);
    }

    private IEnumerator LostControl()
    {
        playerController.canMove = false;
        yield return new WaitForSeconds(lostControlTime);
        playerController.canMove = true;
    }

    // M�todo para actualizar la barra de vida
    private void UpdateLifeBar(float currentLife)
    {
        lifeBar.fillAmount = currentLife / maxLife;
    }
    private IEnumerator DamageEffect()
    {
        spriteRenderer.color = damageColor;
        yield return new WaitForSeconds(damageEffectDuration);
        spriteRenderer.color = originalColor;
        
    }
    private IEnumerator GameOverSequence()
    {
        gameOverScreen.SetActive(true);
        gameOverText.text = "Game Over";


        yield return new WaitForSeconds(1f);

        
    }
}
