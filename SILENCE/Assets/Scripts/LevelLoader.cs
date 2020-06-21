using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] float loadDelaySeconds = 3f;

    private IEnumerator LoadSceneDelay(string scene)
    {
        yield return new WaitForSeconds(loadDelaySeconds);
        SceneManager.LoadScene(scene);
    }

    public void LoadGameOver()
    {
        //LoadSceneDelay("Game Over Screen");
        SceneManager.LoadScene("Game Over Screen");
    }

    public void LoadMainMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Start Screen");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadSampleScene()
    {
        SceneManager.LoadScene("SampleScene");
    }

}
