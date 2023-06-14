using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class BaseScene : MonoBehaviour
{
    public Define.Scene _sceneType { get; protected set; } = Define.Scene.Unknown;

    void Awake() // ���⼭ �����ϸ� �ڽİ͵鵵 �� ��������
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
