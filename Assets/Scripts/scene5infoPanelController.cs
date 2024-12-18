using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro 관련 네임스페이스 추가
using UnityEngine.UI; // UI 버튼 네임스페이스 추가

public class scene5infoPanelController : MonoBehaviour
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
            "모든 학습 과정을 성실히 수료하여 장원 급제했습니다.",
            "전하를 알현하러 근정전으로 가십시오. \n(바닥의 화살표를 따라 이동하시오.)"
            
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
