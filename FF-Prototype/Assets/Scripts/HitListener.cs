using UnityEngine;
using System.Collections;

public class HitListener : MonoBehaviour {
    public GameObject mainCam;

	// Use this for initialization
	void Start () {
        HitBoxTrigger.EventHit.AddListener(PlayAnim);
	}
	

	void PlayAnim()
    {
        mainCam.SetActive(false);        
        StartCoroutine(WaitForDone());
    }

    IEnumerator WaitForDone()
    {
        GetComponent<Animation>().Play();
        float start = 0;
        float animLength = GetComponent<Animation>().clip.length;               
        
        while (start < animLength )
        {
            start += Time.fixedDeltaTime;
            float timeRem = animLength - start;
            Debug.Log(timeRem);

            yield return null;
        }

        
        
        yield return null;
    }

    [SerializeField]
    GameObject fadeimage;
    [SerializeField]
    float fadeTime = 1.5f;
    IEnumerator FadeOut(float ft)
    {        
        float ctime = 0;
        fadeimage.GetComponent<UnityEngine.UI.Image>().enabled = true;
        while (ctime < ft)
        {
            ctime += Time.fixedDeltaTime;
            fadeimage.GetComponent<UnityEngine.UI.Image>().color = new Color(0, 0, 0, ctime / ft);

            yield return null;
        }
        mainCam.SetActive(true);
        fadeimage.GetComponent<UnityEngine.UI.Image>().color = new Color(0, 0, 0, 0);
        fadeimage.GetComponent<UnityEngine.UI.Image>().enabled = false;


    }
}
