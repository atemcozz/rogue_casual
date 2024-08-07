using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Health : MonoBehaviour
{
    [SerializeField] float hp = 100;
    [SerializeField] float maxHp = 100;
    [SerializeField] bool hpEqualsMaxHp = true;
    public UnityAction<float,float> HealthChangeEvent;
    public UnityAction OnDamageTaken;
    public float GetHP => hp;
    // Start is called before the first frame update
    void Start()
    {
        if(hpEqualsMaxHp) hp = maxHp;
        HealthChangeEvent?.Invoke(hp,maxHp);
    }
    public void TakeDamage(float damage){
       hp = Mathf.Clamp(hp-damage,0, maxHp);
        HealthChangeEvent?.Invoke(hp,maxHp);
        OnDamageTaken?.Invoke();
        if(hp<=0){
            this.enabled = false;
        }
      
    }
    public void RestoreHealth(float value){
         hp = Mathf.Clamp(hp+value,0, maxHp);
         HealthChangeEvent?.Invoke(hp,maxHp);
    }
    public void EnableInfinityHealth(){
        maxHp = Mathf.Infinity;
        RestoreHealth(Mathf.Infinity);
    }
}
