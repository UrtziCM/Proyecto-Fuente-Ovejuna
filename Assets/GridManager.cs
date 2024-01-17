using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GridManager : MonoBehaviour
{
    [SerializeField] AudioSource clickAudio;
    [SerializeField] private GameObject destinedPuzzleGrid;

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name.Equals(destinedPuzzleGrid.name)) 
        {
            clickAudio.Play();
            transform.parent.SetParent(other.transform);
            BoxCollider2D box = other.GetComponent<BoxCollider2D>();
            transform.parent.position = box.bounds.center;
            transform.parent.tag = "Untagged";
            transform.parent.GetComponent<BoxCollider2D>().enabled = false;
            if (GameObject.FindGameObjectsWithTag("puzzlePiece").Length == 0) {
                SceneManager.LoadScene("WinScreen", LoadSceneMode.Additive);
            }
        }
    }
    
    public GameObject getDestinedPuzzleGrid()
    {
        return destinedPuzzleGrid;
    }
}
