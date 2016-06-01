using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UnitMono : MonoBehaviour
{
    public int health;

    public int resource;

    public string unitName;

    public Unit unit;


    void Awake()
    {
        unit = new Unit(unitName, health, resource);
    }
}
