//[lzh]
using UnityEngine;
using System.Collections.Generic;
using PureMVC.Patterns;
using PureMVC.Interfaces;

public class UserProxy : Proxy, IProxy
{
    public new const string NAME = "UserProxy";

    /// <summary>
    /// Return data property cast to proper type
    /// </summary>
    public IList<UserVO> Users
    {
        get { return (IList<UserVO>) base.Data; }
    }

    public UserProxy()
            : base(NAME, new List<UserVO>())
    {
        Debug.Log("UserProxy()");
        // generate some test data			
        AddItem(new UserVO("lstooge", "Larry", "Stooge", "larry@stooges.com", "ijk456", "ACCT"));
        AddItem(new UserVO("cstooge", "Curly", "Stooge", "curly@stooges.com", "xyz987", "SALES"));
        AddItem(new UserVO("mstooge", "Moe", "Stooge", "moe@stooges.com", "abc123", "PLANT"));
        AddItem(new UserVO("lzh", "abc", "def", "lzh@stooges.com", "abc123", "IT"));
    }

    /// <summary>
    /// add an item to the data
    /// </summary>
    /// <param name="user"></param>
    public void AddItem(UserVO user)
    {
        Users.Add(user);
    }

    /// <summary>
    /// update an item in the data
    /// </summary>
    /// <param name="user"></param>
    public void UpdateItem(UserVO user)
    {
        for (int i = 0; i < Users.Count; i++)
        {
            if (Users[i].UserName.Equals(user.UserName))
            {
                Users[i] = user;
                break;
            }
        }
    }

    /// <summary>
    /// delete an item in the data
    /// </summary>
    /// <param name="user"></param>
    public void DeleteItem(UserVO user)
    {
        for (int i = 0; i < Users.Count; i++)
        {
            if (Users[i].UserName.Equals(user.UserName))
            {
                Users.RemoveAt(i);
                break;
            }
        }
    }
}
