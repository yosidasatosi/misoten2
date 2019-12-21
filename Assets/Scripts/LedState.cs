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
        TITLE = 1,
        SEA,
        DEEP,
        EVENT,
        FLASH,
        DANGER,
        RESULT
    };

    // Start is called before the first frame update
    void Start()
    {
        m_arduSerialPort.PortName = "COM4";
        m_arduSerialPort.BaudRate = 115200;
        try
        {
            m_arduSerialPort.Open();
        }
        catch(System.Exception e)
        {
            Debug.LogWarning(e.Message);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Set(Situation no)
    {
        if(m_arduSerialPort.IsOpen)
        {
            int scene = (int)no;
            m_arduSerialPort.Write(scene.ToString());
        }        
    }
}
