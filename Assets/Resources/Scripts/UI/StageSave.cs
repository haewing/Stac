using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageSave : MonoBehaviour
{
    [SerializeField] RectTransform Canvers;
    public void FixedUpdate()
    {
        
        if (SceneManager.GetActiveScene().name != "Title") Canvers.localPosition = new Vector3(-2000,0,0);
        else Canvers.localPosition = new Vector3(30, 0, 0);
    }
}