using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Skip : MonoBehaviour
{
    // Start is called before the first frame update
    
    public void skip() {
        SceneManager.LoadScene("DemoScene");
    }
}
