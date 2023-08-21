using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserShot : MonoBehaviour
{
    public GameObject m_Laser;

    public Transform StartPos;
    public Transform EndPos;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("c");
    }
    IEnumerator c()
    {
        yield return new WaitForSeconds(0.2f);
        GameObject L = Instantiate(m_Laser, StartPos.transform.position, Quaternion.identity);
        L.GetComponent<DroneBullet>().Las(EndPos);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
