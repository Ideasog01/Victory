using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Ability", order = 0)]
public class Ability : ScriptableObject
{
    public float maxCooldown;

    public float abilityCooldown;

    public enum AbilityStatus { Available, Active, Disabled, OnCooldown };

    public AbilityStatus abilityStatus;

    public bool cooldownActive;

    public void UseAbility()
    {
        abilityCooldown = maxCooldown;
        abilityStatus = AbilityStatus.OnCooldown;
        cooldownActive = false;
    }

    public void RefreshAbility()
    {
        if(abilityStatus != AbilityStatus.Disabled && abilityStatus != AbilityStatus.Active)
        {
            abilityCooldown = 0;
            abilityStatus = AbilityStatus.Available;
        }
    }
}