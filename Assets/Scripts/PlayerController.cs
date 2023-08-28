using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 5f;
    public float min_Y, max_Y, min_X, max_X;
    public bool DoubleUpFlag = false;
    private bool activeShield = false;
    public int Shieldcounter = 1;

    [SerializeField]
    private GameObject player_Bullet;

    public GameObject shield;
    public Transform attack_Point, leftCannon, rightCannon;
    
    public float fireRate = 0.0f;
    private float nextFire;

    public PlayerBullet playerBullet;
    public GameController gameController;
    private Animator anim;

    public AudioSource source;
    public AudioClip explosionSound;
    public AudioClip fireSound;
    public AudioClip PowerUpSound;
    public AudioClip PowerDownSound;


    void Awake()
    {
        anim = GetComponent<Animator>();
        source = GetComponent<AudioSource>();
    }

    void Start()
    {
        shield.SetActive(false);
    }

    void Update()
    {
        MovePlayer();

        //Fire and fire restriction 
        if (Input.GetKey("k") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;

            Instantiate(player_Bullet, attack_Point.position, Quaternion.identity);

            if (DoubleUpFlag)
            {
                Instantiate(player_Bullet, leftCannon.position, Quaternion.identity);
                Instantiate(player_Bullet, rightCannon.position, Quaternion.identity);
            }

            //Sound when shooting
            source.PlayOneShot(fireSound);
        }
    }

    void MovePlayer()
    {

        if (Input.GetAxisRaw("Vertical") > 0f)
        {

            Vector3 temp = transform.position;
            temp.y += speed * Time.deltaTime;

            if (temp.y > max_Y)
                temp.y = max_Y;

            transform.position = temp;

        }
        else if (Input.GetAxisRaw("Vertical") < 0f)
        {

            Vector3 temp = transform.position;
            temp.y -= speed * Time.deltaTime;

            if (temp.y < min_Y)
                temp.y = min_Y;

            transform.position = temp;
        } 
        //Vertical movement

        if (Input.GetAxisRaw("Horizontal") > 0f)
        {

            Vector3 temp = transform.position;
            temp.x += speed * Time.deltaTime;

            if (temp.x > max_X)
                temp.x = max_X;

            transform.position = temp;

        }
        else if (Input.GetAxisRaw("Horizontal") < 0f)
        {

            Vector3 temp = transform.position;
            temp.x -= speed * Time.deltaTime;

            if (temp.x < min_X)
                temp.x = min_X;

            transform.position = temp;
        } //Horizontal movement
    }


    void TurnoffGameObject()
    {
        gameObject.SetActive(false);
        gameController.GameOver();
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Boundary") || other.gameObject.CompareTag("Player Bullet"))
        {
            return;

        } else if (other.gameObject.CompareTag("Power Up AS"))

        {
            StopCoroutine(AttackSpeed());
            source.PlayOneShot(PowerUpSound);
            StartCoroutine(AttackSpeed());

        } else if (other.gameObject.CompareTag("Power Up MB"))

        {
            StopCoroutine(DoubleUp());
            source.PlayOneShot(PowerUpSound);
            StartCoroutine(DoubleUp());

        } else if (other.gameObject.CompareTag("Power Up SH"))

        {
            source.PlayOneShot(PowerUpSound);
            shield.SetActive(true);
            activeShield = true;
            Shieldcounter = 0;

        } else

        {
            if (activeShield == true && Shieldcounter == 0 && (other.tag == "Enemy" || other.tag =="Enemy Bullet"))
            {
                Shieldcounter += 1;
                activeShield = false;
                shield.SetActive(false);
                source.PlayOneShot(PowerDownSound);
                return;
            }

            anim.Play("Destroy");
            source.PlayOneShot(explosionSound);
            Invoke("TurnoffGameObject", 0.15f);
     
        }  
    }

    IEnumerator AttackSpeed()
    { 
        fireRate = 0.3f;
        playerBullet.speed = 8f;
        yield return new WaitForSeconds(10f);
        source.PlayOneShot(PowerDownSound);
        playerBullet.speed = 6f;
        fireRate = 0.4f;
    }

    IEnumerator DoubleUp()
    {
        DoubleUpFlag = true;
        yield return new WaitForSeconds(10f);
        source.PlayOneShot(PowerDownSound);
        DoubleUpFlag = false;
    }
}
