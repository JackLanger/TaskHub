using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using TaskHub.Model;

namespace TaskHub.DAL
{
    public static class DataAccess
    {
        public static IList <TaskModel> ReadTaskDB()
        {

            IList<TaskModel> tasks = new List<TaskModel>();
            using ( var con = new SqlConnection(@"Data Source=desktop-ihdvud3\sqlexpress;Initial Catalog=TaskTracker;Integrated Security=True"))
            {
                tasks = con.Query<TaskModel>("dbo.SELECT_Tasks", new TaskModel()).ToList();
            }
            return tasks;
        }

        public static void UpdateDb( TaskModel model)
        {
            //TaskDataDataContext tdc = new TaskDataDataContext(Properties.Settings.Default.TaskTrackerConnectionString);
            //tdc.SubmitChanges();

            using (var con = new SqlConnection(@"Data Source=desktop-ihdvud3\sqlexpress;Initial Catalog=TaskTracker;Integrated Security=True"))
            {
                con.Execute("dbo.UPDATE_Task " +
                    "@isActive,"+
                    "@TaskID " 
                    , model);
            }
        }
        public static void WriteNewEntry( TaskModel model)
        {
            //TaskDataDataContext tdc = new TaskDataDataContext(Properties.Settings.Default.TaskTrackerConnectionString);
            //tdc.SubmitChanges();

            using (var con = new SqlConnection(@"Data Source=desktop-ihdvud3\sqlexpress;Initial Catalog=TaskTracker;Integrated Security=True"))
            {
                con.Execute(@"dbo.INSERT_Task 
                                                @TaskName,
	                                            @TaskDescription,
	                                            @PostedBy,
	                                            @DateAdded,
	                                            @isActive,
	                                            @TaskStatus
                                                ", model);
            }
        }

    }
}
