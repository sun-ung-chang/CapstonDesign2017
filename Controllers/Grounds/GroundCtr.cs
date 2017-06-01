using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GroundCtr : MonoBehaviour
{
    public int Power;

    public int Id { get; set; }

    public HouseCtr House;

    static QuizManager QuizManager;

    public bool Belong = false;

    PlayerCtr NActPlayer;

    Action End;



    public void Ani(float value)
    {
        Debug.Log(Id);
        StartCoroutine(StartUpDown(value));
    }

    IEnumerator StartUpDown(float value)
    {
        float deltime = 0;
        Vector3 Startpos, Endpos;
        Startpos = gameObject.transform.localPosition;
        //블록
        Endpos = Startpos;
        Endpos.y -= value;
        while (true)
        {
            if (deltime >= 1)
            {
                gameObject.transform.localPosition = Endpos;
                break;
            }
            
            deltime += Time.deltaTime * 4;
            gameObject.transform.localPosition = Vector3.Lerp(Startpos, Endpos, deltime);
            yield return new WaitForSeconds(0.001f);
        }

        deltime = 0;
        while (true)
        {
            if (deltime >= 1)
            {
                gameObject.transform.localPosition = Startpos;
                break;
            }
            deltime += Time.deltaTime * 4;
            gameObject.transform.localPosition = Vector3.Lerp(Endpos, Startpos, deltime);

            yield return new WaitForSeconds(0.001f);
        }

    }

    public virtual void Act(PlayerCtr player, Action _End)
    {
        if(Belong == true)
        {
            if (!player.DoYouHaveGround(Id))
            {
                player.GetDamaged(Power);
                player.SetUI(true);
                _End();
            }

            else
            {
                _End();
            }
            return;
        }

        if(QuizManager == null)
        {
            QuizManager = GameObject.Find("Quiz").GetComponent<QuizManager>();
        }

        int level = 0;

        if(Power == 100)
        {
            level = 1;
        }
        else if(Power == 150)
        {
            level = 2;
        }
        else
        {
            level = 3;
        }

        QuizCtr quiz = QuizManager.GetQuiz(level);

        if (player.GetType() == typeof(AI))
        {
            quiz.SetQuiz(Done, true);
        }
        else
        {
            quiz.SetQuiz(Done);
        }

        NActPlayer = player;

        this.End = _End;
    }

    public void Done(AnswerState state)
    {
        if(state == AnswerState.Right)
        {
            SetBuilding();
        }
        else
        {
            End();
        }
    }

    void SetBuilding()
    {
        NActPlayer.AddGround(Id);

        Belong = true;

        House.SetHouse(NActPlayer.HairC);

        End();
    }

    public void SetBuilding(PlayerCtr player)
    {
        player.AddGround(Id);

        Belong = true;

        House.SetHouse(player.HairC);
    }
}
