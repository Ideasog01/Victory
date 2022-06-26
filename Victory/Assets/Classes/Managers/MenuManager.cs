using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{ 

    public void StartGame(int difficultyIndex)
    {
        GameObject.Find("GlobalManager").GetComponent<GlobalManager>().playerData.difficultyLevel = (PlayerData.DifficultyLevel)difficultyIndex;
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
