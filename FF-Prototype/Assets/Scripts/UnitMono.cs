using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UnitMono : MonoBehaviour
{
    public int health;
    public int resource;
    public string unitName;

    public Unit unit;
    public Transform target;
    [SerializeField]
    private Animator m_anim;
    public Text speedText;
    public Vector3 origin;

    void Awake()
    {
        Unit unit = new Unit(unitName, health, resource);
        m_anim = GetComponent<Animator>();
        origin = transform.position;

    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
            StopAllCoroutines();
            StartCoroutine(Move(origin));
        }
        if (Input.GetKeyDown((KeyCode.E)))
        {
            StopAllCoroutines();
            StartCoroutine(Move(target.position, 2f));
        }


    }

    void FixedUpdate()
    {
        m_anim.SetFloat("Speed", speed);
    }

    int safecounter = 0;
    [SerializeField]
    float lerpTime = 1.5f;
    [SerializeField]
    float speed;
    
    Vector3 direction;
    float dir;

    IEnumerator Move(Vector3 dest, float offset = 0)
    {
        transform.forward = Vector3.Normalize(dest - transform.position);
        Vector3 start = transform.position;
        float distanceTraveled = 0;
        float travelTime = 0;
        float distanceTotal = Vector3.Distance(start, dest);

        float p = 0;
        
        while (distanceTraveled < distanceTotal - offset)
        {
            Vector3 current = transform.position;

            travelTime += Time.deltaTime;

            p = travelTime / lerpTime;

            distanceTraveled = Vector3.Magnitude(current - start);

            transform.position = Vector3.Lerp(start, dest, p);

            speed = distanceTraveled / travelTime;

            yield return null;
        }

        speed = 0;
    }


}
