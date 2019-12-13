using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SoundManager : SingletonMonoBehaviour<SoundManager> {

    private bool isFadeOut = false;
    private float fadeDeltaTime = 0f;
    private int nextSESourceNum = 0;
    private DEFINE_SOUND.BGMLabel currentBGM = DEFINE_SOUND.BGMLabel.None;
    private DEFINE_SOUND.BGMLabel nextBGM = DEFINE_SOUND.BGMLabel.None;

    // BGMは一つづつ鳴るが、SEは複数同時に鳴ることがある
    private AudioSource bgmSource;
    private List<AudioSource> seSourceList;
    private Dictionary<string, AudioClip> seClipDic;
    private Dictionary<string, AudioClip> bgmClipDic;

    protected override void Awake() {

        for (int i = 0; i < DEFINE_SOUND.SE_SOURCE_NUM + DEFINE_SOUND.BGM_SOURCE_NUM; i++) {
            gameObject.AddComponent<AudioSource>();
        }

        // AudioSourceのデータを登録し曲も追加
        IEnumerable<AudioSource> audioSources = GetComponents<AudioSource>().Select(audio => { audio.playOnAwake = false; audio.volume = DEFINE_SOUND.BGM_VOLUME; audio.loop = true; return audio; });
        bgmSource = audioSources.First();
        seSourceList = audioSources.Skip(DEFINE_SOUND.BGM_SOURCE_NUM).ToList();
        seSourceList.ForEach(audio => { audio.volume = DEFINE_SOUND.SE_VOLUME; audio.loop = false; });

        bgmClipDic = (Resources.LoadAll(DEFINE_SOUND.BGM_PATH) as Object[]).ToDictionary(bgm => bgm.name, bgm => (AudioClip)bgm);
        seClipDic = (Resources.LoadAll(DEFINE_SOUND.SE_PATH) as Object[]).ToDictionary(se => se.name, se => (AudioClip)se);
    }


    /// <summary>
    /// 指定したファイル名のSEを流す。第二引数のdelayに指定した時間だけ再生までの間隔を空ける
    /// </summary>
    /// /// <param name="seLabel"></param>
    /// /// <param name="delay"></param>
    public void PlaySE(DEFINE_SOUND.SELabel seLabel, float delay = 0.0f) => StartCoroutine(DelayPlaySE(seLabel, delay));

    private IEnumerator DelayPlaySE(DEFINE_SOUND.SELabel seLabel, float delay) {
        yield return new WaitForSeconds(delay);
        AudioSource se = seSourceList[nextSESourceNum];
        se.PlayOneShot(seClipDic[seLabel.ToString()]);
        nextSESourceNum = (++nextSESourceNum < DEFINE_SOUND.SE_SOURCE_NUM) ? nextSESourceNum : 0;
    }


    /// <summary>
    /// 指定したBGMを流す。すでに流れている場合はNextに予約し、流れているBGMをフェードアウトさせる
    /// 第二引数でループするかどうかを設定 基本ループ再生
    /// </summary>
    /// <param name="bgmLabel"></param>
    /// <param name="isLoop"></param>
    public void PlayBGM(DEFINE_SOUND.BGMLabel bgmLabel,bool isLoop = true) {

        bgmSource.loop = isLoop;

        if (!bgmSource.isPlaying) {
            currentBGM = bgmLabel;
            nextBGM = DEFINE_SOUND.BGMLabel.None;
            if (bgmClipDic.ContainsKey(bgmLabel.ToString())) {
                bgmSource.clip = bgmClipDic[bgmLabel.ToString()];
            }
            else {
                Debug.LogError($"bgmClipDicに{bgmLabel.ToString()}というKeyはありません");
            }
            bgmSource.Play();
        }
        else if (currentBGM != bgmLabel) {
            isFadeOut = true;
            nextBGM = bgmLabel;
            fadeDeltaTime = 0f;
        }
    }


    /// <summary>
    /// BGMを止める
    /// </summary>
    public void StopSound() {
        bgmSource.Stop();
        seSourceList.ForEach(a => { a.Stop(); });
    }


    private void Update() {
        if (isFadeOut) {
            fadeDeltaTime += Time.deltaTime;
            bgmSource.volume = (1.0f - fadeDeltaTime / DEFINE_SOUND.FADE_OUT_SECONDO) * DEFINE_SOUND.BGM_VOLUME;

            if (fadeDeltaTime >= DEFINE_SOUND.FADE_OUT_SECONDO) {
                isFadeOut = false;
                bgmSource.Stop();
            }
        }
        else if (nextBGM != DEFINE_SOUND.BGMLabel.None) {
            bgmSource.volume = DEFINE_SOUND.BGM_VOLUME;
            PlayBGM(nextBGM);
        }
    }
}