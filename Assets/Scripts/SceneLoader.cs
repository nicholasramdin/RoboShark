using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadMainScene()
    {
        SceneManager.LoadScene("mainScene");
    }

    public void LoadTitleScene()
    {
        SceneManager.LoadScene("titleScene");
    }

    public void LoadInstructionsScene()
    {
        SceneManager.LoadScene("instructionsScene");
    }
}
