using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectChoice : MonoBehaviour
{
    private Button _button;
    
    private Question _question;

    private void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(Select);
        _question = GetComponentInParent(typeof(Question)) as Question; ;
    }
    

    private void Select()
    {
        if (_question.CheckAnswer(GetComponentInChildren<TextMeshProUGUI>().text))
        {
            _button.image.color = Color.green;
        }
        else
        {
            _button.image.color = Color.red;   
        }  
        Invoke(nameof(NextQuestion), 0.3f);
    }
    
    private void NextQuestion()
    {
        _button.image.color = Color.white;
        _question.transform.parent.GetComponent<Questions>().NextQuestion(_question.gameObject);
    }
}
