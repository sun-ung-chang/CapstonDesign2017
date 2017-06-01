using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
    public List<QuizCtr> Quiz;

    public QuizCtr GetQuiz(int Level)
    {
        List<QuizCtr> levelquizs = Quiz.FindAll(x => x.Level == Level);

        int ran = UnityEngine.Random.Range(0, levelquizs.Count);
        
        return levelquizs[ran];
    }


}
