using ChatServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace UserDatabaseServer
{
    //public class UserBusiness
    //{
    //    private static int newID = 1;
    //    private static bool userExist = false;
    //    private static User theUser = null;
    //    private static List<User> userList = new List<User>();
    //    public static User login(string username)
    //    {
    //        userExist = false;
    //        foreach (User user in UserBusiness.userList)
    //        {
    //            if (user.getUserName().Equals(username))
    //            {
    //                userExist = true;
    //                theUser = user;
    //            }
    //        }
    //        if (!userExist)
    //        {
    //            theUser = new User(username, newID);
    //            userList.Add(theUser);
    //            newID++;
    //        }
    //        return theUser;
    //    }
    //}
}
