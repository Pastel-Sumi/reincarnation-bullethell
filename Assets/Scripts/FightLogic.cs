using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FightLogic : MonoBehaviour
{
    [SerializeField] private float life;
    private PlayerControllerScript playerController;
    [SerializeField] private float lostControlTime;
    // Variables para la barra de vida
    public Image lifeBar;
    public float maxLife;
    // Variables para el efecto de daño
    private SpriteRenderer spriteRenderer;
    public Color damageColor = Color.red;
    public float damageEffectDuration = 10f;
    private Color originalColor;
    public Sprite naveDestruidaSprite;


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
            Destroy(gameObject, 0.5f);

        }
        else
        {
            // Iniciar el efecto de daño
            StartCoroutine(DamageEffect());
        }
        
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

    // Método para actualizar la barra de vida
    private void UpdateLifeBar(float currentLife)
    {
        lifeBar.fillAmount = currentLife / maxLife;
    }
    private IEnumerator DamageEffect()
    {
        spriteRenderer.color = damageColor;
        yield return new WaitForSeconds(damageEffectDuration);
        spriteRenderer.color = originalColor;
        Debug.Log("cambió el color");
    }
}
