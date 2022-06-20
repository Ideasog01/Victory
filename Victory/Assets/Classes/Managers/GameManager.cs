using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int exitSceneIndex;
    public static ProjectileManager projectileManager;
    public static PlayerController playerController;
    public static PlayerInterface playerInterface;
    public static InventoryInterface inventoryInterface;

    private void Awake()
    {
        projectileManager = this.GetComponent<ProjectileManager>();
        playerInterface = this.GetComponent<PlayerInterface>();
        inventoryInterface = this.GetComponent<InventoryInterface>();
        playerController = GameObject.Find("PlayerController").GetComponent<PlayerController>();

        SceneManager.sceneLoaded += OnSceneLoad;
    }

    public void PlayerDefeat()
    {
        playerInterface.DisplayRespawnScreen();
    }

    public void ResetPlayer()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        playerController.transform.position = PlayerController.playerCheckpoint;
        playerController.PlayerHealth = playerController.PlayerMaxHealth;
        playerInterface.DisplayPlayerHealth();
        PlayerController.disablePlayer = false;
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    private void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        ProjectileManager.arrowExplosiveProjectileList.Clear();
        ProjectileManager.arrowPoisonProjectileList.Clear();
        ProjectileManager.arrowProjectileList.Clear();
        ProjectileManager.daggerProjectileList.Clear();
        PlayerController.disablePlayer = false;
        PlayerController.nearbyEnemyList.Clear();
    }
}
