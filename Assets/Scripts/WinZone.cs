using UnityEngine;
using UnityEngine.SceneManagement;

public class WinZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.name == "Player") {
            SceneManager.LoadScene("Scenes/LevelClear");
            Level level = GameController.currentLevel;
            int maxScore = level.CoinCount();
            int score = ScoreController.score;
            SaveData.instance.completedLevels[LevelSelectMenu.num] = maxScore == score ? 2 : 1;
            SaveData.instance.Save();
        }
    }
}
