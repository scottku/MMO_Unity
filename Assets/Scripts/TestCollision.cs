using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class TestCollision : MonoBehaviour
{
    // 나 혹은 상대에게 rigidbody가 있을 것(isKinemaic : off)
    // 나와 상대에게 collider가 있을 것(isTrigger : off)

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Collision : {collision.gameObject.name}");
    }

    // 둘 다 collider가 있을 것
    // 둘 중 하나는 IsTrigger : On
    // 둘 중 하나는 Rigidbody
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log($"Trigger : {other.gameObject.name}");
    }

    void Start()
    {
        
    }

    void Update()
    {
        // Local - World - (Viewport - Screen)
        //Debug.Log(Input.mousePosition); //Screen
        //Debug.Log(Camera.main.ScreenToViewportPoint(Input.mousePosition));
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay( Input.mousePosition );

            Debug.DrawRay(Camera.main.transform.position, ray.direction * 100.0f, Color.red, 1.0f);

            //LayerMask.GetMask("Monster") | LayerMask.GetMast("Wall");
            int mask = (1 << 8) | (1 << 9); // 비트를 옆으로 민다 -> 8, 9번째 비트를 1로 변경
                        // or 이니까 둘다 포함된 비트가 나옴 1100000000
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 100.0f, mask))
                Debug.Log($"Raycast Camera : {hit.collider.gameObject.name}");
        }

        // 단순무식 기본버전
        /*if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
            Vector3 dir = mousePos - Camera.main.transform.position;
            dir = dir.normalized;

            Debug.DrawRay(Camera.main.transform.position, dir * 100.0f, Color.red, 1.0f);

            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, dir, out hit, 100.0f))
                Debug.Log($"Raycast Camera : {hit.collider.gameObject.name}");
        }*/

        /*Vector3 look = transform.TransformDirection(Vector3.forward);
        Debug.DrawRay(transform.position + Vector3.up, look * 10, Color.red);

        RaycastHit[] hits;
        hits = Physics.RaycastAll(transform.position + Vector3.up, look, 10);

        foreach (RaycastHit hit in hits )
        {
            Debug.Log($"Raycast : {hit.collider.gameObject.name}");
        }*/
    }
}
