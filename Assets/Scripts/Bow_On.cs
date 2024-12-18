using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class BowAttachWithPointing : MonoBehaviour
{
    public Transform handTransform; // 플레이어 손의 위치
    public Vector3 teleportPosition = new Vector3(-6.933764f, 0f, 10.24331f); // 텔레포트 위치
    private bool isBowEquipped = false;

    // Input Action
    private InputAction gripButtonAction;

    private XRRayInteractor rayInteractor; // XR Ray Interactor
    private GameObject pointedObject; // 현재 포인팅 중인 오브젝트

    private void Awake()
    {
        // Input Action 생성 및 Grip Button 매핑
        gripButtonAction = new InputAction(
            type: InputActionType.Button,
            binding: "<XRController>{RightHand}/grip" // Grip 버튼 매핑
        );
        gripButtonAction.Enable(); // Input Action 활성화

        // Ray Interactor 찾기
        rayInteractor = GetComponent<XRRayInteractor>();
        if (rayInteractor == null)
        {
            Debug.LogError("XR Ray Interactor가 필요합니다. 컨트롤러에 추가해주세요.");
        }
    }

    void Update()
    {
        // Grip 버튼이 눌렸을 때와 Raycast로 포인팅한 오브젝트가 있을 때
        if (gripButtonAction.WasPressedThisFrame() && TryGetPointedObject(out pointedObject))
        {
            if (!isBowEquipped && pointedObject.CompareTag("Bow")) // 활인지 확인
            {
                EquipBow(pointedObject);
            }
        }
    }

    private bool TryGetPointedObject(out GameObject pointedObj)
    {
        pointedObj = null;

        if (rayInteractor != null && rayInteractor.TryGetCurrent3DRaycastHit(out RaycastHit hit))
        {
            pointedObj = hit.collider.gameObject; // Ray가 충돌한 오브젝트 가져오기
            return true;
        }

        return false;
    }

    private void EquipBow(GameObject bow)
    {
        // 활을 플레이어의 손 위치로 이동
        bow.transform.SetParent(handTransform);

        // 활의 위치와 회전을 고정
        bow.transform.localPosition = Vector3.zero;
        //bow.transform.localRotation = Quaternion.Euler(0, 180, 0); // 활을 180도 회전

        // Rigidbody와 충돌 문제 해결
        Rigidbody rb = bow.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true; // 물리 엔진 영향 제거
            rb.useGravity = false; // 중력 비활성화
        }

        Collider collider = bow.GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = false; // 컨트롤러와 충돌 방지 (필요 시)
        }

        // 플레이어의 위치를 텔레포트 위치로 변경
        Transform playerTransform = Camera.main.transform; // 플레이어 Transform 가져오기
        playerTransform.position = teleportPosition;

        // 활 장착 상태 업데이트
        isBowEquipped = true;

        Debug.Log("Bow equipped and teleported!");
    }

    private void OnDestroy()
    {
        // Input Action 비활성화
        gripButtonAction.Disable();
    }
}
