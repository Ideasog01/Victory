using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonExit : MonoBehaviour
{
    [SerializeField]
    private int exitSceneIndex;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.exitSceneIndex = exitSceneIndex;
            GameManager.playerInterface.DisplayDungeonExitScreen(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.playerInterface.DisplayDungeonExitScreen(false);
        }
    }
}
