using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageSave : MonoBehaviour
{
    [SerializeField] GameObject Canvers;
    public void FixedUpdate()
    {
        if (SceneManager.GetActiveScene().name != "Title") Canvers.SetActive(false);
        else Canvers.SetActive(true);
    }
}