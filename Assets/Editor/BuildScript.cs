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
        string keystoreBase64 = "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";
        string keystorePass = "runner";
        string keyAlias = "icegamer";
        string keyPass = "runner";

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
