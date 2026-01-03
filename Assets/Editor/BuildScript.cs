using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;
using System;
using System.IO;

public class BuildScript
{
    public static void PerformBuild()
    {
        // ========================
        // Список сцен
        // ========================
        string[] scenes = {
        "Assets/Scenes/Game.unity",
        };

        // ========================
        // Пути к файлам сборки
        // ========================
        string aabPath = "Ice-Run.aab";
        string apkPath = "Ice-Run.apk";

        // ========================
        // Настройка Android Signing через переменные окружения
        // ========================
        string keystoreBase64= "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";
        string keystorePass = "runner3";
        string keyAlias = "icegamer";
        string keyPass = "runner3";

        string tempKeystorePath = null;

        if (!string.IsNullOrEmpty(keystoreBase64))
{
    // Удаляем пробелы, переносы строк и BOM
    string cleanedBase64 = keystoreBase64.Trim()
                                         .Replace("\r", "")
                                         .Replace("\n", "")
                                         .Trim('\uFEFF');

    // Создаем временный файл keystore
    tempKeystorePath = Path.Combine(Path.GetTempPath(), "TempKeystore.jks");
    File.WriteAllBytes(tempKeystorePath, Convert.FromBase64String(cleanedBase64));

    PlayerSettings.Android.useCustomKeystore = true;
    PlayerSettings.Android.keystoreName = tempKeystorePath;
    PlayerSettings.Android.keystorePass = keystorePass;
    PlayerSettings.Android.keyaliasName = keyAlias;
    PlayerSettings.Android.keyaliasPass = keyPass;

    Debug.Log("Android signing configured from Base64 keystore.");
}
        else
        {
            Debug.LogWarning("Keystore Base64 not set. APK/AAB will be unsigned.");
        }

        // ========================
        // Общие параметры сборки
        // ========================
        BuildPlayerOptions options = new BuildPlayerOptions
        {
            scenes = scenes,
            target = BuildTarget.Android,
            options = BuildOptions.None
        };

        // ========================
        // 1. Сборка AAB
        // ========================
        EditorUserBuildSettings.buildAppBundle = true;
        options.locationPathName = aabPath;

        Debug.Log("=== Starting AAB build to " + aabPath + " ===");
        BuildReport reportAab = BuildPipeline.BuildPlayer(options);
        if (reportAab.summary.result == BuildResult.Succeeded)
            Debug.Log("AAB build succeeded! File: " + aabPath);
        else
            Debug.LogError("AAB build failed!");

        // ========================
        // 2. Сборка APK
        // ========================
        EditorUserBuildSettings.buildAppBundle = false;
        options.locationPathName = apkPath;

        Debug.Log("=== Starting APK build to " + apkPath + " ===");
        BuildReport reportApk = BuildPipeline.BuildPlayer(options);
        if (reportApk.summary.result == BuildResult.Succeeded)
            Debug.Log("APK build succeeded! File: " + apkPath);
        else
            Debug.LogError("APK build failed!");

        Debug.Log("=== Build script finished ===");

        // ========================
        // Удаление временного keystore
        // ========================
        if (!string.IsNullOrEmpty(tempKeystorePath) && File.Exists(tempKeystorePath))
        {
            File.Delete(tempKeystorePath);
            Debug.Log("Temporary keystore deleted.");
        }
    }
}
