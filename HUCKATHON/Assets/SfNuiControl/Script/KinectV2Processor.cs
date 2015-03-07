using System.Collections.Generic;
using System.Linq;
using Assets;
using NuiServiceInterface;
using SfNuiControl;
using UnityEngine;

public class KinectV2Processor : BaseNuiProcessor<KinectV2FrameData>
{
     /// <summary>
    /// コンストラクタ
    /// </summary>
    /// <param name="servicePath">NUIサービス実行ファイルパス</param>
    public KinectV2Processor(string servicePath)
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
        NuiServiceProvider<KinectV2FrameData>.Instance.Open(this.ServicePath);
	}

    /// <summary>
    /// 終了処理
    /// </summary>
    public override void Terminate()
    {
        base.Terminate();

        // NUIサービス終了
        NuiServiceProvider<KinectV2FrameData>.Instance.Close(true);
    }

    /// <summary>
    /// 再接続処理
    /// </summary>
    public override void ReConnect()
    {
        NuiServiceProvider<KinectV2FrameData>.Instance.Close();
        NuiServiceProvider<KinectV2FrameData>.Instance.Open();
    }

    /// <summary>
    /// カラーデータテクスチャのセットアップを行う
    /// </summary>
    protected override void SetupColorTexture()
    {
        this.colorTexture = new Texture2D(KinectV2FrameData.DefaultColorWidth, KinectV2FrameData.DefaultColorHeight, TextureFormat.BGRA32, false);
    }

    /// <summary>
    /// Depthデータテクスチャのセットアップを行う
    /// </summary>
    protected override void SetupDepthTexture()
    {
        this.depthTexture = new Texture2D(512, 424, TextureFormat.RGB24, false);
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
        Dictionary<int,GameObject> interfaces=new Dictionary<int, GameObject>();
        users.Each((user) =>
        {
            var userInterface=user.GetComponent<IUserBody>();
            interfaces.Add(userInterface.BodyIndex,user);
        });
        
        for (var index = 0; index < this.lastFrameData.Bodies.Length; index++)
        {
            var body = this.lastFrameData.Bodies[index];
            if (!interfaces.ContainsKey(index)) return;
            var user = interfaces[index];
            if (!body.IsActive)
            {
                this.users[index].SetActive(false);
                continue;
            }
            user.SetActive(true);
            this.TransformJoint(body, KinectV2JointType.SpineBase, user, "SpineBase");
            this.TransformJoint(body, KinectV2JointType.SpineMid, user, "SpineMid");
            this.TransformJoint(body, KinectV2JointType.Neck, user, "Neck");
            this.TransformJoint(body, KinectV2JointType.Head, user, "Head");
            this.TransformJoint(body, KinectV2JointType.ShoulderLeft, user, "ShoulderLeft");
            this.TransformJoint(body, KinectV2JointType.ElbowLeft, user, "ElbowLeft");
            this.TransformJoint(body, KinectV2JointType.WristLeft, user, "WristLeft");
            this.TransformJoint(body, KinectV2JointType.HandLeft, user, "HandLeft");
            this.TransformJoint(body, KinectV2JointType.ShoulderRight, user, "ShoulderRight");
            this.TransformJoint(body, KinectV2JointType.ElbowRight, user, "ElbowRight");
            this.TransformJoint(body, KinectV2JointType.WristRight, user, "WristRight");
            this.TransformJoint(body, KinectV2JointType.HandRight, user, "HandRight");
            this.TransformJoint(body, KinectV2JointType.HipLeft, user, "HipLeft");
            this.TransformJoint(body, KinectV2JointType.KneeLeft, user, "KneeLeft");
            this.TransformJoint(body, KinectV2JointType.AnkleLeft, user, "AnkleLeft");
            this.TransformJoint(body, KinectV2JointType.FootLeft, user, "FootLeft");
            this.TransformJoint(body, KinectV2JointType.HipRight, user, "HipRight");
            this.TransformJoint(body, KinectV2JointType.KneeRight, user, "KneeRight");
            this.TransformJoint(body, KinectV2JointType.AnkleRight, user, "AnkleRight");
            this.TransformJoint(body, KinectV2JointType.FootRight, user, "FootRight");
            this.TransformJoint(body, KinectV2JointType.SpineShoulder, user, "SpineShoulder");
            this.TransformJoint(body, KinectV2JointType.HandTipLeft, user, "HandTipLeft");
            this.TransformJoint(body, KinectV2JointType.ThumbLeft, user, "ThumbLeft");
            this.TransformJoint(body, KinectV2JointType.HandTipRight, user, "HandTipRight");
            this.TransformJoint(body, KinectV2JointType.ThumbRight, user, "ThumbRight");

            //UserModel userModel = user.transform.GetComponent<UserModel>();
        }
    }

    /// <summary>
    /// 関節の位置設定
    /// </summary>
    /// <param name="body">ボディ情報</param>
    /// <param name="jointType">関節タイプ</param>
    /// <param name="user">ユーザーモデル</param>
    /// <param name="jointName">関節名</param>
    private void TransformJoint(KinectV2Body body, KinectV2JointType jointType, GameObject user, string jointName)
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
