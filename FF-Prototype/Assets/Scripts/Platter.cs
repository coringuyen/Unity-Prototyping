using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Platter : MonoBehaviour
{
    bool canPress = true;
    public List<GameObject> chars;
    [ContextMenu("CirclePlace")]
    void CirclePlace()
    {
        int numChars = chars.Count;

        float angle = 0.0f;

        foreach (GameObject go in chars)
        {
            Debug.Log("angle is " + angle);
            float zPos = Mathf.Cos(angle) * radius;//sin
            float xPos = Mathf.Sin(angle) * radius;//cos
            float yPos = 0.0f;

            Vector3 newPos = new Vector3(xPos, yPos, zPos);
            Debug.Log("Set position to " + newPos);
            go.transform.position = newPos;
            angle += rotAngle;
            go.transform.LookAt(transform.position);

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

    void Update()
    {
        if (canPress)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                canPress = false;
                dir = Direction.LEFT;
                StartCoroutine(RotatePlatter(dir));
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                canPress = false;
                dir = Direction.RIGHT;
                StartCoroutine(RotatePlatter(dir));
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log(transform.rotation.y);
            Debug.Log(transform.rotation.eulerAngles.y);
        }
    }

    enum Direction
    {
        LEFT,
        RIGHT,
    }

    Direction dir = Direction.RIGHT;

    IEnumerator RotatePlatter(Direction d)
    {
        
        float start = transform.rotation.eulerAngles.y;
        Debug.Log("Start rotation is " + start);
        float dest = 0.0f;
        if (d == Direction.RIGHT)
            dest = start + rotAngle * (180.0f / Mathf.PI);
        else if (d == Direction.LEFT)
            dest = start - rotAngle * (180.0f / Mathf.PI);
        float timer = 0.0f;
        Debug.Log("rotate from " + start + " to " + dest);
        while (start != dest)
        {
            timer += Time.deltaTime;
            start = Mathf.Lerp(start, dest, Time.deltaTime * 5.0f);
            transform.eulerAngles = new Vector3(0, start, 0);


            if (Mathf.Abs(dest) - Mathf.Abs(start) <= .5f)
            {
                start = dest;
                break;


            }
            print("in routine");
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
