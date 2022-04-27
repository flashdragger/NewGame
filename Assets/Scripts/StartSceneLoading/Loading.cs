using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Loading : MonoBehaviour
{
    public Scrollbar loadingBar;
    public float continuetime = 5;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        loadingBar.value = 0;
        timer = 0;
        StartCoroutine(LoadScene3("Game"));
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < continuetime)
        {
            timer = timer + Time.deltaTime;
            loadingBar.value = timer / continuetime;
        }
    }
    IEnumerator LoadScene3(string ls)
    {
        AsyncOperation loadscene3 = SceneManager.LoadSceneAsync(ls);
        loadscene3.allowSceneActivation = false;
        while (true)
        {
            if (loadingBar.value >= 1)
                loadscene3.allowSceneActivation = true;
            if (!loadscene3.isDone)
                yield return 0;
        }
    }
}
