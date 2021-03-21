using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    [SerializeField] private PopUp winPopUp;
    [SerializeField] private PopUp losePopUp;
    [SerializeField] private OptionsPopup optionsPopup;
    [SerializeField] private Button optinsButton;
    [SerializeField] Slider levelProgress;
    [SerializeField] Slider fireProgress;
    [SerializeField] Animator fireProgressAnimator;
    [SerializeField] Slider TimerSlider;
    [SerializeField] Text LevelNumber;
    [SerializeField] GameObject levelStartPopUp;
    public static bool OptionsWindwOpened;


    private void Start()
    {
        GameController.OnGameLose += ShowLosePpUp;
        GameController.OnGameWin += ShowWinPopUp;
        LevelNumber.text = "LEVEL " + (UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        optinsButton.onClick.AddListener(ShowOptionsPopup);
    }

    private void Update()
    {
        if (Input.touchCount > 0 || Input.GetMouseButtonDown(0))
        {
            levelStartPopUp.SetActive(false);
        }
        levelProgress.value = (float)GameController.playersPas / (GameController.playerChars.Count -1);
        fireProgress.value = (float)ProjectileScript.FireAmount / ProjectileScript.FireToShoot;

        if (GameController.IsTimedGame)
        {
            TimerSlider.value = (GameTimer.Instance.timeLeft / GameTimer.Instance.timeToComplite);
        }
        fireProgressAnimator.SetInteger("Enemies",ProjectileScript.EnemiesInRange.Count);
    }

    private void OnDestroy()
    {
        GameController.OnGameLose -= ShowLosePpUp;
        GameController.OnGameWin -= ShowWinPopUp;
    }

    public void ShowWinPopUp()
    {
        winPopUp.gameObject.SetActive(true);
    }

    public void ShowLosePpUp()
    {
        losePopUp.gameObject.SetActive(true);
    }

    public void ShowOptionsPopup()
    {
        optionsPopup.gameObject.SetActive(true);
        optionsPopup.Show();
    }
}
