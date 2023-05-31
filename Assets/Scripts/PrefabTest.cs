using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabTest : MonoBehaviour
{
    //public GameObject prefab; // 엔진 툴에서 직접 연결 => 작은 프로젝트에서나 할법

    GameObject tank;
    void Start()
    {
        tank = Managers.Resource.Instantiate("Tank"); ;

        //Managers.Resource.Destroy(tank);
        Destroy(tank, 3.0f);
    }
}
