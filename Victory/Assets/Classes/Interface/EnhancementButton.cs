using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EnhancementButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private bool isEnhancementOption;

    private ClassInterface _classInterface;

    private Enhancement _enhancement;

    private Image _enhancementIcon;

    public Enhancement AssignedEnhancement
    {
        get { return _enhancement; }
        set { _enhancement = value; }
    }

    private void Awake()
    {
        _classInterface = GameObject.Find("GameManager").GetComponent<ClassInterface>();
        _enhancementIcon = this.GetComponent<Image>();
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if(isEnhancementOption)
        {
            _classInterface.DisplayEnhancementDetails(_enhancement, this.transform.position);
            _classInterface.selectedEnhancement = _enhancement;

            Debug.Log("Player hovered over enhancement option, assigning this enhancement to selectedEnhancement variable in ClassInterface");
        }
        else
        {
            _classInterface.enhancementSlotActive = this;
        }
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        if(isEnhancementOption)
        {
            _classInterface.CloseEnhancementDetails();
        }
        else
        {
            _classInterface.enhancementSlotActive = null;
        }
    }

    public void DisplayEnhancementIcon()
    {
        if(_enhancement != null)
        {
            _enhancementIcon.sprite = _enhancement.enhancementIcon;
        }
    }
}
