using System.Collections;
using UnityEngine;

public class LoadAssetBundle : MonoBehaviour
{
    public string bundleName = "player2";
    public string assetName = "player1";

    IEnumerator Start()
    {
        string path = Application.streamingAssetsPath + "/Bundles/" + bundleName;
        AssetBundle bundle = AssetBundle.LoadFromFile(path);

        if (bundle == null)
        {
            Debug.LogError("Load fail");
            yield break;

        }
        GameObject prefab = bundle.LoadAsset<GameObject>(assetName);
        Instantiate(prefab);
        bundle.Unload(false);
    }
}
