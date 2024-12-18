using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TeleportWithBow : MonoBehaviour
{
    public Transform handTransform; // 플레이어 손의 위치
    public Vector3 teleportPosition = new Vector3(-6.933764f, 0f, 10.24331f); // 텔레포트 위치
    public GameObject bow; // 활 오브젝트
    public GameObject xrRig; // XR Rig (플레이어 객체)

    private InputAction gripButtonAction; // Grip 버튼 액션

    private void Awake()
    {
        // Grip 버튼 Input Action 생성
        gripButtonAction = new InputAction(
            type: InputActionType.Button,
            binding: "<XRController>{RightHand}/grip" // Grip 버튼 매핑
        );
        gripButtonAction.Enable(); // Input Action 활성화
    }

    void Update()
    {
        // Grip 버튼을 눌렀을 때 활이 손에 장착된 상태인지 확인
        if (gripButtonAction.WasPressedThisFrame() && IsBowEquipped())
        {
            TeleportPlayer();
        }
    }

    private bool IsBowEquipped()
    {
        // 활이 손의 자식 오브젝트로 설정되어 있으면 장착된 것으로 간주
        return bow != null && bow.transform.parent == handTransform;
    }

    private void TeleportPlayer()
    {
        if (xrRig != null)
        {
            // XR Rig의 Transform을 텔레포트 위치로 이동
            xrRig.transform.position = teleportPosition;
            Debug.Log("Player teleported to: " + teleportPosition);
        }
        else
        {
            Debug.LogError("XR Rig is not assigned. Please assign it in the Inspector.");
        }
    }

    private void OnDestroy()
    {
        // Input Action 비활성화
        gripButtonAction.Disable();
    }
}
