using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static Vector2 movementInput;

    [SerializeField]
    private float jumpForce;

    [SerializeField]
    private float movementSpeed;

    [SerializeField]
    private LayerMask groundLayer;

    private Rigidbody _playerRb;
    private Transform _groundCheck;

    private bool _doubleJumpUsed;

    private void Awake()
    {
        _playerRb = this.GetComponent<Rigidbody>();
        _groundCheck = this.transform.GetChild(1);
    }

    private void FixedUpdate()
    {
        if(movementInput.x != 0)
        {
            MoveRight(movementInput.x);
        }
    }

    public void Jump()
    {
        if(IsGrounded())
        {
            _playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            _doubleJumpUsed = false;
        }
        else if(!_doubleJumpUsed)
        {
            _playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            _doubleJumpUsed = true;
        }
        
    }

    private void MoveRight(float value)
    {
        Vector3 direction = new Vector3(1, this.transform.position.y, this.transform.position.z);
        _playerRb.MovePosition(transform.position + new Vector3(value, 0, 0) * Time.deltaTime * movementSpeed);
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(_groundCheck.position, 0.5f, groundLayer);
    }
}
