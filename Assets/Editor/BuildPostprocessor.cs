using System;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityPlayerSettings = UnityEditor.PlayerSettings;

public class BuildPostprocessor
{
    [PostProcessBuild]
    public static void OnPostprocessBuild(BuildTarget buildTarget, string path)
    {
        UpdateBundleVersion(buildTarget);
    }    

    private static void UpdateBundleVersion(BuildTarget buildTarget)
    {
        string currentVersion = UnityPlayerSettings.bundleVersion;
        string[] currentVersionSplit = currentVersion.Split('.');
        if (currentVersionSplit.Length < 3)
            currentVersionSplit = new string[] { currentVersionSplit[0], currentVersionSplit[1], "0" };

        try
        {
            int major = Convert.ToInt32(currentVersionSplit[0]);
            int minor = Convert.ToInt32(currentVersionSplit[1]);
            int build = Convert.ToInt32(currentVersionSplit[2]) + 1;

            if (build >= 100)
            {
                minor = minor + 1;
                build = 0;
            }

            UnityPlayerSettings.bundleVersion = major + "." + minor + "." + build;

            var buildNumber = major * 10000 + minor * 100 + build;
            if (buildTarget == BuildTarget.Android)
            {
                UnityPlayerSettings.Android.bundleVersionCode = buildNumber;
                Debug.Log("Finished with bundleversioncode: " + UnityPlayerSettings.Android.bundleVersionCode + " and version: " + UnityPlayerSettings.bundleVersion);
            }

        }
        catch (Exception e)
        {
            Debug.LogError(e.Message + "\n" + e.ToString());
            Debug.LogError("AutoIncrementBuildVersion script failed.");
        }
    }
}