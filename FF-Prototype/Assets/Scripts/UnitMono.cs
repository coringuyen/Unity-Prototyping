using UnityEngine;
using UnityEngine.Events;


public class UnitMono : MonoBehaviour, IDamageable
{
    public static DamageEvent EventTakeDamage;

    public int health;

    public int resource;

    public string unitName;

    public Unit unit;
    
    public UnitType ut;

    void Awake()
    {
        unit = new Unit(unitName, health, resource, ut);
        EventTakeDamage = new DamageEvent();        
    }

    public void TakeDamage(int amount)
    {        
        unit.TakeDamage(amount);
        health = unit.health;        
        EventTakeDamage.Invoke(amount, this);
    }

    
}
