using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [SerializeField]
    float _speed = 10.0f; // public 으로 변경 시, 엔진 상에서도 변경 가능

   // bool _moveToDes = false;
    Vector3 _destPos;

    void Start()
    {
        //Managers.Input.KeyAction -= OnKeyboard; // Qt Signal-Slot 이랑 같은 느낌인듯
        //Managers.Input.KeyAction += OnKeyboard;
        Managers.Input.MouseAction -= OnMouseClicked;
        Managers.Input.MouseAction += OnMouseClicked;

        //Temp
        Managers.UI.ShowSceneUI<UI_Inven>();
    }


    // GameObject(Player)
    // Transform
    // PlayerController (*)
    float _yAngle = 0.0f;

    public enum PlayerState
    {
        Die,
        Moving,
        Idle,
        //Channelling,
        //Jumping,
        //Falling,
    }

    PlayerState _state = PlayerState.Idle;

    void UpdateDie()
    {

    }

    void UpdateIdle()
    {
        Animator anim = GetComponent<Animator>();
        anim.SetFloat("Speed", 0);
    }

    void UpdateMoving()
    {

        Vector3 dir = _destPos - transform.position;
        if (dir.magnitude < 0.0001f)
        {
            _state = PlayerState.Idle;
        }
        else
        {
            float moveDist = Mathf.Clamp(_speed * Time.deltaTime, 0, dir.magnitude);
            transform.position = transform.position + dir.normalized * moveDist;
        
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 0.05f);
            transform.LookAt(_destPos);
        }
        
        // animation
        Animator anim = GetComponent<Animator>();
        // 현재 게임 상태 정보를 넘겨준다
        anim.SetFloat("Speed", _speed);
    }

    void Update()
    {
        

        switch(_state)
        {
            case PlayerState.Die:
                UpdateDie();
                break;
            case PlayerState.Moving:
                UpdateMoving();
                break;
            case PlayerState.Idle:
                UpdateIdle();
                break;
        }
    }

    /*void OnKeyboard()
    {
        _yAngle += Time.deltaTime * 100.0f;
        //transform.Rotate(new Vector3(0.0f, Time.deltaTime * 100.0f, 0.0f));

        //transform.rotation = Quaternion.Euler(new Vector3(0.0f, _yAngle, 0.0f));

        if (Input.GetKey(KeyCode.W))
        {
            //transform.rotation = Quaternion.LookRotation(Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.forward), 0.05f);
            //transform.Translate(Vector3.forward * Time.deltaTime * _speed);
            transform.position += Vector3.forward * Time.deltaTime * _speed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            //transform.rotation = Quaternion.LookRotation(Vector3.back);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.back), 0.05f);
            //transform.Translate(Vector3.forward * Time.deltaTime * _speed);
            transform.position += Vector3.back * Time.deltaTime * _speed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            //transform.rotation = Quaternion.LookRotation(Vector3.left);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.left), 0.05f);
            //transform.Translate(Vector3.forward * Time.deltaTime * _speed);
            transform.position += Vector3.left * Time.deltaTime * _speed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            //transform.rotation = Quaternion.LookRotation(Vector3.right);
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Vector3.right), 0.05f);
            //transform.Translate(Vector3.forward * Time.deltaTime * _speed);
            transform.position += Vector3.right * Time.deltaTime * _speed;
        }

        //if (Input.GetKey(KeyCode.W))
        //    transform.Translate(Vector3.forward * Time.deltaTime * _speed);
        //if (Input.GetKey(KeyCode.S))
        //    transform.position += transform.TransformDirection(Vector3.back * Time.deltaTime * _speed);
        //if (Input.GetKey(KeyCode.D))
        //    transform.position += transform.TransformDirection(Vector3.right * Time.deltaTime * _speed);
        //if (Input.GetKey(KeyCode.A))
        //    transform.position += transform.TransformDirection(Vector3.left * Time.deltaTime * _speed);

        _moveToDes = false;
    }*/

    void OnMouseClicked(Define.MouseEvent evt)
    {
        //if(evt != Define.MouseEvent.Click)
        //{
        //    return;
        //}

        if (_state == PlayerState.Die)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

        //LayerMask.GetMask("Monster") | LayerMask.GetMast("Wall");
        //int mask = (1 << 8) | (1 << 9); // 비트를 옆으로 민다 -> 8, 9번째 비트를 1로 변경
                                        // or 이니까 둘다 포함된 비트가 나옴 1100000000
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100.0f, LayerMask.GetMask("Wall")))
        {
            _destPos = hit.point;
            _state = PlayerState.Moving;
        }
            //Debug.Log($"Raycast Camera : {hit.collider.gameObject.name}");
    }
}
