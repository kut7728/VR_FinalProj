using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwordAttachAndTeleport : MonoBehaviour
{
    public GameObject sword; // 장착할 검 오브젝트
    public Transform leftHandTransform; // 왼쪽 컨트롤러 손의 위치
    public Vector3 teleportPosition = new Vector3(10f, 0f, -5f); // 텔레포트 위치
    public GameObject xrRig; // XR Rig (플레이어 객체)

    private bool isSwordEquipped = false; // 검 장착 여부
    private InputAction gripButtonAction; // 왼쪽 Grip 버튼 액션

    private void Awake()
    {
        // Grip 버튼 Input Action 생성
        gripButtonAction = new InputAction(
            type: InputActionType.Button,
            binding: "<XRController>{LeftHand}/grip" // 왼쪽 Grip 버튼 매핑
        );
        gripButtonAction.Enable(); // Input Action 활성화
    }

    void Update()
    {
        // Grip 버튼 입력 확인
        if (gripButtonAction.WasPressedThisFrame())
        {
            if (!isSwordEquipped) // 검이 장착되지 않은 경우
            {
                EquipSword();
            }
            else // 검이 이미 장착된 경우
            {
                TeleportPlayer();
            }
        }
    }

private void EquipSword()
{
    // 검을 왼쪽 손 위치로 이동
    sword.transform.SetParent(leftHandTransform);

    // 검의 위치와 회전을 고정
    sword.transform.localPosition = new Vector3(0, -0.1f, 0); // 칼을 손에서 조금 아래로 위치

    
    sword.transform.localRotation = Quaternion.Euler(-90, 0, 0); // 칼의 위아래를 뒤집어 올바른 방향으로 조정


    // Rigidbody와 충돌 문제 해결
    Rigidbody rb = sword.GetComponent<Rigidbody>();
    if (rb != null)
    {
        rb.isKinematic = true; // 물리 엔진 영향 제거
        rb.useGravity = false; // 중력 비활성화
    }

    Collider collider = sword.GetComponent<Collider>();
    if (collider != null)
    {
        collider.enabled = false; // 컨트롤러와 충돌 방지
    }

    // 검 장착 상태 업데이트
    isSwordEquipped = true;

    Debug.Log("Sword equipped!");
}

    private void TeleportPlayer()
    {
        if (xrRig != null)
        {
            // XR Rig의 Transform을 텔레포트 위치로 이동
            xrRig.transform.position = teleportPosition;
            Debug.Log($"Player teleported to: {teleportPosition}");
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
