using System;
using System.Collections.Generic;

public enum AbilityType
{
    Damage,
    Heal,
}
#region interface
public interface IUnit
{
    int health { get; set; }

    int resource { get; set; }

    string name { get; set; }

    List<Ability> abilities { get; set; }
}

public interface IDamageable
{
    void TakeDamage(int amount);
}


public interface IAbility
{
    void Execute(Unit target);

    int amount{ get; set; }

    int cost{ get; set; }

    string abilityName{ get; set; }


}
#endregion interface

public class Ability : IAbility
{
    Ability()
    {
        m_abilityName = "";
        m_cost = 0;
        m_amount = 0;
    }

    string m_abilityName;

    int m_amount;

    int m_cost;

    AbilityType m_abilityType;

    /// <summary>
    /// Execute the specified source and target.
    /// </summary>
    /// <param name="source">Source.</param>
    /// <param name="target">Target.</param>
    public void Execute(Unit target)
    {
        switch (abilityType)
        {
            case AbilityType.Damage:
                target.TakeDamage(m_amount); 
                break;
            case AbilityType.Heal:         
                target.TakeDamage(-m_amount); 
                break;
            default:
                break;
        }
    }



    AbilityType abilityType
    {
        get
        {
            return m_abilityType;
        }

        set
        {
            m_abilityType = value;
        }
    }

    public int amount
    {
        get
        {
            return m_amount;
        }
        set
        {
            throw new NotImplementedException();
        }
    }

    public int cost
    {
        get
        {
            return m_cost;
        }
        set
        {
            m_cost = value;
        }
    }

    public string abilityName
    {
        get{ return m_abilityName; }
        set{ throw new NotImplementedException(); }
    }


   

}

public class Unit : IUnit, IDamageable
{
    Unit()
    {

    }

    public Unit(string name, int h, int r)
    {
        m_health = h;
        m_resource = r;
        m_name = name;

    }

    public void TakeDamage(int amount)
    {
        health -= amount;
    }

    private string m_name;

    private int m_health;

    private int m_resource;

    private List<Ability> m_abilities = new List<Ability>();

    public List<Ability> abilities
    {
        get
        {
            return m_abilities;
        }

        set
        {
            throw new NotImplementedException();
        }
    }

    public int health
    {
        get
        {
            return m_health;
        }

        set
        {
            throw new NotImplementedException();
        }
    }

    public string name
    {
        get
        { 
            return m_name; 
        }
        set
        {
            throw new NotImplementedException();
        }
    }

    public int resource
    {
        get
        {
            return m_resource;
        }

        set
        {
            throw new NotImplementedException();
        }
    }

 
}


