using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace UGameplay
{

    struct GameState
    {
        public const string Entering = "Entering";
        public const string WaitingToStart = "WaitingToStart";
        public const string InProgress = "InProgress";
        public const string WaitingToEnd = "WaitingToEnd";
        public const string Leaving = "Leaving";
        public const string Aborted = "Aborted";
    }

    public class GameMode : MonoBehaviour
    {
        public AudioSource backsoundSource { set; get; }

        public bool manualGameStart = false;

        private string _gameState = GameState.Entering;
        public string gameState
        {
            set
            {
                if (_gameState == value)
                {
                    return;
                }
                _gameState = value;
                OnMatchStateChange();
            }
            get
            {
                return _gameState;
            }
        }

        int _elapsedTime = 0;
        public float elapsedTime { get { return _elapsedTime; } }

        public bool IsGameInProgress()
        {
            if (gameState == GameState.InProgress)
            {
                return true;
            }
            return false;
        }

        public bool IsGameEnd()
        {
            if (gameState == GameState.WaitingToEnd || gameState == GameState.Leaving)
            {
                return true;
            }
            return false;
        }

        protected virtual void Update()
        {
            if (gameState == GameState.WaitingToStart)
            {
                if (ReadyToStartMatch())
                {
                    gameState = GameState.InProgress;
                }
            }
            if (gameState == GameState.InProgress)
            {
                if (ReadyToEndMatch())
                {
                    EndGame();
                }
            }
        }

        public void InitGame()
        {
            backsoundSource = GetComponent<AudioSource>();
            if (backsoundSource == null)
            {
                backsoundSource = gameObject.AddComponent<AudioSource>();
            }
            gameState = GameState.Entering;
        }

        public void StartGame()
        {

            //等待游戏开始
            if (gameState == GameState.Entering)
            {
                gameState = GameState.WaitingToStart;
            }

            //已经在等待了，那么直接开始
            if (gameState == GameState.WaitingToStart && ReadyToStartMatch())
            {
                gameState = GameState.InProgress;
            }
        }

        public void EndGame()
        {
            if (!IsGameInProgress())
            {
                return;
            }
            gameState = GameState.WaitingToEnd;
        }

        bool ReadyToStartMatch()
        {
            if (manualGameStart)
            {
                return false;
            }
            return true;
        }

        bool ReadyToEndMatch()
        {
            return false;
        }

        void DefaultTimer()
        {
            if (IsGameInProgress())
            {
                ++_elapsedTime;
                HandleDefaultTimer();
            }
        }

        void OnMatchStateChange()
        {
            if (gameState == GameState.WaitingToStart)
            {
                HandleGameIsWaitingToStart();
            }
            else if (gameState == GameState.InProgress)
            {
                HandleGameStarted();
            }
            else if (gameState == GameState.WaitingToEnd)
            {
                HandleGameEnded();
            }
            else if (gameState == GameState.Leaving)
            {
                HandleGameLeaving();
            }
            else if (gameState == GameState.Aborted)
            {
                HandleGameAborted();
            }
        }

        protected virtual void HandleGameIsWaitingToStart()
        {
            _elapsedTime = 0;
        }

        protected virtual void HandleGameStarted()
        {
            InvokeRepeating("DefaultTimer", GameplayStatics.GetEffectiveTimeScale(), GameplayStatics.GetEffectiveTimeScale());
        }

        protected virtual void HandleGameEnded()
        {

        }

        protected virtual void HandleGameLeaving()
        {

        }

        protected virtual void HandleGameAborted()
        {

        }

        protected virtual void HandleDefaultTimer()
        {

        }
    }

}
