//[lzh]
using UnityEngine;
using System.Collections;

public class EventsEnum
{
    public const string STARTUP = "startup";

	public const string STARTRECORD = "startRecord";
	public const string STOPRECORD = "stopRecord";
	public const string PLAYRECORDNORMAL = "playRecordNormal";
	public const string PLAYRECORDHIGHT = "playRecordHight";

    public const string NEW_USER = "newUser";
    public const string DELETE_USER = "deleteUser";
    public const string CANCEL_SELECTED = "cancelSelected";

    public const string USER_SELECTED = "userSelected";
    public const string USER_ADDED = "userAdded";
    public const string USER_UPDATED = "userUpdated";
    public const string USER_DELETED = "userDeleted";

    public const string ADD_ROLE = "addRole";
    public const string ADD_ROLE_RESULT = "addRoleResult";
}
