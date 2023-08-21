using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D m_rigid;
    [SerializeField] GameObject m_Child;
    // Start is called before the first frame update
    void Start()
    {
        m_rigid = GetComponent<Rigidbody2D>();
        Destroy(gameObject, 2);
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(new Vector2(30 * Time.deltaTime, 0));
    }
    public void PlayerLook()
    {
        GameObject target = GameObject.Find("Player");
        if (target != null)
        {
            Vector2 direction = new Vector2(
               transform.position.x - target.transform.position.x,
               transform.position.y - target.transform.position.y + 3
            );

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion angleAxis = Quaternion.AngleAxis(angle + 180f, Vector3.forward);
            Quaternion rotation = Quaternion.Slerp(transform.rotation, angleAxis, 10 * Time.deltaTime);
            //angleAxis.z += 90;
            transform.rotation = angleAxis;
        }

    }
    private void OnDestroy()
    {
        for (int i = 0; i < 12; i++)
        {
            GameObject c = Instantiate(m_Child, transform.position, Quaternion.identity);
            c.transform.Rotate(new Vector3(0, 0, i * 30));
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag != "Boss2" && collision.gameObject.tag != "Object" && collision.gameObject.tag != "Scope")
        {
            Destroy(gameObject);
        }

    }

}
