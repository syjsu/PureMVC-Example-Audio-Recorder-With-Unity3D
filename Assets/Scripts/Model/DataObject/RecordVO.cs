using UnityEngine;
using System.Collections;

public class RecordVO{

	//构造器
	public RecordVO(string RecordName,string RecordTime,string RecordFre,string RecordSize)
	{
		Debug.Log ("进入RecordVO的构造器");

		//构造函数的值
		if(RecordName!=null)
			m_RecordName=RecordName;
		if(RecordTime!=null)
			m_RecordTime=RecordTime;
		if(RecordFre!=null)
			m_RecordFre=RecordFre;
		if(RecordSize!=null)
			m_RecordSize=RecordSize;
	}

	//录音名字
	public string RecordName
	{
		get { return m_RecordName; }
	}
	private string m_RecordName = "";

	//录音时间
	public string RecordTime
	{
		get { return m_RecordTime; }
	}
	private string m_RecordTime = "";

	//录音采样率
	public string RecordFre
	{
		get { return m_RecordFre; }
	}
	private string m_RecordFre = "";

	//录音时长
	public string RecordSize
	{
		get { return m_RecordSize; }
	}
	private string m_RecordSize = "";
}
