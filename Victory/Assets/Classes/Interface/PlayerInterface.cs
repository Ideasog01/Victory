using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInterface : MonoBehaviour
{
    [SerializeField]
    private Slider arrowChargeSlider;

    [SerializeField]
    private Slider specialAbilitySlider;

    [SerializeField]
    private TextMeshProUGUI ability1CooldownText;

    [SerializeField]
    private TextMeshProUGUI ability2CooldownText;

    [SerializeField]
    private TextMeshProUGUI primaryAbilityCooldownText;

    [SerializeField]
    private TextMeshProUGUI secondaryAbilityCooldownText;

    private Ability _ability1;

    private Ability _ability2;

    private Ability _primaryAbility;

    private Ability _secondaryAbility;

    private void Awake()
    {
        _ability1 = GameManager.playerController.Ab1;
        _ability2 = GameManager.playerController.Ab2;
        _primaryAbility = GameManager.playerController.PrimaryAbility;
        _secondaryAbility = GameManager.playerController.SecondaryAbility;
    }


    public void DisplayArrowCharge(float chargeValue)
    {
        arrowChargeSlider.maxValue = GameManager.playerController.GetComponent<ArcherController>().MaxArrowCharge;
        arrowChargeSlider.value = chargeValue;
    }

    public void DisplaySpecialAbilityCharge(float chargeValue)
    {
        specialAbilitySlider.maxValue = GameManager.playerController.MaxSpecialCharge;
        specialAbilitySlider.value = chargeValue;
    }

    public void DisplayAbilityCooldowns()
    {
        if(_ability1.abilityCooldown > 0)
        {
            ability1CooldownText.text = _ability1.abilityCooldown.ToString("F0");
        }
        else
        {
            ability1CooldownText.text = "";
        }

        if (_ability2.abilityCooldown > 0)
        {
            ability2CooldownText.text = _ability2.abilityCooldown.ToString("F0");
        }
        else
        {
            ability2CooldownText.text = "";
        }

        if (_primaryAbility.abilityCooldown > 0)
        {
            primaryAbilityCooldownText.text = _primaryAbility.abilityCooldown.ToString("F0");
        }
        else
        {
            primaryAbilityCooldownText.text = "";
        }

        if (_secondaryAbility.abilityCooldown > 0)
        {
            secondaryAbilityCooldownText.text = _secondaryAbility.abilityCooldown.ToString("F0");
        }
        else
        {
            secondaryAbilityCooldownText.text = "";
        }
    }

    private void Update()
    {
        DisplayAbilityCooldowns();
    }
}
