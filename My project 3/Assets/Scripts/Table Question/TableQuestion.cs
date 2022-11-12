using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class TableQuestion : MonoBehaviour
{
    [SerializeField] private List<string> forms;
    [SerializeField] private List<string> sources;
    [SerializeField][ColorHtmlProperty] private Color highlightColor = Color.yellow;
    [SerializeField][ColorHtmlProperty] private Color correctColor = Color.green;
    [SerializeField][ColorHtmlProperty] private Color wrongColor = Color.red;
    
    
    private int _listCount;
    private List<GameObject> _lists;
    private GameObject _answersFormsList;
    private GameObject _answersSourcesList;
    private GameObject _choicesFormsList;
    private GameObject _choicesSourcesList;
    private readonly Random _random = new Random();

    private void Start()
    {
        CheckArrayException();
        SetupList();
    }
    private void CheckArrayException()
    {
        if (forms.Count != sources.Count)
        {
            //throw out of range error 
            throw new IndexOutOfRangeException("The number of forms and sources must be the same");
        }

        if (forms == null || sources == null)
        {
            //throw null reference error
            throw new NullReferenceException("The forms and sources must not be null");
        }

        if (forms.Count == 0 || sources.Count == 0)
        {
            //throw out of range error 
            throw new IndexOutOfRangeException("The number of forms and sources must be greater than 0");
        }
    }
    private void SetupList()
    {
        _lists =
            gameObject.GetComponentsInChildren<GridLayoutGroup>() //get all grid layout groups component
                .Where(component => component.gameObject.name == "List") //get the one with the name "List"
                .Select(component => component.gameObject).ToList(); //get the game object of the component
        _listCount = forms.Count;

        SetupListCells();
        CacheLists();
        SetupCellText();
       

    }
    private void CacheLists()
    {
        // get the lists under Choices Table
        var choicesLists =
            _lists.Where(listObject => listObject.transform.parent.parent.name == "Choices Table").ToList();
        
        
        // cache the lists under Answers Table
        _choicesFormsList = choicesLists.Find(list => list.transform.parent.name == "Forms");
        _choicesSourcesList = choicesLists.Find(list => list.transform.parent.name == "Sources");
        // get the lists under Answers Table
        var answerLists =
            _lists.Where(listObject => listObject.transform.parent.parent.name == "Answers Table").ToList();
        
        // cache the lists under Answers Table
        _answersFormsList = answerLists.Find(list => list.transform.parent.name == "Forms");
        _answersSourcesList = answerLists.Find(list => list.transform.parent.name == "Sources");
    }

    private void SetupListCells()
    {
        // resize cell size of the grid layout group and instantiate cells
        foreach (var list in _lists)
        {
            GridLayoutGroup gird = list.GetComponent<GridLayoutGroup>();
            Rect rect = list.GetComponent<RectTransform>().rect;
            gird.cellSize = new Vector2(rect.width, rect.height / _listCount);
            while (list.transform.childCount < _listCount)
            {
                Instantiate(list.transform.GetChild(0), list.transform);
            }
        }
    }
    private void SetupCellText()
    {
        
        //clone forms and sources array to avoid changing the original array
        List<string> formsClone = new List<string>(forms);
        List<string> sourcesClone = new List<string>(sources);
        
        //shuffle the arrays
        ShuffleArray(formsClone);
        ShuffleArray(sourcesClone);
        
        if (_choicesFormsList == null)
        {
            Debug.Log("null");
            return;
        }
        //get all the text components of the list game objects in choices
        TextMeshProUGUI[] formsListTextComponents = _choicesFormsList.GetComponentsInChildren<TextMeshProUGUI>();
        TextMeshProUGUI[] sourcesListTextComponents = _choicesSourcesList.GetComponentsInChildren<TextMeshProUGUI>();
        
        for (int i = 0; i < _listCount; i++)
        {
            formsListTextComponents[i].text = formsClone[i];
            sourcesListTextComponents[i].text = sourcesClone[i];
        }
    }

    

    private void ShuffleArray(List<string> array)
    {
        
        for (int i = 0; i < array.Count; i++)
        {
            int randomIndex = _random.Next(0, array.Count);
            (array[i], array[randomIndex]) = (array[randomIndex], array[i]);
        }
    }

    private bool CheckArrayAsDictionary(string key, string value)
    {
        int findIndex = forms.FindIndex(form => form == key);
        if(findIndex == -1)
            return false;
        return sources[findIndex] == value;
    }

    private bool CheckAnswer(int rowIndex)
    {
        //get all the text components of the list game objects in answers
        TextMeshProUGUI[] formsListTextComponents = _answersFormsList.GetComponentsInChildren<TextMeshProUGUI>(); 
        TextMeshProUGUI[] sourcesListTextComponents = _answersSourcesList.GetComponentsInChildren<TextMeshProUGUI>();
        
        // check if there is an empty string
        if (formsListTextComponents[rowIndex].text == "" || sourcesListTextComponents[rowIndex].text == "")
        {
            return false;
        }
        // check if the answer is correct
        // log return from CheckArrayAsDictionary
        Debug.Log(CheckArrayAsDictionary(formsListTextComponents[rowIndex].text, sourcesListTextComponents[rowIndex].text));
        if(CheckArrayAsDictionary(formsListTextComponents[rowIndex].text, sourcesListTextComponents[rowIndex].text))
        {
            formsListTextComponents[rowIndex].transform.parent.GetComponent<Image>().color = correctColor;
            sourcesListTextComponents[rowIndex].transform.parent.GetComponent<Image>().color = correctColor;
            return true;
        }
        else
        {
            formsListTextComponents[rowIndex].transform.parent.GetComponent<Image>().color = wrongColor;
            sourcesListTextComponents[rowIndex].transform.parent.GetComponent<Image>().color = wrongColor;
            return false;
        }
        
    }

    public void CheckAllAnswers()
    {
        bool allCorrect = true;
        for (int i = 0; i < _listCount; i++)
        {
            if (!CheckAnswer(i))
            {
                allCorrect = false;
            }
        }

        if (allCorrect)
        {
            // do something when all answers are correct
        }
        
    }

    public Color GetHighlightColor()
    {
        return highlightColor;
    }
}
