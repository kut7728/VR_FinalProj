using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.InputSystem;

public class ShowObjectsWithTrigger : MonoBehaviour
{
    [Tooltip("보이게 할 3D 오브젝트들을 드래그 앤 드롭으로 추가하세요.")]
    public List<GameObject> objectsToShow; // 활성화할 오브젝트 리스트

    [Tooltip("활 오브젝트를 지정하세요.")]
    public GameObject bow; // 장착된 활 오브젝트

    [Tooltip("텔레포트할 위치를 지정하세요.")]
    public Vector3 teleportPosition; // 텔레포트 위치

    [Tooltip("플레이어의 XR Rig를 지정하세요.")]
    public GameObject xrRig; // XR Rig (플레이어 객체)

    private int currentIndex = 0; // 현재 활성화할 오브젝트 인덱스
    private InputAction triggerButtonAction; // Trigger 버튼 액션

    private void Awake()
    {
        // Trigger 버튼 Input Action 생성
        triggerButtonAction = new InputAction(
            type: InputActionType.Button,
            binding: "<XRController>{RightHand}/trigger" // Trigger 버튼 매핑
        );
        triggerButtonAction.Enable(); // Input Action 활성화
    }

    private void Start()
    {
        // 모든 오브젝트를 비활성화
        foreach (var obj in objectsToShow)
        {
            obj.SetActive(false);
        }
    }

    private void Update()
    {
        // Trigger 버튼을 눌렀을 때
        if (triggerButtonAction.WasPressedThisFrame())
        {
            ShowNextObject();
        }
    }

    private void ShowNextObject()
    {
        if (currentIndex < objectsToShow.Count)
        {
            objectsToShow[currentIndex].SetActive(true); // 현재 인덱스의 오브젝트 활성화
            Debug.Log($"{objectsToShow[currentIndex].name} is now visible.");
            currentIndex++; // 다음 오브젝트로 진행
        }
        else
        {
            Debug.Log("All objects are now visible.");
            FinalizeSequence(); // 마지막 단계에서 활 비활성화 및 텔레포트
        }
    }

    private void FinalizeSequence()
    {
        // 활 비활성화
        if (bow != null)
        {
            bow.SetActive(false);
            Debug.Log("Bow has been disabled.");
        }

        // XR Rig 텔레포트
        if (xrRig != null)
        {
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
        triggerButtonAction.Disable();
    }
}
