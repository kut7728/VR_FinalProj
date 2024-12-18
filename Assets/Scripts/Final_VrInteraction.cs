using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InteractionHandler : MonoBehaviour
{
    [Header("Bow Settings")]
    public GameObject bow; // 장착된 활 오브젝트
    public Transform bowHandTransform; // 활 장착 위치
    public Vector3 bowTeleportPosition; // 활 장착 후 텔레포트 위치

    [Header("Sword Settings")]
    public GameObject sword; // 장착된 칼 오브젝트
    public Transform swordHandTransform; // 칼 장착 위치
    public Vector3 swordTeleportPosition; // 칼 장착 후 텔레포트 위치
    public Vector3 swordCompletionTeleportPosition; // 칼로 모든 작업 완료 후 복귀 위치
    public List<GameObject> strawStages; // 짚단 오브젝트 리스트

    [Header("Audio")]
    public AudioClip shootSound; // 활 쏠 때 사운드
    public AudioClip cutSound; // 칼로 짚단 베는 사운드

    [Header("XR Rig")]
    public GameObject xrRig; // 플레이어 XR Rig

    private int currentStrawStage = 0; // 짚단 단계
    private bool isBowEquipped = false; // 활 장착 여부
    private bool isSwordEquipped = false; // 칼 장착 여부

    private InputAction rightGripAction; // 오른쪽 Grip 버튼 액션
    private InputAction leftGripAction; // 왼쪽 Grip 버튼 액션
    private InputAction triggerButtonAction; // 오른쪽 Trigger 버튼 액션

    private AudioSource audioSource; // 오디오 소스

    private void Awake()
    {
        // Input Actions 생성
        rightGripAction = new InputAction(
            type: InputActionType.Button,
            binding: "<XRController>{RightHand}/grip" // 오른쪽 Grip 버튼
        );
        leftGripAction = new InputAction(
            type: InputActionType.Button,
            binding: "<XRController>{LeftHand}/grip" // 왼쪽 Grip 버튼
        );
        triggerButtonAction = new InputAction(
            type: InputActionType.Button,
            binding: "<XRController>{RightHand}/trigger" // 오른쪽 Trigger 버튼
        );

        // Input Actions 활성화
        rightGripAction.Enable();
        leftGripAction.Enable();
        triggerButtonAction.Enable();

        // AudioSource 초기화
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    private void Start()
    {
        // 모든 짚단 비활성화
        foreach (var straw in strawStages)
        {
            straw.SetActive(false);
        }
        if (strawStages.Count > 0)
        {
            strawStages[0].SetActive(true); // 첫 번째 짚단 활성화
        }
    }

    private void Update()
    {
        // 오른쪽 Grip 버튼 확인 (활 장착 및 텔레포트)
        if (rightGripAction.WasPressedThisFrame())
        {
            if (!isBowEquipped)
            {
                EquipBow();
            }
            else
            {
                TeleportToPosition(bowTeleportPosition);
            }
        }

        // 왼쪽 Grip 버튼 확인 (칼 장착 및 텔레포트)
        if (leftGripAction.WasPressedThisFrame())
        {
            if (!isSwordEquipped)
            {
                EquipSword();
            }
            else
            {
                TeleportToPosition(swordTeleportPosition);
            }
        }

        // 오른쪽 Trigger 버튼 확인 (활쏘기 또는 짚단 베기)
        if (triggerButtonAction.WasPressedThisFrame())
        {
            if (isBowEquipped)
            {
                ShootArrow();
            }
            else if (isSwordEquipped)
            {
                CutStraw();
            }
        }
    }

    private void EquipBow()
    {
        // 활 장착
        bow.transform.SetParent(bowHandTransform);
        bow.transform.localPosition = Vector3.zero;
        bow.transform.localRotation = Quaternion.Euler(0, 180, 0);

        // 물리 엔진 영향 제거
        Rigidbody rb = bow.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }

        Collider collider = bow.GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        isBowEquipped = true;
        isSwordEquipped = false; // 칼 비활성화
        Debug.Log("Bow equipped.");
    }

    private void EquipSword()
    {
        // 칼 장착
        sword.transform.SetParent(swordHandTransform);
        sword.transform.localPosition = new Vector3(0, -0.1f, 0); // 위치 조정
        sword.transform.localRotation = Quaternion.Euler(-90, 0, 0);

        // 물리 엔진 영향 제거
        Rigidbody rb = sword.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = true;
            rb.useGravity = false;
        }

        Collider collider = sword.GetComponent<Collider>();
        if (collider != null)
        {
            collider.enabled = false;
        }

        isSwordEquipped = true;
        isBowEquipped = false; // 활 비활성화
        Debug.Log("Sword equipped.");
    }

    private void TeleportToPosition(Vector3 position)
    {
        if (xrRig != null)
        {
            xrRig.transform.position = position;
            Debug.Log($"Teleported to: {position}");
        }
        else
        {
            Debug.LogError("XR Rig is not assigned.");
        }
    }

    private void ShootArrow()
    {
        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }

        Debug.Log("Arrow shot!");
        // 활쏘기 동작 추가 (화살 발사 로직)
    }

    private void CutStraw()
    {
        if (currentStrawStage < strawStages.Count - 1)
        {
            strawStages[currentStrawStage].SetActive(false); // 현재 짚단 비활성화
            currentStrawStage++;
            strawStages[currentStrawStage].SetActive(true); // 다음 짚단 활성화

            if (audioSource != null && cutSound != null)
            {
                audioSource.PlayOneShot(cutSound);
            }

            Debug.Log($"Straw stage {currentStrawStage + 1} activated.");
        }
        else
        {
            Debug.Log("All straw stages completed.");
            CompleteSwordSequence();
        }
    }

    private void CompleteSwordSequence()
    {
        Debug.Log("Sword sequence completed. Returning to base position.");
        TeleportToPosition(swordCompletionTeleportPosition); // 완료 후 복귀
    }

    private void OnDestroy()
    {
        // Input Actions 비활성화
        rightGripAction.Disable();
        leftGripAction.Disable();
        triggerButtonAction.Disable();
    }
}
