using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceController nearbyResource;

    private PlayerController _playerController;

    private void Awake()
    {
        _playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();
    }

    private void Update()
    {
        if(nearbyResource != null)
        {
            float distanceToPlayer = Vector3.Distance(nearbyResource.transform.position, _playerController.transform.position);

            if(distanceToPlayer >= nearbyResource.DistanceThreshold)
            {
                nearbyResource = null;
            }
            else
            {
                //Display Interact Prompt
            }
        }
    }

    public void Interact()
    {
        if(nearbyResource != null)
        {
            Debug.Log("Resource Added!");
            nearbyResource.gameObject.SetActive(false);
            nearbyResource = null;
        }
    }
}
