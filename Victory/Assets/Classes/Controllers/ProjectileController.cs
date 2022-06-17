using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    [SerializeField]
    private Vector3 projectileMovementSpeed;

    [SerializeField]
    private Transform _projectileTarget;

    [SerializeField]
    private Vector3 projectileRotationSpeed;

    [SerializeField]
    private string collisionTag;

    [SerializeField]
    private int projectileDamage;

    [SerializeField]
    private float projectileDuration;

    [SerializeField]
    private bool rotateMesh;

    [SerializeField]
    private bool _isActive = true;

    private float _projectileTimer;

    public Vector3 ProjectileMovementSpeed
    {
        set { projectileMovementSpeed = value; }
        get { return projectileMovementSpeed; }
    }

    public int ProjectileDamage
    {
        set { projectileDamage = value; }
        get { return projectileDamage; }
    }

    public void InitialiseProjectile()
    {
        _projectileTimer = projectileDuration;
        _isActive = true;
    }

    private void Update()
    {
        if(_isActive)
        {
            if (_projectileTarget != null)
            {
                TargetMovement();
            }
            else
            {
                ForwardMovement();
            }

            if(rotateMesh)
            {
                this.transform.GetChild(0).Rotate(projectileRotationSpeed * Time.deltaTime);
            }
            else
            {
                this.transform.Rotate(projectileRotationSpeed * Time.deltaTime);
            }
        }

        if (_projectileTimer > 0)
        {
            _projectileTimer -= Time.deltaTime * 1;
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

    private void ForwardMovement()
    {
        this.transform.position += this.transform.TransformDirection(Vector3.forward * projectileMovementSpeed.x * Time.deltaTime);
    }

    private void TargetMovement()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, _projectileTarget.position, Time.deltaTime * projectileMovementSpeed.x);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(_isActive)
        {
            if (other.CompareTag(collisionTag))
            {
                if (collisionTag == "Enemy")
                {
                    other.GetComponent<EnemyController>().TakeDamage(projectileDamage);
                    this.gameObject.SetActive(false);
                }
            }

            if(!other.CompareTag("Player"))
            {
                _isActive = false;
            }
        }
        
    }
}
