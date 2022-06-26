using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public enum InteractableType { Resource, Chest, Dialogue, Weapon, SpecialItem };

    [SerializeField]
    private InteractableType interactableType;

    [SerializeField]
    private float distanceThreshold = 5f;

    private PlayerController _playerController;

    private bool _isActive = true;

    public float DistanceThreshold
    {
        get { return distanceThreshold; }
    }

    public PlayerController Player
    {
        get { return _playerController; }
    }

    public void Interact()
    {
        if(_isActive)
        {
            if (interactableType == InteractableType.Resource)
            {
                this.GetComponent<ResourceController>().PickupItem();
            }

            if (interactableType == InteractableType.Chest)
            {
                this.GetComponent<ChestController>().OpenChest();
            }

            if(interactableType == InteractableType.Dialogue)
            {
                this.GetComponent<DialogueTrigger>().DialogueStart();
            }

            if(interactableType == InteractableType.Weapon)
            {
                this.GetComponent<WeaponInteractive>().EquipWeapon();
            }

            if(interactableType == InteractableType.SpecialItem)
            {
                this.GetComponent<SpecialItemInteractive>().EquipSpecialItem();
            }

            _isActive = false;
        }
    }

    private void Awake()
    {
        _playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();
    }

    private void Update()
    {
        if(_isActive)
        {
            float distanceToPlayer = Vector3.Distance(this.transform.position, _playerController.transform.position);

            if (PlayerController.nearbyInteractable != this)
            {
                if (distanceToPlayer < distanceThreshold)
                {
                    GameManager.playerInterface.DisplayInteractPrompt(true);
                    PlayerController.nearbyInteractable = this;
                }
            }
            else
            {
                if (distanceToPlayer >= distanceThreshold)
                {
                    GameManager.playerInterface.DisplayInteractPrompt(false);
                    PlayerController.nearbyInteractable = null;
                }
            }
        }
    }
}
