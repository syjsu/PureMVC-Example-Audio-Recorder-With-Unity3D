//[lzh]
using UnityEngine;
using System.Collections;
using PureMVC.Patterns;
using PureMVC.Interfaces;

public class ApplicationFacade : Facade
{
	//单例启动
    public new static IFacade Instance
    {
        get
        {
            if(m_instance == null)
            {
                lock(m_staticSyncRoot)
                {
                    if (m_instance == null)
                    {
                        Debug.Log("启动PureMVC的入口函数ApplicationFacade");
                        m_instance = new ApplicationFacade();
                    }
                }
            }
            return m_instance;
        }
    }

	//开始执行
	public void Startup(RecordUI r)
    {
		Debug.Log("Startup()函数，发送消息EventsEnum.STARTUP到RecordUI的UI总控制那里");
		SendNotification(EventsEnum.STARTUP,r);

		SendNotification(EventsEnum.STARTRECORD);
    }
		
	//初始化
    protected override void InitializeController()
    {
        Debug.Log("初始化PureMVC框架");
        base.InitializeController();
        RegisterCommand(EventsEnum.STARTUP, typeof(StartupCommand));
        //RegisterCommand(EventsEnum.DELETE_USER, typeof(DeleteUserCommand));
    }
}
