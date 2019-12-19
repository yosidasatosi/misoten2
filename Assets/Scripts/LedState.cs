using System.Collections;
using System.Collections.Generic;
using System.IO.Ports;
using UnityEngine;

public class LedState : SingletonMonoBehaviour<LedState>
{
    private SerialPort m_arduSerialPort = new SerialPort();

    // LEDエフェクトの種類
    public enum Situation
    {
        // タイトル画面
        TITLE = 1,
        // ゲーム画面
        SEA = 2,
        // 深海
        DEEP = 3,
        // 回避（ボタン一回）
        DODGE = 4,
        // ボタン連打
        DANGER = 5,
        // リザルト画面
        RESULT = 6
    };

    // Start is called before the first frame update
    void Start()
    {
        m_arduSerialPort.PortName = "COM3";
        m_arduSerialPort.BaudRate = 115200;
        m_arduSerialPort.Open();
    }

    // Update is called once per frame
    void Update()
    {
       //Set(Situation.DANGER);
    }

    public void Set(Situation no)
    {
        int situation = (int)no;

        m_arduSerialPort.Write(situation.ToString());
    }
}
