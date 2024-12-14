
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine;
using TMPro; // TextMesh Pro 네임스페이스 추가
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement; // 씬 전환을 위한 네임스페이스

public class DialogueManager : MonoBehaviour
{
    [Header("UI Elements")]
    public TextMeshProUGUI dialogueText; // TextMesh Pro UI Text
    public GameObject dialoguePanel; // 대화창 패널

    [Header("Dialogue Settings")]
    public string[] dialogues; // 대사 배열
    private int currentDialogueIndex = 0; // 현재 대사의 인덱스

    private InputAction leftTriggerAction; // 왼쪽 Trigger 버튼 액션

    private void Awake()
    {
        // 왼쪽 Trigger 버튼 Input Action 생성
        leftTriggerAction = new InputAction(
            type: InputActionType.Button,
            binding: "<XRController>{LeftHand}/trigger" // 왼쪽 Trigger 버튼 매핑
        );
        leftTriggerAction.Enable(); // Input Action 활성화
    }

    private void Start()
    {
        // 대화 패널 초기화
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(true); // 대화창 활성화
        }

        // 첫 번째 대사 출력
        UpdateDialogue();
    }

    private void Update()
    {
        // 왼쪽 Trigger 버튼 입력 확인
        if (leftTriggerAction.WasPressedThisFrame())
        {
            AdvanceDialogue(); // 다음 대사로 진행
        }
    }

    private void AdvanceDialogue()
    {
        if (currentDialogueIndex < dialogues.Length - 1)
        {
            currentDialogueIndex++; // 다음 대사로 진행
            UpdateDialogue(); // 대사 업데이트
        }
        else
        {
            EndDialogue(); // 대화 종료
        }
    }

    private void UpdateDialogue()
    {
        if (dialogueText != null)
        {
            dialogueText.text = dialogues[currentDialogueIndex]; // 현재 대사 업데이트
            Debug.Log($"Dialogue updated: {dialogues[currentDialogueIndex]}");
        }
    }

    private void EndDialogue()
    {
        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false); // 대화창 비활성화
        }
        Debug.Log("Dialogue ended.");

        // 다음 씬으로 전환 (빌드 인덱스 기반)
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex); // 다음 씬 로드
        }
        else
        {
            Debug.LogWarning("No more scenes in the build order!");
        }
    }

    private void OnDestroy()
    {
        // Input Action 비활성화
        leftTriggerAction.Disable();
    }
}
