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
    float y = 0;
    // Update is called once per frame
    void Update()
    {
        if(GameObject.Find("DmgPos")!= null)
        {

        Vector3 CreatPos = Camera.main.WorldToScreenPoint(GameObject.Find("DmgPos").transform.position);
        y += Time.deltaTime * 50;
        transform.position = new Vector2(CreatPos.x + rand, CreatPos.y +y );
        }



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
