using UnityEngine;
using System.Collections;

public interface INuiProcessor 
{
    /// <summary>
    /// 初期化処理
    /// </summary>
    /// <param name="parent">親オブジェクト</param>
    void Initialize(NuiInterface parent);

    /// <summary>
    /// フレーム処理
    /// </summary>
    void FrameProcess();

    /// <summary>
    /// 終了処理
    /// </summary>
    void Terminate();

    /// <summary>
    /// 再接続処理
    /// </summary>
    void ReConnect();

    /// <summary>
    /// GUI更新イベント通知
    /// </summary>
    void RaiseGUIUpdateEvent();
}
