using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.Video;
using DG.Tweening;

using AREnums;

public class ARVideoPlayer : MonoBehaviour
{
    public static ARVideoPlayer Instance;

    [SerializeField] GameObject videoPanel;
    [SerializeField] VideoPlayer videoPlayer;
    //[SerializeField] VideoClip vidClip_unscrew_capacitor;

    private void Awake()
    {
        if(!Instance)
        {
            Instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PlayTutorialVideo(VideoCode code)
    {
        //        videoPanel.SetActive(true);

        //videoPlayer = Camera.main.gameObject.AddComponent<VideoPlayer>();
        //videoPlayer.renderMode = VideoRenderMode.RenderTexture;
        //videoPlayer.targetTexture = Resources.Load("VideoRenderTexture") as UnityEngine.RenderTexture;


        videoPanel.SetActive(true);

        switch (code)
        {
            case VideoCode.UnscrewCapacitorBoard:
                videoPlayer.clip = Resources.Load("Videos/board_unscrew_clip") as VideoClip;
                videoPlayer.isLooping = true;
                StartCoroutine(PlayVideoOnDelay());
                break;
        }
    }

    public void StopTutorialVideo()
    {
        videoPlayer.Stop();
        videoPanel.SetActive(false);
    }

    IEnumerator PlayVideoOnDelay()
    {
        //videoPanel.GetComponent<RawImage>().DOColor(new Color(1, 1, 1, 1), 1.5f);
        yield return new WaitForSeconds(1f);
        videoPlayer.Play();
    }

}
