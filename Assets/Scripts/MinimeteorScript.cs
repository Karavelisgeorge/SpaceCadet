using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimeteorScript : MonoBehaviour
{
    public float speed;

    private Transform player;
    private Vector2 target;
    public GameObject explosion;
    public AudioClip explosionSound;

    void Start()
    {
        

        player = GameObject.FindGameObjectWithTag("Spaceship").transform;

        target = new Vector2(player.position.x, player.position.y);
    }

   
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if(transform.position.x == target.x && transform.position.y == target.y)
        {
            ;
            Invoke("DestroyProjectile", 0.1f);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Spaceship")|| other.CompareTag("Player Bullet"))
        {
            
            Invoke("DestroyProjectile",0.1f);
        }
    }

     void DestroyProjectile()
    {
          GameObject expl = Instantiate(explosion, transform.position, Quaternion.identity);

          AudioSource.PlayClipAtPoint(explosionSound, transform.position);
          Destroy(expl, 0.4f);
          Destroy(gameObject);
    }

}
