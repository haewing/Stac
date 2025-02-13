using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireChild : MonoBehaviour
{
    [SerializeField] GameObject Effect;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(new Vector2(0, 30 * Time.deltaTime));
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Floor" || collision.gameObject.tag == "Wall")
        {
            Destroy(gameObject);
            GameObject Eff = Instantiate(Effect, transform.position, Quaternion.identity);
        }
    }
}
