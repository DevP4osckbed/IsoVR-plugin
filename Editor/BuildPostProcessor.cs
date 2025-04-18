#if UNITY_EDITOR

using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Isorld.XR.IsoVR.Editor
{
    public class BuildPostProcessor : IPostprocessBuildWithReport
    {
        public int callbackOrder => 0;

        public void OnPostprocessBuild(BuildReport report)
        {
            ExportAppIconToStreamingAssets();

            if (report.summary.platform == BuildTarget.iOS)
            {
                string path = report.summary.outputPath;

                // Optional: Handle your iOS-specific build steps here
                // Like in your original snippet for deleting meta files, etc.
            }
        }

        private void ExportAppIconToStreamingAssets()
        {
            var icons = PlayerSettings.GetIcons(NamedBuildTarget.Android, IconKind.Application);
            if (icons.Length == 0 || icons[0] == null)
            {
                Debug.LogWarning("No app icon found in PlayerSettings.");
                return;
            }

            Texture2D icon = icons[0];
            byte[] pngData = icon.EncodeToPNG();

            if (pngData != null)
            {
                string folder = Application.dataPath + "/StreamingAssets";
                if (!Directory.Exists(folder))
                    Directory.CreateDirectory(folder);

                string filePath = Path.Combine(folder, "icon.png");
                File.WriteAllBytes(filePath, pngData);

                Debug.Log("Exported app icon to StreamingAssets: " + filePath);
            }
            else
            {
                Debug.LogError("Failed to encode icon texture.");
            }
        }
    }
}
#endif
