using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using TaskHub.Model;
using TaskHub.Models;
using TaskHub.ViewModels;

namespace TaskHub.DAL
{
    public static class DataAccess
    {
        public static IList<TaskModel> ReadTaskDB()
        {
            IList<TaskModel> tasks = new List<TaskModel>();
            using (var con = new SqlConnection(@"Data Source=desktop-ihdvud3\sqlexpress;Initial Catalog=TaskTracker;Integrated Security=True"))
            {
                tasks = con.Query<TaskModel>("dbo.SELECT_Tasks", new TaskModel()).ToList();
            }
            return tasks;
        }

        internal static IEnumerable<ProjectModel> ReadProjectDb()
        {
            IList<ProjectModel> projects = new List<ProjectModel>();
            using (var con = new SqlConnection(@"Data Source=desktop-ihdvud3\sqlexpress;Initial Catalog=TaskTracker;Integrated Security=True"))
            {
                projects = con.Query<ProjectModel>("dbo.SELECT_Projects", new ProjectModel()).ToList();
            }
            return projects;
        }

        public static void UpdateDb( TaskModel model)
        {

            using (var con = new SqlConnection(@"Data Source=desktop-ihdvud3\sqlexpress;Initial Catalog=TaskTracker;Integrated Security=True"))
            {
                con.Execute("dbo.UPDATE_Task " +
                    "@TaskID,"+
                    "@TaskName,"+
                    "@TaskDescription," +
                    "@isActive," +
                    "@TaskStatus" 
                    , model);
            }
        }

        public static void WriteNewEntry( TaskModel model)
        {

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

        public static void RemoveEntry(TaskModel model)
        {
            using (var con = new SqlConnection(@"Data Source=desktop-ihdvud3\sqlexpress;Initial Catalog=TaskTracker;Integrated Security=True"))
                con.Execute(@"dbo.DELETE_Task @TaskID", model);
        }

        
    }
}
