using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeteorSpawnerScript : MonoBehaviour
{
    public GameObject pipe;
    public float spawnRate = 2;
    private float timer = 0;
    public float spawnRadius = 3f;
    void Start()
    {
        spawnPipe();
    }

    // Update is called once per frame
    void Update()
    {   
        if(timer < spawnRate)
        {
            timer = timer + Time.deltaTime;
        }
        else
        {
            spawnPipe();
            timer = 0;
        }
    }
    void spawnPipe()
    {
        // Calcular una posici�n aleatoria dentro del radio especificado
        Vector3 spawnPosition = transform.position + Random.insideUnitSphere * spawnRadius;
        spawnPosition.z = 0; // Asegurarse de que la posici�n Z sea la misma que la del spawner

        // Instanciar el meteorito en la posici�n aleatoria generada
        Instantiate(pipe, spawnPosition, Quaternion.identity);
    }
}
