using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System.Collections.Generic;

public class EnemyController : MonoBehaviour
{
    public enum AIState { Idle, Chase, Attack };

    [SerializeField]
    private int characterMaxHealth;

    [SerializeField]
    private float chaseThreshold;

    [SerializeField]
    private float attackThreshold;

    [SerializeField]
    private Vector3[] patrolLocations;

    private int _characterHealth;
    private int _patrolIndex;

    private bool _patrolLocationSet;

    private Slider _enemyHealthSlider;

    private NavMeshAgent _navMeshAgent;

    [SerializeField]
    private AIState _enemyState;

    private PlayerController _playerController;

    private Vector3 _enemyDestination;

    private void Awake()
    {
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        _enemyHealthSlider = this.transform.GetChild(1).GetChild(0).GetComponent<Slider>();

        _playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();

        _characterHealth = characterMaxHealth;
        DisplayEnemyHealth();
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

    public void TakeDamage(int amount)
    {
        _characterHealth -= amount;

        DisplayEnemyHealth();

        if(_characterHealth <= 0)
        {
            Destroy(this.gameObject);
        }
    }

    public void DisplayEnemyHealth()
    {
        _enemyHealthSlider.maxValue = characterMaxHealth;
        _enemyHealthSlider.value = _characterHealth;
    }

    public void SetEnemyDestination(Vector3 position)
    {
        _navMeshAgent.SetDestination(position);
        _enemyDestination = position;
    }

    public void EnemyAIStateMachine()
    {
        float distanceToPlayer = Vector3.Distance(this.transform.position, _playerController.transform.position);

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
    }
}
