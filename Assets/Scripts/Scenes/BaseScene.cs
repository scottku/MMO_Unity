using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseScene : MonoBehaviour
{
    public Define.Scene _sceneType { get; protected set; } = Define.Scene.Unknown;

    void Awake() // 여기서 실행하면 자식것들도 다 실행해줌
    {
        Init();
    }

    protected virtual void Init()
    {
        Object obj = GameObject.FindObjectOfType(typeof(EventSystem));
        if(obj == null) 
        {
            Managers.Resource.Instantiate("UI/EventSystem").name = "@EventSystem";
        }

    }

    public abstract void Clear();
}
