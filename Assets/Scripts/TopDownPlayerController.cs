using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TopDownPlayerController : MonoBehaviour {


    private bool m_FacingRight;
    private bool m_FacingLeft;

    private bool m_FacingUp;
    private bool m_FacingDown;

    // Start is called before the first frame update
    private float direction;
    private float directionx;
    private float directiony;
    private Rigidbody2D _rb;
    public float speed;
    private float magnitude;
    private float Isboost; // running mechanic, User will run if button is pressed
    public Object ammo;

    public int ammunition; //ammo the user has
    public int health; //number of health points
    public Vector3 spawnPoint; // where the user will spawn and respawn

    private AudioSource source; //Audio source
    public AudioClip Damage; // sound effect to play when the user takes damage
    public AudioClip hit; //sound effect to play when user hits an enemy
    public AudioClip Shoot; //sound effect to play when user shoots
    public AudioClip healthGain; //sound effect to play when user gains health

    private Animator anim;

    void Start() {

        anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        magnitude = 0.5f;
        Isboost = 0f;
        //Set up spawnpoint
        spawnPoint = transform.position;
        //load save data, If there is no save data use the default values
        health = PlayerPrefs.GetInt("health", 3);
        ammunition = PlayerPrefs.GetInt("Ammo", 5);
        //prepare audio source
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        BOOST();
        Fire();

        if (health < 1)
        {
            SceneManager.LoadScene(((SceneManager.sceneCountInBuildSettings) - 1));
        }

        
    }

    void FixedUpdate() {
        Movement();
        if (_rb.velocity.magnitude < 0.5)
        {

            if (m_FacingLeft)
            {
                anim.Play("LeftIdle");
            }
            if (m_FacingRight)
            {
                anim.Play("RightIdle");
            }

            if (m_FacingUp)
            {
                anim.Play("BackIdle");
            }

            if (m_FacingDown)
            {
                anim.Play("FrontIdle");
            }
        }
    }


    //MOVEMENT player will move to the given direction and their player model will rotate to that direction. If up arrow was pressed, then player will now face up
    void Movement() {
        if (Input.GetKey(KeyCode.UpArrow )|| Input.GetKey(KeyCode.W)) {
            anim.Play("BackWalk");
            _rb.velocity = new Vector2(0, (magnitude + Isboost) * speed);
            direction = 0f;
            directiony = .5f;
            directionx = 0f;
            m_FacingUp = true;
            m_FacingDown = false;
            m_FacingLeft = false;
            m_FacingRight = false;
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) {
            anim.Play("FrontWalk");
            _rb.velocity = new Vector2(0, (magnitude + Isboost) * -speed);
            direction = 1f;
            directiony = -.5f;
            directionx = 0f;
            m_FacingUp = false;
            m_FacingDown = true;
            m_FacingLeft = false;
            m_FacingRight = false;
        } 
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
            anim.Play("LeftWalk");
            _rb.velocity = new Vector2((magnitude + Isboost) * -speed, 0);
            direction = 2f;
            directionx = -.5f;
            directiony = 0f;
            m_FacingUp = false;
            m_FacingDown = false;
            m_FacingLeft = true;
            m_FacingRight = false;
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
            anim.Play("SideWalk");
            _rb.velocity = new Vector2((magnitude + Isboost) * speed, 0);
            direction = 3f;
            directionx = .5f;
            directiony = 0f;
            m_FacingUp = false;
            m_FacingDown = false;
            m_FacingLeft = false;
            m_FacingRight = true;
        }
    }

    void BOOST() {
        //if button is pressed the user will move faster
        if (Input.GetKey(KeyCode.Z)) {
            Isboost = 1;
        } else {
            Isboost = 0;
        }
    }

    void Fire() {
        //User can't use their 
        if (Input.GetKeyDown(KeyCode.X) && Isboost == 0f && ammunition > 0) {
            Object.Instantiate(ammo, new Vector3(transform.position.x + directionx, transform.position.y + directiony, direction), Quaternion.identity);
            ammunition--;
            source.PlayOneShot(Shoot, 1);
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "enemy") {
            health--;
            source.PlayOneShot(Damage, 1);
            transform.position = spawnPoint;

        }

        if (collision.gameObject.tag == "hazard") {
            health--;
            transform.position = spawnPoint;
            source.PlayOneShot(Damage, 1);
        }

        if (collision.gameObject.tag == "goal") {
            //Save current health points and refill ammo
            PlayerPrefs.SetInt("health", health);
            PlayerPrefs.SetInt("Ammo", 5);

        }

        if (collision.gameObject.tag == "HealthPack") {
            //Saves the restored health and ammo
            PlayerPrefs.SetInt("health", 3);
            PlayerPrefs.SetInt("Ammo", 5);

            //restore health and ammo
            health = 3;
            ammunition = 5;
            source.PlayOneShot(healthGain, 1);

        }

        if (collision.gameObject.tag == "bulletTag") {
            source.PlayOneShot(Damage, 1);
            respawn();
        }

        if (collision.gameObject.tag == "door") {
            source.PlayOneShot(hit, 1);

        }
    }

    void respawn() {
        transform.position = spawnPoint;
        health--;
    }



    
    private void Flip() {
        // Switch the way the player is labelled as facing.
        //m_FacingRight = !m_FacingRight; //does this elswewhere

        // Multiply the player's x local scale by -1.
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}