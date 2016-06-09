using UnityEngine;
using UnityEngine.Events;


public class UnitMono : MonoBehaviour, IDamageable
{   
    
    public int health;

    public int resource;

    public string unitName;

    public Unit unit;
    
    public UnitType ut;
    public static AnimationEvent EventPlayerAttack;
    public static DamageEvent EventTakeDamage;
    void Awake()
    {
        unit = new Unit(unitName, health, resource, ut);
        EventPlayerAttack = new AnimationEvent();
        EventTakeDamage = new DamageEvent();        
    }
    public float Attack(UnitMono um)
    {
        um.TakeDamage(25);
        return um.health;
    }
    public void TakeDamage(int amount)
    {        
        unit.TakeDamage(amount);
        health = unit.health;        
        EventTakeDamage.Invoke(amount, this);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            EventPlayerAttack.Invoke(this);
    }

    
}
