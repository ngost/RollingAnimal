using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class DialogManager : MonoBehaviour
{
    public Queue<string> sentences;
    public Text nameText;
    public Text dialogText;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();

    }

    public void StartDialog(Dialog dialog)
    {
        Debug.Log("Starting conversation");

        try
        {

        }catch(NullReferenceException e)
        {
            Debug.Log(e.ToString());
            nameText.text = dialog.name;
            foreach (string sentence in dialog.sentences)
            {
                sentences.Enqueue(sentence);
            }
            DisplayNextSentence();
        }

    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialog();
            return;
        }

        string message = sentences.Dequeue();
        Debug.Log(message);
        dialogText.text = message;
    }

    void EndDialog()
    {
        Debug.Log("end Conversation");
    }
}
