using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]       //데이터 직렬화
public class Sound
{
    public string name;
    public AudioClip clip;
}


public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;        

    [SerializeField] List<Sound> sfx = null;
    [SerializeField] Sound[] bgm = null;

    [SerializeField] AudioSource bgmPlayer = null;  //브금은 한개씩만 플레이
    [SerializeField] AudioSource[] sfxPlayer = null;


    private void Start()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PlayBGM(string bgmName)
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            if (bgmName == bgm[i].name)     //파라매터로 넘어온 이름이 있는지 검사
            {
                bgmPlayer.clip = bgm[i].clip;
                bgmPlayer.Play();           //있으면 플레이
                return;
            }
        }
    }
    public void StopBGM()
    {
        bgmPlayer.Stop();
    }
    public void PlaySFX(string sfxName)
    {
        int idx = sfx.FindIndex(x => x.name == sfxName);
        if (idx < 0)
            return;
        for (int i = 0; i < sfxPlayer.Length; i++)
        {
            if (!sfxPlayer[i].isPlaying)     //check if available audiosrc left
            {
                sfxPlayer[i].clip = sfx[idx].clip;
                sfxPlayer[i].Play();
                return;
            }
        }
        Debug.Log("남아있는 오디오소스가 없습니다");
        return;
    }
}
