using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    [SerializeField] private string[] scenes = new string[3];
    private int currentIndex = 0;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);  
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SwitchScene();
        }
    }

    private void SwitchScene()
    {
        currentIndex = (currentIndex + 1) % scenes.Length;
        SceneManager.LoadScene(scenes[currentIndex]);
    }
}