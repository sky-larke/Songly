using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TopDownPlayerController : MonoBehaviour {

    // animator uses these to know which animation to use
    private bool m_FacingRight = false;
    private bool m_FacingLeft = false;
    private bool m_FacingUp = false;
    private bool m_FacingDown = false;

    //used for flipping character horizontally
    private Vector3 rightVector = new Vector3(-1, 1, 1);
    private Vector3 leftVector = new Vector3(1, 1, 1);

    private Animator anim;
    private Rigidbody2D _rb;
    public float speed;
    private float magnitude;        //speed multiplier

    // Start is called before the first frame update
    void Start() {
        anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        magnitude = 0.5f; 
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate() {
        Movement();

        //play correct idle  animation based on direction (when velocity is low)
        if (_rb.velocity.magnitude < 0.5) {
            if (m_FacingLeft) {
                anim.Play("SideIdle");
            }
            else if (m_FacingRight) {
                anim.Play("SideIdle");
            }
            else if (m_FacingUp) {
                anim.Play("BackIdle");
            }
            else if (m_FacingDown) {
                anim.Play("FrontIdle");
            }
        }
    }


    //MOVEMENT player will move to the given direction.
    void Movement() {
        //UP
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
            anim.Play("BackWalk");
            _rb.velocity = new Vector2(0, magnitude * speed);
            m_FacingUp = true;
            m_FacingDown = false;
            m_FacingLeft = false;
            m_FacingRight = false;
        }
        //DOWN
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) {
            anim.Play("FrontWalk");
            _rb.velocity = new Vector2(0, magnitude * -speed);
            m_FacingUp = false;
            m_FacingDown = true;
            m_FacingLeft = false;
            m_FacingRight = false;
        }
        //LEFT
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
            anim.Play("SideWalk");
            _rb.velocity = new Vector2(magnitude * -speed, 0);
            m_FacingUp = false;
            m_FacingDown = false;
            m_FacingLeft = true;
            m_FacingRight = false;
        }
        //RIGHT
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
            anim.Play("SideWalk");
            _rb.velocity = new Vector2(magnitude * speed, 0);
            m_FacingUp = false;
            m_FacingDown = false;
            m_FacingLeft = false;
            m_FacingRight = true;
        }

        //Flip Left/Right
        if (m_FacingRight) {
            gameObject.transform.localScale = rightVector;
        } else if (m_FacingLeft) {
            gameObject.transform.localScale = leftVector;
        }
    }

    
    private void Flip() {
        // Switch the way the player is facing. (Left or Right)
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        //transform.Rotate(new Vector3(0, 180, 0)); //this might be better
    }
}