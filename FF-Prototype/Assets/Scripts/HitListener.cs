﻿using UnityEngine;
using System.Collections;
using System;

public class HitListener : MonoBehaviour
{
    [SerializeField]
    GameObject UI;
    [SerializeField]
    GameObject m_mainCam;

    [SerializeField]
    GameObject m_fadeImage;

    [SerializeField]
    AnimationCurve m_ac;

    Animator m_anim;

    public float slowDuration;
    public float fadeDuration;
    static int actionState = Animator.StringToHash("Base.actionshot");
    void Awake()
    {
        m_anim = GetComponent<Animator>();
        UI = GameObject.Find("CombatPanel");
    }
    void Start()
    {
        HitBoxTrigger.EventHit.AddListener(PlayAnim);
    }


    void PlayAnim()
    {
        StopAllCoroutines();

        StartCoroutine(StartSequence());
    }

    //hit
    //stoptime
    //rotate camera
    //enabletime
    //fade
    //enablecam

    IEnumerator StartSequence()
    {
        StartCoroutine(Fade(fadeDuration, 0));
        UI.SetActive(false);
        m_anim.SetBool("actionshot", true);
        
        Func<bool> animdone = () =>
        {
            return (
                m_anim.GetCurrentAnimatorStateInfo(0).IsName("actionshot")
                &&
                m_anim.GetCurrentAnimatorStateInfo(0).normalizedTime <= m_anim.GetCurrentAnimatorClipInfo(0).Length);
        };


        m_mainCam.SetActive(false);
        m_anim.speed = 1 / slowDuration;
        Time.timeScale = 0;
        yield return new WaitUntil(animdone);        
        yield return StartCoroutine(SlowMo(slowDuration));
        yield return StartCoroutine(Fade(fadeDuration, 1));


        m_anim.SetBool("actionshot", false);
        //account for if the curve doesn't end at 1
        if (Time.timeScale < 1) Time.timeScale = 1;


        m_mainCam.SetActive(true);
        UI.SetActive(true);
        yield return StartCoroutine(Fade(fadeDuration, 0));
        StopAllCoroutines();
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


    }

    IEnumerator Fade(float duration, float to)
    {
        float elapsedTime = 0;
        m_fadeImage.SetActive(true);
        while (elapsedTime < duration)
        {
            elapsedTime += Time.fixedDeltaTime;

            float t = (to > 0) ? elapsedTime / duration : 1 - (elapsedTime / duration);
            //lerp the alpha to full 
            m_fadeImage.GetComponent<UnityEngine.UI.Image>().color = new Color(0, 0, 0, t);

            yield return null;
        }

        

    }


}
