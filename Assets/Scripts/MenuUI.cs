using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    public void PlayScene()
    {
        SceneManager.LoadScene("Testroom", LoadSceneMode.Single);
    }
}
