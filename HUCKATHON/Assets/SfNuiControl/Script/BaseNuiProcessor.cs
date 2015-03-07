using UnityEngine;
using System.Collections;
using System;
using NuiServiceInterface;

public abstract class BaseNuiProcessor<F> : INuiProcessor {

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
    /// カラーデータテクスチャ
    /// </summary>
    protected Texture2D colorTexture;

    /// <summary>
    /// カラーデータテクスチャ
    /// </summary>
    protected Texture2D depthTexture;

    /// <summary>
    /// 直近のフレームデータ
    /// </summary>
    protected F lastFrameData = default(F);

    /// <summary>
    /// 排他ロック用オブジェクト
    /// </summary>
    protected object lockObject;

    /// <summary>
    /// 親オブジェクト
    /// </summary>
    NuiInterface Parent { get; set; }

    /// <summary>
    /// NUIサービス実行ファイルパス
    /// </summary>
    public string ServicePath { get; protected set; }

    /// <summary>
    /// 初期化処理
    /// </summary>
    /// <param name="parent">親オブジェクト</param>
    public virtual void Initialize(NuiInterface parent)
    {
        this.lockObject = new object();
        this.Parent = parent;

        this.PopulatePropertyValues();
        this.lastFrameData = Activator.CreateInstance<F>();

        this.SetupColorTexture();
        this.SetupDepthTexture();
        //Debug.Log(this.colorTargets);
        foreach (GameObject target in this.colorTargets)
        {
            target.GetComponent<Renderer>().material.mainTexture = this.colorTexture;
        }
        foreach (GameObject target in this.depthTargets)
        {
            target.GetComponent<Renderer>().material.mainTexture = this.depthTexture;
        }
        NuiServiceProvider<F>.Instance.ProcessFrameDataEvent += this.DefaultProcessFrameDataHandler;
    }

    /// <summary>
    /// フレーム処理
    /// </summary>
    public virtual void FrameProcess()
    {
        if (this.lastFrameData == null)
        {
            return;
        }
        lock (this.lockObject)
        {
            this.ProcessColor();
            this.ProcessDepth();
            this.ProcessBody();
            try
            {
                this.Parent.SendMessage(NuiUtils.RaiseFrameDataUpdateEventMessage, this.lastFrameData);
            }
            catch
            {
                Debug.LogWarning("Has no receiver!");
            }
        }
    }

    /// <summary>
    /// 終了処理
    /// </summary>
    public virtual void Terminate()
    {
        NuiServiceProvider<F>.Instance.ProcessFrameDataEvent -= this.DefaultProcessFrameDataHandler;
    }

    /// <summary>
    /// 再接続処理
    /// </summary>
    public abstract void ReConnect();

    /// <summary>
    /// 呼び出し元オブジェクトのプロパティ値を自身にコピーする
    /// </summary>
    protected virtual void PopulatePropertyValues()
    {
        this.colorTargets = this.Parent.colorTargets;
        this.depthTargets = this.Parent.depthTargets;
        this.users = this.Parent.users;
    }

    /// <summary>
    /// フレームデータハンドラ
    /// </summary>
    /// <param name="frameData"></param>
    protected void DefaultProcessFrameDataHandler(F frameData)
    {
        lock (this.lockObject)
        {
            this.lastFrameData = frameData;
        }
    }

    /// <summary>
    /// カラーデータテクスチャのセットアップを行う
    /// </summary>
    protected abstract void SetupColorTexture();

    /// <summary>
    /// Depthデータテクスチャのセットアップを行う
    /// </summary>
    protected abstract void SetupDepthTexture();

    /// <summary>
    /// カラーデータ処理
    /// </summary>
    protected abstract void ProcessColor();

    /// <summary>
    /// Depthデータ処理
    /// </summary>
    protected abstract void ProcessDepth();

    /// <summary>
    /// ボディデータ処理
    /// </summary>
    protected abstract void ProcessBody();

    /// <summary>
    /// GUIイベント通知
    /// </summary>
    public void RaiseGUIUpdateEvent()
    {
        this.Parent.SendMessage(NuiUtils.RaiseGUIUpdateEventMessage, this.lastFrameData);
    }
}
