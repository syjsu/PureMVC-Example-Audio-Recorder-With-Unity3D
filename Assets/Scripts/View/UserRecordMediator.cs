using UnityEngine;
using System.Collections;
using PureMVC.Patterns;
using PureMVC.Interfaces;
using System.Collections.Generic;

public class UserRecordMediator : Mediator, IMediator {

	//初始化
	private UserRecord View
	{
		get { return (UserRecord)ViewComponent; }
	}

	//获取数据的接口
	private RecordProxy recordProxy;
	public new const string NAME = "UserRecordMediator";

	public override void OnRegister()
	{
		Debug.Log("进入Mediator()的OnRegister()");
		base.OnRegister();
		recordProxy = Facade.RetrieveProxy(RecordProxy.NAME) as RecordProxy;
	}

	//构造函数
	public UserRecordMediator(UserRecord viewComponent):base(NAME, viewComponent)
	{
		Debug.Log("进入Mediator()的构造函数");

		// others
		View.StartRecoder += Listener_StartRecoder;
		View.StopRecoder += Listener_StopRecoder;
		View.PlayRecoderNomal += Listener_PlayRecoderNomal;
		View.PlayRecoderHight += Listener_PlayRecoderHight;
	}

	//接收按钮的消息
	public void Listener_StartRecoder(){
		Debug.Log("进入Mediator()的StartRecoder");
		recordProxy.StartRecord ();
		SendNotification(EventsEnum.STARTRECORD,UserRecordMediator.NAME);
	}

	public void Listener_StopRecoder(){
		Debug.Log("进入Mediator()的StopRecoder");
		recordProxy.StopRecord ();
		SendNotification(EventsEnum.STOPRECORD,UserRecordMediator.NAME);
	}

	public void Listener_PlayRecoderNomal(){
		Debug.Log("进入Mediator()的PlayRecoderNomal");
		recordProxy.PlayRecordNomal ();
		SendNotification(EventsEnum.PLAYRECORDNORMAL,UserRecordMediator.NAME);
	}

	public void Listener_PlayRecoderHight(){
		Debug.Log("进入Mediator()的PlayRecoderHight");
		recordProxy.PlayRecordHight ();
		SendNotification(EventsEnum.PLAYRECORDHIGHT,UserRecordMediator.NAME);
	}

	//接收广播的监听
	public override void HandleNotification(INotification note)
	{
		Debug.Log("进入Mediator()的HandleNotification() 响应的消息是："+note.Name);

		switch (note.Name)
		{
		case EventsEnum.STARTUP:
			View.ChangeTitle ("欢迎使用录音变声软件 Make By @小小酥XX");
			break;
		}
	}
}
