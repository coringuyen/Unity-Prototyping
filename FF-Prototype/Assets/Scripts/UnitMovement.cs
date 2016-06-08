using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.Events;


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

    public void SetTarget(Transform t)
    {
        target = t;
    }

    public void SetOrigin(Transform t)
    {
        origin = t;
    }

    public LerpType lerpType;

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

    void Start()
    {        
        m_anim.SetBool("BattleReady", true);
    }

    void OnAnimatorMove()
    {
        if (m_insVel <= 0.001)
            m_insVel = 0;
        m_anim.SetFloat("Speed", m_insVel);
        m_anim.SetFloat("Direction", 1);
    }

    [SerializeField]
    bool isAttacking;
    [SerializeField]
    bool isMoving;
    /// <summary>
    /// callback function assigned to the ui buttons
    /// </summary>
    /// <param name="t"></param>
    public void Action(Transform t)
    {
        supercrit = true;
        if (!isMoving && !isAttacking)
        {
            StopAllCoroutines();
            StartCoroutine(ActionRoutine(t));
        }
    }

  
    public enum AnimationName
    {
        HiKick = 0,
        SpinKick = 1,
        Jab = 2,
        RisingPunch = 3,
        ScrewKick = 4,
        None = 5,


    }
    public AnimationName attackName;
    bool win = false;

    /// <summary>
    /// move then attack
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    IEnumerator ActionRoutine(Transform t)
    {

        //if we are at least 1 unit away we will walk
        if (Vector3.Distance(transform.position, t.position) > 2)
        {
            isMoving = true;
            yield return StartCoroutine(Move(t, animTime));
            isMoving = false;
        }

        if (t.name != "PlayerSpawn")
        {
            isAttacking = true;

            int randatt = UnityEngine.Random.Range(0, 5);
            randatt = 1;
            yield return StartCoroutine(Attack(((AnimationName)randatt).ToString()));

            yield return new WaitForSeconds(1);
            isAttacking = false;
            if (!win)
            {
                yield return StartCoroutine(Move(origin, 1));
                transform.forward *= -1;
            }
            if (win)
            {

                transform.LookAt(Camera.main.transform);
                Camera.main.GetComponent<UnityStandardAssets.Cameras.LookatTarget>().SetTarget(transform);
                FindObjectOfType<GameStateManager>().OnWin();
                m_anim.SetBool("BattleReady", false);
                m_anim.SetTrigger("Dance");
            }
        }
    }
    public bool supercrit = true;
    IEnumerator Attack(string animName)
    {

        m_anim.SetTrigger(animName);

        while (!m_anim.GetCurrentAnimatorStateInfo(0).IsName(animName))
        {
            yield return null;
        }


        float animNormalTime = m_anim.GetCurrentAnimatorStateInfo(0).normalizedTime;

        while (animNormalTime < .27f)
        {
            animNormalTime = m_anim.GetCurrentAnimatorStateInfo(0).normalizedTime;

            yield return null;
        }
        

        GetComponent<AudioSource>().Play();
        if (GetComponent<UnitMono>().Attack(target.GetComponent<SpawnController>().unit) < 1)        
            win = true;
        
        UnitMono.EventPlayerAttack.Invoke(GetComponent<UnitMono>());
        
        
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
                    t = Mathf.Sin(t * Mathf.PI * 0.5f);
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

        while (m_anim.IsInTransition(0))
            yield return null;

        m_avgVel = 0;

        m_insVel = 0;

    }
}
