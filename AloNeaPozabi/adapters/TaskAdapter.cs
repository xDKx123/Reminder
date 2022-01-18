using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reminder
{
    class TaskAdapter
    {
        static public Dictionary<String, String> toJson(Task model)
        {
            Dictionary<String, String> jsonObject = new Dictionary<string, string>() {
                {"id", model.Id.ToString() },
                {"name", model.Name },
                {"description", model.Description },
                {"completed", model.Completed.ToString().ToLower() },
                {"deadline", model.Deadline.ToString("u") },
                {"task_list_fk", model.TaskListFk.ToString() },
            };

            return jsonObject;
        }
    }
}
