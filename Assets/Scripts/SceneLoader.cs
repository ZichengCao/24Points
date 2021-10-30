using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneLoader : MonoBehaviour
{
    // Start is called before the first frame update

    public void loadScene(int i) {
        if (i == 1) {
            SceneManager.LoadScene("DemoScene");

        } else if (i == 0) {
            SceneManager.LoadScene("StartScene");
        }
    }
}
