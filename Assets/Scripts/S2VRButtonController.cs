using UnityEngine;

public class S2VRButtonController : MonoBehaviour
{
    public scene2infoPanelController infoPanelController; // 스토리 패널 관리 스크립트 참조

    void Update()
    {
        // VR 컨트롤러에서 특정 Input (예: Oculus Trigger)을 감지
        if (Input.GetButtonDown("Fire1")) // Fire1은 기본적으로 왼쪽 트리거에 매핑됨
        {
            infoPanelController.OnNextButtonClicked(); // 버튼 클릭 직접 호출
        }
    }
}
