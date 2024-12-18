using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro 관련 네임스페이스 추가
using UnityEngine.UI; // UI 버튼 네임스페이스 추가

public class scene4infoPanelController : MonoBehaviour
{
    public GameObject panel; // 패널 GameObject (UI 전체를 포함)
    public TextMeshProUGUI storyText; // TextMeshProUGUI 컴포넌트 연결
    public Button nextButton; // "다음으로" 버튼 연결

    public string[] storyParagraphs; // 문장 배열
    private int currentIndex = 0; // 현재 문장 인덱스

    void Start()
    {
        // 스토리를 배열에 작성
        storyParagraphs = new string[]
        {
            "이번 훈련장에서는 화포류와 수군에 대하여 배울걸세.",
            "11시 방향에서는 그 외의 포와 보조도구에 대한 설명을 확인할 수 있네.",
            "익힌 화포 사용법에 대해서는 7시 방향에서 형사총통으로 실습해볼 수 있다네",
            "마지막으로 2시방향에서는 군선과 수전에서의 병법에 대한 정보를 확인하실 수 있다네.",
            "천천히 공부하고 충분히 익혔다면 부둣가 끝에서 무과등용령을 확인하여 자네가 어디로 배치될지 확인하게" 
            
        };

        // 첫 번째 문장 표시
        UpdateText();

        // 버튼에 클릭 이벤트 등록
        nextButton.onClick.AddListener(OnNextButtonClicked);
    }

    void UpdateText()
    {
        // 현재 인덱스에 맞는 문장 표시
        storyText.text = storyParagraphs[currentIndex];
    }

    public void OnNextButtonClicked()
    {
        // 현재 인덱스 증가
        currentIndex++;

        // 모든 문장을 표시했으면 패널 숨기기
        if (currentIndex >= storyParagraphs.Length)
        {
            panel.SetActive(false); // 패널 비활성화
        }
        else
        {
            UpdateText(); // 다음 문장 표시
        }
    }
}
