using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

//View层
public class UserRecord : MonoBehaviour {

	//UI
	public UnityEngine.UI.Text title;
	public UnityEngine.UI.Button Btn_rec;
	public UnityEngine.UI.Button Btn_normal;
	public UnityEngine.UI.Button Btn_hight;

	// others
	public System.Action StartRecoder;
	public System.Action StopRecoder;
	public System.Action PlayRecoderNomal;
	public System.Action PlayRecoderHight;

	//Add Listener
	public void Start(){
		Btn_rec.onClick.AddListener (Btn_rec_OnClick);
		Btn_normal.onClick.AddListener (Btn_normal_OnClick);
		Btn_hight.onClick.AddListener (Btn_hight_OnClick);
	}

	//判断是否在录音
	public bool isRecord = false;

	//点击录制的按钮
	public void Btn_rec_OnClick(){
		Debug.Log ("UI Components 点击录制的按钮");
		if (isRecord == false) {
			StartRecoder ();
			isRecord = true;
			Btn_rec.GetComponentInChildren<UnityEngine.UI.Text> ().text = "正在录制，点击停止";
		} else {
			StopRecoder ();
			isRecord = false;
			Btn_rec.GetComponentInChildren<UnityEngine.UI.Text> ().text = "录制完成，点击重录";
		}
	}

	//点击正常播放的按钮
	public void Btn_normal_OnClick(){
		Debug.Log ("UI Components 点击正常播放的按钮");
		PlayRecoderNomal ();
	}

	//点击高音播放的按钮
	public void Btn_hight_OnClick(){
		Debug.Log ("UI Components 点击高音播放的按钮");
		PlayRecoderHight ();
	}

	//改变应用的标题
	public void ChangeTitle(string myTitle)
	{
		Debug.Log ("UI Components 开始改变标题的文字");
		title.text = myTitle;
	}

	//改变中间按钮的文字部分
	public void ChangeButtonText(string myText)
	{
		Btn_rec.GetComponentInChildren<UnityEngine.UI.Text> ().text = myText;
	}
}
