using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro 관련 네임스페이스 추가
using UnityEngine.UI; // UI 버튼 네임스페이스 추가

public class infoPanelController : MonoBehaviour
{
    public GameObject panel; // 패널 GameObject (UI 전체를 포함)
    public TextMeshProUGUI storyText; // TextMeshProUGUI 컴포넌트 연결
    public Button nextButton; // "다음으로" 버튼 연결

    private string[] storyParagraphs; // 문장 배열
    private int currentIndex = 0; // 현재 문장 인덱스

    void Start()
    {
        // 스토리를 배열에 작성
        storyParagraphs = new string[]
        {
            "이곳은 잊혀진 왕국의 폐허입니다.",
            "100년 전, 이 왕국은 엄청난 재앙을 맞이하면서 모든 것이 사라졌습니다.",
            "당신의 임무는 이 왕국의 비밀을 밝혀내는 것입니다.",
            "시작하세요. 이제 모험이 기다립니다!"
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

    void OnNextButtonClicked()
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
