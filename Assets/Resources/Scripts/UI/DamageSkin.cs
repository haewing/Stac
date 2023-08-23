using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageSkin : MonoBehaviour
{
    // Start is called before the first frame update
    int rand;
    void Start()
    {
        rand = Random.Range(-20, 20);
        Destroy(gameObject, 1);
        StartCoroutine(coloralphachange());
    }
    float a = 1;
    // Update is called once per frame
    void Update()
    {
        Vector3 CreatPos = Camera.main.WorldToScreenPoint(GameObject.Find("Boss").transform.position);
        transform.Translate(Vector2.up * Time.deltaTime * 50);
        transform.position = new Vector2(CreatPos.x + rand , transform.position.y);



    }
    IEnumerator coloralphachange()
    {
        while (a > 0)
        {

            a -= Time.deltaTime;
            gameObject.GetComponent<Text>().color = new Color(1, 0.23f, 0.23f, a );
            yield return new WaitForSeconds(0.01f);
        }

    }

}
