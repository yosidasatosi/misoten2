using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTako : MonoBehaviour
{
    private TakoManager takomanager;
    private GameObject gameobject;

    //private GameObject MainSumiObj;
    //private GameObject SubSumiObj;

    //private ParticleSystem MainSumiPar;
    //private ParticleSystem SubSumiPar;

    private ParticleSystem SumiParticle;

    private float Speed;
    private int MoveMode;
    private float Limit;

    private bool Fire;

    // Start is called before the first frame update
    void Start()
    {
        // ゲームオブジェクトの探索
        gameobject = GameObject.Find("Tako");

        // コンポーネント取得
        takomanager = gameobject.GetComponent<TakoManager>();

        // 移動速度を保存
        Speed = takomanager.GetSpeed();

        // 動作方法の保存
        MoveMode = takomanager.GetMoveMode();

        Limit = takomanager.GetLimit();

        SumiParticle = this.GetComponent<ParticleSystem>();
        SumiParticle.Stop();

        Fire = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch(MoveMode)
        {
            case 0:
                if (transform.localPosition.y > Limit)
                {
                    // 下方向に移動
                    transform.Translate(0.0f, Speed * Time.deltaTime * -1, 0.0f, Space.Self);
                }
                else
                {
                    if (!Fire)
                    {
                        //墨を吐く処理
                        SumiParticle.Play();

                        Fire = true;
                    }
                }
                break;
            case 1:
                if (transform.localPosition.y < Limit)
                {
                    // 上方向に移動
                    transform.Translate(0.0f, Speed * Time.deltaTime, 0.0f, Space.Self);
                }
                else
                {
                    if (!Fire)
                    {
                        //墨を吐く処理
                        SumiParticle.Play();

                        Fire = true;
                    }
                }
                break;
        }
    }
}
