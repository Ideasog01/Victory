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

    private List<StatusEffect> enemyStatusEffects = new List<StatusEffect>();

    private void Awake()
    {
        _enemyAnimator = this.GetComponent<Animator>();
        _navMeshAgent = this.GetComponent<NavMeshAgent>();
        _enemyHealthSlider = this.transform.GetChild(1).GetChild(0).GetComponent<Slider>();

        _playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();

        _characterHealth = characterMaxHealth;
        DisplayEnemyHealth();
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

    public void TakeDamage(int amount)
    {
        _characterHealth -= amount;

        DisplayEnemyHealth();

        if(_characterHealth <= 0)
        {
            if(PlayerController.nearbyEnemyList.Contains(this))
            {
                PlayerController.nearbyEnemyList.Remove(this);

                if(PlayerController.currentTarget == this)
                {
                    if(PlayerController.nearbyEnemyList.Count > 0)
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
        _navMeshAgent.SetDestination(position);
        _enemyDestination = position;
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
            SetEnemyDestination(this.transform.position);
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
