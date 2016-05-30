using UnityEngine;
using System.Collections.Generic;
using System;

public class PartyPlacement : MonoBehaviour
{
    public float offset;
    [SerializeField]
    List<GameObject> units;
    // Use this for initialization
    
    [ContextMenu("Spread")]
    public void Spread()
    {
   
        
        foreach (Transform t in transform)
        {
            t.transform.localPosition = Vector3.zero;
            t.transform.localRotation = Quaternion.identity;
        }
        units.Clear();
        int num = 0;
        foreach (Transform t in transform)
        {
            if (t.gameObject.activeSelf)
            {
                units.Add(t.gameObject);
                t.position = new Vector3(t.position.x + (num * offset), t.position.y, t.position.z);
                num++;
            }
        }
                
    }
 
}
