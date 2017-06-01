using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{
    public Image image;

    IEnumerator Faid(Action<SceneState> end,SceneState state)
    {
        Color Start = new Color(0, 0, 0, 0);

        Color End = new Color(0, 0, 0, 1);

        float deltime = 0;

        while(true)
        {
            if(deltime>= 1)
            {
                image.color = End;
                break;
            }
            deltime += Time.deltaTime * 4;

            image.color = Color.Lerp(Start, End, deltime);

            yield return new WaitForSeconds(0.001f);
        }

        end(state);
    }

    public void GoStart()
    {
        StartCoroutine(Faid(PSceneManager.ChangeScene, SceneState.Start));
    }

    public void Login()
    {
        StartCoroutine(Faid(PSceneManager.ChangeScene, SceneState.Character));
    }

    public void Regi()
    {
        StartCoroutine(Faid(PSceneManager.ChangeScene, SceneState.Register));
    }

    public void Theme()
    { 
        StartCoroutine(Faid(PSceneManager.ChangeScene, SceneState.Theme));
    }

    public void ARScene()
    {
        StartCoroutine(Faid(PSceneManager.ChangeScene, SceneState.scene0));
    }
    public void GetAR()
    {
        StartCoroutine(Faid(PSceneManager.ChangeScene, SceneState.character2));
    }

    public void GameStart()
    {
        StartCoroutine(Faid(PSceneManager.ChangeScene,SceneState.MainGameScene));        
    }

    public void Back()
    {
        StartCoroutine(Faid(PSceneManager.ChangeScene, PSceneManager.BackState()));
    }
    public void eixt()
    {
        Application.Quit();
    }

}
