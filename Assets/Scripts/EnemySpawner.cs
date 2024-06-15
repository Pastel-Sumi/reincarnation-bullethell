using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject pipe;
    public GameObject Boss;
    public float spawnRate = 2f;
    public float xOffset = 3f; // Desplazamiento en el eje X
    public int spawnMax = 10;
    private int enemySpawned = 0;
    private float timer = 0f;
    bool bossFlag = false;

    void Start()
    {
        spawnPipe();
    }

    void Update()
    {
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {   if(enemySpawned < spawnMax)
            {
                spawnPipe();
                timer = 0f;
                enemySpawned += 1;
            }
            else
            {
                if (!bossFlag && GameObject.FindGameObjectWithTag("Enemy") == null)
                {
                    spawnBoss();
                    bossFlag = true;
                }
            }
            
        }
        transform.position += Vector3.up * 2.0f * Time.deltaTime;
    }

    void spawnPipe()
    {
        // Determinar aleatoriamente si la posición será a la izquierda o a la derecha
        float xPosition;
        if (Random.value > 0.5f)
        {
            // Generar posición a la derecha
            xPosition = transform.position.x + xOffset;
        }
        else
        {
            // Generar posición a la izquierda
            xPosition = transform.position.x - xOffset;
        }

        // Mantener la posición Y e Z del spawner
        Vector3 spawnPosition = new Vector3(xPosition, transform.position.y, transform.position.z);

        // Instanciar el meteorito en la posición generada
        Instantiate(pipe, spawnPosition, transform.rotation);
    }
    void spawnBoss()
    {
        Instantiate(Boss, transform.position, Quaternion.identity);
    }
}
