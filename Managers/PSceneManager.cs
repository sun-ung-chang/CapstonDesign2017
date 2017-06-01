using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SceneState
{
    Start =0,
    Register =1,
    Character =2,
    Theme =3,
    MainGameScene =4,
    character2 =5,
    scene0 = 6
}

public class PSceneManager
{
    static public SceneState NowState;

    static public void ChangeScene(SceneState state)
    {
        if(state == SceneState.MainGameScene)
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;
        }

        else if (state == SceneState.Start || state == SceneState.Register || state == SceneState.Character || state == SceneState.Theme || state == SceneState.scene0)
        {
            Screen.orientation = ScreenOrientation.Portrait;
        }

        NowState = state;
        SceneManager.LoadScene(state.ToString());
    }

    public static SceneState BackState()
    {
        if (NowState == SceneState.Character)
        {
            return SceneState.Start;
        }
        else if (NowState == SceneState.Register)
        {
            return SceneState.Start;
        }
        else if (NowState == SceneState.Theme)
        {
            return SceneState.Character;
        }
        else if(NowState == SceneState.scene0)
        {
            return SceneState.scene0;
        }
        else
        {
            return SceneState.Start;
        }
    }
}
