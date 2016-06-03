using UnityEngine;
using System.Collections;

public class HitListener : MonoBehaviour
{

    [SerializeField]
    GameObject m_mainCam;

    [SerializeField]
    GameObject m_fadeImage;

    [SerializeField]
    AnimationCurve m_ac;

    Animator m_anim;

    public float slowDuration = 5f;
    public float fadeDuration;

    void Awake()
    {
        m_anim = GetComponent<Animator>();
    }
    void Start ()
    {
        HitBoxTrigger.EventHit.AddListener(PlayAnim);
	}

    
	void PlayAnim()
    {
        StopAllCoroutines();
        m_mainCam.SetActive(false);
        m_anim.SetBool("actionshot", true);
        m_anim.speed = 1 / slowDuration;        
        StartCoroutine(SlowMo(slowDuration));
    }
    static int actionState = Animator.StringToHash("Base.actionshot");
    void ResetFade()
    {
        m_fadeImage.SetActive(false);
        //reset it back to normal
        m_fadeImage.GetComponent<UnityEngine.UI.Image>().color = new Color(0, 0, 0, 0);
    }
    //hit
    //stoptime
    //rotate camera
    //enabletime
    //fade
    //enablecam
    void Update()
    {
        if (m_anim.GetCurrentAnimatorStateInfo(0).IsName("actionshot"))
            Debug.Log("is action");
        if (m_anim.GetCurrentAnimatorStateInfo(0).IsName("idle"))
            Debug.Log("is idle");
    }
 
    IEnumerator SlowMo(float duration)
    {
        float elapsedTime = 0;
        while (elapsedTime < duration)
        {
            elapsedTime += Time.fixedDeltaTime;
            float t = elapsedTime / duration;
            Time.timeScale = m_ac.Evaluate(t);

            yield return null;
        }
        yield return StartCoroutine(FadeOut(1.5f));
        m_mainCam.SetActive(true);
        Time.timeScale = 1;
    }

    IEnumerator FadeOut(float duration)
    {        
        float ctime = 0;
        m_fadeImage.SetActive(true);
        while (ctime < duration)
        {
            ctime += Time.fixedDeltaTime;
            
            float p = ctime / duration;
            //lerp the alpha to full 
            m_fadeImage.GetComponent<UnityEngine.UI.Image>().color = new Color(0, 0, 0, p);
                            
            yield return null;
        }

        ResetFade();
        m_anim.SetBool("actionshot", false);
    }


}
