using System.Collections;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    [Header("====== Audio Source")]
    public AudioSource bgSound;
    public AudioSource btSound;
    public AudioSource dashSoundNotice;
    public AudioSource collectRing;
    public AudioSource collectOrb;
    public AudioSource pickUpSound;
    public AudioSource hitEmenySource;

    [Header("====== UI Audio Clip")]
    public AudioClip menuSound;
    public AudioClip[] ingameSound;
    public AudioClip backSound;
    public AudioClip forwardSound;
    public AudioClip playBtSound;
    public AudioClip popupSound;
    public AudioClip changeTabSound;

    [Header("===== Player Audio Clip")]
    public AudioClip jumpSound;
    public AudioClip landSound;
    public AudioClip rollSound;
    public AudioClip grindSound;
    public AudioClip dashReadySound;
    public AudioClip startDashSound;
    public AudioClip onDashSound;
    public AudioClip deathSound;
    public AudioClip hitBeeSound;
    public AudioClip hitCrabSound;
    public AudioClip hitMotoBugSound;

    [Header("===== Collcet Audio Clip")]
    public AudioClip collcetRingSound;
    public AudioClip collcetOrbSound;
    public AudioClip pickUpMagetSound;
    public AudioClip pickUpPowerSound;
    public AudioClip hoopSound;
    public AudioClip ringBankSound;
    public AudioClip ringdropSound;

    [Header("===== Enemy Audio Clip")]
    public AudioClip motorBugPass;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PlaySound(bgSound,menuSound,true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound(AudioSource source,AudioClip audio,bool isloop = false)
    {
        source.loop = isloop;
        source.clip = audio;
        source.Play();
    }

    public void PlayCurrentSound(AudioSource source)
    {
        if(source.clip != null)
        {
            source.Play();
        }
    }

    public void StopSound(AudioSource source)
    {
        source.Stop();
    }

    IEnumerator PlaySchedulesSound(AudioSource source,AudioClip sound1, AudioClip sound2, bool isLoop = false)
    {
        PlaySound(source, sound1);
        yield return new WaitForSeconds(sound1.length);
        PlaySound(source,sound2,isLoop);
    }

    public void PlayTwoSound(AudioSource source, AudioClip sound1, AudioClip sound2, bool isLoop = false)
    {
        StartCoroutine(PlaySchedulesSound(source,sound1,sound2,isLoop));
    }
}
