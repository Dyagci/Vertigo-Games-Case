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
        [SerializeField] private TextMeshProUGUI rewardInfoText;

        [Header("Buttons")]
        [SerializeField] private ButtonBase spinButton;
        [SerializeField] private ButtonBase collectButton;
        [SerializeField] private ButtonBase restartButton;

        [Header("Panels")]
        [SerializeField] private GameObject mainPanel;
        [SerializeField] private GameObject gameOverPanel;

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
            GameManager.Instance.OnGameWin += HandleWin;
            GameManager.Instance.OnSpinStarted += HandleSpinStarted;
            GameManager.Instance.OnSpinEnded += HandleSpinEnded;

            // Initialize UI
            if (mainPanel != null) mainPanel.SetActive(true);
            if (gameOverPanel != null) gameOverPanel.SetActive(false);

            UpdateZoneUI(GameManager.Instance.CurrentZone);
            UpdateRewardsUI(GameManager.Instance.TotalRewards);
            UpdateCollectButtonVisibility();
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
                GameManager.Instance.OnGameWin -= HandleWin;
                GameManager.Instance.OnSpinStarted -= HandleSpinStarted;
                GameManager.Instance.OnSpinEnded -= HandleSpinEnded;
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
            if (mainPanel != null) mainPanel.SetActive(true);

            if (spinButton != null) spinButton.SetInteractable(true);

            GameManager.Instance.RestartGame();
            UpdateCollectButtonVisibility();
        }

        private void HandleSpinStarted()
        {
            if (collectButton != null)
                collectButton.SetInteractable(false);
            if (spinButton != null)
                spinButton.SetInteractable(false);
        }

        private void HandleSpinEnded()
        {
            if (spinButton != null)
                spinButton.SetInteractable(true);
            UpdateCollectButtonVisibility();
        }

        private void UpdateZoneUI(int zone)
        {
            var config = GameManager.Instance.GetCurrentWheelConfig();

            if (GameManager.Instance.IsSuperZone())
                zoneTypeText.text = "GOLDEN SPIN";
            else if (GameManager.Instance.IsSafeZone())
                zoneTypeText.text = "SILVER SPIN";
            else
                zoneTypeText.text = "BRONZE SPIN";

            zoneTypeText.color = config.textColor;

            if (rewardInfoText != null)
            {
                rewardInfoText.text = config.rewardInfoText;
                rewardInfoText.color = config.textColor;
            }

            UpdateCollectButtonVisibility();
        }

        private void UpdateRewardsUI(int rewards)
        {
            if (rewardsText != null)
                rewardsText.text = rewards.ToString();
        }

        private void UpdateCollectButtonVisibility()
        {
            if (collectButton == null) return;

            bool canCollect = GameManager.Instance.IsSafeZone() || GameManager.Instance.IsSuperZone();
            collectButton.SetInteractable(canCollect);
        }

        private void ShowGameOver()
        {
            if (mainPanel != null) mainPanel.SetActive(false);
            if (gameOverPanel != null) gameOverPanel.SetActive(true);

            if (spinButton != null) spinButton.SetInteractable(false);
            if (collectButton != null) collectButton.SetInteractable(false);
        }

        private void HandleWin()
        {
            Debug.Log($"Player collected {GameManager.Instance.TotalRewards} rewards!");
            GameManager.Instance.RestartGame();
        }
    }
}