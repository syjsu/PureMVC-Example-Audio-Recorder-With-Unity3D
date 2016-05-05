using UnityEngine;
using System.Collections;

public class RecordUI : MonoBehaviour {

	public UserRecord myRecord;

	void Awake()
	{
		//启动PureMVC程序
		ApplicationFacade facade = ApplicationFacade.Instance as ApplicationFacade;
		facade.Startup(this);
	}
}
