using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{

    public Transform m_Turret;
    public LineRenderer m_Line;

    [HideInInspector]public float  m_EnemyHP = 30;
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        GameObject target = GameObject.Find("Player");
        if (target != null)
        {
            Vector2 direction = new Vector2(
                transform.position.x - target.transform.position.x,
                transform.position.y - target.transform.position.y + 1.5f
            ); ;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion angleAxis = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
            transform.rotation = angleAxis;
        }
        
        if(m_EnemyHP <= 0)
        {
            InfoMng.GetIns.TurretCount--;
            Destroy(gameObject);
        }
    }






}
