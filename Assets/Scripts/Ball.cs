using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Square") ||
            collision.collider.CompareTag("Triangle"))
            Destroy(gameObject);
        else if (collision.collider.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
        else if (collision.collider.CompareTag("Bonus"))
        {
            EventManager.SendBonusPickedUp();
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}
