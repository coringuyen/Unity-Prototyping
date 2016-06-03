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
    public Text avgVelocity;
    public Text insVelocity;
    public Transform target;
    public Transform origin;


    private Animator m_anim;
    private Rigidbody m_rigidBody;
    private float avgVel;
    private float insVel;

    void Awake()
    {
        m_anim = GetComponent<Animator>();
        m_rigidBody = GetComponent<Rigidbody>();
        transform.forward = Vector3.right;
        
    }
    public AnimationCurve ac;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))        
            Time.timeScale = Mathf.Clamp(Time.timeScale + .1f, 0, 100f);
        
        if (Input.GetKeyDown(KeyCode.DownArrow))        
            Time.timeScale = Mathf.Clamp(Time.timeScale - .1f, 0, 100f);  
        
        avgVelocity.text = "Average Velocity: " + avgVel.ToString();
        insVelocity.text = "Instant Velocity: " + insVel.ToString();
        if (Input.GetKeyDown(KeyCode.Space))
            m_anim.SetTrigger("Hikick");   

    }
    
    

    void OnAnimatorMove()
    {
        if (insVel <= 0.001)
            insVel = 0;
        m_anim.SetFloat("Speed", insVel);
        m_anim.SetFloat("Direction", 1);
    }
    [SerializeField]
    float animTime = 2;
    [SerializeField]
    float attackDistance = 1.5f;
    
    public void Action(Transform t)
    {
        StopAllCoroutines();        
        
        StartCoroutine(Move(t, animTime));
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

            avgVel = Vector3.Magnitude(dest - start) / lerpTime;

            insVel = Mathf.Clamp(Vector3.Magnitude(newp - oldp) / Time.deltaTime, 0, 7.5f) / 7.5f;            
                
            yield return null;
        } 

        avgVel = 0;

        insVel = 0;

    }

    IEnumerator JabAndKick()
    {
        m_anim.SetTrigger("Jab");
 
        m_anim.SetTrigger("RisingP");
        
        yield return null;
    }


}
