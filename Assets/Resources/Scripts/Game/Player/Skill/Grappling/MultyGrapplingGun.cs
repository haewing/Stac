using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class MultyGrapplingGun : MonoBehaviourPun
{
    SoundMng m_Sound;
    public GameObject m_HookParticle;
    public MultyPlayerControll m_PlayerController;
    [Header("Scripts Ref:")]
    public MultyGrapplingRope grappleRope;

    [Header("Layers Settings:")]
    [SerializeField] private bool grappleToAll = false;
    [SerializeField] private int grappableLayerNumber = 9;

    [Header("Main Camera:")]
    Camera m_camera;

    [Header("Transform Ref:")]
    public Transform gunHolder;
    public Transform gunPivot;
    public Transform firePoint;

    [Header("Physics Ref:")]
    public SpringJoint2D m_springJoint2D;
    public Rigidbody2D m_rigidbody;

    [Header("Rotation:")]
    [SerializeField] private bool rotateOverTime = true;
    [Range(0, 60)] [SerializeField] private float rotationSpeed = 4;

    [Header("Distance:")]
    [SerializeField] private bool hasMaxDistance = false;
    [SerializeField] private float maxDistnace = 20;

    public PhotonView PV;

    private enum LaunchType
    {
        Transform_Launch,
        Physics_Launch
    }

    [Header("Launching:")]
    [SerializeField] private bool launchToPoint = true;
    [SerializeField] private LaunchType launchType = LaunchType.Physics_Launch;
    [SerializeField] private float launchSpeed = 1;

    [Header("No Launch To Point")]
    [SerializeField] public bool autoConfigureDistance = false;
    [SerializeField] private float targetDistance = 3;
    [SerializeField] private float targetFrequncy = 1;

    [HideInInspector] public Vector2 grapplePoint;
    [HideInInspector] public Vector2 grappleDistanceVector;


    private void Start()
    {
        m_camera = GameObject.Find("Main Camera").GetComponent<Camera>();
        m_Sound = GameObject.Find("SoundMng").GetComponent<SoundMng>();
        grappleRope.enabled = false;
        m_springJoint2D.enabled = false;

    }

    private void Update()
    {
        if (PV.IsMine)
        {

                    //fool();
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                SetGrapplePoint();


            }
            else if (Input.GetKey(KeyCode.Mouse1))
            {

                if (grappleRope.enabled)
                {
                    RotateGun(grapplePoint, false);


                }
                else
                {
                    Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
                    RotateGun(mousePos, true);
                }

                if (launchToPoint && grappleRope.isGrappling)
                {
                    if (launchType == LaunchType.Transform_Launch)
                    {

                        Vector2 firePointDistnace = firePoint.position - gunHolder.localPosition;
                        Vector2 targetPos = grapplePoint - firePointDistnace;
                        gunHolder.position = Vector2.Lerp(gunHolder.position, targetPos, Time.deltaTime * launchSpeed);
                    }
                }
            }
            else if (Input.GetKeyUp(KeyCode.Mouse1))
            {

                grappleRope.enabled = false;
                m_springJoint2D.enabled = false;
                m_rigidbody.gravityScale = 2.5f;
                if (GameMng.GetIns.PlayerGrapplingHitCheck) m_PlayerController.Tumbling();
                GameMng.GetIns.PlayerGrapplingHitCheck = false;

            }
            else
            {
                Vector2 mousePos = m_camera.ScreenToWorldPoint(Input.mousePosition);
                RotateGun(mousePos, true);
            }
        }
    }

    void RotateGun(Vector3 lookPoint, bool allowRotationOverTime)
    {
        Vector3 distanceVector = lookPoint - gunPivot.position;

        float angle = Mathf.Atan2(distanceVector.y, distanceVector.x) * Mathf.Rad2Deg;
        if (rotateOverTime && allowRotationOverTime)
        {
            gunPivot.rotation = Quaternion.Lerp(gunPivot.rotation, Quaternion.AngleAxis(angle, Vector3.forward), Time.deltaTime * rotationSpeed);
        }
        else
        {
            gunPivot.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    void SetGrapplePoint()
    {
        try
        {
            int layerMask = (-1) - (1 << LayerMask.NameToLayer("NoGrapple"));

            Vector2 distanceVector = m_camera.ScreenToWorldPoint(Input.mousePosition) - gunPivot.position;
            distanceVector.Normalize();

            //if (Physics2D.Raycast(firePoint.position, distanceVector))
            {
                RaycastHit2D _hit = Physics2D.Raycast(firePoint.position, distanceVector, Mathf.Infinity, layerMask);
                if (_hit.transform.gameObject.layer == grappableLayerNumber || grappleToAll)
                {
                    if (Vector2.Distance(_hit.point, firePoint.position) <= maxDistnace || !hasMaxDistance)
                    {
                        Scene scene = SceneManager.GetActiveScene();
                        if (_hit.transform.gameObject.tag == "Scope" && scene.name == "Stage3")
                        {
                            GameObject.Find("Scope").GetComponent<Scope>().Hit();
                            GameObject.Find("Boss").GetComponent<Boss2>().StartHit();
                        }
                        if (_hit.transform.gameObject.tag == "Scope" && scene.name == "Stage2")
                        {
                            GameObject.Find("Scope").GetComponent<Scope>().Hit();
                            GameObject.Find("Boss").GetComponent<Boss>().StartHit();
                        }
                        if (_hit.transform.gameObject.tag == "Scope" && scene.name == "Stage1")
                        {
                            GameObject.Find("Scope").GetComponent<Scope>().Hit();
                            GameObject.Find("Boss").GetComponent<TBoss>().StartHit();
                        }




                        grapplePoint = _hit.point;
                        grappleDistanceVector = grapplePoint - (Vector2)gunPivot.position;
                        grappleRope.enabled = true;
                    }
                }
            }

        }
        catch (Exception e) { Debug.Log(e); }


    }

    //public void fool()
    //{
    //    if (Input.GetKeyDown(KeyCode.H))
    //    {
    //        if (!launchToPoint)
    //        {
    //            if (autoConfigureDistance)
    //            {
    //                m_springJoint2D.autoConfigureDistance = true;
    //                m_springJoint2D.frequency = 0;
    //                GameMng.GetIns.PlayerGrapplingHitCheck = true;
    //                GameObject b = Instantiate(m_HookParticle, grapplePoint, Quaternion.identity);
    //                Destroy(b, 1);
    //                //////////////////
    //            }

    //            m_springJoint2D.connectedAnchor = grapplePoint;
    //            m_springJoint2D.enabled = true;
    //            GameMng.GetIns.PlayerGrapplingHitCheck = true;
    //            ////////////////
    //            m_Sound.Sound_Player(4, false, false).Play();
    //            GameObject c = Instantiate(m_HookParticle, grapplePoint, Quaternion.identity);

    //            Destroy(c, 1);

    //        }
    //    }
    //}
    public void Grapple()
    {

        m_springJoint2D.autoConfigureDistance = false;
        if (!launchToPoint && !autoConfigureDistance)
        {
            m_springJoint2D.distance = targetDistance;
            m_springJoint2D.frequency = targetFrequncy;
        }
        if (!launchToPoint)
        {
            if (autoConfigureDistance)
            {
                m_springJoint2D.autoConfigureDistance = true;
                m_springJoint2D.frequency = 0;
                GameMng.GetIns.PlayerGrapplingHitCheck = true;
                GameObject b = Instantiate(m_HookParticle, grapplePoint, Quaternion.identity);
                Destroy(b, 1);
                //////////////////
            }

            m_springJoint2D.connectedAnchor = grapplePoint;
            m_springJoint2D.enabled = true;
            GameMng.GetIns.PlayerGrapplingHitCheck = true;
            ////////////////
            m_Sound.Sound_Player(4, false, false).Play();
            GameObject c = Instantiate(m_HookParticle, grapplePoint, Quaternion.identity);

            Destroy(c, 1);

        }
        else
        {
            switch (launchType)
            {

                case LaunchType.Physics_Launch:
                    m_springJoint2D.connectedAnchor = grapplePoint;

                    Vector2 distanceVector = firePoint.position - gunHolder.position;

                    m_springJoint2D.distance = distanceVector.magnitude;
                    m_springJoint2D.frequency = launchSpeed;
                    m_springJoint2D.enabled = true;
                    GameMng.GetIns.PlayerGrapplingHitCheck = true;
                    GameObject a = Instantiate(m_HookParticle, grapplePoint, Quaternion.identity);
                    Destroy(a, 1);
                    //////////////////

                    break;
                case LaunchType.Transform_Launch:
                    m_rigidbody.gravityScale = 0;
                    m_rigidbody.velocity = Vector2.zero;
                    break;
            }
        }
    }


    private void OnDrawGizmosSelected()
    {
        if (firePoint != null && hasMaxDistance)
        {
            Gizmos.color = Color.green;


            Gizmos.DrawWireSphere(firePoint.position, maxDistnace);
        }
    }
}