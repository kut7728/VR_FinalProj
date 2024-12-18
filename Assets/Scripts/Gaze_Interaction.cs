using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;

public class GazeInteractionTMP : MonoBehaviour
{
    public GameObject uiBox; // UI 박스 (Canvas)
    public TextMeshProUGUI uiText; // TextMesh Pro 텍스트

    private void Start()
    {
        // UI 비활성화 (처음에는 텍스트 숨김)
        uiBox.SetActive(false);
    }

    public void ShowText()
    {
        // 텍스트 활성화 및 내용 설정
        uiBox.SetActive(true);
        uiText.text = "archery practice";
    }

    public void HideText()
    {
        // 텍스트 숨기기
        uiBox.SetActive(false);
    }
}
