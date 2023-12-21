using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LetterBoxSpawner : MonoBehaviour
{
    [SerializeField] private GameObject letterBoxPrefab;
    const int WORDSEARCH_DIMENSION = 11;
    public GameObject[,] wordSearch;
    [SerializeField] private List<WordSearchWord> words;
    void Start()
    {
        wordSearch = new GameObject[WORDSEARCH_DIMENSION,WORDSEARCH_DIMENSION];
        for (int i = 0; i < WORDSEARCH_DIMENSION; i++)
        {
            for (int j = 0; j < WORDSEARCH_DIMENSION; j++)
            {
                GameObject letterBox = Instantiate(letterBoxPrefab, new Vector2(transform.position.x + i*85,transform.position.y - j*85), Quaternion.identity);
                letterBox.transform.SetParent(transform);
                letterBox.transform.GetComponent<LetterBoxComponent>().SetCoordinates(i,j);
                GameObject textGameObject = letterBox.transform.GetChild(0).gameObject;
                TMPro.TextMeshProUGUI textContainer = textGameObject.GetComponent<TMPro.TextMeshProUGUI>();
                LetterBoxComponent.ChangeLetter(textContainer,GetRandomLetter());
                wordSearch[i,j] = letterBox;

            }
        }
        foreach (var word in words) {
            PlaceWordInWordSearch(word);
        }
    }
    void Update(){}
    private static string GetRandomLetter()
    {
        string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        int num = Random.Range(0, chars.Length - 1);
        return chars[num]+"";
    }

    private void PlaceWordInWordSearch(WordSearchWord wordComponent) {
        if (!wordComponent.enabled) return;
        wordComponent.word = wordComponent.word.ToUpper();
        switch (wordComponent.orientation) {
            case Orientation.VERTICAL:
                for (int i = 0; i < wordComponent.word.Length; i++)
                {
                    LetterBoxComponent.ChangeLetter(wordSearch[wordComponent.startingX, wordComponent.startingY + i].transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>(), wordComponent.word[i]+"");
                }
                break;
            case Orientation.HORIZONTAL:
                for (int i = 0; i < wordComponent.word.Length; i++)
                {
                    LetterBoxComponent.ChangeLetter(wordSearch[wordComponent.startingX + i, wordComponent.startingY].transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>(), wordComponent.word[i]+"");
                }
                break;
            case Orientation.DIAGONAL:
                for (int i = 0; i < wordComponent.word.Length; i++)
                {
                    LetterBoxComponent.ChangeLetter(wordSearch[wordComponent.startingX + i, wordComponent.startingY+ i].transform.GetChild(0).GetComponent<TMPro.TextMeshProUGUI>(), wordComponent.word[i]+"");
                }
                break;
        }
    }
    public List<WordSearchWord> GetWords() {
        return words;
    }

}
[System.Serializable]
public enum Orientation {
    VERTICAL = 1,
    HORIZONTAL = 2,
    DIAGONAL = 3,
    NONE = -1
}
[System.Serializable]
public class WordSearchWord {
    [SerializeField] public string word;
    [SerializeField] public int startingX,startingY;
    [SerializeField] public Orientation orientation = Orientation.HORIZONTAL;
    [SerializeField] public bool enabled = false;
}