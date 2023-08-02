using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DebugKeys : MonoBehaviour
{
  [SerializeField] private KeyCode loadNextLevelKey = KeyCode.L;
  [SerializeField] private KeyCode disableCollisionKey = KeyCode.C;
  private CollisionHandler cHandler_script;
  // Start is called before the first frame update
  void Start()
  {
    cHandler_script = GetComponent<CollisionHandler>();
    // Debug.Log(cHandler_script);
  }

  // Update is called once per frame
  void Update()
  {
    loadNextLevel();
    disableCollision();
    quitApplication();
  }

  void loadNextLevel()
  {

    if (Input.GetKeyDown(loadNextLevelKey))
    {
      Debug.Log("Loading next level (admin)");
      int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
      int nextSceneIndex = currentSceneIndex + 1;
      if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
      {
        nextSceneIndex = 0;
      }

      SceneManager.LoadScene(nextSceneIndex);
    }
  }

  void disableCollision()
  {
    if (Input.GetKeyDown(disableCollisionKey))
    {
      cHandler_script.collisionIsDisabled = !cHandler_script.collisionIsDisabled;
      Debug.Log(cHandler_script.collisionIsDisabled ? "Collision disabled" : "Collision enabled");
    }
  }

  void quitApplication()
  {
    if (Input.GetKeyDown(KeyCode.Escape))
    {
      Application.Quit();
    }
  }
}
