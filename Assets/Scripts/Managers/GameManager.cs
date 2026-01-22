using UnityEngine;
using System;
using System.Collections.Generic;
using WheelOfFortune.Core;

namespace WheelOfFortune.Managers
{
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        public event Action<int> OnZoneChanged;
        public event Action<int> OnRewardsUpdated;
        public event Action OnGameOver;
        public event Action OnGameWin;
        public event Action OnSpinStarted;
        public event Action OnSpinEnded;
        public event Action<WheelSliceData> OnRewardCollected;

        [Header("Wheel Configs")]
        [SerializeField] private WheelConfiguration bronzeConfig;
        [SerializeField] private WheelConfiguration silverConfig;
        [SerializeField] private WheelConfiguration goldenConfig;

        [Header("References")]
        [SerializeField] private WheelController wheelController;

        private int currentZone = 1;
        private int totalRewards = 0;
        private List<WheelSliceData> collectedRewards = new List<WheelSliceData>();

        public int CurrentZone => currentZone;
        public int TotalRewards => totalRewards;
        public List<WheelSliceData> CollectedRewards => collectedRewards;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);
        }

        private void Start()
        {
            wheelController.OnSpinEnded += HandleSpinResult;
            LoadCurrentZone();
        }

        private void OnDestroy()
        {
            if (wheelController != null)
                wheelController.OnSpinEnded -= HandleSpinResult;
        }

        public WheelConfiguration GetCurrentWheelConfig()
        {
            if (currentZone % 30 == 0) return goldenConfig;
            if (currentZone % 5 == 0) return silverConfig;
            return bronzeConfig;
        }

        public bool IsSafeZone()
        {
            return currentZone % 5 == 0;
        }

        public bool IsSuperZone()
        {
            return currentZone % 30 == 0;
        }

        private void LoadCurrentZone()
        {
            var config = GetCurrentWheelConfig();
            wheelController.SetupWheel(config, config.slices);
            OnZoneChanged?.Invoke(currentZone);
        }

        private void HandleSpinResult(WheelSliceData result)
        {
            OnSpinEnded?.Invoke();

            if (result.IsBomb)
            {
                HitBomb();
            }
            else
            {
                collectedRewards.Add(result);
                OnRewardCollected?.Invoke(result);
                AddReward(result.rewardAmount);
                NextZone();
                LoadCurrentZone();
            }
        }

        public void SpinWheel()
        {
            if (!wheelController.IsSpinning)
            {
                OnSpinStarted?.Invoke();
                wheelController.Spin();
            }
        }

        public void AddReward(int amount)
        {
            totalRewards += amount;
            OnRewardsUpdated?.Invoke(totalRewards);
        }

        public void HitBomb()
        {
            totalRewards = 0;
            collectedRewards.Clear();
            OnGameOver?.Invoke();
        }

        public void NextZone()
        {
            currentZone++;
            OnZoneChanged?.Invoke(currentZone);
        }

        public void CollectAndLeave()
        {
            // Transfer session rewards to permanent inventory
            if (InventoryManager.Instance != null)
            {
                InventoryManager.Instance.AddRewardsFromSession(collectedRewards);
            }

            OnGameWin?.Invoke();
        }

        public void RestartGame()
        {
            currentZone = 1;
            totalRewards = 0;
            collectedRewards.Clear();
            OnZoneChanged?.Invoke(currentZone);
            OnRewardsUpdated?.Invoke(totalRewards);
            LoadCurrentZone();
        }
    }
}