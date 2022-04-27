using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingGameScene : MonoBehaviour
{
    public void startGame()
    {
        SceneManager.LoadScene("LoadingScene");
    }
    public void withDrawGame()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
