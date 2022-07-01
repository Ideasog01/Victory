using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour
{
    public enum AIState { Idle, Chase, Attack, Disabled };

    [SerializeField]
    private int characterMaxHealth;

    [SerializeField]
    private float chaseThreshold;

    [SerializeField]
    private float attackThreshold;

    [SerializeField]
    private float stoppingThreshold;

    [SerializeField]
    private Vector3[] patrolLocations;

    [SerializeField]
    private float enemySpecialCharge;

    [SerializeField]
    private int enemyXp;

    private int _characterHealth;
    private int _patrolIndex;

    private bool _patrolLocationSet;

    private Slider _enemyHealthSlider;

    private NavMeshAgent _navMeshAgent;

    private Animator _enemyAnimator; 

    private AIState _enemyState;

    private PlayerController _playerController;

    private Vector3 _enemyDestination;

    private Vector3 _targetRotation;

    private float _oldMovementSpeed;

    private Rigidbody _enemyRb;
    private Vector3 _appliedForce;
    private float _forceTimer;
    private bool _forceActivated;


    private List<StatusEffect> enemyStatusEffects = new List<StatusEffect>();

    private void Awake()
    {
        _enemyRb = this.GetComponent<Rigidbody>();
        _enemyAnimator = this.GetComponent<Animator>();
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        _enemyHealthSlider = this.transform.GetChild(1).GetChild(0).GetComponent<Slider>();

        _playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();
        _oldMovementSpeed = _navMeshAgent.speed;
        _characterHealth = characterMaxHealth;
        DisplayEnemyHealth();
    }

    private void FixedUpdate()
    {
        if(_forceActivated)
        {
            if (_forceTimer > 0)
            {
                _enemyRb.AddForce(_appliedForce);
                _navMeshAgent.enabled = false;
                _forceTimer -= Time.deltaTime * 1;
            }
            else
            {
                _enemyRb.velocity = Vector3.zero;
                _enemyRb.angularVelocity = Vector3.zero;
                _navMeshAgent.enabled = true;
                _forceActivated = false;
            }
        }
        
    }

    public Animator EnemyAnimator
    {
        get { return _enemyAnimator; }
    }

    public int CharacterMaxHealth
    {
        get { return characterMaxHealth; }
    }

    public int CharacterHealth
    {
        get { return _characterHealth; }
    }
    
    public AIState EnemyState
    {
        set { _enemyState = value; }
        get { return _enemyState; }
    }

    public PlayerController Player
    {
        get { return _playerController; }
    }

    public Vector3[] PatrolLocations
    {
        get { return patrolLocations; }
    }

    public int PatrolIndex
    {
        get { return _patrolIndex; }
        set { PatrolIndex = value; }
    }

    public bool PatrolLocationSet
    {
        get { return _patrolLocationSet; }
        set { _patrolLocationSet = value; }
    }

    public Vector3 EnemyDestination
    {
        get { return _enemyDestination; }
    }

    public NavMeshAgent EnemyNavMeshAgent
    {
        get { return _navMeshAgent; }
    }

    public void SetMovementSpeed(float amount)
    {
        if(amount == 0)
        {
            if(_navMeshAgent != null)
            {
                _navMeshAgent.speed = _oldMovementSpeed;
            }
        }
        else
        {
            if (_navMeshAgent != null)
            {
                _oldMovementSpeed = _navMeshAgent.speed;
                _navMeshAgent.speed -= amount;
            }
        }
    }

    public void ApplyForce(Vector3 direction, float force, float duration)
    {
        _forceTimer = duration;
        _appliedForce = direction * force;
        _forceActivated = true;
    }

    public void TakeDamage(int amount)
    {
        if(this.gameObject != null)
        {
            _characterHealth -= amount;

            DisplayEnemyHealth();

            if (_characterHealth <= 0)
            {
                if (PlayerController.nearbyEnemyList.Contains(this))
                {
                    PlayerController.nearbyEnemyList.Remove(this);

                    if (PlayerController.currentTarget == this)
                    {
                        if (PlayerController.nearbyEnemyList.Count > 0)
                        {
                            PlayerController.currentTarget = PlayerController.nearbyEnemyList[0];
                        }
                    }
                }

                GameManager.playerController.AddExperience(enemyXp);
                GameManager.playerController.AddSpecialCharge(enemySpecialCharge);

                Destroy(this.gameObject);
            }
        }
    }

    public void AddEffect(StatusEffect statusEffect)
    {
        bool effectFound = false;

        foreach(StatusEffect effect in enemyStatusEffects)
        {
            if(!effect.isActiveAndEnabled)
            {
                effect.gameObject.SetActive(true);
                effect.SetEffectProperties(statusEffect.ApplyTime, statusEffect.DurationTime, statusEffect.Effect, statusEffect.EffectAmount);
                effect.ApplyEffect(this);

                effectFound = true;
                break;
            }
        }

        if(!effectFound)
        {
            StatusEffect effect = Instantiate(statusEffect, this.transform.position, Quaternion.identity);
            effect.transform.SetParent(this.transform);
            effect.ApplyEffect(this);
            enemyStatusEffects.Add(effect);
        }
    }

    public void DisplayEnemyHealth()
    {
        _enemyHealthSlider.maxValue = characterMaxHealth;
        _enemyHealthSlider.value = _characterHealth;
    }

    public void SetEnemyDestination(Vector3 position)
    {
        if(_navMeshAgent.isActiveAndEnabled)
        {
            _navMeshAgent.SetDestination(position);
            _enemyDestination = position;
        }
    }

    public void EnemyAIStateMachine()
    {
        float distanceToPlayer = Vector3.Distance(this.transform.position, _playerController.transform.position);

        if(_enemyState == AIState.Disabled)
        {
            _navMeshAgent.SetDestination(this.transform.position);
            return;
        }

        if(distanceToPlayer < stoppingThreshold)
        {
            if(_navMeshAgent.isActiveAndEnabled)
            {
                SetEnemyDestination(this.transform.position);
            }
        }

        if(distanceToPlayer < attackThreshold)
        {
            _enemyState = AIState.Attack;
        }
        else if(distanceToPlayer < chaseThreshold)
        {
            _enemyState = AIState.Chase;
        }
        else
        {
            _enemyState = AIState.Idle;
        }

        if (EnemyState == AIState.Attack || EnemyState == AIState.Chase)
        {
            FacePlayer();
        }
    }

    private void FacePlayer()
    {
        Quaternion rotationAngle = Quaternion.LookRotation(Player.transform.position - this.transform.position);
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, rotationAngle, Time.deltaTime * 180);
    }
}
