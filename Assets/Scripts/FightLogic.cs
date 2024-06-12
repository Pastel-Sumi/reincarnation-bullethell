using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightLogic : MonoBehaviour
{
    [SerializeField] private float life;
    private PlayerControllerScript playerController;
    [SerializeField] private float lostControlTime;
    
    private LifeBar lifeBar;

    private void Start()
    {
        playerController = GetComponent<PlayerControllerScript>();
        lifeBar = FindObjectOfType<LifeBar>();  // Encuentra la barra de vida en la escena

        if (lifeBar != null)
        {
            lifeBar.maxLife = life;  // Configura la vida máxima de la barra de vida
            lifeBar.UpdateLife(life);  // Inicializa la barra de vida con la vida actual
        }


    }

    public void takeDamage(float damage)
    {
        life -= damage;
        if (lifeBar != null)
        {
            lifeBar.UpdateLife(life);
        }

        if (life <= 0)
        {
            // Aquí puedes agregar la lógica para cuando la vida llegue a cero
            Debug.Log("Player is dead");
        }
    }

    public void takeDamage(float damage, Vector2 position)
    {
        life -= damage;
        
        //lostControl
        playerController.Rebound(position);

    }

    private IEnumerator LostControl()
    {
        playerController.canMove = false;
        yield return new WaitForSeconds(lostControlTime);
        playerController.canMove = true;
    }
}
