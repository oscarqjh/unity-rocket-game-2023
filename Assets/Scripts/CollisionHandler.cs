using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
  private Movement m_script;
  private AudioSource audioSource;
  [SerializeField] private float delayToNextLevel = 1f;
  [SerializeField] private float delayToReload = 1f;
  [SerializeField] private AudioClip crash;
  [SerializeField] private AudioClip success;
  [SerializeField] private ParticleSystem crashParticle;
  [SerializeField] private ParticleSystem successParticle;
  bool isTransitioning = false;
  public bool collisionIsDisabled = false;

  // Start is called before the first frame update
  void Start()
  {
    m_script = GetComponent<Movement>();
    audioSource = GetComponent<AudioSource>();
  }

  // Update is called once per frame
  void Update()
  {

  }

  private void OnCollisionEnter(Collision other)
  {
    if (isTransitioning || collisionIsDisabled)
    {
      return;
    }
    switch (other.gameObject.tag)
    {
      case "Friendly":
        Debug.Log("friendly");
        break;
      case "Finish":
        OnFinish(delayToNextLevel);
        break;
      default:
        OnCrash(delayToReload);
        break;
    }
  }

  private void OnCrash(float delay)
  {
    isTransitioning = true;
    audioSource.Stop();
    audioSource.PlayOneShot(crash);
    m_script.enabled = false;
    crashParticle.Play();

    Invoke("ReloadLevel", delay);


  }

  private void OnFinish(float delay)
  {
    isTransitioning = true;
    audioSource.Stop();
    audioSource.PlayOneShot(success);
    m_script.enabled = false;
    successParticle.Play();

    Invoke("LoadNextLevel", delay);

  }

  private void ReloadLevel()
  {
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    SceneManager.LoadScene(currentSceneIndex);
    m_script.enabled = true;
  }

  private void LoadNextLevel()
  {
    int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    int nextSceneIndex = currentSceneIndex + 1;
    if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
    {
      nextSceneIndex = 0;
    }

    SceneManager.LoadScene(nextSceneIndex);
    m_script.enabled = true;
  }
}
