using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LetterBoxComponent : MonoBehaviour
{
    [SerializeField]
    public int x,y;
    private static LetterBoxComponent startingPos,endingPos;


    // Start is called before the first frame update
    void Start()
    {}

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void ChangeLetter(TMPro.TextMeshProUGUI textComponent, string letter) {
        textComponent.text = letter;
    }
    public void DragStarted() {
        startingPos = transform.GetComponent<LetterBoxComponent>();
        //Debug.Log(transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text);
    }
    public void DragEnded() {
        endingPos = transform.GetComponent<LetterBoxComponent>();
        Orientation ori = ChosenOrientation(startingPos.x, startingPos.y, endingPos.x,endingPos.y);
        var currentWord = ReadWord(startingPos,endingPos,ori);
        if (WordHintSpawner.instance.CheckWord(currentWord)) {
            PaintTrail(startingPos,endingPos,ori);
        }
    }
    public static Orientation ChosenOrientation(int startingX,int startingY, int endingX, int endingY) {
        if (startingX == endingX && startingY != endingY) {
            return Orientation.VERTICAL;
        }
        if (startingX != endingX && startingY == endingY) {
            return Orientation.HORIZONTAL;
        }
        if (endingX - startingX == endingY - startingY) {
            return Orientation.DIAGONAL;
        }
        return Orientation.NONE;
    }
    public void SetCoordinates(int x, int y) {
        this.x = x;
        this.y = y;
    }
    public string ReadWord(LetterBoxComponent starting, LetterBoxComponent ending, Orientation orientation) {
        string endWord ="";
        switch (orientation) {
            case Orientation.HORIZONTAL:
                for (int i = starting.x; i <= ending.x; i++)
                {
                    endWord += transform.parent.GetComponent<LetterBoxSpawner>().wordSearch[i,ending.y].transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text;
                }
                return endWord;
            case Orientation.VERTICAL:
                for (int i = starting.y; i <= ending.y; i++)
                {
                    endWord += transform.parent.GetComponent<LetterBoxSpawner>().wordSearch[ending.x,i].transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text;
                }
                return endWord;
            case Orientation.DIAGONAL:
                for (int i = starting.x, j = starting.y; i <= ending.x; i++,j++)
                {
                    endWord += transform.parent.GetComponent<LetterBoxSpawner>().wordSearch[i,j].transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>().text;
                }
                return endWord;
            default:
                return null;
        }

    }
    private void PaintTrail(LetterBoxComponent startingPos, LetterBoxComponent endingPos,Orientation ori) {
        switch (ori) {
            case Orientation.HORIZONTAL:
                for (int i = startingPos.x; i <= endingPos.x; i++)
                {
                    transform.parent.GetComponent<LetterBoxSpawner>().wordSearch[i,endingPos.y].transform.GetComponent<UnityEngine.UI.Image>().color = Color.green;
                }
                break;
            case Orientation.VERTICAL:
                for (int i = startingPos.y; i <= endingPos.y; i++)
                {
                    transform.parent.GetComponent<LetterBoxSpawner>().wordSearch[endingPos.x,i].transform.GetComponent<UnityEngine.UI.Image>().color = Color.green;
                }
                break;
            case Orientation.DIAGONAL:
                for (int i = startingPos.x, j = startingPos.y; i <= endingPos.x; i++,j++)
                {
                    transform.parent.GetComponent<LetterBoxSpawner>().wordSearch[i,j].transform.GetComponent<UnityEngine.UI.Image>().color = Color.green;
                }
                break;
            }
    }
}