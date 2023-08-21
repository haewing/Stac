using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public GameObject m_TurretBullet;
    public GameObject m_TurretBulletPerent;


    [SerializeField] private float defDistanceRay = 100;
    public Transform LaserFirePoint;
    public LineRenderer m_lineRenderer;
    Transform m_transform;

    bool IsLaser = false;
    private void Awake()
    {
        m_transform = GetComponent<Transform>();
    }
    private void Start()
    {
        StartCoroutine("Attack");
    }
    private void Update()
    {
        ShootLaser(); 
    }
    void ShootLaser()
    {
        int layerMask = (-1) - (1 << LayerMask.NameToLayer("NoLaser"));
        if (Physics2D.Raycast(m_transform.position, transform.right,100,layerMask))
        {
            //m_lineRenderer.enabled = true;
            RaycastHit2D _hit = Physics2D.Raycast(LaserFirePoint.position, transform.right, Mathf.Infinity, layerMask);
            Draw2DRay(LaserFirePoint.position, _hit.point);
            IsLaser = true;
        }
        else
        {
            m_lineRenderer.enabled = false;
            //Draw2DRay(LaserFirePoint.position, LaserFirePoint.transform.right * defDistanceRay);
            IsLaser = false;
        }
    }
    void Draw2DRay(Vector2 startPos, Vector2 endPos)
    {
        m_lineRenderer.SetPosition(0, startPos);
        m_lineRenderer.SetPosition(1, endPos);
    }

    IEnumerator Attack()
    {
        while (true)
        {
            if (IsLaser)
            {
                m_lineRenderer.enabled = false;
                yield return new WaitForSeconds(Random.Range(1.0f, 4.0f));

                m_lineRenderer.enabled = true;

                yield return new WaitForSeconds(0.2f);

                m_lineRenderer.enabled = false;
                GameObject LaserBullet = Instantiate(m_TurretBullet, LaserFirePoint.position, Quaternion.identity, m_TurretBulletPerent.transform);
                LaserBullet.GetComponent<DroneBullet>().Launch(200);
                Destroy(LaserBullet, 10);

                yield return new WaitForSeconds(1.5f);
            }
            yield return new WaitForSeconds(0.1f);
        }
    }
}
