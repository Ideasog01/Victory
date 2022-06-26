using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcherController : PlayerController
{
    [SerializeField]
    private LayerMask enemyLayer;

    [SerializeField]
    private Transform hunterTrap;

    [Header("Strafe Settings")]

    [SerializeField]
    private float dashSpeed;

    [SerializeField]
    private float dashDuration;

    private float _dashTimer;

    public void UpdateArcher()
    {
        if (_dashTimer > 0)
        {
            _dashTimer -= Time.deltaTime * 1;
            PlayerCharacterController.Move(new Vector3(movementInput.y, 0, -movementInput.x) * Time.deltaTime * dashSpeed);
        }
    }

    public void ActivateDash()
    {
        _dashTimer = dashDuration;
    }

    public void SpawnHunterTrap()
    {
        Instantiate(hunterTrap, this.transform.position + new Vector3(0, -0.5f, 0), this.transform.rotation);
    }
}
