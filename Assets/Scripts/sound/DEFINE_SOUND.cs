using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// サウンド関連の定数群
/// </summary>
public class DEFINE_SOUND {


    // BGMの素材パス
    public const string BGM_PATH = "Sound/BGM";
    // SEの素材パス
    public const string SE_PATH = "Sound/SE";
    // 生成するオブジェクトの名前
    public const string SOUND_OBJECT_NAME = "SoundManager";
    // 同時に再生できる総数(BGM)
    public const int BGM_SOURCE_NUM = 1;
    // 同時に再生できる総数(SE)
    public const int SE_SOURCE_NUM = 5;
    // 再生終了時のフェードアウトの時間
    public const float FADE_OUT_SECONDO = 0.5f;
    // BGMの大きさ
    public const float BGM_VOLUME = 0.5f;
    // SEの大きさ
    public const float SE_VOLUME = 0.3f;

    // BGMのラベル
    // 素材と同じ名前にするように気をつけてください
    public enum BGMLabel {
        None,
        Title_BGM,
        Result_BGM,
       
    }

    // SEのラベル
    // 素材と同じ名前にするように気をつけてください
    public enum SELabel {
        Title_SE,
        Coin_SE,
        Jump_SE,
        Serect_SE
    }
}