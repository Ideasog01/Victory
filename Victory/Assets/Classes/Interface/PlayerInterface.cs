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

    [Header("Abilities Display")]

    [SerializeField]
    private TextMeshProUGUI ability1CooldownText;

    [SerializeField]
    private TextMeshProUGUI ability2CooldownText;

    [SerializeField]
    private TextMeshProUGUI ability3CooldownText;

    [SerializeField]
    private TextMeshProUGUI ability4CooldownText;

    [SerializeField]
    private Image ability1Icon;

    [SerializeField]
    private Image ability2Icon;

    [SerializeField]
    private Image ability3Icon; 

    [SerializeField]
    private Image ability4Icon;

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

    private Ability _ability3;

    private Ability _ability4;

    private int _targetXp;

    [Header("Interact Prompt")]

    [SerializeField]
    private GameObject controllerInteractPrompt;

    [SerializeField]
    private GameObject keyboardInteractPrompt;

    private PlayerData _playerData;

    public Image Ability1Icon
    {
        get { return ability1Icon; }
    }

    public Image Ability2Icon
    {
        get { return ability2Icon; }
    }

    public Image Ability3Icon
    {
        get { return ability3Icon; }
    }

    public Image Ability4Icon
    {
        get { return ability4Icon; }
    }

    private void Awake()
    {
        _ability1 = GameManager.playerController.ability1;
        _ability2 = GameManager.playerController.ability2;
        _ability3 = GameManager.playerController.ability3;
        _ability4 = GameManager.playerController.ability4;

        _playerData = GameObject.Find("GlobalManager").GetComponent<GlobalManager>().playerData;

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
        playerExperienceSlider.maxValue = _playerData.playerMaxExperience;
        _targetXp = _playerData.playerExperience;
        playerLevelText.text = _playerData.playerLevel.ToString();
    }


    public void DisplayArrowCharge(float chargeValue, float maxValue)
    {
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

        if (_ability3.abilityCooldown > 0)
        {
            ability3CooldownText.text = _ability3.abilityCooldown.ToString("F0");
        }
        else
        {
            ability3CooldownText.text = "";
        }

        if (_ability4.abilityCooldown > 0)
        {
            ability4CooldownText.text = _ability4.abilityCooldown.ToString("F0");
        }
        else
        {
            ability4CooldownText.text = "";
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
            _playerData.playerLevel++;
            _playerData.playerExperience = 0;
            _playerData.playerMaxExperience += 200;
            _targetXp = 0;
            playerExperienceSlider.minValue = 0;
            playerExperienceSlider.value = 0;
            DisplayXP();
        }
    }
}
