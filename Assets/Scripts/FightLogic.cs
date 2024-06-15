using System.Collections;
using System.Collections.Generic;
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
    // Variables para el efecto de daÒo
    private SpriteRenderer spriteRenderer;
    public Color damageColor = Color.red;
    public float damageEffectDuration = 10f;
    private Color originalColor;
    public Sprite naveDestruidaSprite;
    public GameObject gameOverScreen;
    public Text gameOverText;
    


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

    public void takeDamage(float damage)
    {
        life -= damage;
        if (lifeBar != null)
        {
            UpdateLifeBar(life);
        }

        if (life <= 0)
        {
            
            Debug.Log("Player is dead");
            spriteRenderer.sprite = naveDestruidaSprite;
            StartCoroutine(GameOverSequence());
            //Destroy(gameObject, 0.5f);
            

        }
        else
        {
            // Iniciar el efecto de daÒo
            StartCoroutine(DamageEffect());
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

    // MÈtodo para actualizar la barra de vida
    private void UpdateLifeBar(float currentLife)
    {
        lifeBar.fillAmount = currentLife / maxLife;
    }
    private IEnumerator DamageEffect()
    {
        spriteRenderer.color = damageColor;
        yield return new WaitForSeconds(damageEffectDuration);
        spriteRenderer.color = originalColor;
        Debug.Log("cambiÅEel color");
    }
    private IEnumerator GameOverSequence()
    {
        gameOverScreen.SetActive(true);
        gameOverText.text = "Game Over";

        Debug.Log("Game Over text set");

        yield return new WaitForSeconds(1f);

        gameOverText.text = "Game Over\n...";
        Debug.Log("... added to text");

        yield return new WaitForSeconds(1f);

        gameOverText.text = "Game Over\n...\nor not";
        Debug.Log("or not added to text");
        
    }
}
