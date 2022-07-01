using UnityEngine;

[CreateAssetMenu(fileName = "Ability", menuName = "Ability", order = 0)]
public class Ability : ScriptableObject
{
    public float maxCooldown;

    public float abilityCooldown;

    public float activeTime;

    public float activeTimer;

    public enum AbilityStatus { Available, Active, Disabled, OnCooldown };

    public AbilityStatus abilityStatus;

    public bool cooldownActive;

    public bool activeTimerActive;

    public bool isSpecial;

    public Sprite abilityIcon;

    public Enhancement[] equipedEnhancements;

    public Enhancement[] abilityEnhancements;

    public string abilityName;

    [TextArea(10, 5)]
    public string abilityDescription;

    public void ActivateAbility()
    {
        abilityStatus = AbilityStatus.Active;
        activeTimer = activeTime;
    }

    public void UseAbility()
    {
        abilityCooldown = maxCooldown;
        abilityStatus = AbilityStatus.OnCooldown;
        cooldownActive = false;
    }

    public void RefreshAbility()
    {
        if(abilityStatus != AbilityStatus.Disabled)
        {
            abilityCooldown = 0;
            abilityStatus = AbilityStatus.Available;
            activeTimer = 0;
            activeTimerActive = false;
            cooldownActive = false;
        }
    }
}