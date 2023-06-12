using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using UnityEngine;

public class UIManager
{
    int _order = 10; // canvas�� sort order ������
    
    Stack<UI_Popup> _popupStack = new Stack<UI_Popup>();
    UI_Scene _sceneUI = null;

    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("@UI_Root");
            if (root == null)
            {
                root = new GameObject { name = "@UI_Root" };
            }
            return root;
        }
    }

    public void SetCanvas(GameObject go, bool sort = true)
    {
        Canvas canvas = Util.GetOrAddComponent<Canvas>(go);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;

        if(sort)
        {
            canvas.sortingOrder = _order;
            _order++;
        }
        else
        {
            canvas.sortingOrder = 0;
        }
    }

    public T ShowSceneUI<T>(string prefabName = null) where T : UI_Scene
    {
        if (string.IsNullOrEmpty(prefabName))
            prefabName = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/Scene/{prefabName}");
        T _scene = Util.GetOrAddComponent<T>(go);
        if (typeof(T) == typeof(UI_Scene))
            _sceneUI = _scene.GetComponent<UI_Scene>();

        go.transform.SetParent(Root.transform);

        return _scene;
    }

    public T ShowPopupUI<T>(string prefabName = null) where T : UI_Popup
    {
        if(string.IsNullOrEmpty(prefabName))
            prefabName = typeof(T).Name;

        GameObject go = Managers.Resource.Instantiate($"UI/Popup/{prefabName}");

        T Popup = Util.GetOrAddComponent<T>(go);
        _popupStack.Push( Popup );

        go.transform.SetParent(Root.transform);

        return Popup;
    }

    public void ClosePopupUI(UI_Popup popup)
    {
        if (_popupStack.Count == 0)
        {
            return;
        }

        if(_popupStack.Peek() != popup)
        {
            Debug.Log("close popup failed");
        }

        ClosePopupUI();
    }

    public void ClosePopupUI()
    {
        if(_popupStack.Count == 0 ) 
        {
            return;        
        }

        UI_Popup _popup = _popupStack.Pop();
        Managers.Resource.Destroy(_popup.gameObject);
        _popup = null;

        _order--;
    }

    public void CloseAllPopupUI()
    {
        while(_popupStack.Count > 0)
        {
            ClosePopupUI();
        }
    }
}
