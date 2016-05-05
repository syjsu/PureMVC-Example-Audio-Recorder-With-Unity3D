using UnityEngine;
using System.Collections;
using PureMVC.Patterns;
using PureMVC.Interfaces;

public class StartupCommand : SimpleCommand, ICommand
{
    public override void Execute(INotification notification)
    {
		Debug.Log("执行StartupCommand.Execute()的函数");

		Facade.RegisterProxy(new RecordProxy());

		RecordUI r = notification.Body as RecordUI;
		Facade.RegisterMediator(new UserRecordMediator(r.myRecord));
    }
}
