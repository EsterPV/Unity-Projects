using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ninjaScript : MonoBehaviour
{
    public GameObject shurikenPrefab;
    public GameObject monheco;
    private Animator Animator;

    private float lastShoot;
    private int Health = 2;


    void Start()
    {
       
        Animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (monheco == null) return;

        Vector3 direction = monheco.transform.position - transform.position;
        if (direction.x >= 0.0f) transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        else transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

        float distance = Mathf.Abs(monheco.transform.position.x - transform.position.x);

        if(distance < 1.5f && Time.time > lastShoot + 0.25f)
        {
           
            Shoot();
            lastShoot = Time.time;
            Animator.SetBool("disparando", true);
        }
        else
        {
            Animator.SetBool("disparando", false);
        }

      
    }

    private void Shoot()
    {
        Vector3 direction;
        if (transform.localScale.x == 1.0f) direction = Vector3.right;
        else direction = Vector3.left;

        GameObject Bullet = Instantiate(shurikenPrefab, transform.position + direction * 0.5f, Quaternion.identity);
        Bullet.GetComponent<shurikenScript>().setDirection(direction);
    }

    public void Hit()
    {
        Health -=  1;
        if (Health == 0) Destroy(gameObject);
    }
}
