using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AbilitiesGUI : MonoBehaviour
{
    public Button attack, defend, endTurn;
    public Text aveVel, insVel;
    private GameObject player;
    public Transform Origin;
    void Start ()
    {
        StartCoroutine(FindPlayer());
    }

    IEnumerator FindPlayer()
    {
        if(player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        Transform target = player.GetComponent<UnitMovement>().target;

        attack.onClick.AddListener(delegate { player.GetComponent<UnitMovement>().Action(target); });
        endTurn.onClick.AddListener(delegate { player.GetComponent<UnitMovement>().Action(Origin); });

        //player.GetComponent<UnitMovement>().avgVelocity = aveVel;
        //player.GetComponent<UnitMovement>().insVelocity = insVel;

        yield return new WaitForSeconds(0.0f);
    }
}
