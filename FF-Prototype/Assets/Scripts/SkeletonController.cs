using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
public class SkeletonController : MonoBehaviour {
    Animator m_anim;
    [SerializeField]
    AnimationCurve m_ac;
    void Awake()
    {
        m_anim = GetComponent<Animator>();
    }
	// Use this for initialization
	void Start ()
    {
        HitBoxTrigger.EventHit.AddListener(OnHit);
	}
    public float slowtime = 15;
	void OnHit()
    {
        StopAllCoroutines();
        m_anim.SetTrigger("hit");        
        StartCoroutine(SlowMo(slowtime));
        //GetComponent<AudioSource>().Play();
    }

    IEnumerator SlowMo(float totalTime)
    {
        float elapsedTime = 0;
        while(elapsedTime < totalTime)
        {
            elapsedTime += Time.fixedDeltaTime;
            float t = elapsedTime / totalTime;         
            Time.timeScale = m_ac.Evaluate(t);
         
            yield return null;
        }
    }
}
