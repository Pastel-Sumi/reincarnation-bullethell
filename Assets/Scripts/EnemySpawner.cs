using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject pipe;
    public float spawnRate = 2f;
    public float xOffset = 3f; // Desplazamiento en el eje X
    private float timer = 0f;

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
        {
            spawnPipe();
            timer = 0f;
        }
        transform.position += Vector3.up * 2.0f * Time.deltaTime;
    }

    void spawnPipe()
    {
        // Determinar aleatoriamente si la posici�n ser� a la izquierda o a la derecha
        float xPosition;
        if (Random.value > 0.5f)
        {
            // Generar posici�n a la derecha
            xPosition = transform.position.x + xOffset;
        }
        else
        {
            // Generar posici�n a la izquierda
            xPosition = transform.position.x - xOffset;
        }

        // Mantener la posici�n Y e Z del spawner
        Vector3 spawnPosition = new Vector3(xPosition, transform.position.y, transform.position.z);

        // Instanciar el meteorito en la posici�n generada
        Instantiate(pipe, spawnPosition, transform.rotation);
    }
}
