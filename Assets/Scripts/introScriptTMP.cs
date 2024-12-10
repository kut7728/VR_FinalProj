using System.Collections;
using TMPro;
using UnityEngine;

public class IntroScriptTMP : MonoBehaviour
{
    public TextMeshProUGUI mainText;  // TextMeshPro 텍스트 컴포넌트
    private string[] storyLines;  // 텍스트 배열
    private int currentLineIndex = 0;  // 현재 텍스트 순서

    void Start()
    {
        // 텍스트 내용 초기화
        storyLines = new string[]
        {
            // 텍스트 내용 => 오프닝 스크립트
            "조선 후기, 변화의 시대.",
            "두 세기 전, 나라를 뒤흔든 임진왜란과 병자호란은 역사를 갈라놓았다.",
            "왕조는 겨우 재건되었지만, 균열이 생긴 나라는 더 이상 과거의 영광을 되찾지 못했다.",
            
            
            "세상을 밝히던 우리 가문의 빛은 이제 희미해졌고, 남은 것은 가세가 기운 초라한 재산과 하인 몇 명뿐이었다.",
            
            "가난과 절망이 나를 덮쳤지만, 나는 아직 포기하지 않았다.",

            "'붓으로는 더 이상 배고픈 백성들과 쇠락한 가문을 일으킬 수 없다'",
            "‘나라를 지키고, 가족의 이름을 되찾기 위해서는 칼을 들어야 할때이다’",
            
            "나의 목표는 단 하나. 무과에 장원급제하여 가문의 이름을 다시 세우고, 조선 최고의 명장을 꿈꾸는 것.",
            
            "가문의 영광을 되찾기 위한 싸움이 이제 시작된다."
        };

        // 코루틴으로 텍스트가 순차적으로 나타나도록 설정
        StartCoroutine(DisplayStory());
    }

    IEnumerator DisplayStory()
    {
        // 텍스트 배열 줄 단위로 출력
        while (currentLineIndex < storyLines.Length)
        {
            mainText.text = storyLines[currentLineIndex];  // 현재 텍스트 설정
            currentLineIndex++;  // 다음 줄로 이동

            // 텍스트 간 대기시간 (3초)
            yield return new WaitForSeconds(3f);
        }
        // 모든 텍스트 출력 완료 후 작업 추가 (다음 씬 이동 등)
        OnIntroComplete();
    }

    void OnIntroComplete()
    {
        // 예: 다음 씬으로 이동
        Debug.Log("인트로 스토리 출력 완료");
        UnityEngine.SceneManagement.SceneManager.LoadScene("Scene2");
    }
}
