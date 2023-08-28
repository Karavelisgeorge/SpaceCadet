using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpScript : MonoBehaviour
{
    public float speed = 6f;
    

    void Start()
    {
        
        Destroy(gameObject, 4f);
    }


    void Update()
    {
        Move();
    }

    void Move()
    {
        Vector3 temp = transform.position;
        temp.x -= speed * Time.deltaTime;
        transform.position = temp;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "Spaceship")
        {
            return;
        }
        
        gameObject.SetActive(false);
    }


}