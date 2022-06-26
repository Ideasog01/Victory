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
    public static DialogueManager dialogueManager;

    public static SpawnManager spawnManager;

    [SerializeField]
    private GameObject playerHud;

    [SerializeField]
    private GameObject pauseMenu;

    private bool _gamePaused;

    private void Awake()
    {
        projectileManager = this.GetComponent<ProjectileManager>();
        playerInterface = this.GetComponent<PlayerInterface>();
        inventoryInterface = this.GetComponent<InventoryInterface>();
        dialogueManager = this.GetComponent<DialogueManager>();
        spawnManager = this.GetComponent<SpawnManager>();

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
        PlayerController.disablePlayer = false;
        PlayerController.nearbyEnemyList.Clear();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void PauseGame()
    {
        if(!_gamePaused)
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            playerHud.SetActive(false);
            PlayerController.disablePlayer = true;
            _gamePaused = true;
        }
        else
        {
            Time.timeScale = 1;
            pauseMenu.SetActive(false);
            playerHud.SetActive(true);
            PlayerController.disablePlayer = false;
            _gamePaused = false;
        }
    }
}
