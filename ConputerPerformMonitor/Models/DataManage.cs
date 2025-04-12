using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using System.Windows.Documents;
using ConputerPerformMonitor.ViewModels.TabsVM;
namespace ConputerPerformMonitor.Models
{
    public class DataManage
    {
        SQLiteConnection sqliteConnection;
        static DateTime Lastestdatetime;

        public void CreateDBFile()
        {

            DateTime today = DateTime.Today;
            Lastestdatetime = today;
            if (!File.Exists($"./PerformanceLog_{today}.db"))
            {
                SQLiteConnection.CreateFile($@"PerformanceLog_{Lastestdatetime.Year}{Lastestdatetime.Month}{Lastestdatetime.Day}.db");
                CreateTable();
                Log log = new Log()
                {
                    date = DateTime.Today.ToShortDateString(),
                    time = DateTime.Now.ToShortTimeString(),
                    cpu=ReportManageTabVM._cpuUsage.ToString(),
                    memory = ReportManageTabVM._ramUsage.ToString(),
                    gpu = ReportManageTabVM._gpuUsage.ToString()
                };
                InsertLog(log);
            }
            
        }
        public void CreateDBFile(DateTime datetime)
        {
            Lastestdatetime = datetime;
            if (!File.Exists($"./PerformanceLog_{datetime}.db"))
            {

                SQLiteConnection.CreateFile($@"PerformanceLog_{datetime}.db");
                CreateTable();
            }
            

        }
        public void CreateTable()
        {
            sqliteConnection = new SQLiteConnection($"Data Source=./PerformanceLog_{Lastestdatetime.Year}{Lastestdatetime.Month}{Lastestdatetime.Day}.db");
            sqliteConnection.Open();
            string sql = "CREATE TABLE IF NOT EXISTS PerformanceLog (ID INTEGER PRIMARY KEY AUTOINCREMENT, Date TEXT, Time TEXT, CPU TEXT, Memory TEXT, GPU TEXT)";
            SQLiteCommand command = new SQLiteCommand(sql, sqliteConnection);
            command.ExecuteNonQuery();
            sqliteConnection.Close();
        }

        public void InsertLog(Log log)
        {
            using (var connection = new SQLiteConnection($"Data Source=./PerformanceLog_{Lastestdatetime.Year}{Lastestdatetime.Month}{Lastestdatetime.Day}.db"))
            {
                connection.Open();
                string sql = "INSERT INTO PerformanceLog (Date, Time, CPU, Memory, GPU) VALUES (@Date, @Time, @CPU, @Memory, @GPU)";
                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Date", log.date);
                    command.Parameters.AddWithValue("@Time", log.time);
                    command.Parameters.AddWithValue("@CPU", log.cpu);
                    command.Parameters.AddWithValue("@Memory", log.memory);
                    command.Parameters.AddWithValue("@GPU", log.gpu);

                    try
                    {
                        command.ExecuteNonQuery();

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error inserting log: {ex.Message}");
                    }
                }
            }
        }

        public DataManage()
        {
            
        }


    }
}
