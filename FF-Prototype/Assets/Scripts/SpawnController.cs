using UnityEngine;
using System.Collections;
using UnityEngine.UI;
 
public class SpawnController : MonoBehaviour
{
    
    [SerializeField]
    GameObject prefab;
    [SerializeField]
    GameObject UIPrefab;
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

            if (UIPrefab != null)
            {
                GameObject ap = Instantiate(UIPrefab) as GameObject;
                var buttons = ap.GetComponentsInChildren<Button>();
                UnitMovement um = pawn.GetComponent<UnitMovement>();
                foreach (var b in buttons)
                {
                    if (b.name.Contains("Attack"))
                    {
                        b.onClick.AddListener(
                          delegate { um.Action(GameObject.Find("EnemySpawn").transform); }
                          );
                    }
                    if(b.name.Contains("Endturn"))
                        b.onClick.AddListener(
                          delegate { um.Action(GameObject.Find("PlayerSpawn").transform); }
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
