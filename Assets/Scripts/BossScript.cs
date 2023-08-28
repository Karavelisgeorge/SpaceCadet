using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public float speed;
    public float stoppingDistance;
    public float retreatDistance;
    public int Health;

    private Transform player;
    public GameObject projectile;

    private float timeBtwShots;
    public float startTimeBtwShots;

    public GameObject explosion;

    public GameObject floatingPoints;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Spaceship").transform;
        timeBtwShots = startTimeBtwShots;

    }

    
    void Update()
    {
        if(Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

        } else if (Vector2.Distance(transform.position, player.position) < stoppingDistance && Vector2.Distance(transform.position, player.position) > retreatDistance)
        {
            transform.position = this.transform.position;
        } else if (Vector2.Distance(transform.position, player.position) < retreatDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * Time.deltaTime);
        }

        if (timeBtwShots <= 0)
        {
            Instantiate(projectile, transform.position, Quaternion.identity);
            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Boundary" || other.tag == "Enemy" || other.tag == "Power Up AS" || other.tag == "Power Up MB" || other.tag == "Power Up SH")
        {
            return;
        }
        
        Health -= 1;       

        if (Health <= 0)
        {
            ScoreScript.scoreValue += 200;
            Invoke("Destroy", 0.1f);              
        }
    }

    void Destroy()
    {
        GameObject points = Instantiate(floatingPoints, transform.position, Quaternion.identity) as GameObject;
        points.transform.GetChild(0).GetComponent<TextMesh>().text = "200";

        GameObject explo = Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(explo, 0.4f);
        Destroy(gameObject);
    }
}
