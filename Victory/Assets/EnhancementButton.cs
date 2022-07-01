using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EnhancementButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

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
        _classInterface.DisplayEnhancementDetails(_enhancement, this.transform.position);
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        _classInterface.CloseEnhancementDetails();
    }
}
