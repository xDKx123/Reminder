using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace Reminder
{
    public class Configuration
    {
        public static readonly string serverIp = "http://127.0.0.1:6060";
        public static readonly HttpClient client = new HttpClient();

        //routes
        public static readonly string getTaskLists = serverIp + "/getTaskLists";
        public static readonly string updateTaskList = serverIp + "/updateTaskList";
        public static readonly string deleteTaskList = serverIp + "/deleteTaskList";
        public static readonly string addTaskList = serverIp + "/addTaskList";

        public static readonly string getTasks = serverIp +  "/getTasks";
        public static readonly string updateTask = serverIp + "/updateTask";
        public static readonly string deleteTask = serverIp + "/deleteTask";
        public static readonly string addTask = serverIp + "/addTask";
    }
}
