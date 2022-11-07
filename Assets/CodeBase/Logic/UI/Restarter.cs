using UnityEngine.SceneManagement;
using UnityEngine;

public class Restarter : MonoBehaviour
{
    public void RestartScene() 
        => SceneManager.LoadScene(SceneManager.GetActiveScene().name);

}
