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
    public class TaskRepository
    {
        public static async Task<List<Task>> getTasks(int taskListId)
        {
            try
            {


                Dictionary<string, string> values = new Dictionary<string, string> { };
                values.Add("task_list_id", taskListId.ToString());

                FormUrlEncodedContent content = new FormUrlEncodedContent(values);

                var response = await Configuration.client.PostAsync(Configuration.getTasks, content);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string cont = await response.Content.ReadAsStringAsync();

                    Dictionary<string, List<Task>> deserialized = JsonConvert.DeserializeObject<Dictionary<string, List<Task>>>(cont);

                    return deserialized["tasks"];
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return new List<Task>();
        }

        public static async Task<Task> addTask(Task task)
        {
            try
            {
                var values = TaskAdapter.toJson(task);

                FormUrlEncodedContent content = new FormUrlEncodedContent(values);

                var response = await Configuration.client.PostAsync(Configuration.addTask, content);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string cont = await response.Content.ReadAsStringAsync();

                    Dictionary<string, Task> deserialized = JsonConvert.DeserializeObject<Dictionary<string, Task>>(cont);

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

        public static async Task<Task> updateTask(Task task)
        {
            try
            {


                Dictionary<string, string> values = TaskAdapter.toJson(task);

                FormUrlEncodedContent content = new FormUrlEncodedContent(values);

                var response = await Configuration.client.PostAsync(Configuration.updateTask, content);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    string cont = await response.Content.ReadAsStringAsync();

                    Dictionary<string, Task> deserialized = JsonConvert.DeserializeObject<Dictionary<string, Task>>(cont);

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

        public static async Task<bool> deleteTask(int id)
        {
            try
            {


                Dictionary<string, string> values = new Dictionary<string, string> { { "id", id.ToString() } };

                FormUrlEncodedContent content = new FormUrlEncodedContent(values);

                var response = await Configuration.client.PostAsync(Configuration.deleteTask, content);

                return response.StatusCode == HttpStatusCode.OK;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
