using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net;
using Newtonsoft.Json;

namespace Reminder
{
    public class TaskListRepository
    {
        public static async Task<List<TaskList>> getTaskLists()
        {
            try
            {
                Dictionary<string, string> values = new Dictionary<string, string> { };

                FormUrlEncodedContent content = new FormUrlEncodedContent(values);

                var response = await Configuration.client.PostAsync(Configuration.getTaskLists, content);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string cont = await response.Content.ReadAsStringAsync();

                    Dictionary<string, List<TaskList>> deserialized = JsonConvert.DeserializeObject<Dictionary<String, List<TaskList>>>(cont);


                    //return 0;
                    return deserialized["tasks"];
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return new List<TaskList>();
        }

        public static async Task<TaskList> addTaskLists(TaskList task)
        {
            try
            {
                var values = TaskListAdapter.toJson(task);

                FormUrlEncodedContent content = new FormUrlEncodedContent(values);

                var response = await Configuration.client.PostAsync(Configuration.addTaskList, content);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string cont = await response.Content.ReadAsStringAsync();

                    Dictionary<string, TaskList> deserialized = JsonConvert.DeserializeObject<Dictionary<String, TaskList>>(cont);


                    //return 0;
                    return deserialized["tasks"];
                }

                throw new Exception();

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }



        public static async Task<TaskList> updateTaskList(TaskList task) 
        {
            try
            {
                var values = TaskListAdapter.toJson(task);

                FormUrlEncodedContent content = new FormUrlEncodedContent(values);

                var response = await Configuration.client.PostAsync(Configuration.updateTaskList, content);


                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string cont = await response.Content.ReadAsStringAsync();

                    Dictionary<string, TaskList> deserialized = JsonConvert.DeserializeObject<Dictionary<String, TaskList>>(cont);


                    //return 0;
                    return deserialized["tasks"];
                }

                throw new Exception();
            }
                        
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
}

        public static async Task<bool> deleteTaskList(TaskList task)
        {
            try
            {


                Dictionary<string, string> values = new Dictionary<string, string> {
                    { "id", task.Id.ToString() }
                };

                FormUrlEncodedContent content = new FormUrlEncodedContent(values);

                var response = await Configuration.client.PostAsync(Configuration.deleteTaskList, content);

                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK:
                        return true;
                    case HttpStatusCode.Conflict:
                        return false;
                    default:
                        return false;

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
    }
}
