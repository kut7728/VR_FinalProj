using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class shipTriggerChange : MonoBehaviour
{
    public GameObject objectB; // 활성화할 오브젝트
    public GameObject objectC; // 비활성화할 오브젝트
    private bool isON = true;

    void Start()
    {
        // Debug.Log로 이벤트가 잘 설정됐는지 확인
        Debug.Log("XR Simple Interaction 스크립트가 초기화되었습니다.");
    }

    public void OnSelectEntered(SelectEnterEventArgs args)
    {
        // ObjectB 활성화
        if (isON == false)
        {
            objectB.SetActive(false);
            objectC.SetActive(true);
            Debug.Log("Object B가 활성화되었습니다.");
            Debug.Log("Object C가 비활성화되었습니다.");
            isON = true;
        }

        // ObjectC 비활성화
        else if (isON == true)
        {
            objectB.SetActive(true);
            objectC.SetActive(false);
            isON = false;
            
            
        }
    }
}
