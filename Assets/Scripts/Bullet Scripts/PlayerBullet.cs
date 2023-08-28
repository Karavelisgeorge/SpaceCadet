using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 6f;

    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector3 temp = transform.position;
        temp.x += speed * Time.deltaTime;
        transform.position = temp;
    }   
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Boundary" || other.tag == "Spaceship" || other.tag == "Player Bullet" || other.tag == "Power Up AS" || other.tag == "Power Up MB" || other.tag == "Power Up SH")
        {
            return;
        }

            gameObject.SetActive(false);


    }
}
