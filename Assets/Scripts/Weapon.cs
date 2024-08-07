using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

   public WeaponStat Stat;
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Animator animator;
    [SerializeField] Transform projectilePoint;
    [SerializeField] float rotationSpeed =10f;
    [SerializeField] LayerMask meleeAttackLayers;
 
    float _nextFireTime;
    int ammo;
    public int Ammo => ammo;
    float targetRotationZ;
   // public SpriteRenderer SpriteRend => spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Stat.Rotatable){
           transform.rotation = Quaternion.Euler(transform.eulerAngles.x,transform.eulerAngles.y, Mathf.LerpAngle(transform.eulerAngles.z,targetRotationZ,rotationSpeed *  Time.deltaTime));
        }
    }
    public void Rotate(Vector2 direction){
        
         // difference = Vector2.Lerp(difference, PlayerInput.Instance.TargetGunPointer - (PlayerInput.Instance.layout == PlayerInput.ControlLayout.PC ? (Vector2)transform.position : Vector2.zero), Time.deltaTime * rotationSpeed);
     //  difference.Normalize();
       
      targetRotationZ  = Mathf.Atan2(direction.y,direction.x) * Mathf.Rad2Deg;

      
        
       
    }
    public void Flip(bool value){
      //  spriteRenderer.flipY = value;
     if(Stat.Rotatable)transform.localScale = new Vector3(transform.localScale.x,value ? -1 : 1,transform.localScale.z);
     else transform.localScale = new Vector3(value ? -1 : 1,transform.localScale.y,transform.localScale.z);
    
     
      
    }
    public void Attack(Projectile.Belongings belonging = Projectile.Belongings.Player){
        
       
             if(Time.time > _nextFireTime){ 
            
                 animator.SetTrigger("Attack"); 
                 if(Stat.UsesAmmo) Stat.Ammo--;
                 _nextFireTime = Time.time + Stat.ReloadTime;
                 if(Stat._DamageType == WeaponStat.DamageType.Melee){
                   StartCoroutine(MeleeAttackDelayed(Stat.AttackTime/2));
                 }
            if(Stat.Projectile != null){
                Projectile projectile = Instantiate(Stat.Projectile,projectilePoint.position,transform.rotation).GetComponent<Projectile>();
                projectile.Direction = projectilePoint.right;
                projectile.Damage = Stat.BaseDamage;
                projectile.CriticalChance = Stat.CriticalChance;
                projectile.Speed = Stat.ProjectileSpeed;
                projectile.Spread = Stat.Spread;
                projectile.Belonging = belonging;
             }
        }
    }
    void OnTriggerEnter2D(Collider2D collision){
      //  if(!collision.TryGetComponent(out PlayerControls player))print(1);
       // if(animator.GetCurrentAnimatorStateInfo(0).tagHash)
    }
    public void Test(){

    }
    IEnumerator MeleeAttackDelayed(float delay){
      RaycastHit2D hit = Physics2D.Raycast(transform.position,transform.right,Stat.AttackRadius,meleeAttackLayers);
                   Debug.DrawRay(transform.position,transform.right * Stat.AttackRadius,Color.red,5f);
                  
      yield return new WaitForSeconds(delay); 
      if(hit.collider != null && hit.collider.TryGetComponent(out Health health)){
                     health.TakeDamage(Stat.BaseDamage);
                   }

    }
}
