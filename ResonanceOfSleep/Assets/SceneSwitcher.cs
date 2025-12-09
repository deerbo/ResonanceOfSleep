using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SceneManager.LoadScene("Beach_Ocean");

        if (Input.GetKeyDown(KeyCode.Alpha2))
            SceneManager.LoadScene("City_Rainfall");

        if (Input.GetKeyDown(KeyCode.Alpha3))
            SceneManager.LoadScene("Meadow");

        if (Input.GetKeyDown(KeyCode.Alpha4))
            SceneManager.LoadScene("Underwater");
    }
}
