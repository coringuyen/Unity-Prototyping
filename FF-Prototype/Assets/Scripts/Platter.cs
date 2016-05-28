using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Platter : MonoBehaviour
{
    bool canPress = true;
    public List<GameObject> chars;
    public GameObject currentCharacter;
    
    void Awake()
    {
        if (currentCharacter == null)
        {

            currentCharacter = chars[charsIndex];
        }
    }
    [ContextMenu("CirclePlace")]
    void CirclePlace()
    {
        

        float angle = 0.0f;

        foreach (GameObject go in chars)
        {         
            float zPos = Mathf.Cos(angle) * radius;//sin
            float xPos = Mathf.Sin(angle) * radius;//cos
            float yPos = 0.0f;

            Vector3 newPos = new Vector3(xPos, yPos, zPos);
         
            go.transform.position = newPos;
            angle += rotAngle;
			go.transform.LookAt(Vector3.zero);

        }
    }

    [ContextMenu("Reset")]
    void Reset()
    {
        chars.Clear();
        foreach (Transform t in transform)
            chars.Add(t.gameObject);
        foreach (GameObject go in chars)
            go.transform.position = Vector3.zero;
    }
    private int charsIndex = 0;
    int getIndex(Direction d)
    { 
        if (d == Direction.LEFT)
        {            
            charsIndex -= 1;
            if (charsIndex < 0)
                charsIndex = chars.Count - 1;
        }
        else if (d == Direction.RIGHT)
        {
            charsIndex += 1;
            if (charsIndex > chars.Count - 1)
                charsIndex = 0;
        }
        return charsIndex;
    }

    void Update()
    {
        if (canPress)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow) || InputManager.Left )
            {                
                canPress = false;
                currentCharacter = SetChar(Direction.LEFT);
                InputManager.ActiveModel = currentCharacter;
                StartCoroutine(RotatePlatter(Direction.LEFT));
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow) ||InputManager.Right)
            {
                canPress = false;                
                currentCharacter = SetChar(Direction.RIGHT);
                InputManager.ActiveModel = currentCharacter;
                StartCoroutine(RotatePlatter(Direction.RIGHT));
            }
        }
 
    }
    GameObject SetChar(Direction d)
    {
        
        return chars[getIndex(d)];
        
    }
    enum Direction
    {
        LEFT,
        RIGHT,
    }
     

    IEnumerator RotatePlatter(Direction d)
    {
        
        float start = transform.rotation.eulerAngles.y;
        Debug.Log("Start rotation is " + start);
        float dest = 0.0f;
        if (d == Direction.RIGHT)
            dest = start - rotAngle * (180.0f / Mathf.PI);
        if (d == Direction.LEFT)
            dest = start + rotAngle * (180.0f / Mathf.PI);
        //Debug.Log("rotate from " + start + " to " + dest);
        while (start != dest)
        {
            start = Mathf.LerpAngle(start, dest, Time.deltaTime * 5.0f);
            transform.eulerAngles = new Vector3(0, start, 0);

            if (Mathf.Abs(dest-start) <= .5f)
            {
                start = dest;
                break;
            }
            //print("in routine");
            yield return null;
        }

        transform.eulerAngles = new Vector3(0, dest, 0);
        print("coroutine done");
        canPress = true;

        yield return null;
    }

    void Start()
    {
        //3.14 radians = 180 degrees
        //3.14 radians / 180 = 1 degree
        //1 radian = 180 degrees / 3.14
        //pi rads = 180 degrees
        Debug.Log(rotAngle * (180.0f / Mathf.PI));
    }

    float rotAngle
    {
        get
        {
            return 2 * Mathf.PI / chars.Count;
        }
    }

    public float radius = 5.0f;
}
