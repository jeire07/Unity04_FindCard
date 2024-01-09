using UnityEngine;
using UnityEngine.SceneManagement;

public class EndTxt : MonoBehaviour
{
    public void RetryGame()
    {
        SceneManager.LoadScene("MainScene");
    }
}
