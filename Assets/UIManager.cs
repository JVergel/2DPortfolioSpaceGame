using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using  TMPro;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI ammoText;
    [SerializeField]
    private  TextMeshProUGUI  scoreText;
    [SerializeField]
    private TextMeshProUGUI gameOverText;
    [SerializeField]
    private TextMeshProUGUI restartText;
    private Player player;
    [SerializeField]
    private Sprite[] LivesIMG;
    [SerializeField]
    private Image CurrentIMG;
    [SerializeField]
    private Sprite LivesSprite;
    [SerializeField]
    private Image Charge;
    private bool _GameOverState;
    public bool GameOverState
    {
        get
        {
            return _GameOverState;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        _GameOverState = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    // Update is called once per frame
    public void changeScore()
    {
        scoreText.text = "Score:" + player.Score;
    }
    public void changeAmmo()
    {
        ammoText.text = "Ammo:" + player.Ammo+ "/"+player.MaxAmmo;
    }
    public void changeLives(int lives)
    {
        if(lives>0)
        {
            CurrentIMG.sprite = LivesIMG[lives-1];
        }
        
    }
    public void changeCharge()
    {
        Charge.fillAmount = player.BoostPercentage();
    }
    public void ChangeGameState(int stateID)
    {
        ///ID 0 GamePaused
        ///ID 1 Gameover
        ///ID 2 GameOn
        if(stateID==1)
        {
            gameOverText.gameObject.SetActive(true);
            restartText.gameObject.SetActive(true);
            _GameOverState = true;
            StartCoroutine(FlickGameOver());
            Debug.Log(GameOverState);
        }
    }
    private IEnumerator FlickGameOver()
    {
        while (true)
        {
            gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.4f);
            gameOverText.text = " ";
            yield return new WaitForSeconds(0.4f);
        }

    }
    public IEnumerator FlickNewWave(int CurrentWave)
    {
        int Flicks = 5;
        while (Flicks>0)
        {
            gameOverText.text = "     Wave  "+ CurrentWave;
            yield return new WaitForSeconds(0.4f);
            gameOverText.text = " ";
            yield return new WaitForSeconds(0.4f);
            Flicks--;

        }

    }
    public void Restart(InputAction.CallbackContext context)
    {
        if (context.started == true)
        {
            Debug.Log("R");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            

        }

    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Escape(InputAction.CallbackContext context)
    {
        if (context.started == true)
        {
            Debug.Log("QUIT");
            Application.Quit();

        }

    }

}
