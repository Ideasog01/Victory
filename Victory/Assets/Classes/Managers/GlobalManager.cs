using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalManager : MonoBehaviour
{
    public PlayerData playerData;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
