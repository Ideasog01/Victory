using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CivilianManager : MonoBehaviour
{
    public static CivilianController selectedCivilian;

    private Camera _gameCamera;

    private void Awake()
    {
        _gameCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    public void CivilianInteract()
    {
        if(selectedCivilian != null)
        {
            RaycastHit hit;

            if(Physics.Raycast(_gameCamera.ScreenPointToRay(BuildManager.mousePosition), out hit))
            {
                if(hit.collider.CompareTag("Ground"))
                {
                    selectedCivilian.SetCivilianDestination(hit.point);
                    selectedCivilian = null;
                    Debug.Log("Civ Destination Set!");
                }
            }
        }
        else
        {
            RaycastHit hit;

            if (Physics.Raycast(_gameCamera.ScreenPointToRay(BuildManager.mousePosition), out hit))
            {
                if(hit.collider.CompareTag("Civilian"))
                {
                    selectedCivilian = hit.collider.GetComponent<CivilianController>();
                    Debug.Log("Civ Set!");
                }
            }
        }
    }
}
