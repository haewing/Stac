using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;

public class MThrow : MonoBehaviourPun
{
    // Start is called before the first frame update
    void Start()
    {

    }
    public void MyQuaternion(Vector3 a)
    {
        transform.eulerAngles = a;
    }

    public void MyQuat()
    {
        Vector3 mPosition = Input.mousePosition; //마우스 좌표 저장
        Vector3 oPosition = transform.position; //게임 오브젝트 좌표 저장
        mPosition.z = oPosition.z - Camera.main.transform.position.z;
        Vector3 target = Camera.main.ScreenToWorldPoint(mPosition);
        float dy = target.y - oPosition.y;
        float dx = target.x - oPosition.x;
        float rotateDegree = Mathf.Atan2(dy, dx) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, rotateDegree + 180);
    }
    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(new Vector2(-80 * Time.deltaTime, 0));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(collision.GetComponent<PhotonView>().IsMine) 
            { 
                
                
            }

        }
    }

    void DmgTxtfunction(int dmg, Collider2D coll)
    {
        Vector3 CreatPos = Camera.main.WorldToScreenPoint(coll.transform.position);

        
    }
}
