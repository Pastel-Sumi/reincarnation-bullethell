using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeLogic : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerControllerScript player = collision.gameObject.GetComponent<PlayerControllerScript>();
            player.Upgrade(1);
            Destroy(gameObject);
        }
    }
}
