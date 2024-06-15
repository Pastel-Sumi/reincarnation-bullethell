using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserLogic : MonoBehaviour
{
    // Start is called before the first frame update
    public FightLogic playerFightLogic;
    void Start()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        playerFightLogic = player.GetComponent<FightLogic>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {

        FightLogic player = collision.gameObject.GetComponent<FightLogic>();
        EnemyLogic enemy = collision.gameObject.GetComponent<EnemyLogic>();
        BossLogic boss = collision.gameObject.GetComponent<BossLogic>();

        if (enemy != null)
        {
            enemy.TakeDamage(playerFightLogic.bulletDamage);
        }
        if (boss != null)
        {
            boss.TakeDamage(playerFightLogic.bulletDamage);
        }

        if (player != null)
        {
            player.takeDamage(20, collision.GetContact(0).normal);
        }

        // Destruir la bala despues de colisionar
        Destroy(gameObject);
    }
}
