using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PuzzlePiece : MonoBehaviour
{
    List<Transform> puzzlePieces;
    Transform piece1;
    Transform piece2;

    // Start is called before the first frame update
    void Start()
    {
        puzzlePieces = new List<Transform>();
        foreach (Transform child in transform)
        {
            puzzlePieces.Add(child.transform.GetChild(0));
        }
        piece1 = puzzlePieces[Random.Range(0, puzzlePieces.Count)];
        piece2 = puzzlePieces[Random.Range(0, puzzlePieces.Count)];
        while (piece2.name.Equals(piece1.name) == true)
        {
            piece2 = puzzlePieces[Random.Range(0, puzzlePieces.Count)];
        }
        GameObject[] grids = GameObject.FindGameObjectsWithTag("pieceGrid");
        foreach (GameObject grid in grids)
        {
            if (piece1.GetComponent<GridManager>().getDestinedPuzzleGrid().name.Equals(grid.name))
            {
                piece1.transform.parent.SetParent(grid.transform);
                BoxCollider2D box = grid.GetComponent<BoxCollider2D>();
                piece1.transform.parent.position = box.bounds.center;
                piece1.transform.parent.tag = "Untagged";
                piece1.transform.parent.GetComponent<BoxCollider2D>().enabled = false;
            }
            if (piece2.GetComponent<GridManager>().getDestinedPuzzleGrid().name.Equals(grid.name))
            {
                piece2.transform.parent.SetParent(grid.transform);
                BoxCollider2D box = grid.GetComponent<BoxCollider2D>();
                piece2.transform.parent.position = box.bounds.center;
                piece2.transform.parent.tag = "Untagged";
                piece2.transform.parent.GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }
}
