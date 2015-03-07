using UnityEngine;
using System.Collections;
using SfNuiControl;
using NuiServiceInterface;
using System;
using System.Threading;
using System.IO;

public class KinectV1Processor : BaseNuiProcessor<KinectFrameData>
{
    /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="servicePath">NUIサービス実行ファイルパス</param>
    public KinectV1Processor(string servicePath)
    {
        this.ServicePath = servicePath;
    }

    /// <summary>
    /// 初期化処理
    /// </summary>
    /// <param name="parent">親オブジェクト</param>
    public override void Initialize(NuiInterface parent)
    {
        base.Initialize(parent);

        // NUIサービス開始
        NuiServiceProvider<KinectFrameData>.Instance.Open(this.ServicePath);
    }

    /// <summary>
    /// 終了処理
    /// </summary>
    public override void Terminate()
    {
        base.Terminate();

        // NUIサービス終了
        NuiServiceProvider<KinectFrameData>.Instance.Close(true);
    }

    /// <summary>
    /// 再接続処理
    /// </summary>
    public override void ReConnect()
    {
        NuiServiceProvider<KinectFrameData>.Instance.Close();
        NuiServiceProvider<KinectFrameData>.Instance.Open();
    }

    /// <summary>
    /// カラーデータテクスチャのセットアップを行う
    /// </summary>
    protected override void SetupColorTexture()
    {
        this.colorTexture = new Texture2D(KinectFrameData.DefaultColorWidth, KinectFrameData.DefaultColorHeight, TextureFormat.BGRA32, false);
    }

    /// <summary>
    /// Depthデータテクスチャのセットアップを行う
    /// </summary>
    protected override void SetupDepthTexture()
    {
        this.depthTexture = new Texture2D(320, 240, /*512, 424,*/ TextureFormat.RGB24, false);
    }

    /// <summary>
    /// カラーデータ処理
    /// </summary>
    protected override void ProcessColor()
    {
        byte[] colorPixels = this.lastFrameData.ColorPixels;
        if (colorPixels == null)
        {
            return;
        }
        byte[] colorData = colorPixels;
        for (int i = 0; i + 3 < colorData.Length; i += 4)
        {
            colorData[i + 3] = 255;//A
        }
        this.colorTexture.LoadRawTextureData(colorData);
        this.colorTexture.Apply();
    }

    /// <summary>
    /// depthデータ処理
    /// </summary>
    protected override void ProcessDepth()
    {
        byte[] depthPixels = this.lastFrameData.DepthPixels;
        if (depthPixels == null)
        {
            return;
        }
        this.depthTexture.LoadRawTextureData(depthPixels);
        this.depthTexture.Apply();
    }

    /// <summary>
    /// ボディデータ処理
    /// </summary>
    protected override void ProcessBody()
    {
        if (this.lastFrameData.Bodies == null)
        {
            return;
        }
        foreach (KinectBody body in this.lastFrameData.Bodies)
        {
            if (!body.IsActive)
            {
                continue;
            }

            foreach (GameObject user in this.users)
            {
                this.TransformJoint(body, KinectJointType.Spine, user, "SpineBase");
                this.TransformJoint(body, KinectJointType.Head, user, "Head");
                this.TransformJoint(body, KinectJointType.ShoulderLeft, user, "ShoulderLeft");
                this.TransformJoint(body, KinectJointType.ElbowLeft, user, "ElbowLeft");
                this.TransformJoint(body, KinectJointType.WristLeft, user, "WristLeft");
                this.TransformJoint(body, KinectJointType.HandLeft, user, "HandLeft");
                this.TransformJoint(body, KinectJointType.ShoulderRight, user, "ShoulderRight");
                this.TransformJoint(body, KinectJointType.ElbowRight, user, "ElbowRight");
                this.TransformJoint(body, KinectJointType.WristRight, user, "WristRight");
                this.TransformJoint(body, KinectJointType.HandRight, user, "HandRight");
                this.TransformJoint(body, KinectJointType.HipLeft, user, "HipLeft");
                this.TransformJoint(body, KinectJointType.KneeLeft, user, "KneeLeft");
                this.TransformJoint(body, KinectJointType.AnkleLeft, user, "AnkleLeft");
                this.TransformJoint(body, KinectJointType.FootLeft, user, "FootLeft");
                this.TransformJoint(body, KinectJointType.HipRight, user, "HipRight");
                this.TransformJoint(body, KinectJointType.KneeRight, user, "KneeRight");
                this.TransformJoint(body, KinectJointType.AnkleRight, user, "AnkleRight");
                this.TransformJoint(body, KinectJointType.FootRight, user, "FootRight");
                this.TransformJoint(body, KinectJointType.ShoulderCenter, user, "SpineShoulder");

                user.transform.Find("SpineMid").gameObject.SetActive(false);
                user.transform.Find("Neck").gameObject.SetActive(false);
                user.transform.Find("HandTipLeft").gameObject.SetActive(false);
                user.transform.Find("ThumbLeft").gameObject.SetActive(false);
                user.transform.Find("HandTipRight").gameObject.SetActive(false);
                user.transform.Find("ThumbRight").gameObject.SetActive(false);

                //UserModel userModel = user.transform.GetComponent<UserModel>();

            }
        }
    }

    /// <summary>
    /// 関節の位置設定
    /// </summary>
    /// <param name="body">ボディ情報</param>
    /// <param name="jointType">関節タイプ</param>
    /// <param name="user">ユーザーモデル</param>
    /// <param name="jointName">関節名</param>
    private void TransformJoint(KinectBody body, KinectJointType jointType, GameObject user, string jointName)
    {
        UserModel userModel = user.transform.GetComponent<UserModel>();
        UnityEngine.Vector3 point = new UnityEngine.Vector3(
           body.Joints[(int)jointType].X * userModel.scale.x,
           body.Joints[(int)jointType].Y * userModel.scale.y,
           body.Joints[(int)jointType].Z * userModel.scale.z
           );
        GameObject joint = user.transform.Find(jointName).gameObject;
        joint.transform.localPosition = point;
    }
}
