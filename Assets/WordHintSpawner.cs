using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordHintSpawner : MonoBehaviour
{
    public static WordHintSpawner instance;
    [SerializeField] private LetterBoxSpawner letterBox;
    [SerializeField] private GameObject wordPrefab;
    private Hashtable hintWords;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        hintWords = new Hashtable();
        int i = 0;
        foreach (var word in letterBox.GetWords())
        {
            var thisObject = Instantiate(wordPrefab, transform.position + Vector3.up * -75 * (i - (i %2)) + Vector3.right * 500 * (i %2), Quaternion.identity);
            thisObject.transform.SetParent(this.transform);
            thisObject.transform.GetComponent<TMPro.TextMeshProUGUI>().text = word.word;
            hintWords.Add(word.word.ToUpper(), thisObject);
            i++;
        }
    }

    public bool CheckWord(string word) {
        foreach (var wordToCheck in letterBox.GetWords()) {
            if (wordToCheck.word.Equals(word)) {
                ((GameObject)hintWords[word]).transform.GetComponent<TMPro.TextMeshProUGUI>().text = "<s>"+ ((GameObject)hintWords[word]).transform.GetComponent<TMPro.TextMeshProUGUI>().text+ "</s>";
                return true;
            }
        }
        return false;
    }

    // Update is called once per frame
    void Update(){}
}
