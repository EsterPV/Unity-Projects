using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    public float JumpForce;
    public float Speed;
    public GameObject bulletPrefab;

    private Rigidbody2D Rigidbody2D;
    private float Horizontal;
    private bool Grounded;
    private Animator Animator;
    private float LastShoot;
    private int Health = 5;

    //transicion cuando muere
    public UnityEngine.UI.Image gameOver;
   
    float valorAlfaDeseadoGameOver;

    // Start is called before the first frame update
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");

        if (Horizontal < 0.0f) transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        else if (Horizontal > 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);

        Animator.SetBool("running", Horizontal != 0.0f);
       

        Debug.DrawRay(transform.position, Vector3.down * 0.5f, Color.red);
        if (Physics2D.Raycast(transform.position, Vector3.down, 0.5f))
        {
            Grounded = true;
            Animator.SetBool("jumping", false);


        }
        else { 
            Grounded = false;
            Animator.SetBool("jumping", true);
        }

        if (Input.GetKeyDown(KeyCode.W) && Grounded ) 
        {
           
            Jump();
        }

        if (Input.GetKey(KeyCode.Space) && Time.time > LastShoot + 0.25f)
        {
            Shoot();
            LastShoot = Time.time;
            Animator.SetBool("disparando", true);
        }
        else Animator.SetBool("disparando", false);

        

        if (Input.GetKeyDown(KeyCode.P)) valorAlfaDeseadoGameOver = 1;
        if (Input.GetKeyDown(KeyCode.O)) valorAlfaDeseadoGameOver = 0;
    }

    private void Jump()
    {
        Rigidbody2D.AddForce(Vector2.up * JumpForce);
        

    }

    private void Shoot()
    {
        Vector3 direction;
        if (transform.localScale.x == 1.0f) direction = Vector3.right;
        else direction = Vector3.left;

       GameObject Bullet =  Instantiate(bulletPrefab, transform.position + direction * 0.4f, Quaternion.identity);
        Bullet.GetComponent<BulletScript>().setDirection(direction);
    }

    private void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(Horizontal * Speed, Rigidbody2D.velocity.y);

        //manejar transicion gameover
        float valorAlfa = Mathf.Lerp(gameOver.color.a, valorAlfaDeseadoGameOver, .1f);

        gameOver.color = new Color(0, 1, 1, valorAlfa);
       
    }

    public void Hit()
    {
        Health -= 1;
        if(Health == 0) Destroy(gameObject);
    }
}
