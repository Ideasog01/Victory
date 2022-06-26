using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialItemController : MonoBehaviour
{

    [SerializeField]
    private SpecialItem specialItem;

    private PlayerController _playerController;

    private float _activeTimer;

    private float _cooldownTimer;

    private void Awake()
    {
        _playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();
        _activeTimer = specialItem.itemDuration;
    }

    private void Update()
    {
        ItemBehaviour();
    }

    private void ItemBehaviour()
    {
        if(specialItem.itemType == SpecialItem.ItemType.HealingWell)
        {
            if(IsPlayerNear())
            {
                if(_cooldownTimer <= 0)
                {
                    _playerController.Heal(specialItem.itemApplyAmount);
                    _cooldownTimer = specialItem.itemCooldown;
                }
            }
        }

        if(specialItem.itemType == SpecialItem.ItemType.FireTotem)
        {
            Collider[] colliderArray = Physics.OverlapSphere(this.transform.position, 10);

            foreach(Collider collider in colliderArray)
            {
                if(collider.CompareTag("Enemy"))
                {
                    this.transform.LookAt(collider.transform.position);
                }
            }

            if(_cooldownTimer <= 0)
            {
                GameManager.projectileManager.SpawnProjectile(specialItem.itemPrefab, this.transform.position, this.transform.localEulerAngles, 10, 10);
                _cooldownTimer = specialItem.itemCooldown;
            }
        }

        if(_cooldownTimer > 0)
        {
            _cooldownTimer -= Time.deltaTime * 1;
        }

        if(_activeTimer > 0)
        {
            _activeTimer -= Time.deltaTime * 1;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private bool IsPlayerNear()
    {
        float distanceToPlayer = Vector3.Distance(this.transform.position, _playerController.transform.position);

        if(distanceToPlayer < specialItem.itemActiveDistance)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
