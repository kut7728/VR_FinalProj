using UnityEngine;
using UnityEngine.SceneManagement; // 씬 전환을 위해 필요
using UnityEngine.XR.Interaction.Toolkit; // XR Interaction Toolkit을 사용하기 위해 필요

public class nextSceneButton : MonoBehaviour
{
    public GameObject hoverPanel; // "무기고로" UI 패널
    public string nextSceneName; // 이동할 다음 씬의 이름

    // XR Interaction Toolkit 이벤트 감지기
    private void OnEnable()
    {
        var interactable = GetComponent<XRBaseInteractable>();
        if (interactable != null)
        {
            interactable.hoverEntered.AddListener(OnHoverEnter);
            interactable.hoverExited.AddListener(OnHoverExit);
            interactable.selectEntered.AddListener(OnSelectEnter);
        }
        else
        {
            Debug.LogError("XRBaseInteractable 컴포넌트가 이 오브젝트에 없습니다. 반드시 추가하세요!");
        }
    }

    private void OnDisable()
    {
        var interactable = GetComponent<XRBaseInteractable>();
        if (interactable != null)
        {
            interactable.hoverEntered.RemoveListener(OnHoverEnter);
            interactable.hoverExited.RemoveListener(OnHoverExit);
            interactable.selectEntered.RemoveListener(OnSelectEnter);
        }
    }

    private void Start()
    {
        // 시작 시 패널을 비활성화 상태로 설정
        if (hoverPanel != null)
        {
            hoverPanel.SetActive(false);
        }
    }

    public void OnHoverEnter(HoverEnterEventArgs args)
    {
        // Ray가 이 오브젝트를 바라볼 때 패널 활성화
        if (hoverPanel != null)
        {
            hoverPanel.SetActive(true);
        }
        Debug.Log("Ray로 쳐다봤습니다: Hover Enter");
    }

    public void OnHoverExit(HoverExitEventArgs args)
    {
        // Ray가 이 오브젝트에서 벗어날 때 패널 비활성화
        if (hoverPanel != null)
        {
            hoverPanel.SetActive(false);
        }
        Debug.Log("Ray가 벗어났습니다: Hover Exit");
    }

    public void OnSelectEnter(SelectEnterEventArgs args)
    {
        // Ray로 클릭했을 때 다음 씬 로드
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            Debug.Log("클릭으로 다음 씬으로 이동합니다: " + nextSceneName);
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogError("씬 이름이 설정되지 않았습니다! Inspector에서 씬 이름을 설정하세요.");
        }
    }
}
