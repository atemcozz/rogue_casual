using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float destroyTime;
   [HideInInspector] public Vector2 Direction;
   [HideInInspector] public float Damage,Spread, CriticalChance, Speed;
    public enum Belongings{Player,Enemy}
    public Belongings Belonging;
   
    void Start()
    {
        Destroy(gameObject,destroyTime);
    }
}
