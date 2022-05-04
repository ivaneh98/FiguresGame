using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pocket : MonoBehaviour
{
    public string objTag;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(objTag))
        {
            EventManager.SendEnemyKilled();
            Destroy(collision.gameObject);
        }
        else if(!collision.CompareTag("Player"))
        {
            EventManager.SendDamage();
            Destroy(collision.gameObject);
        }
        else
        {
            Destroy(collision.gameObject);

        }

    }
}
