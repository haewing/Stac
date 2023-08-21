using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class AlphaMap : MonoBehaviour
{
    public GameObject m_InsideWall;
    public GameObject m_Door;
    public Tilemap m_OuterWall;
    public GameObject m_DroneSpawn;

    float a = 1;
    bool b = true;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        map();

    }
    void map()
    {
        GameObject Player = GameObject.Find("Player");
        if(Player.transform.position.x >= 137 && b)
        {
            GameMng.GetIns.PlayerCastleIn = true;
            m_InsideWall.SetActive(true);
            m_DroneSpawn.SetActive(true);
            m_Door.SetActive(false);
            a -= Time.deltaTime * 2;
            m_OuterWall.color = new Color(0.8773585f, 0.7896227f, 0.7896227f, a);
            Invoke("c",0.5f);
        }

    }
    void c()
    {
        b = false;
    }


}
