using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance; // static���� ���ϼ� ����
    static Managers Instance { get { Init(); return s_instance; } }

    InputManager _input = new InputManager();
    ResourceManager _resource = new ResourceManager(); 
    UIManager _ui = new UIManager();
    public static InputManager Input { get { return Instance._input; } }
    public static ResourceManager Resource { get { return Instance._resource; } }
    public static UIManager UI { get { return Instance._ui; } }

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
        _input.OnUpdate();   
    }

    static void Init()
    {
        if(s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers"); // ������ �ٷ� �־��ְ�, ������ ������ object�� ���� �־���
            if(go == null)
            {
                go = new GameObject { name = "@Managers" };
                go.AddComponent<Managers>();
            }

            // ���縮 �������� �ʰ� ������ݴϴ�.
            DontDestroyOnLoad(go);

            s_instance = go.GetComponent<Managers>();
        }
        
    }
}
