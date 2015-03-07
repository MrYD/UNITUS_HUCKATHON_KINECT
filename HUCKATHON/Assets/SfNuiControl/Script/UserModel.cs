using UnityEngine;
using System.Collections;

/// <summary>
/// ユーザーモデル
/// </summary>
public class UserModel : MonoBehaviour,IUserBody {

    /// <summary>
    /// トラッキングID
    /// </summary>
    public uint TrackingId { get; set; }

    /// <summary>
    /// 関節マッピングポイント
    /// </summary>
    public GameObject SpineBase;
    public GameObject SpineMid;
    public GameObject Neck;
    public GameObject Head;
    public GameObject ShoulderLeft;
    public GameObject ElbowLeft;
    public GameObject WristLeft;
    public GameObject HandLeft;
    public GameObject ShoulderRight;
    public GameObject ElbowRight;
    public GameObject WristRight;
    public GameObject HandRight;
    public GameObject HipLeft;
    public GameObject KneeLeft;
    public GameObject AnkleLeft;
    public GameObject FootLeft;
    public GameObject HipRight;
    public GameObject KneeRight;
    public GameObject AnkleRight;
    public GameObject FootRight;
    public GameObject SpineShoulder;
    public GameObject HandTipLeft;
    public GameObject ThumbLeft;
    public GameObject HandTipRight;
    public GameObject ThumbRight;

    public bool IsHandLeftGripping;
    public bool IsHandRightGripping;

    public Vector3 scale;


    /// <summary>
    /// コンストラクタ
    /// </summary>
    public UserModel()
    {
        this.IsHandLeftGripping = false;
        this.IsHandRightGripping = false;
        this.scale = Vector3.one;
    }

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    #region IUserBody メンバー

    public int BodyIndex { get; set; }

    #endregion
}
