using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum AnswerState
{
    Ready,
    Right,
    Wrong
}

public class QuizCtr : MonoBehaviour
{
    public int Level;

    public GameObject Question;
    public GameObject Solve;

    AnswerState state;

    Action<AnswerState> act;

    public void SetQuiz(Action<AnswerState> end)
    {
        Solve.SetActive(false);

        state = AnswerState.Ready;

        act = end;

        ButtonOn();

        Question.SetActive(true);

        gameObject.SetActive(true);
    }

    public void SetQuiz(Action<AnswerState> end,bool AI)
    {
        Solve.SetActive(false);

        state = AnswerState.Ready;

        act = end;


        if(AI)
        {
            ButtonOff();
            Invoke("RightAnswer", 2);    
        }
        else
        {
            ButtonOn();
        }

        Question.SetActive(true);

        gameObject.SetActive(true);
    }

    void ButtonOff()
    {
        for(int i=0; i< Question.transform.childCount;++i)
        {
            if (Question.transform.GetChild(i).GetComponent<Button>() != null)
                Question.transform.GetChild(i).GetComponent<Button>().interactable = false;
        }
    }

    void ButtonOn()
    {
        for (int i = 0; i < Question.transform.childCount; ++i)
        {
            if(Question.transform.GetChild(i).GetComponent<Button>() != null)
            Question.transform.GetChild(i).GetComponent<Button>().interactable = true;
        }
    }

    public void RightAnswer()
    {
        state = AnswerState.Right;
        Question.SetActive(false);
        Solve.SetActive(true);
    }

    public void WrongAnswer()
    {
        state = AnswerState.Wrong;
        Question.SetActive(false);
        Solve.SetActive(true);
    }

    public void DoneSolve()
    {
        Solve.SetActive(false);
        gameObject.SetActive(false);
        act(state);
    }
}
