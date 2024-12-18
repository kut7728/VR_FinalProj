using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class XRInteractionManager : MonoBehaviour
{
    public GameObject standardObject; // Standard GameObject
    public GameObject hakyikjinSetup; // HakyikjinSetup GameObject
    public GameObject heajindoSetup;  // HeajindoSetup GameObject


    public float delayBeforeActivation = 2f; // 2초 후 오브젝트 전환
    public float resetDelay = 10f;           // 초기화 시간 (기본 10초)

    private XRBaseInteractor interactor; // XR 컨트롤러 인터랙터

    private void Start()
    {
        // XR Interaction Manager의 인터랙터 설정
        interactor = FindObjectOfType<XRBaseInteractor>();
    }

    public void OnTriggerInteraction(XRBaseInteractor controller)
    {
        if (gameObject.name.Contains("Hakyikjin"))
        {
            StartCoroutine(HandleInteraction(hakyikjinSetup));
        }
        else if (gameObject.name.Contains("TurtleShipHeajindo"))
        {
            StartCoroutine(HandleInteraction(heajindoSetup));
        }
    }

    IEnumerator HandleInteraction(GameObject setupObject)
    {

        // 2초 후 Standard 비활성화 및 특정 Setup 활성화
        yield return new WaitForSeconds(delayBeforeActivation);

        if (standardObject != null)
        {
            standardObject.SetActive(false);
        }

        if (setupObject != null)
        {
            setupObject.SetActive(true);
        }

        // 10초 후 초기화
        yield return new WaitForSeconds(resetDelay);

        // Standard 다시 활성화 및 나머지 비활성화
        if (standardObject != null)
        {
            standardObject.SetActive(true);
        }

        if (hakyikjinSetup != null)
        {
            hakyikjinSetup.SetActive(false);
        }

        if (heajindoSetup != null)
        {
            heajindoSetup.SetActive(false);
        }
    }
}
