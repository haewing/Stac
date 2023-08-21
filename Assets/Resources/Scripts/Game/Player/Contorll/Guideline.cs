using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guideline : MonoBehaviour
{


    [SerializeField] private float defDistanceRay = 100;
    public Transform LaserFirePoint;
    public LineRenderer m_lineRenderer;
    Transform m_transform;
    public GameObject m_TargetImg;
    private void Awake()
    {
        m_transform = GetComponent<Transform>();
    }
    private void Start()
    {
    }
    private void Update()
    {
        ShootLaser();
    }
    void ShootLaser()
    {
        int layerMask = (-1) - (1 << LayerMask.NameToLayer("NoGrapple"));

        if(GameObject.Find("New Game Object") != null)
        {
            GameObject Dot = GameObject.Find("New Game Object");
            //Dot.GetComponent<SpriteRenderer>().sor
        }

        if (!GameMng.GetIns.PlayerGrapplingHitCheck)
        {
            if (Physics2D.Raycast(m_transform.position, transform.right, 50, layerMask))
            {

                m_lineRenderer.enabled = true;
                RaycastHit2D _hit = Physics2D.Raycast(LaserFirePoint.position, transform.right, 50, layerMask);
                //Draw2DRay(LaserFirePoint.position, _hit.point);

                DotLine.DotLine.Instance.DrawDottedLine(LaserFirePoint.position, _hit.point);
                m_TargetImg.SetActive(true);
                m_TargetImg.transform.position = _hit.point;
               
            }
            else
            {
                m_TargetImg.SetActive(false);
                m_lineRenderer.enabled = false;
                //Draw2DRay(LaserFirePoint.position, LaserFirePoint.transform.right * defDistanceRay);

            }
        }
        else
        {
            m_TargetImg.SetActive(false);
            m_lineRenderer.enabled = false;
        }
    }
    void Draw2DRay(Vector2 startPos, Vector2 endPos)
    {
        m_lineRenderer.SetPosition(0, startPos);
        m_lineRenderer.SetPosition(1, endPos);
    }
}
