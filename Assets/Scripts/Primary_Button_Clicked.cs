using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RightSecondaryButtonLogger : MonoBehaviour
{
    private InputAction secondaryButtonAction; // B 버튼 액션

    private void Awake()
    {
        // Secondary Button Input Action 생성
        secondaryButtonAction = new InputAction(
            type: InputActionType.Button,
            binding: "<XRController>{RightHand}/secondaryButton" // B 버튼 매핑
        );
        secondaryButtonAction.Enable(); // Input Action 활성화
    }

    private void Update()
    {
        // Secondary Button 입력 확인
        if (secondaryButtonAction.WasPressedThisFrame())
        {
            Debug.Log("Right Secondary Button (B) was pressed!");
        }
    }

    private void OnDestroy()
    {
        // Input Action 비활성화
        secondaryButtonAction.Disable();
    }
}
