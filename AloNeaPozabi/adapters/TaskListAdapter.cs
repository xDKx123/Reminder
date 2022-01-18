using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reminder
{
    class TaskListAdapter
    {
        static public Dictionary<String, String> toJson(TaskList model)
        {
            Dictionary<String, String> jsonObject = new Dictionary<string, string>() {
                {"id", model.Id.ToString() },
                {"name", model.Name },
                { "description", model.Description },
            };

            return jsonObject;
        }
    }
}
