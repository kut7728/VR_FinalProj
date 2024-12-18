using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class TriggerHoldToChangeScene : MonoBehaviour
{
    public XRController controller; // VR 컨트롤러를 할당
    public InputActionProperty triggerAction; // Input Action을 연결
    public string nextSceneName; // 전환할 씬 이름
    public float requiredHoldTime = 3.0f; // 버튼을 눌러야 하는 시간 (초)

    private float holdTime = 0.0f; // 버튼이 눌린 시간을 추적하는 변수
    private bool isHolding = false; // 버튼이 눌리고 있는 상태 여부

    void Update()
    {
        // 1. 트리거 버튼의 값 읽기 (0~1 범위)
        float triggerValue = triggerAction.action.ReadValue<float>();
        
        // 2. 버튼이 눌린 상태인지 확인 (일정 값 이상 눌린 경우)
        if (triggerValue > 0.8f) // 0.8f는 "트리거가 거의 완전히 눌린 상태"를 의미
        {
            // 만약 버튼을 누르기 시작했다면 딱 한 번 로그를 출력
            if (!isHolding)
                Debug.Log("트리거 버튼 눌림 시작");
            
            // 버튼이 눌렸음을 기록
            isHolding = true;

            // 누르고 있는 상태를 추적 (시간 누적)
            holdTime += Time.deltaTime;

            // 3. 3초 이상 지속되었는지 확인
            if (holdTime >= requiredHoldTime)
            {
                Debug.Log($"트리거 버튼을 {requiredHoldTime}초 동안 눌렀음. 다음 씬으로 이동 중...");
                
                // 씬 전환
                LoadNextScene();
            }
        }
        else
        {
            // 버튼을 뗀 경우
            if (isHolding)
            {
                Debug.Log($"트리거 버튼에서 손을 뗌. 누른 시간: {holdTime}초");
            }

            // 상태 초기화
            isHolding = false;
            holdTime = 0.0f;
        }
    }

    // 다음 씬을 불러오는 함수
    private void LoadNextScene()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogError("다음 씬 이름이 설정되지 않았습니다!");
        }
    }
}
