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
            "이곳은 조선 중앙군 훈련도감의 훈련원일세.",
            "자네 같은 신참들에게 보급과 조직구조, 전술, 궁술, 검술, 포술등",
            "각종 군사훈련과 지식을 가르치는 중앙군 직속의 훈련소지!",
            "먼저 보급과 함께 전술과 각종 신호체계를 공부하게나",
            "모두 익혔다면 반대쪽 문을 통해 밖으로 이동해서 무술 교습소로 이동하게."
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
