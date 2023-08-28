using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPatrol : MonoBehaviour
{
    public float speed;
    private float waitTime;
    public float startWaitTime;
    public int Health;

    public Transform[] moveSpots;
    private int randomSpot;

    public GameObject explosion;
    public GameObject floatingPoints;

    public Transform attack_Point1, attack_Point2, attack_Point3;
    public GameObject bullet_Prefab;

    private Animator anim;
    public AudioSource source;
    public AudioClip explosionSound;
    public AudioClip fireSound;

    void Awake()
    {
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }

    void Start()
    {
        randomSpot = Random.Range(0, moveSpots.Length);
        Invoke("StartShooting", Random.Range(1f, 2f)); 
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, moveSpots[randomSpot].position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, moveSpots[randomSpot].position) < 0.2f)
        {
            if(waitTime <= 0)
            {
                randomSpot = Random.Range(0, moveSpots.Length); //next random spot to go
                waitTime = startWaitTime; 

            } else {

                waitTime -= Time.deltaTime;

            }
        }
    }

    void StartShooting()
    {
        GameObject bullet1 = Instantiate(bullet_Prefab, attack_Point1.position, Quaternion.identity);
        GameObject bullet2 = Instantiate(bullet_Prefab, attack_Point2.position, Quaternion.identity);
        GameObject bullet3 = Instantiate(bullet_Prefab, attack_Point3.position, Quaternion.identity);

        bullet1.GetComponent<EnemyBullet>();
        bullet2.GetComponent<EnemyBullet>();
        bullet3.GetComponent<EnemyBullet>();

        source.PlayOneShot(fireSound);
     
        Invoke("StartShooting", Random.Range(0.5f, 1f));
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Boundary" || other.tag == "Enemy" || other.tag == "Power Up AS" || other.tag == "Power Up MB" || other.tag == "Power Up SH")
        {
            return;
        }     
        
        anim.Play("hitanim");
  
        Health -= 1;
       
        
        if (Health <= 0)
        {
            ScoreScript.scoreValue += 550;
            Invoke("Destroy", 0.1f);
        }
    }

    void Destroy()
    {
        GameObject points = Instantiate(floatingPoints, transform.position, Quaternion.identity) as GameObject;
        points.transform.GetChild(0).GetComponent<TextMesh>().text = "550";

        GameObject explo = Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(explo, 0.4f);
        Destroy(gameObject);
    }





}
