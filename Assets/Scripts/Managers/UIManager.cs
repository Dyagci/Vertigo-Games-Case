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
            GameManager.Instance.OnSpinStarted += HandleSpinStarted;
            GameManager.Instance.OnSpinEnded += HandleSpinEnded;

            // Initialize UI
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
                GameManager.Instance.OnGameWin -= ShowCollect;
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
            if (GameManager.Instance.IsSuperZone())
                zoneTypeText.text = "GOLDEN SPIN";
            else if (GameManager.Instance.IsSafeZone())
                zoneTypeText.text = "SILVER SPIN";
            else
                zoneTypeText.text = "BRONZE SPIN";

            UpdateCollectButtonVisibility();
        }

        private void UpdateRewardsUI(int rewards)
        {
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
            if (gameOverPanel != null) gameOverPanel.SetActive(true);
        }

        private void ShowCollect()
        {
            Debug.Log($"Player collected {GameManager.Instance.TotalRewards} rewards!");
            GameManager.Instance.RestartGame();
        }
    }
}