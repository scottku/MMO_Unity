using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{

    public override void Clear()
    {
        
    }

    // �θ��ʿ��� ��������
    //void Start()
    //{
    //    Init();
    //}

    protected override void Init()
    {
        base.Init();

        _sceneType = Define.Scene.Game;

        Managers.UI.ShowSceneUI<UI_Inven>();
    }
}
