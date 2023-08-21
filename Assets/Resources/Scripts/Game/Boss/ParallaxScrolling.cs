using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxScrolling : MonoBehaviour
{
    public Transform[] backgrounds;
    public Vector2[] parallaxScales; // x축, y축 스크롤 속도를 각각 설정
    public float smoothing = 1f;

    private Transform cam;
    private Vector3 previousCamPosition;

    private void Awake()
    {
        cam = Camera.main.transform;
    }

    private void Start()
    {
        previousCamPosition = cam.position;
        

        for (int i = 0; i < backgrounds.Length; i++)
        {

            //parallaxScales[i] = new Vector2(backgrounds[i].position.z * -1, backgrounds[i].position.z * -1);
        }
    }

    private void Update()
    {

        for (int i = 0; i < backgrounds.Length; i++)
        {
            float parallaxX = (previousCamPosition.x - cam.position.x) * parallaxScales[i].x;
            float backgroundTargetPosX = backgrounds[i].position.x + parallaxX;
            float parallaxY = (previousCamPosition.y - cam.position.y) * parallaxScales[i].y;
            float backgroundTargetPosY = backgrounds[i].position.y + parallaxY;

            Vector3 backgroundTargetPos = new Vector3(backgroundTargetPosX, backgroundTargetPosY, backgrounds[i].position.z);

            backgrounds[i].position = Vector3.Lerp(backgrounds[i].position, backgroundTargetPos, smoothing * Time.deltaTime);
        }

        previousCamPosition = cam.position;
    }
}
