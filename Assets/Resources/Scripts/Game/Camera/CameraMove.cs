using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraMove : MonoBehaviour
{
    [HideInInspector]public string targetObjectName;
    public float speed = 1;

    public Transform ClampP;
    public Transform ClampX;
    public Transform ClampY;

    GameObject targetObject;
    Rigidbody2D rbody;


    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {


        //switch (GameMng.GetIns.CameraMode)
        //{
        //    case 0:
        //        targetObjectName = "Player";
        //        break;
        //    case 1:
        //        targetObjectName = "viewchange";
        //        break;
        //    case 2:
        //        targetObjectName = "Mon";
        //        break;
        //    case 3:
        //        targetObjectName = "Boss";
        //        break;
            
        //    default:
        //        return;

        //}

        //targetObject = GameObject.Find(targetObjectName);

        //Vector2 LerpPos = Vector2.Lerp(transform.position, targetObject.transform.position, 0.1f);
        //transform.position = new Vector3(LerpPos.x, LerpPos.y, -5);


        //transform.position = new Vector3(
        //Mathf.Clamp(transform.position.x, ClampX.position.x, ClampY.position.x),
        //Mathf.Clamp(transform.position.y, ClampX.position.y, ClampY.position.y),
        //-5);

    }
    void PlayerCastle()
    {
        GameMng.GetIns.PlayerCastleIn = false;
    }

}
