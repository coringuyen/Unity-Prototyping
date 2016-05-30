using UnityEngine;
using System.Collections;

public class UnitMono : MonoBehaviour
{
    public string unitName;
    public Unit unit;
    public int health;
    public int resource;
    public Transform target;

    void Awake()
    {
        Unit unit = new Unit(unitName, health, resource);
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            GetComponent<Animator>().SetBool("Run", true);
        if (Input.GetKeyDown((KeyCode.E)))
        {
            Vector3 pos = target.position;
            StartCoroutine(Move(pos));
        }
    }


    IEnumerator Move(Vector3 dest)
    {
        Vector3 start = transform.position;
        float distance = Vector3.Distance(start, dest);
        while (distance > 0.2f)
        {
            //transform.Translate();
            yield return null;
        }


    }

    
}
