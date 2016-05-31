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
    Rigidbody m_rigidBody;

    void Awake()
    {
        Unit unit = new Unit(unitName, health, resource);
        m_anim = GetComponent<Animator>();
        m_rigidBody = GetComponent<Rigidbody>();
        origin = transform.position;

    }
    public bool runit = false;
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.R))
        {
            StopAllCoroutines();
            StartCoroutine(Move(origin));
        }
        if (Input.GetKeyDown((KeyCode.E)) || runit)
        {
            StopAllCoroutines();
            StartCoroutine(Move(target.position, 3f, 2f));
        }
    }
    public float forwardSpeed = 3.0f;

    public float backwardSpeed = 3.0f;

    public float rotateSpeed = 5.0f;
    bool turning = false;
    [SerializeField]
    float h;

    [SerializeField]
    float v;
    void FixedUpdate()
    {
         h = Input.GetAxis("Horizontal");
         v = Input.GetAxis("Vertical");

        m_anim.SetFloat("Speed", v);
        m_anim.SetFloat("Direction", h);
        Vector3 velocity = new Vector3(0, 0, v);
        
        velocity = transform.TransformDirection(velocity);
        m_rigidBody.velocity = velocity;
        if (v > 0.1f)
        {
            velocity *= forwardSpeed;
            if (h > .5f || h < -.5f)
                forwardSpeed = 1;
            else
                forwardSpeed = 3;
        }
        if (v < -0.1f)
            velocity *= backwardSpeed;

       // transform.Rotate(0, h * rotateSpeed, 0);

       // transform.position += velocity * Time.fixedDeltaTime;
    }


    int safecounter = 0;

    [SerializeField]
    float speed;

    [SerializeField]
    float velocity;
    IEnumerator Move(Vector3 dest, float duration = 3f, float offset = 0)
    {
        yield return new WaitForSeconds(.5f);
        //Debug.LogError("stop");
        transform.forward = Vector3.Normalize(dest - transform.position);
        Vector3 start = transform.position;
        float distanceTraveled = 0;
        float travelTime = 0;
        float distanceTotal = Vector3.Distance(start, dest);

        float p = 0;

        while (distanceTraveled < distanceTotal - offset)
        {
            travelTime += Time.deltaTime;

            speed = distanceTraveled / travelTime;

            Vector3 current = transform.position;

            p = travelTime / duration;

            transform.position = Vector3.Lerp(start, dest, p);

            distanceTraveled = Vector3.Magnitude(current - start);


            yield return null;
        }

        speed = 0;
    }


}
