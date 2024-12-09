using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BowAttachAndTeleport : MonoBehaviour
{
    public GameObject bow; // 장착할 활 오브젝트
    public Transform handTransform; // 플레이어 손의 위치
    public Vector3 teleportPosition = new Vector3(-6.933764f, 0f, 10.24331f); // 텔레포트 위치
    public GameObject xrRig; // XR Rig (플레이어 객체)
    public AudioClip clickSound; // 클릭 시 재생할 MP3 파일
    private AudioSource audioSource; // 오디오 소스

    private bool isBowEquipped = false; // 활 장착 여부
    private InputAction gripButtonAction; // Grip 버튼 액션
    private InputAction triggerButtonAction; // Trigger 버튼 액션

    private void Awake()
    {
        // Grip 버튼 Input Action 생성
        gripButtonAction = new InputAction(
            type: InputActionType.Button,
            binding: "<XRController>{RightHand}/grip" // Grip 버튼 매핑
        );
        gripButtonAction.Enable(); // Input Action 활성화

        // Trigger 버튼 Input Action 생성
        triggerButtonAction = new InputAction(
            type: InputActionType.Button,
            binding: "<XRController>{RightHand}/trigger" // Trigger 버튼 매핑
        );
        triggerButtonAction.Enable(); // Input Action 활성화

        // AudioSource 초기화
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false; // 시작 시 재생하지 않음
        audioSource.clip = clickSound; // 지정한 MP3 파일 설정
    }

    void Update()
    {
        // Grip 버튼 입력 확인
        if (gripButtonAction.WasPressedThisFrame())
        {
            if (!isBowEquipped) // 활이 장착되지 않은 경우
            {
                EquipBow();
            }
            else // 활이 장착된 경우
            {
                TeleportPlayer();
            }
        }

        // Trigger 버튼 입력 확인
        if (triggerButtonAction.WasPressedThisFrame())
        {
            PlayClickSound(); // Trigger 버튼 클릭 시 사운드 재생
        }
    }

    private void EquipBow()
    {
        // 활을 플레이어의 손 위치로 이동
        bow.transform.SetParent(handTransform);

        // 활의 위치와 회전을 고정
        bow.transform.localPosition = Vector3.zero;
        bow.transform.localRotation = Quaternion.Euler(0, 180, 0); // 활을 180도 회전

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
            collider.enabled = false; // 컨트롤러와 충돌 방지
        }

        // 활 장착 상태 업데이트
        isBowEquipped = true;

        Debug.Log("Bow equipped!");
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

    private void PlayClickSound()
    {
        if (audioSource != null && clickSound != null)
        {
            audioSource.Play(); // MP3 파일 재생
            Debug.Log("Click sound played.");
        }
        else
        {
            Debug.LogWarning("AudioSource or AudioClip is missing!");
        }
    }

    private void OnDestroy()
    {
        // Input Action 비활성화
        gripButtonAction.Disable();
        triggerButtonAction.Disable();
    }
}
