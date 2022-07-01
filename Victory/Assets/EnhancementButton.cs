using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnhancementButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private bool enhancementEquipSlot;

    private ClassInterface _classInterface;

    private Enhancement _enhancement;

    public Enhancement AssignedEnhancement
    {
        get { return _enhancement; }
        set { _enhancement = value; }
    }

    private void Awake()
    {
        _classInterface = GameObject.Find("GameManager").GetComponent<ClassInterface>();
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        if(!enhancementEquipSlot)
        {
            _classInterface.DisplayEnhancementDetails(_enhancement, this.transform.position);
            _classInterface.selectedEnhancement = _enhancement;
        }
        else
        {
            _classInterface.enhancementSlotActive = this;
        }
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        if(!enhancementEquipSlot)
        {
            _classInterface.CloseEnhancementDetails();
            _classInterface.selectedEnhancement = null;
        }
        else
        {
            _classInterface.enhancementSlotActive = null;
        }
    }
}
