using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace UGameplay
{

    public class GameplayStatics
    {
        public static GameMode gameMode { set; get; }

        public static PlayerController playerController { set; get; }

        public static PlayerCamera playerCamera { set; get; }

        public static PlayerCharacter playerCharacter { set; get; }

        public static PlayerHUD playerHUD { set; get; }

        static PlayerStart _playerStart;
        public static PlayerStart playerStart
        {
            set
            {
                _playerStart = value;
            }

            get
            {
                _playerStart = GameObject.FindObjectOfType<PlayerStart>();

                if (!_playerStart)
                    _playerStart = PlayerStart.DefaultStart();

                return _playerStart;
            }
        }


        public static GameInstance GetGameInstance()
        {
            return GameInstance.Instance;
        }

        public static float GetEffectiveTimeScale()
        {
            return Time.timeScale;
        }

        public static T GetPlayerController<T>() where T : PlayerController
        {
            return playerController as T;
        }

        public static T GetPlayerCamera<T>() where T : PlayerCamera
        {
            return playerCamera as T;
        }

        public static T GetPlayerCharacter<T>() where T : PlayerCharacter
        {
            return playerCharacter as T;
        }

        public static T GetPlayerHUD<T>() where T : PlayerHUD
        {
            return playerHUD as T;
        }

        public static T GetGameMode<T>() where T : GameMode
        {
            return gameMode as T;
        }

        public static void OpenScene(string levelName)
        {
            GameInstance.Instance.isCurrentSceneInited = false;
            SceneManager.LoadSceneAsync(levelName, LoadSceneMode.Single);
        }

        public static void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            GameInstance.Instance.InitScene();
        }

        public static void PlaySound(AudioClip clip, Vector3 pos, float volume = 1.0f)
        {
            AudioSource.PlayClipAtPoint(clip, pos, volume);
        }

        public static void PlayBacksound(string soundName)
        {
            var clip = (AudioClip)Resources.Load("Sound/" + soundName, typeof(AudioClip));
            if (clip == null)
            {
                Debug.LogError(soundName + "not exist Resources/Sound/");
                return;
            }

            var backSource = gameMode.backsoundSource;
            backSource.loop = true;
            backSource.clip = clip;
            backSource.Play();
        }
    }

}
