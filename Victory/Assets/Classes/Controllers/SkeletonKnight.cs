using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonKnight : EnemyController
{
    private void Update()
    {
        EnemyAIStateMachine();
        EnemyBehaviour();
    }

    private void EnemyBehaviour()
    {
        if(EnemyState == AIState.Chase)
        {
            SetEnemyDestination(Player.transform.position);
        }
        else if(EnemyState == AIState.Idle)
        {
            if(!PatrolLocationSet)
            {
                if (PatrolLocations.Length > 0)
                {
                    PatrolIndex = Random.Range(0, PatrolLocations.Length);
                    SetEnemyDestination(PatrolLocations[PatrolIndex]);
                }
            }
            else
            {
                if(this.transform.position == EnemyDestination)
                {
                    PatrolLocationSet = false;
                }
            }
        }
    }
}
