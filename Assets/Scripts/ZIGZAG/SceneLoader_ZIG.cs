using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader_ZIG : MonoBehaviour
{
    public void LoadScene(string sceneName = "")
    {
        if (sceneName == "")
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        else
            SceneManager.LoadScene(sceneName);
    }

}
