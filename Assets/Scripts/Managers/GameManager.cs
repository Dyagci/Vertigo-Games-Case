using UnityEngine;
using WheelOfFortune.Core;
using WheelOfFortune.Data;

namespace WheelOfFortune.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        private GameState currentState = GameState.Idle;

        public GameState CurrentState => currentState;

        protected override void Awake()
        {
            base.Awake();
            SubscribeToEvents();
        }

        private void Start()
        {
            SetState(GameState.Idle);
        }

        private void OnDestroy()
        {
            UnsubscribeFromEvents();
        }

        private void SubscribeToEvents()
        {
            GameEvents.OnSpinStarted += OnSpinStarted;
            GameEvents.OnSpinEnded += OnSpinEnded;
            GameEvents.OnBombHit += OnBombHit;
        }

        private void UnsubscribeFromEvents()
        {
            GameEvents.OnSpinStarted -= OnSpinStarted;
            GameEvents.OnSpinEnded -= OnSpinEnded;
            GameEvents.OnBombHit -= OnBombHit;
        }

        public void SetState(GameState newState)
        {
            if (currentState == newState) return;

            currentState = newState;
            GameEvents.RaiseGameStateChanged(newState);

            OnStateEnter(newState);
        }

        private void OnStateEnter(GameState state)
        {
            switch (state)
            {
                case GameState.Idle:
                    HandleIdleState();
                    break;
                case GameState.Spinning:
                    HandleSpinningState();
                    break;
                case GameState.ShowingResult:
                    HandleShowingResultState();
                    break;
                case GameState.GameOver:
                    HandleGameOverState();
                    break;
                case GameState.Collecting:
                    HandleCollectingState();
                    break;
            }
        }

        private void HandleIdleState()
        {
            // Player can spin or leave (if in safe/super zone)
        }

        private void HandleSpinningState()
        {
            // Wheel is spinning, player cannot interact
        }

        private void HandleShowingResultState()
        {
            // Showing what player won
        }

        private void HandleGameOverState()
        {
            // Player hit bomb, show death panel
        }

        private void HandleCollectingState()
        {
            // Player chose to leave, show rewards summary
        }

        private void OnSpinStarted()
        {
            SetState(GameState.Spinning);
        }

        private void OnSpinEnded(IWheelSlice result)
        {
            if (result != null && result.IsBomb)
            {
                GameEvents.RaiseBombHit();
            }
            else
            {
                SetState(GameState.ShowingResult);
            }
        }

        private void OnBombHit()
        {
            SetState(GameState.GameOver);
        }

        public void RequestSpin()
        {
            if (currentState != GameState.Idle)
            {
                Debug.LogWarning("Cannot spin: Game is not in Idle state");
                return;
            }

            GameEvents.RaiseSpinStarted();
        }

        public void RequestCollectAndLeave()
        {
            if (currentState != GameState.Idle)
            {
                Debug.LogWarning("Cannot collect: Game is not in Idle state");
                return;
            }

            if (!ZoneManager.Instance.CanPlayerLeave())
            {
                Debug.LogWarning("Cannot leave: Not in a safe or super zone");
                return;
            }

            SetState(GameState.Collecting);
        }

        public void ProcessRewardAndContinue(WheelSliceData sliceData)
        {
            if (sliceData == null || sliceData.IsBomb) return;

            RewardManager.Instance.AddReward(sliceData.Reward);
            ZoneManager.Instance.AdvanceToNextZone();
            SetState(GameState.Idle);
        }

        public void RestartGame()
        {
            GameEvents.RaiseGameReset();
            SetState(GameState.Idle);
        }

        public bool CanSpin()
        {
            return currentState == GameState.Idle;
        }

        public bool CanLeave()
        {
            return currentState == GameState.Idle && ZoneManager.Instance.CanPlayerLeave();
        }
    }
}
