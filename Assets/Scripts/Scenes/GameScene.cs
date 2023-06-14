using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : BaseScene
{

    public override void Clear()
    {
        
    }

    // 부모쪽에서 실행해줌
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
