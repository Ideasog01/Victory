using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static ProjectileManager projectileManager;
    public static PlayerController playerController;
    public static PlayerInterface playerInterface;

    private void Awake()
    {
        projectileManager = this.GetComponent<ProjectileManager>();
        playerInterface = this.GetComponent<PlayerInterface>();
        playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();
    }
}
