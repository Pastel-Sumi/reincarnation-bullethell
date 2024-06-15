using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.LowLevel;


public class Bullet : MonoBehaviour
{
    public float life = 3f;
    private Camera mainCamera;
    private float screenHeight;
    public FightLogic playerFightLogic; 
    

    private void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerFightLogic = player.GetComponent<FightLogic>();
    }


    void Awake()
    {
        
        // Destruir el objeto después de "life" segundos
        Destroy(gameObject, life);

        // Referencia a la cámara principal
        mainCamera = Camera.main;

        // Calcular la altura de la pantalla en unidades del mundo
        float cameraHeight = mainCamera.orthographicSize * 2;
        screenHeight = cameraHeight;
    }

    void Update()
    {
        // Teletransportar la bala si cruza los límites superiores o inferiores
        TeleportIfNeeded();
    }

    void TeleportIfNeeded()
    {
        // Obtener la posición actual de la bala
        Vector2 position = transform.position;

        // Obtener la posición Y de la cámara
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
       
        FightLogic player = collision.gameObject.GetComponent<FightLogic>();
        EnemyLogic enemy = collision.gameObject.GetComponent<EnemyLogic>();
        
        if (enemy != null)
        {
            enemy.TakeDamage(playerFightLogic.bulletDamage);
        }

        
        if (player != null)
        {
            player.takeDamage(20, collision.GetContact(0).normal);
        }

        // Destruir la bala después de colisionar
        Destroy(gameObject);
    }
}
