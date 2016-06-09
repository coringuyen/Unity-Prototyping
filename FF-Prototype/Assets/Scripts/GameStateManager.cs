using UnityEngine;
using System.Collections;

public class GameStateManager : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();
    }
}
