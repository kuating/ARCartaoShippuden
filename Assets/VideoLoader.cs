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
    private double progressCheck;
    private int frameCounter;
    private int timeoutFrameLimit = 500;

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
        
        for (int i = 0; i < mBundle.GetAllAssetNames().Length; i++)
        {
            Debug.Log((i+1) + " : " + mBundle.GetAllAssetNames()[i]);
        }

        VideoClip newVideoClip = mBundle.LoadAsset<VideoClip>(fileName);
        mVideoPlayer.clip = newVideoClip;
        mVideoPlayer.Play();
        Debug.Log("Saiu!");//
        //while (true) if(mVideoPlayer.isPaused) break;
        Debug.Log("Parou");//

        mVideoPlayer.targetTexture.Release();
    }

    private IEnumerator GetBundle()
    {
        //WWW request = WWW.LoadFromCacheOrDownload(mUrl, 0);
        UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(mUrl);
        Debug.Log("Download Started");
        /*yield return*/ request.SendWebRequest();
        Debug.Log("Download Ended");
        progressCheck = 0; frameCounter = 0;
        while (!request.isDone)
        {
            Debug.Log(request.downloadedBytes);
            mLoadFill = request.downloadProgress;
            if (request.downloadedBytes == progressCheck) frameCounter++;
            else frameCounter = 0;
            progressCheck = request.downloadedBytes;

            if(frameCounter > timeoutFrameLimit) { Debug.LogError("Timeout"); break; }

            yield return null;
        }

        mLoadFill = 0f;

        if (request.isNetworkError || request.isHttpError)
            Debug.Log(request.error);
         
        else
        {
            mBundle = ((DownloadHandlerAssetBundle)request.downloadHandler).assetBundle;
            //Debug.Log("Download sucedido");
        } 

        yield return null;
    }
}
