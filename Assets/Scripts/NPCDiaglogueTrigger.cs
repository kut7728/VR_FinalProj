using UnityEngine;

public class NPCDialogueTrigger : MonoBehaviour
{
    public GameObject dialogueUIPanel;    // UI Panel (대화 인터페이스). Inspector에서 드래그 앤 드롭
    public string playerTag = "Player";  // 플레이어의 태그 (기본: Player)

    private bool playerInRange = false;  // 플레이어가 콜라이더 범위 안에 있는지 체크

    void Start()
    {
        // 처음엔 대화 패널 비활성화
        if (dialogueUIPanel != null)
        {
            dialogueUIPanel.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // 플레이어가 콜라이더에 들어왔는지 확인
        if (other.CompareTag(playerTag))
        {
            playerInRange = true;
            Debug.Log("대화 시작!");

            // 대화 UI 패널 활성화
            if (dialogueUIPanel != null)
            {
                dialogueUIPanel.SetActive(true);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        // 플레이어가 콜라이더를 나갔는지 확인
        if (other.CompareTag(playerTag))
        {
            playerInRange = false;

            // 대화 UI 패널 비활성화
            if (dialogueUIPanel != null)
            {
                dialogueUIPanel.SetActive(false);
            }
        }
    }

    void Update()
    {
        // 플레어가 범위 안에 있고 특정 키(예: E)를 눌렀을 때 대화 시작
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            // 대화 UI에서 대화 시작 로직을 추가하거나 처리
            Debug.Log("대화 시작!");
        }
    }
}
