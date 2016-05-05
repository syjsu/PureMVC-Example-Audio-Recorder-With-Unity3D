//[lzh]
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UserList_Item : MonoBehaviour
{
    //public Text txt_conent;
    public Text txt_userName;
    public Text txt_firstName;
    public Text txt_lastName;
    public Text txt_email;
    public Text txt_department;

    public UserVO userData;

	// Use this for initialization
	void Start ()
	{
    }
	
    public void UpdateData(UserVO data)
    {
        this.userData = data;

        //System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //string interval = "\t\t\t";
        //sb.Append(data.UserName);
        //sb.Append(interval);
        //sb.Append(data.FirstName);
        //sb.Append(interval);
        //sb.Append(data.LastName);
        //sb.Append(interval);
        //sb.Append(data.Email);
        //sb.Append(interval);
        //sb.Append(data.Department);
        //txt_conent.text = sb.ToString();

        txt_userName.text = data.UserName;
        txt_firstName.text = data.FirstName;
        txt_lastName.text = data.LastName;
        txt_email.text = data.Email;
        txt_department.text = data.Department;
    }
}
