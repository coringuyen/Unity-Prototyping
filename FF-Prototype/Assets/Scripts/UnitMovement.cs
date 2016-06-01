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
        
    }

    public LerpType lerpType;
    public Text avgVelocity;
    public Text insVelocity;
    public Transform target;
    public Vector3 origin;


    private Animator m_anim;
    private Rigidbody m_rigidBody;
    private float avgVel;
    private float insVel;

    void Awake()
    {
        m_anim = GetComponent<Animator>();
        m_rigidBody = GetComponent<Rigidbody>();
        origin = transform.position;
    }
    public AnimationCurve ac;

    void Update()
    {
        avgVelocity.text = "Average Velocity: " + avgVel.ToString();
        insVelocity.text = "Instant Velocity: " + insVel.ToString();
    }


    void OnAnimatorMove()
    {
        m_anim.SetFloat("Speed", insVel / 7.5f);
    }
    [SerializeField]
    float animTime = 2;
    public void Action(Transform t)
    {
        Vector3 dest = new Vector3(t.position.x - 1, t.position.y, t.position.z);
        transform.forward = dest - t.position.normalized;
        StartCoroutine(Move(dest, animTime));
    }


    [SerializeField]
    float speed;

    [SerializeField]
    float velocity;

    //When t = 0 returns a. When t = 1 returns b. When t = 0.5 returns the point midway between a and b.
    IEnumerator Move(Vector3 dest, float lerpTime)
    {
        Vector3 start = transform.position;

        float currentTime = 0;

        while (currentTime < lerpTime)
        {
            Vector3 oldp = transform.position;

            currentTime += Time.deltaTime;
            if (currentTime > lerpTime)
                currentTime = lerpTime;

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
            }           

            transform.position = Vector3.Lerp(start, dest, t);

            Vector3 newp = transform.position;

            avgVel = Vector3.Magnitude(dest - start) / lerpTime;

            Mathf.Clamp(insVel, 0, 7.5f);

            insVel = Vector3.Magnitude(newp - oldp) / Time.deltaTime;

            yield return null;
        }

        speed = 0;
        avgVel = 0;
        insVel = 0;

    }


}
