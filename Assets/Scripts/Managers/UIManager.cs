// UIManager.cs
using UnityEngine;
using TMPro;
using WheelOfFortune.Managers;

namespace WheelOfFortune.UI
{
    public class UIManager : MonoBehaviour
    {
        [Header("Texts")]
        [SerializeField] private TextMeshProUGUI rewardsText;
        [SerializeField] private TextMeshProUGUI zoneTypeText;

        [Header("Buttons")]
        [SerializeField] private ButtonBase spinButton;
        [SerializeField] private ButtonBase collectButton;
        [SerializeField] private ButtonBase restartButton;

        [Header("Panels")]
        [SerializeField] private GameObject gameOverPanel;
        [SerializeField] private GameObject collectPanel;

        private void Start()
        {
            // Subscribe to buttons
            if (spinButton != null) spinButton.OnClick += HandleSpin;
            if (collectButton != null) collectButton.OnClick += HandleCollect;
            if (restartButton != null) restartButton.OnClick += HandleRestart;

            // Subscribe to game events
            GameManager.Instance.OnZoneChanged += UpdateZoneUI;
            GameManager.Instance.OnRewardsUpdated += UpdateRewardsUI;
            GameManager.Instance.OnGameOver += ShowGameOver;
            GameManager.Instance.OnGameWin += ShowCollect;

            // Initialize UI
            if (gameOverPanel != null) gameOverPanel.SetActive(false);
            if (collectPanel != null) collectPanel.SetActive(false);

            UpdateZoneUI(GameManager.Instance.CurrentZone);
            UpdateRewardsUI(GameManager.Instance.TotalRewards);
        }

        private void OnDestroy()
        {
            if (spinButton != null) spinButton.OnClick -= HandleSpin;
            if (collectButton != null) collectButton.OnClick -= HandleCollect;
            if (restartButton != null) restartButton.OnClick -= HandleRestart;

            if (GameManager.Instance != null)
            {
                GameManager.Instance.OnZoneChanged -= UpdateZoneUI;
                GameManager.Instance.OnRewardsUpdated -= UpdateRewardsUI;
                GameManager.Instance.OnGameOver -= ShowGameOver;
                GameManager.Instance.OnGameWin -= ShowCollect;
            }
        }

        private void HandleSpin()
        {
            GameManager.Instance.SpinWheel();
        }

        private void HandleCollect()
        {
            GameManager.Instance.CollectAndLeave();
        }

        private void HandleRestart()
        {
            if (gameOverPanel != null) gameOverPanel.SetActive(false);
            if (collectPanel != null) collectPanel.SetActive(false);
            GameManager.Instance.RestartGame();
        }

        private void UpdateZoneUI(int zone)
        {
            if (GameManager.Instance.IsSuperZone())
                zoneTypeText.text = "GOLDEN SPIN";
            else if (GameManager.Instance.IsSafeZone())
                zoneTypeText.text = "SILVER SPIN";
            else
                zoneTypeText.text = "BRONZE SPIN";
        }

        private void UpdateRewardsUI(int rewards)
        {
            rewardsText.text = rewards.ToString();
        }

        private void ShowGameOver()
        {
            if (gameOverPanel != null) gameOverPanel.SetActive(true);
        }

        private void ShowCollect()
        {
            if (collectPanel != null) collectPanel.SetActive(true);
        }
    }
}