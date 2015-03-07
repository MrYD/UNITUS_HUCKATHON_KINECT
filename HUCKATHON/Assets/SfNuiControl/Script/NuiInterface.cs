using SfNuiControl;
using System;
using UnityEngine;


/// <summary>
/// NUI入力デバイスリスト
/// </summary>
public enum NuiInputDevice
{
    KinectV1,
    KinectV2
}

/// <summary>
/// NUI APIインターフェイス
/// </summary>
public class NuiInterface : MonoBehaviour
{
    /// <summary>
    /// NUI入力デバイス
    /// </summary>
    protected NuiInputDevice nuiInputDevice;

    /// <summary>
    /// カラーデータターゲット
    /// </summary>
    public GameObject[] colorTargets;

    /// <summary>
    /// depthデータターゲット
    /// </summary>
    public GameObject[] depthTargets;

    /// <summary>
    /// ユーザー
    /// </summary>
    public GameObject[] users;

    /// <summary>
    /// サービス再接続キー
    /// </summary>
    public KeyCode serviceReconnectKey = KeyCode.R;

    /// <summary>
    /// NUIサービス V1 実行ファイルパス
    /// </summary>
    protected string nuiServiceV1Path = System.Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\SystemFriend\\NuiService\\NuiService.exe";

    /// <summary>
    /// NUIサービス V2 実行ファイルパス
    /// </summary>
    public string nuiServiceV2Path = System.Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + "\\SystemFriend\\NuiService\\NuiServiceV2.exe";

    /// <summary>
    /// ターゲットNUIプロセッサ
    /// </summary>
    private INuiProcessor TargetNuiProcessor { get; set; }

    public static KinectV2FrameData LastFrameData { get; set; }

    private DateTime startTime;
    private bool startRefreshed = false;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public NuiInterface()
    {
        this.nuiInputDevice = NuiInputDevice.KinectV2;
    }

	// Use this for initialization
	void Start () {
        //Debug.Log(Application.streamingAssetsPath);
        switch (this.nuiInputDevice)
        {
            case NuiInputDevice.KinectV1:
                this.TargetNuiProcessor = new KinectV1Processor(this.nuiServiceV1Path);
                break;
            case NuiInputDevice.KinectV2:
                this.TargetNuiProcessor = new KinectV2Processor(this.nuiServiceV2Path);
                break;
        }
        this.TargetNuiProcessor.Initialize(this);

        this.startTime = DateTime.Now;
    }
	
	// Update is called once per frame
	void Update () {
        if (!this.startRefreshed && (DateTime.Now - this.startTime) >= new TimeSpan(0, 0, 5))
        {
            this.TargetNuiProcessor.ReConnect();
            this.startRefreshed = true;
        }
        this.TargetNuiProcessor.FrameProcess();

        //rキーでNuiServiceへの再接続を行う
        if (Input.GetKeyDown(this.serviceReconnectKey))
        {
            this.TargetNuiProcessor.ReConnect();
        }
    }

    /// <summary>
    /// 終了処理
    /// </summary>
    void OnDestroy()
    {
        this.TargetNuiProcessor.Terminate();
    }

    /// <summary>
    /// GUI更新処理
    /// </summary>
    void OnGUI()
    {
        this.TargetNuiProcessor.RaiseGUIUpdateEvent();
    }

    /// <summary>
    /// フレームデータ更新イベントハンドラ
    /// </summary>
    /// <param name="param">パラメータ</param>
    public void ProcessFrameDataUpdateEvent(object param)
    {
		if (param is KinectV2FrameData)
		{
			KinectV2FrameData frameData = (param as KinectV2FrameData);
			NuiInterface.LastFrameData = frameData;
		}    
	}

    public void ProcessGUIUpdateEvent(object param)
    {
    }
}
