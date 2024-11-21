using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMainMenu : MonoBehaviour
{
    private float Timer = 5.0f;
    void Update()
    {
        if (Timer > 0) Timer -= Time.deltaTime;
        if (Timer <= 0) SceneManager.LoadScene(0);
    }
}
