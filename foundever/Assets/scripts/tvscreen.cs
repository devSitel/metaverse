using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Video;

public class tvscreen : MonoBehaviour
{
    public string[] url;
    public Texture[] tvSlides;
    public VideoClip[] tvVideos;
    public VideoPlayer _videoPlayer;
    public RawImage _image;
    public Texture _texture;
    private float lastFrame;
    void Start()
    {
        
        int random_img = Random.Range(0, url.Length);
        StartCoroutine(RandomImg());
       // StartCoroutine(DownloadImage(url[random_img]));
    }

    IEnumerator DownloadImage(string MediaUrl)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if (request.isNetworkError || request.isHttpError)
            Debug.Log(request.error);
        else
            //Debug.Log("aa");
            //_image.texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            
        StartCoroutine(RandomImg());
    }
    IEnumerator RandomImg()
    {
        
        int random_img = Random.Range(10, tvSlides.Length);
        _image.texture = tvSlides[random_img];
        
        if (random_img == 17)
        {
            StartCoroutine(RandomVid());
            yield break;
        }
        else
        {
            yield return new WaitForSeconds(2f);
            StartCoroutine(RandomImg());
        }
        
        // StartCoroutine(DownloadImage(url[random_img]));
    }

    IEnumerator RandomVid()
    {
        int random_vid = Random.Range(0, tvVideos.Length);

        //_image.texture = tvSlides[random_vid];
        _videoPlayer.clip = tvVideos[random_vid];
        _videoPlayer.Play();
        _videoPlayer.SetDirectAudioVolume(_videoPlayer.audioTrackCount, 0.01f);
        lastFrame = _videoPlayer.frameCount;
        yield return new WaitForSeconds(2f);
    }
    private void Update()
    {
        if(_videoPlayer.clip != null)
        {
            if (_videoPlayer.frame + 1 == lastFrame)
            {
                _videoPlayer.clip = null;
                StartCoroutine(RandomImg());
            }
        }
        
    }
}
