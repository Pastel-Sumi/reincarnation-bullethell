using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeLogic : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            FightLogic player = collision.gameObject.GetComponent<FightLogic>();
            player.Heal(20);
            Destroy(gameObject);
        }
    }
}
