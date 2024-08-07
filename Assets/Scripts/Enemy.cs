using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    NavMeshAgent agent;
    PlayerControls player;
    [SerializeField] Weapon weapon;
    [SerializeField] bool isStatic = false;
    [SerializeField] float playerDetectionDistance, attackStoppingDistance;
    [SerializeField] LayerMask raycastLayers;
    [SerializeField] Transform patrolPointsTransform;
    [SerializeField] GameObject bloodParticle, deathParticle;
    List<Vector3> patrolPoints = new List<Vector3>();
    bool isChasing = false;
   [SerializeField] SpriteRenderer spriteRenderer;
   Health health;

    void OnEnable(){ 
        health = GetComponent<Health>();
        health.HealthChangeEvent += UpdateHealth;
        health.OnDamageTaken +=OnDamage;
    }
    void OnDisable(){
        health.HealthChangeEvent -= UpdateHealth;
        health.OnDamageTaken -=OnDamage;
    }
    int currentPointIndex = 0;
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
       
        agent.updateRotation =false;
        agent.updateUpAxis = false;
        agent.stoppingDistance = 0;
        //player = FindObjectOfType<PlayerControls>();
        
        //patrolPoints = new Vector3[startPatrolPoints.Length];
        foreach(Transform point in patrolPointsTransform){
            patrolPoints.Add(point.position);
        }
        patrolPointsTransform.parent = null;
        
        agent.SetDestination(patrolPoints[0]);

    }
    void UpdateHealth(float hp, float maxHp){
        
         if(hp<=0) Death();
    }
    void OnDamage(){
        Instantiate(bloodParticle,transform.position,Quaternion.identity);
    }
    void Death(){
        Instantiate(deathParticle,transform.position,Quaternion.identity);
        Destroy(gameObject);
    }
    // Update is called once per frame
    /*
    void Update()
    {
       if(isChasing) {
           agent.SetDestination(player.transform.position);
       }
       else{
           agent.stoppingDistance = 0f;
           agent.SetDestination(patrolPoints[currentPointIndex]);
           if(agent.remainingDistance < 0.5f){
               currentPointIndex++;
           }
       }
    }*/
   
    void Flip(bool  value){
        spriteRenderer.flipX = value;
      if(weapon != null)  weapon.Flip(value);
    }
    void Update()
    {
        if(!isChasing)agent.stoppingDistance = 0f;
        else agent.stoppingDistance = attackStoppingDistance;
        if(!isChasing && !isStatic && agent.remainingDistance <=1f){
            
            if(currentPointIndex >= patrolPoints.Count) currentPointIndex = 0;
            agent.SetDestination(patrolPoints[currentPointIndex]);
           weapon.Rotate(patrolPoints[currentPointIndex] - transform.position);
            if(agent.destination.x - transform.position.x <= 0)Flip(true);
            else Flip(false);
               
           
          currentPointIndex = (currentPointIndex +1) % patrolPoints.Count;
        }
        
         if(CheckForPlayer(out Transform player)){
             isChasing = true;
             weapon.Rotate(player.position - transform.position);   
             if(player.position.x - transform.position.x <= 0)Flip(true);
            else Flip(false);
            agent.SetDestination(player.position);
             weapon.Attack(Projectile.Belongings.Enemy); 
           Debug.DrawRay(transform.position,player.position - transform.position, Color.green);
         } 
         else{
             isChasing = false;
         } 


         bool CheckForPlayer(out Transform playerPos){
        var colliders = Physics2D.OverlapCircleAll(transform.position,playerDetectionDistance);
        foreach(Collider2D collider in colliders){
            if(collider.gameObject.TryGetComponent(out PlayerControls collidedPlayer)){
                 RaycastHit2D hit = Physics2D.Raycast(transform.position, collidedPlayer.transform.position - transform.position,playerDetectionDistance,raycastLayers);

                 if(hit.collider != null){
                     if(hit.transform == collidedPlayer.transform){
                          playerPos = collidedPlayer.transform;
                            return true;
                     }

             }
               
            }
        }
        playerPos = null;
        return false;
    }
         
        
    } 
    void OnDrawGizmos(){
       // Gizmos.DrawWireSphere(transform.position,playerDetectionDistance);
       List<Vector3> list = new List<Vector3>();
       foreach(Transform point in patrolPointsTransform){
            list.Add(point.position);
        }
        for(int i = 0; i<list.Count; i++){
            if(i+1 >= list.Count){
                Gizmos.DrawRay(list[i],list[0] - list[i]);
            }
            else{
                Gizmos.DrawRay(list[i],list[i+1] - list[i]);
            }
        }
    }
    void OnDrawGizmosSelected(){
        Gizmos.DrawWireSphere(transform.position,playerDetectionDistance);
    }
    void OnCollisionEnter2D(Collision2D collision){
        
        if(collision.collider.TryGetComponent(out Projectile projectile) && projectile.Belonging == Projectile.Belongings.Player){
           health.TakeDamage(projectile.Damage);
            //Instantiate(bloodParticle,projectile.transform.position,projectile.transform.rotation);
        } 
    }
    void OnTriggerEnter2D(Collider2D trigger){
       
    }
}
