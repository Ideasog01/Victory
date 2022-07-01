using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ClassInterface : MonoBehaviour
{
    public Enhancement selectedEnhancement;

    public EnhancementButton enhancementSlotActive;

    [SerializeField]
    private GameObject classInterface;

    [SerializeField]
    private GameObject playerHud;

    [SerializeField]
    private GameObject enhancementDetailPanel;

    [Header("Enhancement Panel Display")]

    [SerializeField]
    private TextMeshProUGUI enhancementNamePanelText;

    [SerializeField]
    private TextMeshProUGUI enhancementDescriptionPanelText;

    [SerializeField]
    private Vector2 enhancementDetailPanelOffset;

    [SerializeField]
    private EnhancementButton[] enhancementButtons;

    [Header("Enhancement Display")]

    [SerializeField]
    private TextMeshProUGUI enhancementNameText;

    [SerializeField]
    private TextMeshProUGUI enhancementDescriptionText;

    [Header("Ability Display")]

    [SerializeField]
    private TextMeshProUGUI abilityNameText;

    [SerializeField]
    private TextMeshProUGUI abilityDescriptionText;

    [SerializeField]
    private Image abilityIcon;

    [SerializeField]
    private Button[] enhancementSlotButtons;

    private Ability _selectedAbility;

    [Header("Class Display")]

    [SerializeField]
    private TextMeshProUGUI classNameText;

    [SerializeField]
    private TextMeshProUGUI classDescriptionText;

    [SerializeField]
    private Image classIcon;

    private InventoryInterface _inventoryInterface;
    private PlayerController _playerController;
    private GlobalManager _globalManager;

    private void Awake()
    {
        _inventoryInterface = this.GetComponent<InventoryInterface>();
        _playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();
        _globalManager = GameObject.Find("GlobalManager").GetComponent<GlobalManager>();
    }

    public void DisplayClassScreen()
    {
        _inventoryInterface.DisplayInventory();
        classInterface.SetActive(true);
        playerHud.SetActive(false);
        LoadClassDetails();
        SelectAbility(0);
    }

    public void CloseClassScreen()
    {
        _inventoryInterface.DisplayInventory();
        classInterface.SetActive(false);
    }

    public void SelectAbility(int index)
    {
        //0 = Ability 1, 4 = Special Ability 1

        switch (index)
        {
            case 0:
                _selectedAbility = _playerController.ability1;
                break;
            case 1:
                _selectedAbility = _playerController.ability2;
                break;
            case 2:
                _selectedAbility = _playerController.ability3;
                break;
            case 3:
                _selectedAbility = _playerController.ability4;
                break;
            case 4:
                _selectedAbility = _playerController.specialAbility1;
                break;
            case 5:
                _selectedAbility = _playerController.specialAbility2;
                break;
            case 6:
                _selectedAbility = _playerController.specialAbility3;
                break;
        }

        for (int i = 0; i < enhancementButtons.Length; i++)
        {
            enhancementButtons[i].AssignedEnhancement = _selectedAbility.abilityEnhancements[i];
        }

        abilityNameText.text = _selectedAbility.abilityName;
        abilityDescriptionText.text = _selectedAbility.abilityDescription;
        abilityIcon.sprite = _selectedAbility.abilityIcon;
    }

    public void SelectEnhancementSlot(int index)
    {
        if(_selectedAbility != null)
        {
            Enhancement enhancement = _selectedAbility.abilityEnhancements[index];

            if (enhancement != null)
            {
                enhancementNameText.text = enhancement.enhancementName;
                enhancementDescriptionText.text = enhancement.enhancementDescription;
            }
        }
    }

    public void DisplayEnhancementDetails(Enhancement enhancement, Vector2 slotPosition)
    {
        if (_selectedAbility != null)
        {
            enhancementDetailPanel.SetActive(true);
            enhancementDetailPanel.transform.position = slotPosition + enhancementDetailPanelOffset;

            enhancementNamePanelText.text = enhancement.enhancementName;
            enhancementDescriptionPanelText.text = enhancement.enhancementDescription;
        }
    }

    public void CloseEnhancementDetails()
    {
        enhancementDetailPanel.SetActive(false);
    }

    private void LoadClassDetails()
    {
        if (_playerController.playerClass == PlayerController.Class.VoidServant)
        {
            classNameText.text = "Void Servant";
            classDescriptionText.text = _globalManager.playerData.voidServantDescription;
            classIcon.sprite = _globalManager.playerData.voidServantIcon;
        }
    }

    public void EquipEnhancement()
    {
        if(enhancementSlotActive != null)
        {
            enhancementSlotActive.AssignedEnhancement = selectedEnhancement;
            LoadEquipedEnhancements();
            selectedEnhancement = null;
            enhancementSlotActive = null;
        }
        else
        {
            selectedEnhancement = null;
        }
    }

    private void LoadEquipedEnhancements()
    {

    }
}
