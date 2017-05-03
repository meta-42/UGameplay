using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace UGameplay
{
    public class GameInstance : SingletonBehaviour<GameInstance>
    {
        public PlayerController playerControllerPrefab;

        public PlayerCamera playerCameraPrefab;

        public PlayerCharacter playerCharacterPrefab;

        public GameMode gameModePrefab;

        public PlayerHUD playerHUDPrefab;

        public bool isCurrentSceneInited { set; get; }

        public override void Awake()
        {
            base.Awake();
            isCurrentSceneInited = false;
            UnityEngine.SceneManagement.SceneManager.sceneLoaded += GameplayStatics.OnSceneLoaded;
        }

        void OnDestroy()
        {
            UnityEngine.SceneManagement.SceneManager.sceneLoaded -= GameplayStatics.OnSceneLoaded;
        }

        public void InitScene()
        {
            if (isCurrentSceneInited) return;

            if (!gameModePrefab) { Debug.LogError("haven't GameMode"); }
            if (!playerControllerPrefab) { Debug.LogError("haven't PlayerController"); }
            if (!playerCameraPrefab) { Debug.LogError("haven't PlayerCamera"); }
            if (!playerCharacterPrefab) { Debug.LogError("haven't PlayerCharacter"); }
            if (!playerHUDPrefab) { Debug.LogWarning("haven't PlayerHUD"); }


            GameplayStatics.gameMode = GameMode.Instantiate(gameModePrefab);
            GameplayStatics.gameMode.InitGame();

            GameplayStatics.playerCharacter = GameMode.Instantiate(playerCharacterPrefab,
                GameplayStatics.playerStart.transform.position,
                GameplayStatics.playerStart.transform.rotation);

            GameplayStatics.playerCamera = GameMode.Instantiate(playerCameraPrefab,
                GameplayStatics.playerStart.transform.position,
                GameplayStatics.playerStart.transform.rotation);

            if (playerHUDPrefab)
                GameplayStatics.playerHUD = GameMode.Instantiate(playerHUDPrefab);

            GameplayStatics.playerController = GameMode.Instantiate(playerControllerPrefab);
            GameplayStatics.playerController.Possess(GameplayStatics.playerCharacter);

            GameplayStatics.playerStart.gameObject.SetActive(false);

            GameplayStatics.gameMode.StartGame();

            isCurrentSceneInited = true;
        }


    }
}
