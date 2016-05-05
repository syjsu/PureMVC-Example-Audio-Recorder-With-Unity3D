//[lzh]
using UnityEngine;
using System.Collections;
using PureMVC.Patterns;
using PureMVC.Interfaces;
using System.Collections.Generic;

public class UserFormMediator : Mediator, IMediator
{
    private UserProxy userProxy;

    public new const string NAME = "UserFormMediator";

    private UserForm View
    {
        get { return (UserForm)ViewComponent; }
    }

    public UserFormMediator(UserForm viewComponent)
        : base(NAME, viewComponent)
    {
        Debug.Log("UserFormMediator()");

        View.AddUser += UserForm_AddUser;
        View.UpdateUser += UserForm_UpdateUser;
        View.CancelUser += UserForm_CancelUser;
    }

    public override void OnRegister()
    {
        base.OnRegister();
        userProxy = Facade.RetrieveProxy(UserProxy.NAME) as UserProxy;
    }

    void UserForm_AddUser()
    {
        UserVO user = View.User;
        userProxy.AddItem(user);
        SendNotification(EventsEnum.USER_ADDED, user);
        View.ClearForm();
    }

    void UserForm_UpdateUser()
    {
        UserVO user = View.User;
        userProxy.UpdateItem(user);
        SendNotification(EventsEnum.USER_UPDATED, user);
        View.ClearForm();
    }

    void UserForm_CancelUser()
    {
        SendNotification(EventsEnum.CANCEL_SELECTED);
        View.ClearForm();
    }

    public override IList<string> ListNotificationInterests()
    {
        IList<string> list = new List<string>();
        list.Add(EventsEnum.NEW_USER);
        list.Add(EventsEnum.USER_DELETED);
        list.Add(EventsEnum.USER_SELECTED);
        return list;
    }

    public override void HandleNotification(INotification note)
    {
        UserVO user;
        switch (note.Name)
        {
            case EventsEnum.NEW_USER:
                user = (UserVO)note.Body;
                View.ShowUser(user, UserFormMode.ADD);
                break;

            case EventsEnum.USER_DELETED:
                View.ClearForm();
                break;

            case EventsEnum.USER_SELECTED:
                user = (UserVO)note.Body;
                View.ShowUser(user, UserFormMode.EDIT);
                break;

        }
    }
}
