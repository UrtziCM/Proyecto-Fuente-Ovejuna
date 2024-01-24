using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OnBackPressed : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (SceneManager.GetActiveScene().name.Equals("TitleScreen")) {
                Application.Quit();
            } else if (SceneManager.GetActiveScene().name.Equals("MapaDeLaCosta")){
                SceneManager.LoadSceneAsync("Assets/Scenes/TitleScreen.unity", LoadSceneMode.Single);
            } else {
                SceneManager.LoadSceneAsync("Assets/Scenes/MapaDeLaCosta.unity", LoadSceneMode.Single);
            }
        }
    }
}
