using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using UnityEngine.Networking;

public class VideoLoader : MonoBehaviour
{

    //https://drive.google.com/file/d//view?usp=sharing

    //1LKfWcpm1jP6XzcpA8LsxqWX7qMYrThXW
    //https://drive.google.com/uc?export=download&id=
    //https://drive.google.com/uc?export=download&id=1LKfWcpm1jP6XzcpA8LsxqWX7qMYrThXW

    public string mUrl = "";
    public string fileName = "";
    public bool mClearChache = false;
    private float mLoadFill = 0f;

    [SerializeField]
    private Image mDisk = null;
    private VideoPlayer mVideoPlayer = null;
    private AssetBundle mBundle = null;

    void Awake()
    {
        mVideoPlayer = GetComponent<VideoPlayer>();
        Caching.compressionEnabled = false;
        if (mClearChache) Caching.ClearCache();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(DownloadAndPlay());
        }
        mDisk.fillAmount = mLoadFill;
    }

    private IEnumerator DownloadAndPlay()
    {
        yield return GetBundle();

        if (!mBundle)
        {
            Debug.Log("Falha no Download do Bundle");
            yield break;
        }

        VideoClip newVideoClip = mBundle.LoadAsset<VideoClip>(fileName);
        mVideoPlayer.clip = newVideoClip;
        mVideoPlayer.Play();
    }

    private IEnumerator GetBundle()
    {
        //WWW request = WWW.LoadFromCacheOrDownload(mUrl, 0);
        UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(mUrl);
        request.SendWebRequest();
        
        while (!request.isDone)
        {
            Debug.Log(request.downloadProgress);
            mLoadFill = request.downloadProgress;
            yield return null;
        }
        mLoadFill = 0f;

        if (!request.isNetworkError)
        {
            mBundle = ((DownloadHandlerAssetBundle)request.downloadHandler).assetBundle;
            Debug.Log("Download sucedido");
        }
        else Debug.Log(request.error);

        yield return null;
    }
}
