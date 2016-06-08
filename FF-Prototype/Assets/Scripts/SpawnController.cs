using UnityEngine;
using System.Collections;
using UnityEngine.UI;
 
public class SpawnController : MonoBehaviour
{
    
    [SerializeField]
    GameObject prefab;
    [SerializeField]
    GameObject UIPrefab;

    [SerializeField]
    Transform target;
    
    private UnitMono m_unit;
    public UnitMono unit
    {
        get { return m_unit; }
    }
    public bool faceRight;
    public void Setup()
    {
        if (prefab != null)
        {
            GameObject pawn = Instantiate(prefab, transform.localPosition, Quaternion.identity) as GameObject;
            pawn.transform.SetParent(transform);
            m_unit = pawn.GetComponent<UnitMono>();
            UnitMovement um = pawn.GetComponent<UnitMovement>();
            um.SetOrigin(transform);
            um.SetTarget(target);
            if (UIPrefab != null)
            {
                GameObject ap = Instantiate(UIPrefab) as GameObject;
                var buttons = ap.GetComponentsInChildren<Button>();
                
                foreach (var b in buttons)
                {
                    if (b.name.Contains("Attack"))
                    {
                        b.onClick.AddListener(
                          delegate { um.Action(GameObject.Find("EnemySpawn").transform); }
                          );
                    }
                    else if(b.name.Contains("Endturn"))
                        b.onClick.AddListener(
                          delegate { print("u pressed endturn"); }
                          );
                }

            }
            
            if (pawn.GetComponent<UnitMono>() == null)
                throw new UnityException("SpawnController is trying to spawn an object that does not have a unit mono");
            m_unit = pawn.GetComponent<UnitMono>();
            if (faceRight)
                pawn.transform.forward = Vector3.right;
            else
                pawn.transform.forward = Vector3.left;
        }        
    }
    

 

}
