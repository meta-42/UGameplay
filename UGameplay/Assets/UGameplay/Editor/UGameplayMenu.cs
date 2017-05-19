using UnityEngine;
using UnityEditor;
using System.Collections;
using System;

public class UGameplayMenu
{
    [MenuItem("GameObject/UGameplay/PlayerCamera/FixedLook")]
    static void AddPlayerCameraFixedLook()
    {
        CreateObject("Assets/UGameplay/Prefab/PlayerCameraFixedLook.prefab");
    }

    [MenuItem("GameObject/UGameplay/PlayerCamera/FreeLook")]
    static void AddPlayerCameraFreeLook()
    {
        CreateObject("Assets/UGameplay/Prefab/PlayerCameraFreeLook.prefab");
    }

    [MenuItem("GameObject/UGameplay/PlayerCharacter")]
    static void AddPlayerCharacter()
    {
        CreateObject("Assets/UGameplay/Prefab/PlayerCharacter.prefab");
    }

    [MenuItem("GameObject/UGameplay/PlayerController")]
    static void AddPlayerController()
    {
        CreateObject("Assets/UGameplay/Prefab/PlayerController.prefab");
    }

    [MenuItem("GameObject/UGameplay/PlayerHUD")]
    static void AddPlayerHUD()
    {
        CreateObject("Assets/UGameplay/Prefab/PlayerHUD.prefab");
    }

    [MenuItem("GameObject/UGameplay/PlayerStart")]
    static void AddPlayerStart()
    {
        CreateObject("Assets/UGameplay/Prefab/PlayerStart.prefab");
    }

    [MenuItem("GameObject/UGameplay/GameMode")]
    static void AddGameMode()
    {
        CreateObject("Assets/UGameplay/Prefab/GameMode.prefab");
    }

    [MenuItem("GameObject/UGameplay/GameInstance")]
    static void AddGameInstance()
    {
        CreateObject("Assets/UGameplay/Prefab/GameInstance.prefab");
    }

    static public GameObject CreateObject(string path, Transform parent = null)
    {
        var prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
        if (prefab == null)
        {
            throw new ArgumentException(string.Format("Prefab not found at path {0}.", path));
        }

        var go = UnityEngine.Object.Instantiate(prefab) as GameObject;

        var go_parent = parent ?? Selection.activeTransform;

        if (go_parent != null)
        {
            go.transform.SetParent(go_parent, false);
        }

        go.name = prefab.name;


        FixInstantiated(prefab, go);

        return go;
    }

    static public void FixInstantiated(Component source, Component instance)
    {
        FixInstantiated(source.gameObject, instance.gameObject);
    }

    static public void FixInstantiated(GameObject source, GameObject instance)
    {
        var defaultTrans = source.GetComponent<Transform>();

        var trans = instance.GetComponent<Transform>();

        trans.localPosition = defaultTrans.localPosition;
        trans.position = defaultTrans.position;
        trans.rotation = defaultTrans.rotation;
        trans.localScale = defaultTrans.localScale;
    }
}

