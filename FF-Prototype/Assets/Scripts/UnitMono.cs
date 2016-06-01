using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UnitMono : MonoBehaviour
{
	public Text avgVelocity;
	public Text insVelocity;
	[HideInInspector]
	public int health;
	[HideInInspector]
	public int resource;
	[HideInInspector]
	public string unitName;

	public Unit unit;
	public Transform target;
    

	public Vector3 origin;
	[SerializeField]
	Animator m_anim;
	Rigidbody m_rigidBody;

	void Awake ()
	{
		Unit unit = new Unit (unitName, health, resource);
		m_anim = GetComponent<Animator> ();
		m_rigidBody = GetComponent<Rigidbody> ();
		origin = transform.position;
		Debug.Log (Time.timeScale);

	}

	bool slow = false;

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.DownArrow))
			Time.timeScale -= .1f;
		if (Input.GetKeyDown (KeyCode.UpArrow))
			Time.timeScale += .1f;
	
		if (Input.GetKeyDown (KeyCode.R))
		{
			StopAllCoroutines ();
			StartCoroutine (Move (target.position, 2));
		}
		if (Input.GetKeyDown ((KeyCode.E)))
		{

			StopAllCoroutines ();
			StartCoroutine (Move (origin, 3));           
		}
		avgVelocity.text = "Average Velocity: " + avgVel.ToString ();
		insVelocity.text = "Instant Velocity: " + insVel.ToString ();

	}

	void OnAnimatorMove ()
	{
		m_anim.SetFloat ("Speed", insVel / 7.5f);

	}


	int safecounter = 0;

	[SerializeField]
	float speed;

	[SerializeField]
	float velocity;
    
	//When t = 0 returns a. When t = 1 returns b. When t = 0.5 returns the point midway between a and b.
	IEnumerator Move (Vector3 dest, float totalTime)
	{
		Vector3 start = transform.position;	 
	 
		float elapsedTime = 0;
		float startTime = 0;
		while (elapsedTime < totalTime)
		{ 
			
			elapsedTime += Time.deltaTime;

			float t = elapsedTime / totalTime;
			Vector3 oldp = transform.position;

			//set the position to a fraction of
			//smoothlerp
			//transform.position = Vector3.Lerp (start, dest, t * t * t * (t * (6f*t - 15f) + 10f));
			//exponentiallerp

			transform.position = Vector3.Lerp (start, dest, t * t);
			Vector3 newp = transform.position;

			avgVel = Vector3.Magnitude (dest - start) / totalTime;
			Mathf.Clamp (insVel, 0, 7.5f);
			insVel = Vector3.Magnitude (newp - oldp) / Time.deltaTime;


			yield return null;
		}
		speed = 0;
		avgVel = 0;
		insVel = 0;
        
	}

	float avgVel;
	float insVel;
}
