using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;


public class BowInteraction : MonoBehaviour
{
    public Transform player;               // 플레이어의 Transform (활이 붙을 대상)
    public Transform attachPoint;          // 활이 붙을 위치
    public XRNode controllerNode = XRNode.RightHand; // 사용할 컨트롤러 (오른손 컨트롤러)
    private InputDevice controller;        // 컨트롤러 객체
    private bool isAttached = false;       // 활이 사용자에게 붙었는지 여부

    void Start()
    {
        // 컨트롤러 초기화
        InitializeController();
    }

    void Update()
    {
        if (!controller.isValid)
        {
            // 컨트롤러가 유효하지 않다면 다시 초기화
            InitializeController();
        }

        // Trigger 값 읽기
        if (controller.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue) && triggerValue > 0.1f)
        {
            if (!isAttached)
            {
                AttachBow();
            }
        }
    }

    void InitializeController()
    {
        // 지정된 XRNode에 해당하는 컨트롤러를 찾음
        controller = InputDevices.GetDeviceAtXRNode(controllerNode);

        if (!controller.isValid)
        {
            Debug.LogWarning("Right hand controller not found.");
        }
        else
        {
            Debug.Log("Controller initialized: " + controller.name);
        }
    }

    void AttachBow()
    {
        // 활 위치를 사용자에게 붙임
        transform.position = attachPoint.position;
        transform.rotation = attachPoint.rotation;
        isAttached = true;

        // Console 메시지 출력
        Debug.Log("활을 착용했습니다.");
    }
}
