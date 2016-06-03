using UnityEngine;
using System.Collections;

public class HitListener : MonoBehaviour {
    public GameObject mainCam;

	// Use this for initialization
	void Start () {
        HitBoxTrigger.EventHit.AddListener(PlayAnim);
	}
	IEnumerator WaitForDone()
    {
        float animLength = GetComponent<Animation>().clip.length;
        float start = 0;
        while(start < animLength)
        {
            start += Time.deltaTime;
            yield return null;
        }
        mainCam.SetActive(true);
        yield return null;
    }

	void PlayAnim()
    {
        mainCam.SetActive(false);
        GetComponent<Animation>().Play();
        StartCoroutine(WaitForDone());
    }

    void Fade()
    {
        StartCoroutine(FadeOut());
    }
    [SerializeField]
    GameObject fadeimage;
    IEnumerator FadeOut()
    {
        float fadeTime = 2.5f;
        float ctime = 0;
        fadeimage.GetComponent<UnityEngine.UI.Image>().enabled = true;
        while (ctime < fadeTime)
        {
            ctime += Time.fixedDeltaTime;
            fadeimage.GetComponent<UnityEngine.UI.Image>().color = new Color(0, 0, 0, ctime / fadeTime);
                
            yield return null;
        }
        fadeimage.GetComponent<UnityEngine.UI.Image>().enabled = false;
        fadeimage.GetComponent<UnityEngine.UI.Image>().color = new Color(0, 0, 0, 1);
    }
}
