using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public TileBoard board;
    public CanvasGroup gameOver;
    public CanvasGroup gameWin;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI hiscoreText;

    public AudioSource winSound;
    public AudioSource gameOverAudio;

    private int score;

    private void Start()
    {
        NewGame();
    }

    public void NewGame()
    {
        SetScore(0);
        hiscoreText.text = LoadHiscore().ToString();
        gameOver.alpha = 0f;
        gameOver.interactable = false;
        gameWin.alpha = 0f;
        gameWin.interactable = false;

        board.ClearBoard();
        board.CreateTile();
        board.CreateTile();
        board.enabled = true;

    }
    
    public void GameOver()
    {
        board.enabled = false;
        gameOver.interactable = true;
        gameOverAudio.Play();
        StartCoroutine(Fade(gameOver, 1f, 1f));
    }

    private IEnumerator Fade(CanvasGroup canvasGroup, float to, float delay)
    {
        yield return new WaitForSeconds(delay);

        float elapsed = 0f;
        float duration = 0.5f;
        float from = canvasGroup.alpha;

        while (elapsed < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;

        }
        canvasGroup.alpha = to;

    }

    public void InscreaseScore(int points)
    {
        SetScore(score + points);
        
    }

    private void SetScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString();

        SaveHiscore();
    }

    private void SaveHiscore()
    {
        int hiscore = LoadHiscore();

        if(score > hiscore)
        {
            PlayerPrefs.SetInt("hiscore", score);
        }
    }

    public void WinGame()
    {
        board.enabled = false;
        gameWin.interactable = true;
        gameOverAudio.Play();
        StartCoroutine(Fade(gameWin, 1f, 1f));
    }

    private int LoadHiscore()
    {
        return PlayerPrefs.GetInt("hiscore", 0);
    }

    public void Continue()
    {
        board.enabled = true;
        gameWin.interactable = false;
    }

}
