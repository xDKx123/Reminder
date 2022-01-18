using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Reminder.tests.integration
{
    public interface TaskListAPI
    {

        [Fact]
        public async System.Threading.Tasks.Task AllAPItest()
        {
            try
            {
                TaskList list = new TaskList();
                list.Name = "this is a new list";
                list.Description = "this is a description for new list";

                TaskList newTaskList = await TaskListRepository.addTaskLists(list);

                Assert.Equal(list.Name, newTaskList.Name);
                Assert.Equal(list.Description, newTaskList.Description);
                Assert.True(list.Id > 0);


                //Update;
                newTaskList.Name = "changed name";
                TaskList newTaskList2 = await TaskListRepository.updateTaskList(newTaskList);
                Assert.Equal(newTaskList.Name, newTaskList2.Name);
                Assert.NotEqual(list.Name, newTaskList2.Name);
                Assert.Equal(newTaskList.Description, newTaskList2.Description);

                newTaskList.Description = "changed description";
                TaskList newTaskList3 = await TaskListRepository.updateTaskList(newTaskList);
                Assert.Equal(newTaskList2.Description, newTaskList3.Description);
                Assert.NotEqual(list.Description, newTaskList3.Description);
                Assert.Equal(newTaskList2.Name, newTaskList3.Name);

                bool deleted = await TaskListRepository.deleteTaskList(newTaskList3);
                Assert.True(deleted);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        [Fact]
        public void Testing()
        {
            Assert.Equal(0, 0);
        }
    }
}
