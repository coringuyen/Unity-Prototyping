using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class UnitMovement : MonoBehaviour
{

    /// <summary>
    /// Equations from the following website
    /// https://chicounity3d.wordpress.com/2014/05/23/how-to-lerp-like-a-pro/
    /// </summary>


    public enum LerpType
    {
        Linear,
        Exponential,
        EaseIn,
        EaseOut,
        Smoothe,
        Smoother,
        Custom,
        
    }

    public LerpType lerpType;
    [HideInInspector]
    public Text UI_avgVelocity;
    [HideInInspector]
    public Text UI_insVelocity;
    public Transform target;
    public Transform origin;
    public AnimationCurve ac;


    private Animator m_anim;
    private Rigidbody m_rigidBody;
    private float m_avgVel;
    private float m_insVel;

    [SerializeField]
    float animTime = 2;
    [SerializeField]
    float attackDistance = 1.5f;

    void Awake()
    {
        m_anim = GetComponent<Animator>();
        m_rigidBody = GetComponent<Rigidbody>();
        transform.forward = Vector3.right;
        
    }
    bool canAttack = true;
    void FixedUpdate()
    {
        
        if (Input.GetKeyDown(KeyCode.DownArrow))        
            Time.timeScale = Mathf.Clamp(Time.timeScale - .1f, 0, 100f);  
        if(UI_avgVelocity != null)
            UI_avgVelocity.text = "Average Velocity: " + m_avgVel.ToString();
        if(UI_insVelocity != null)
            UI_insVelocity.text = "Instant Velocity: " + m_insVel.ToString();
        if (canAttack)
        {
            canAttack = false;
            if (Input.GetKeyDown(KeyCode.Space))
                m_anim.SetTrigger("Hikick");
        }
        if(m_anim.GetCurrentAnimatorStateInfo(0).IsName("combatidle"))
        {
            canAttack = true;
        }
    }
    
    

    void OnAnimatorMove()
    {        
        if (m_insVel <= 0.001)
            m_insVel = 0;
        m_anim.SetFloat("Speed", m_insVel);
        m_anim.SetFloat("Direction", 1);
    }
    bool moving;
    public void Action(Transform t)
    {        
        if (!moving && canAttack)
        {
            moving = true;
            StopAllCoroutines();
            StartCoroutine(MoveAndAttack(t));
        }
    }

    IEnumerator MoveAndAttack(Transform t)
    {
        //if we are at least 1 unit away we will walk
        if(Vector3.Distance(transform.position, t.position) > 5)
            yield return StartCoroutine(Move(t, animTime));
        moving = false;
        if(t.name != "Origin")
            m_anim.SetTrigger("Hikick");
        else
        {
            transform.forward *= -1;
        }
        
        
    }
 
 
    //When t = 0 returns a. When t = 1 returns b. When t = 0.5 returns the point midway between a and b.
    IEnumerator Move(Transform tdest, float lerpTime)
    {

        float dx = tdest.position.x;
        float dy = tdest.position.y;
        float dz = tdest.position.z;

        Vector3 start = transform.position;
        //if moving to attack set the dest to the attack distance offset
        Vector3 dest = (tdest == origin) ? new Vector3(dx, dy, dz) : new Vector3(dx - attackDistance, dy, dz);

        transform.forward = dest.normalized;
        float currentTime = 0;

        while (currentTime < lerpTime)
        {
            Vector3 oldp = transform.position;

            Vector3 dir = (dest - oldp).normalized;

            transform.TransformDirection(dir);

            currentTime += Time.deltaTime;

            if (currentTime > lerpTime)
            {
                currentTime = lerpTime;
            }
            
            float t = currentTime / lerpTime;           
            
            switch (lerpType)
            {
                case LerpType.Linear:                    
                    break;
                case LerpType.Exponential:
                    t = t * t;
                    break;                
                case LerpType.EaseIn:
                    t = 1f - Mathf.Cos(t * Mathf.PI * 0.5f);
                    break;
                case LerpType.EaseOut:
                    t =  Mathf.Sin(t * Mathf.PI * 0.5f);
                    break;
                case LerpType.Smoothe:
                    t = t * t * (3f - 2f * t);
                    break;
                case LerpType.Smoother:
                    t = t * t * t * (t * (6f * t - 15f) + 10f);
                    break;
                case LerpType.Custom:
                    t = ac.Evaluate(t);
                    break;
                default:
                    break;
            }           

            transform.position = Vector3.Lerp(start, dest, t);

            Vector3 newp = transform.position;           

            m_avgVel = Vector3.Magnitude(dest - start) / lerpTime;

            m_insVel = Mathf.Clamp(Vector3.Magnitude(newp - oldp) / Time.deltaTime, 0, 7.5f) / 7.5f;            
                
            yield return null;
        } 

        m_avgVel = 0;

        m_insVel = 0; 
    }
}
