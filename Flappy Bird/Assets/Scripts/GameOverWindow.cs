using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverWindow : MonoBehaviour
{
    private TextMeshProUGUI ScoreCountText;
    private Button RestartGameButton;

    private void Awake()
    {
        ScoreCountText = transform.Find("ScoreCountText").GetComponent<TextMeshProUGUI>();
        RestartGameButton = transform.Find("RestartGameButton").GetComponent<Button>();

        Debug.Log("GameOver Awake!");
    }

    private void Start()
    {
        Debug.Log("GameOver Start!");

        Bird.GetInstance().OnDied += Bird_OnDiedGameOver;
        RestartGameButton.onClick.AddListener(() => {
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
        });

        Hide();
    }

    private void Bird_OnDiedGameOver(object sender, System.EventArgs e)
    {
        ScoreCountText.text = Level.GetInstance().GetPipesPassedCount().ToString();
        Show();

        //UnityEngine.SceneManagement.SceneManager.LoadScene("GameScene");
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
}
