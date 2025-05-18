using UnityEngine;

public class Mainmenu : MonoBehaviour
{
  public void StartGame()
  {
    // Load the game scene
    UnityEngine.SceneManagement.SceneManager.LoadScene("Level1");
  }
}
