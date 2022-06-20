using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerInterface : MonoBehaviour
{
    [Header("Player HUD")]

    [SerializeField]
    private Slider arrowChargeSlider;

    [SerializeField]
    private Slider specialAbilitySlider;

    [SerializeField]
    private Slider playerHealthSlider;

    [SerializeField]
    private Slider playerExperienceSlider;

    [SerializeField]
    private float playerExperienceSliderSpeed;

    [SerializeField]
    private TextMeshProUGUI playerLevelText;

    [SerializeField]
    private TextMeshProUGUI ability1CooldownText;

    [SerializeField]
    private TextMeshProUGUI ability2CooldownText;

    [SerializeField]
    private TextMeshProUGUI primaryAbilityCooldownText;

    [SerializeField]
    private TextMeshProUGUI secondaryAbilityCooldownText;

    [Header("Respawn Screen")]

    [SerializeField]
    private GameObject respawnScreen;

    [SerializeField]
    private Image[] respawnImages;

    [SerializeField]
    private TextMeshProUGUI[] respawnTexts;

    [SerializeField]
    private float respawnFadeSpeed;

    [Header("Dungeon Exit Screen")]

    [SerializeField]
    private Animator dungeonExitAnimator;

    private Ability _ability1;

    private Ability _ability2;

    private Ability _primaryAbility;

    private Ability _secondaryAbility;

    private int _targetXp;

    [Header("Interact Prompt")]

    [SerializeField]
    private GameObject controllerInteractPrompt;

    [SerializeField]
    private GameObject keyboardInteractPrompt;

    private void Awake()
    {
        _ability1 = GameManager.playerController.Ab1;
        _ability2 = GameManager.playerController.Ab2;
        _primaryAbility = GameManager.playerController.PrimaryAbility;
        _secondaryAbility = GameManager.playerController.SecondaryAbility;

        foreach (Image image in respawnImages)
        {
            image.canvasRenderer.SetAlpha(0.0f);
        }

        foreach (TextMeshProUGUI text in respawnTexts)
        {
            text.canvasRenderer.SetAlpha(0.0f);
        }
    }

    public void DisplayDungeonExitScreen(bool active)
    {
        dungeonExitAnimator.SetBool("isActive", active);
        PlayerController.disablePlayer = active;
    }

    public void DisplayPlayerHealth()
    {
        playerHealthSlider.maxValue = GameManager.playerController.PlayerMaxHealth;
        playerHealthSlider.value = GameManager.playerController.PlayerHealth;
    }

    public void DisplayRespawnScreen()
    {
        respawnScreen.SetActive(true);
        foreach (Image image in respawnImages)
        {
            image.CrossFadeAlpha(1.0f, respawnFadeSpeed, false);
        }

        foreach (TextMeshProUGUI text in respawnTexts)
        {
            text.CrossFadeAlpha(1.0f, respawnFadeSpeed, false);
        }
    }

    public void DisplayXP()
    {
        playerExperienceSlider.maxValue = GameManager.playerController.playerData.playerMaxExperience;
        _targetXp = GameManager.playerController.playerData.playerExperience;
        playerLevelText.text = GameManager.playerController.playerData.playerLevel.ToString();
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

    public void DisplayInteractPrompt(bool active)
    {
        if(InputManager.controllerConnected)
        {
            controllerInteractPrompt.SetActive(active);
        }
        else
        {
            keyboardInteractPrompt.SetActive(active);
        }

        if(!active)
        {
            controllerInteractPrompt.SetActive(false);
            keyboardInteractPrompt.SetActive(false);
        }
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

        if(_targetXp > playerExperienceSlider.value)
        {
            PlayerXPSlider();
        }
    }

    private void PlayerXPSlider()
    {
        playerExperienceSlider.value = Mathf.Lerp(playerExperienceSlider.value, _targetXp, Time.deltaTime * playerExperienceSliderSpeed);

        if(playerExperienceSlider.value >= playerExperienceSlider.maxValue)
        {
            PlayerData playerData = GameManager.playerController.playerData;
            playerData.playerLevel++;
            playerData.playerExperience = 0;
            playerData.playerMaxExperience += 200;
            _targetXp = 0;
            playerExperienceSlider.minValue = 0;
            playerExperienceSlider.value = 0;
            DisplayXP();
        }
    }
}
