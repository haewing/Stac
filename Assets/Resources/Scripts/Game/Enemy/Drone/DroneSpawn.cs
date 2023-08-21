using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneSpawn : MonoBehaviour
{
    public GameObject Drone;

    public GameObject Perent;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("Spawn");
    }
    IEnumerator Spawn()
    {
        while (true)
        {
            Vector2 Pos;
            Pos.x = Random.Range(142, transform.position.x + 10);
            Pos.y = Random.Range(50, 60);
            GameObject D = Instantiate(Drone, Pos, Quaternion.identity, Perent.transform);
            D.GetComponent<Helicopter>().MovePettern(new Vector3(Pos.x + Random.Range(-10, 10), Pos.y, 0));
            yield return new WaitForSeconds(15.0f);
        }
    }
}
