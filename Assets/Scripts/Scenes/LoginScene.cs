using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginScene : BaseScene
{
    public override void Clear()
    {
        Debug.Log("Login Scene Clear!");
    }
    protected override void Init()
    {
        base.Init();

        _sceneType = Define.Scene.Login;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Managers.Scene.LoadScene(Define.Scene.Game);
        }
    }
}
