using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bgm : MonoBehaviour
{
    // Start is called before the first frame update
    private void Awake()
    {

        DontDestroyOnLoad(this.gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        
        Scene scene = SceneManager.GetActiveScene();
        if(scene.name == "GameClear")
        {
            gameObject.SetActive(false);  
        }
    }
}
