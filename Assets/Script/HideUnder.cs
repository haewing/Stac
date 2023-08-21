using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class HideUnder : MonoBehaviour
{
    bool In = false;
    float a = 1;
    [SerializeField] Tilemap t;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        t.color = new Color(1,1,1,a);
        if (In) a = Mathf.Lerp(a,0.4f, 1*Time.deltaTime);
        else a = Mathf.Lerp(a,1f, 1*Time.deltaTime);

        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            In = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player")
        {
            In = false;
        }
    }
}
