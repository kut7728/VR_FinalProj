using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // TextMeshPro 관련 네임스페이스 추가
using UnityEngine.UI; // UI 버튼 네임스페이스 추가

public class scene2infoPanelController : MonoBehaviour
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
            "이곳은 조선 중앙군 훈련도감 행관청일세.",
            "새롭게 들어올 무관에 대한 군수품 보급과 기초 전략 지식을 익히게 하는 기관이지.",
            "쭈욱 돌아보면서 구군복과 기초 전술교본, 신호체계를 익히도록 하게나",
            "충분히 익혔다면 반대편 문으로 야외 전술훈련소로 이동하여 기본 전술 훈련을 받도록!"
            
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
