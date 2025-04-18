using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;
using Unity.VisualScripting;
using UnityEditor;

public class IsoAndroidMenu : MonoBehaviour
{
    public static IsoAndroidMenu instance;
    public Transform povit;
    public TMP_Text title;
    public RawImage icon;

    public bool open;
    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "icon.png");
        StartCoroutine(LoadIcon(path));

#if UNITY_EDITOR
        if (icon != null)
        {
            var icons = UnityEditor.PlayerSettings.GetIconsForTargetGroup(BuildTargetGroup.Android, IconKind.Application);
            if (icons != null && icons.Length > 0)
                icon.texture = icons[0];
        }
#endif


        title.text = Application.productName;
        Close();
    }

    public void Update()
    {
        povit.gameObject.SetActive(open);
    }

    IEnumerator LoadIcon(string path)
    {
        var www = new WWW("file://" + path);
        yield return www;
        icon.texture = www.texture;
    }


    public void resume() {
        Close();
    }

    public void exit()
    {
        Application.Quit();
    }

    public static void Open()
    {
        instance.open=true;
        instance.transform.position = IsoXRRig.device.cameraViewer.transform.position;
        instance.povit.localEulerAngles = new Vector3(0, IsoXRRig.device.cameraViewer.transform.localEulerAngles.y, 0);
        IsoXRRig.device.onPause.Invoke();
    }

    public static void Close()
    {
        instance.open = false;
        IsoXRRig.device.onResume.Invoke();
    }

    public static void Toggle()
    {
        if (instance.open) Close();
        else Open();
    }
}
