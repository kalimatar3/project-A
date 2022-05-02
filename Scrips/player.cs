using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class player : MonoBehaviour
{
    // Biến di chuyển
    public float MaxSpeed;
    Rigidbody2D myBody;
    Animator myAnim;
    bool facing,Gravity;
    // biến bơi
    float swim;
    Vector3 Right = new Vector3(-1,0,-1)
           ,Left = new Vector3(1,0,-1),
            Down = new Vector3(0,-1,0),
            Up   = new Vector3(0,1,0);
    Quaternion theRotation;
    // biến nhảy
    public float Jumpheight;
    bool Grounded, Walled;
    // bien tan cong
    bool Attack;
    public Transform playerface;
    public GameObject Attackzone;
    float Firerate = 0.5f;
    float Nextfire = 0;
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        facing = true;
    }
    void FixedUpdate()
    {
        // di chuyển
        swim = 0;
        Vector3 GunTip = transform.localPosition - new Vector3(1.35f, 1, 0);
        Vector3 theScale = transform.localScale;
        float move = Input.GetAxis("Horizontal");
        myAnim.SetFloat("speed", Mathf.Abs(move));
        myBody.velocity = new Vector2(move * MaxSpeed, myBody.velocity.y);
        if (Gravity)
        {
            if (move > 0)   facing = true;                  
            if (move < 0)   facing = false;              
            if (facing == true)
            {
                LandDirection(Right);
                Attackable(Right);
                playerface.position = GunTip + new Vector3(1.5f, 0, 0);
            }
            if (facing == false)
            {
                 LandDirection(Left);
                Attackable(Left);
                playerface.position = GunTip + new Vector3(1f, 0, 0);
            }
            if (Walled && !Grounded)
            {
                myBody.velocity = new Vector2(0, myBody.velocity.y);
            }
        }
        // nhảy
        myAnim.SetBool("jump", Grounded);
        if (Input.GetKeyDown(KeyCode.K) && Grounded)
        {
            StartCoroutine(Jump());
                Grounded = false;
                Jump();
        }
        //tan cong
        //Bơi
        myAnim.SetBool("Gravity", Gravity);
       
        if (Gravity)
        {
            myBody.gravityScale = 5;
        }
        if (Gravity == false)
        {

            myBody.gravityScale = 0;
           
            theRotation = transform.localRotation;
            theRotation = Quaternion.Euler(0, 0, 0);
            transform.localRotation = theRotation;
            if (facing==true)
            {
                Attackable(Right);
                playerface.position = GunTip + new Vector3(1, 0, 0);
            }
            if (facing==false)
            {
                Attackable(Left);
                playerface.position = GunTip + new Vector3(-1, 0, 0);
            }
            if (Input.GetKey(KeyCode.W))
            {
                swim = 1;
                Attackable(Up);
                WaterDirection(Up);
                playerface.position = GunTip + new Vector3(0, 2.9f, 0);
            }
            if (Input.GetKey(KeyCode.S))
            {
                Attackable(Down);
                swim = -1;
                WaterDirection(Down);
                playerface.position = GunTip + new Vector3(0, -1.5f, 0);
            }
            if (Input.GetKey(KeyCode.A))
            {
                facing = false;
                Attackable(Left);
                WaterDirection(Left);
                playerface.position = GunTip + new Vector3(-0.5f, 0, 0);
            }
            if (Input.GetKey(KeyCode.D))
            {
                facing = true;
                Attackable(Right);               
                WaterDirection(Right);
                playerface.position = GunTip + new Vector3(2.5f, 0, 0);
            }
            myBody.velocity = new Vector2(myBody.velocity.x, swim * MaxSpeed);
        } 
        myAnim.SetFloat("swim", Mathf.Abs(swim+move));
    }
    void LandDirection( Vector3 v)// huong nhan vat tren mat dat
    {
        theRotation = transform.localRotation;
        theRotation = Quaternion.Euler(0,(v.x+1)*90,0);
        transform.localRotation = theRotation;

    }
    void WaterDirection( Vector3 v)// huong nhan vat duoi nuoc
    {
        theRotation = transform.localRotation;
        theRotation = Quaternion.Euler((v.y-1 + Mathf.Abs(v.x))*90 , (v.x - 1 + Mathf.Abs(v.y) )*-90,-v.z*90);
        transform.localRotation = theRotation;
    }
     void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Ground") {
            Grounded = true;
        }
        if (other.gameObject.tag == "Wall") {
            Walled = true;
        } else Walled = false;
    }
     void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "GravityEvironment")
        {
            Gravity = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "GravityEvironment")
        {
            Gravity = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "GravityEvironment")
        {
            Gravity =false;
        }
    }
    void Attackable(Vector3 v)
    {
       
        myAnim.SetBool("Attack", Attack);
        if (Input.GetKeyDown(KeyCode.J))
        {
            StartCoroutine(Attackfalse());
            if (Time.time > Nextfire)
            { 
                Nextfire = Time.time + Firerate;
                Instantiate(Attackzone, playerface.position, Quaternion.Euler(0,0,(v.x-v.z-v.y)*90));
            }
        }
    }
    
    IEnumerator Jump()
    {
        yield return new WaitForSeconds(0.5f);
        myBody.velocity = new Vector2(myBody.velocity.x, Jumpheight);
    }   
    IEnumerator Attackfalse()
    {
        Attack = true;
        yield return new WaitForSeconds(0.5f);
        Attack = false;
       
    }
}