using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Projectile
{
    Rigidbody2D rb;
    [SerializeField] GameObject particle, bloodParticle;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
       Spread = Random.Range(-Spread,Spread);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.velocity = Quaternion.Euler(0f,0f,Spread) * Direction * Speed;
    }
    void OnCollisionEnter2D(Collision2D collision){
        if(particle != null){
            /*
           if((collision.collider.TryGetComponent(out PlayerControls player) || collision.collider.TryGetComponent(out Enemy enemy)) && bloodParticle != null){
                Instantiate(bloodParticle,transform.position,transform.rotation); 
           }
           else */ Instantiate(particle,transform.position,transform.rotation);
        }
        if(collision.collider.TryGetComponent(out Health health)){
            //health.TakeDamage(Damage);
        }
        Destroy(gameObject);
    }
}
