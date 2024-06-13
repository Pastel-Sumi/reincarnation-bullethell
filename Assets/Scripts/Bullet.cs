using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bullet : MonoBehaviour
{
    public float life = 3f;
    private Camera mainCamera;
    private float screenHeight;
   
    void Awake()
    {
        
        // Destruir el objeto despu�s de "life" segundos
        Destroy(gameObject, life);

        // Referencia a la c�mara principal
        mainCamera = Camera.main;

        // Calcular la altura de la pantalla en unidades del mundo
        float cameraHeight = mainCamera.orthographicSize * 2;
        screenHeight = cameraHeight;
    }

    void Update()
    {
        // Teletransportar la bala si cruza los l�mites superiores o inferiores
        TeleportIfNeeded();
    }

    void TeleportIfNeeded()
    {
        // Obtener la posici�n actual de la bala
        Vector2 position = transform.position;

        // Obtener la posici�n Y de la c�mara
        float cameraY = mainCamera.transform.position.y;

        // Chequear si la bala ha cruzado el borde superior
        if (position.y > cameraY + screenHeight / 2)
        {
            position.y = cameraY - screenHeight / 2;
            transform.position = position;
        }
        // Chequear si la bala ha cruzado el borde inferior
        else if (position.y < cameraY - screenHeight / 2)
        {
            position.y = cameraY + screenHeight / 2;
            transform.position = position;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Verificar si el objeto colisionado tiene el componente EnemyLogic
        EnemyLogic enemy = collision.gameObject.GetComponent<EnemyLogic>();
        if (enemy != null)
        {
            enemy.TakeDamage(20);
        }

        // Verificar si el objeto colisionado tiene el componente FightLogic (jugador)
        FightLogic player = collision.gameObject.GetComponent<FightLogic>();
        if (player != null)
        {
            player.takeDamage(20, collision.GetContact(0).normal);
        }

        // Destruir la bala despu�s de colisionar
        Destroy(gameObject);
    }
}
