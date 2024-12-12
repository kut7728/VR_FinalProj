using UnityEngine;
using UnityEngine.SceneManagement; // 씬 전환 이벤트를 처리하기 위해 필요

public class MusicManager : MonoBehaviour
{
    private static MusicManager instance; // 싱글톤 패턴 사용
    private AudioSource audioSource; // 음악 재생용 AudioSource
    private bool isFirstScene = true; // 첫 로드 여부를 추적하는 플래그

    void Awake()
    {
        // 이 오브젝트가 이미 존재하면 새로 생성된 것을 파괴 (중복 방지)
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            // 첫 번째로 생성된 MusicManager를 유지
            instance = this;
            DontDestroyOnLoad(gameObject); // 씬 이동 시 파괴되지 않도록 설정
        }

        // AudioSource 컴포넌트를 가져옴 (존재하지 않으면 자동 추가)
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void OnEnable()
    {
        // 씬 로드 완료 이벤트에 OnSceneLoaded 메서드 등록
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        // 씬 로드 완료 이벤트에서 메서드 해제
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Start()
    {
        // 첫 로드시 볼륨은 1로 설정
        SetVolume(1.0f);
    }

    // 씬이 로드된 후 호출되는 메서드
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (isFirstScene)
        {
            // 첫 번째 씬 이후에는 플래그를 false로 설정
            isFirstScene = false;
            Debug.Log($"첫 씬 로드 완료: {scene.name}, 볼륨 유지 (1.0)");
        }
        else
        {
            // 그 이후 씬에는 볼륨을 0.5로 설정
            SetVolume(0.5f);
            Debug.Log($"다음 씬 로드 완료: {scene.name}, 볼륨 조정 (0.5)");
        }
    }

    // 볼륨을 설정하는 메서드
    private void SetVolume(float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume = volume;
            Debug.Log("오디오 볼륨 설정: " + volume);
        }
        else
        {
            Debug.LogWarning("AudioSource가 설정되지 않았습니다!");
        }
    }
}
