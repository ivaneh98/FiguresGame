using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    [SerializeField]
    private float speed = 1f;
    private void FixedUpdate()
    {
        Move();
    }
    private void Move()
    {
        transform.position = new Vector3(transform.position.x+ speed*Time.fixedDeltaTime, transform.position.y, transform.position.z);
    }
}
