using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    public void PlayScene()
    {
        SceneManager.LoadScene("CustumiseScene", LoadSceneMode.Single);
    }
}
