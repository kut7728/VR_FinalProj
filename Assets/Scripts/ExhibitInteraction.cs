using UnityEngine;
using TMPro; // TextMeshPro 네임스페이스 포함

public class ExhibitInteraction : MonoBehaviour
{
    public GameObject uiPanel; // UI 패널
    public TextMeshProUGUI descriptionText; // TMP 텍스트 필드
    private bool isPanelVisible = false; // UI 상태 저장
    private GameObject currentExhibit = null; // 현재 선택된 전시물

    void Start()
    {
        if (uiPanel == null)
        {
            Debug.LogError("uiPanel이 설정되지 않았습니다! Inspector에서 연결해 주세요.");
        }

        if (descriptionText == null)
        {
            Debug.LogError("descriptionText가 설정되지 않았습니다! Inspector에서 연결해 주세요.");
        }

        // 처음 시작 시 UI 비활성화
        uiPanel.SetActive(false);
    }

    public void OnExhibitSelected(GameObject exhibit)
    {
        if (exhibit == null)
        {
            Debug.LogError("OnExhibitSelected: Exhibit GameObject가 null입니다!");
            return;
        }

        if (currentExhibit == exhibit) // 이미 같은 전시물이 선택되었는지 확인
        {
            ToggleDescription();
            return;
        }

        currentExhibit = exhibit; // 현재 선택 업데이트
        ShowDescription(exhibit);
    }

    private void ShowDescription(GameObject exhibit)
    {
        if (exhibit == null)
        {
            Debug.LogError("ShowDescription: Exhibit GameObject가 null입니다!");
            return;
        }

        // ExhibitInfo 컴포넌트를 가져옴
        ExhibitInfo exhibitInfo = exhibit.GetComponent<ExhibitInfo>();
        if (exhibitInfo == null)
        {
            Debug.LogError("ShowDescription: ExhibitInfo 컴포넌트를 " + exhibit.name + "에서 찾을 수 없습니다.");
            descriptionText.text = "설명이 없습니다."; // 설명이 없을 경우 기본값
        }
        else
        {
            descriptionText.text = string.IsNullOrEmpty(exhibitInfo.description) ? "설명 없음." : exhibitInfo.description;
        }

        uiPanel.SetActive(true); // UI 패널 활성화
        isPanelVisible = true;
    }

    private void HideDescription()
    {
        isPanelVisible = false;
        currentExhibit = null;
        uiPanel.SetActive(false); // UI 패널 비활성화
    }

    private void ToggleDescription()
    {
        if (isPanelVisible)
        {
            HideDescription();
        }
        else if (currentExhibit != null)
        {
            ShowDescription(currentExhibit);
        }
    }
}
