using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.IO;
using System.Windows.Documents;
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
                SQLiteConnection.CreateFile($@"PerformanceLog_{today}.db");
                CreateTable();
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
            sqliteConnection = new SQLiteConnection($"Data Source=./PerformanceLog_{Lastestdatetime}.db");
            sqliteConnection.Open();
            string sql = "CREATE TABLE IF NOT EXISTS PerformanceLog (ID INTEGER PRIMARY KEY AUTOINCREMENT, Date TEXT, Time TEXT, CPU TEXT, Memory TEXT, GPU TEXT)";
            SQLiteCommand command = new SQLiteCommand(sql, sqliteConnection);
            command.ExecuteNonQuery();
            sqliteConnection.Close();
        }

        public void InsertLog(string date, string time, string cpu, string memory, string gpu)
        {
            using (var connection = new SQLiteConnection($"Data Source=./PerformanceLog_{Lastestdatetime}.db"))
            {
                connection.Open();
                string sql = "INSERT INTO PerformanceLog (Date, Time, CPU, Memory, GPU) VALUES (@Date, @Time, @CPU, @Memory, @GPU)";
                using (var command = new SQLiteCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@Date", date);
                    command.Parameters.AddWithValue("@Time", time);
                    command.Parameters.AddWithValue("@CPU", cpu);
                    command.Parameters.AddWithValue("@Memory", memory);
                    command.Parameters.AddWithValue("@GPU", gpu);

                    command.ExecuteNonQuery();
                }
            }
        }

        public DataManage()
        {
            
        }


    }
}
