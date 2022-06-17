using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceController : MonoBehaviour
{
    [SerializeField]
    private float distanceThreshold;

    [SerializeField]
    private Item resourceItem;

    private PlayerController _playerController;

    public float DistanceThreshold
    {
        get { return distanceThreshold; }
    }

    public Item ResourceItem
    {
        get { return resourceItem; }
    }

    private void Awake()
    {
        _playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();
    }

    private void Update()
    {
        if(ResourceManager.nearbyResource != this)
        {
            float distanceToPlayer = Vector3.Distance(this.transform.position, _playerController.transform.position);

            if (distanceToPlayer < distanceThreshold)
            {
                ResourceManager.nearbyResource = this;
            }
        }
    }
}
