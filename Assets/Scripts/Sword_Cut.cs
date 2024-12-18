using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StrawCutSequence : MonoBehaviour
{
    [Tooltip("짚단 오브젝트들을 긴 순서부터 모두 베어진 순서까지 드래그 앤 드롭으로 추가하세요.")]
    public List<GameObject> strawStages; // 짚단 오브젝트 리스트

    [Tooltip("Trigger 버튼을 눌렀을 때 재생할 사운드 클립을 드래그 앤 드롭하세요.")]
    public AudioClip cutSound; // 재생할 MP3 파일

    [Tooltip("장착된 칼 오브젝트를 지정하세요.")]
    public GameObject sword; // 장착된 칼 오브젝트

    [Tooltip("텔레포트할 위치를 지정하세요.")]
    public Vector3 teleportPosition; // 텔레포트 위치

    [Tooltip("플레이어의 XR Rig를 지정하세요.")]
    public GameObject xrRig; // XR Rig (플레이어 객체)

    private int currentStageIndex = 0; // 현재 활성화된 단계의 인덱스
    private InputAction triggerButtonAction; // Trigger 버튼 액션
    private AudioSource audioSource; // AudioSource 컴포넌트

    private void Awake()
    {
        // Trigger 버튼 Input Action 생성
        triggerButtonAction = new InputAction(
            type: InputActionType.Button,
            binding: "<XRController>{LeftHand}/trigger" // 왼쪽 Trigger 버튼 매핑
        );
        triggerButtonAction.Enable(); // Input Action 활성화

        // AudioSource 컴포넌트 추가
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false; // 시작 시 자동 재생하지 않음
        audioSource.clip = cutSound; // 지정된 MP3 파일 설정
    }

    private void Start()
    {
        // 모든 짚단 단계를 비활성화하고 첫 번째 단계만 활성화
        for (int i = 0; i < strawStages.Count; i++)
        {
            strawStages[i].SetActive(i == 0); // 첫 번째 단계만 활성화
        }
    }

    private void Update()
    {
        // Trigger 버튼을 눌렀을 때
        if (triggerButtonAction.WasPressedThisFrame())
        {
            PlayCutSound(); // 사운드 재생
            AdvanceToNextStage(); // 짚단 단계를 진행
        }
    }

    private void AdvanceToNextStage()
    {
        if (currentStageIndex < strawStages.Count - 1)
        {
            // 현재 단계 비활성화
            strawStages[currentStageIndex].SetActive(false);

            // 다음 단계 활성화
            currentStageIndex++;
            strawStages[currentStageIndex].SetActive(true);

            Debug.Log($"Stage {currentStageIndex + 1} activated: {strawStages[currentStageIndex].name}");
        }
        else
        {
            Debug.Log("All straw stages have been activated.");
            FinalizeSequence(); // 마지막 단계에서 칼 비활성화 및 텔레포트
        }
    }

    private void FinalizeSequence()
    {
        // 장착된 칼 비활성화
        if (sword != null)
        {
            sword.SetActive(false);
            Debug.Log("Sword has been disabled.");
        }

        // XR Rig를 텔레포트 위치로 이동
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

    private void PlayCutSound()
    {
        if (audioSource != null && cutSound != null)
        {
            audioSource.Play(); // MP3 파일 재생
            Debug.Log("Cut sound played.");
        }
        else
        {
            Debug.LogWarning("AudioSource or AudioClip is missing!");
        }
    }

    private void OnDestroy()
    {
        // Input Action 비활성화
        triggerButtonAction.Disable();
    }
}
