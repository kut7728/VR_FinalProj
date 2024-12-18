using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class MessageController : MonoBehaviour
{
    public GameObject Message1; // Message1 텍스트
    public GameObject Message2; // Message2 텍스트

    private int currentMessageIndex = 0; // 현재 메시지 상태

    private InputDevice leftController; // 왼쪽 컨트롤러
    private InputDevice rightController; // 오른쪽 컨트롤러

    void Start()
    {
        // 초기 메시지 상태 설정
        Message1.SetActive(true);
        Message2.SetActive(false);

        // XR 컨트롤러 초기화
        InitializeControllers();
    }

    void InitializeControllers()
    {
        // 왼쪽과 오른쪽 컨트롤러 가져오기
        var inputDevices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Left, inputDevices);
        if (inputDevices.Count > 0)
            leftController = inputDevices[0];

        InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.Right, inputDevices);
        if (inputDevices.Count > 0)
            rightController = inputDevices[0];
    }

    void Update()
    {
        // 컨트롤러 트리거 입력 확인
        CheckTriggerInput(leftController);
        CheckTriggerInput(rightController);
    }

    void CheckTriggerInput(InputDevice controller)
    {
        if (controller.isValid)
        {
            // 트리거 버튼 값 감지
            bool triggerPressed;
            if (controller.TryGetFeatureValue(CommonUsages.triggerButton, out triggerPressed) && triggerPressed)
            {
                OnMessageClicked(); // 트리거 입력 시 메시지 변경 함수 호출
            }
        }
    }

    public void OnMessageClicked()
    {
        if (currentMessageIndex == 0)
        {
            // Message1 -> Message2로 변경
            Message1.SetActive(false);
            Message2.SetActive(true);
            currentMessageIndex = 1;
        }
        else if (currentMessageIndex == 1)
        {
            // Message2 -> UI 비활성화
            Message2.SetActive(false);
            currentMessageIndex = 2; // 종료 상태
        }
    }
}
