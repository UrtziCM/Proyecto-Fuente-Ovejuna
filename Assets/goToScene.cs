using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class goToScene : MonoBehaviour
{
    void Update()
    {}
    public void GoToMap() {
        SceneManager.LoadScene("MapaDeLaCosta");
    }
}
