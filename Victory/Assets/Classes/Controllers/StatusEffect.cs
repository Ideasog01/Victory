using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusEffect : MonoBehaviour
{
    public enum EffectName { Poison };

    [SerializeField]
    private float applyTime;

    [SerializeField]
    private float durationTime;

    [SerializeField]
    private EffectName effectName;

    [SerializeField]
    private int effectAmount;

    private EnemyController _appliedEnemy;

    private float _applyTimer;

    private float _durationTimer;

    public float ApplyTime
    {
        get { return applyTime; }
    }

    public float DurationTime
    {
        get { return durationTime; }
    }

    public EffectName Effect
    {
        get { return effectName; }
    }

    public int EffectAmount
    {
        get { return effectAmount; }
    }

    private void Update()
    {
        if(_appliedEnemy != null)
        {
            if (_durationTimer <= 0)
            {
                this.gameObject.SetActive(false);
            }
            else
            {
                _durationTimer -= Time.deltaTime * 1;
            }

            if (_applyTimer <= 0)
            {
                //Apply Effect

                switch (effectName)
                {
                    case EffectName.Poison:

                        _appliedEnemy.TakeDamage(effectAmount);

                        break;
                }

                _applyTimer = applyTime;
            }
            else
            {
                _applyTimer -= Time.deltaTime * 1;
            }
        }
    }

    public void ApplyEffect(EnemyController enemy)
    {
        _appliedEnemy = enemy;

        _applyTimer = applyTime;
        _durationTimer = durationTime;
    }

    public void SetEffectProperties(float applyTime, float durationTime, EffectName name, int effectAmount)
    {

    }
}
