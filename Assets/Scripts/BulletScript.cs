using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed = 1f;
    public float bulletLife = 1f; //How long does the bullet keeps being alive.
    public float rotation = 0f;
    // Comentario de ejemplo

    private Vector2 spawnPoint;
    private float timer = 0f;
    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = new Vector2(transform.position.x, transform.position.y);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= bulletLife)
        {
            Destroy(this.gameObject);
            return;
        }
            
        
        transform.position = Movement(timer);
    }

    private Vector2 Movement(float timer)
    {
        Vector2 movement = transform.up * speed * timer;
        //float x = timer * speed * transform.right.x;
        //float y = timer * speed * transform.right.y;
        //return new Vector2(x + spawnPoint.x, y + spawnPoint.y);
        return spawnPoint + movement;
    }
}
