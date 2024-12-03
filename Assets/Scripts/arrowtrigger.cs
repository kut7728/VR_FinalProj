using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

using UnityEngine.XR.Interaction.Toolkit;

public class BowAndArrow : MonoBehaviour
{
    public Transform bowStringAnchor;  // 활시위의 위치
    public Transform arrowSpawnPoint; // 화살 발사 위치
    public GameObject arrowPrefab;    // 화살 프리팹
    public float maxDrawDistance = 0.5f; // 최대 당기는 거리
    public float arrowForce = 500f;   // 화살의 발사 속도

    private XRController rightController; // 오른쪽 컨트롤러
    private bool isDrawing = false;
    private float drawDistance = 0f;

    void Start()
    {
        rightController = GetComponent<XRController>();
    }

    void Update()
    {
        // 컨트롤러의 Trigger 값을 가져옴 (활 당기기)
        if (rightController.inputDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue))
        {
            if (triggerValue > 0.1f)
            {
                isDrawing = true;
                drawDistance = Mathf.Clamp(triggerValue * maxDrawDistance, 0, maxDrawDistance);
                UpdateBowStringPosition();
            }
            else if (isDrawing)
            {
                isDrawing = false;
                ReleaseArrow();
            }
        }
    }

    void UpdateBowStringPosition()
    {
        // 활시위의 위치를 당긴 거리만큼 이동
        bowStringAnchor.localPosition = new Vector3(0, 0, -drawDistance);
    }

    void ReleaseArrow()
    {
        // 화살 생성 및 발사
        GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, arrowSpawnPoint.rotation);
        Rigidbody rb = arrow.GetComponent<Rigidbody>();
        rb.AddForce(arrowSpawnPoint.forward * drawDistance * arrowForce);
    }
}
