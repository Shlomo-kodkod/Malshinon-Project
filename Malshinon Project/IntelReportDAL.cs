using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Malshinon_Project
{
    internal class IntelReportDAL
    {
        private string connStr = "server=localhost;user=root;password=;database=malshinon";

        public void AddReport(int reportId, int targetId, string Text)
        {
            try
            {
                using (var conn = new MySqlConnection(connStr))
                {
                    conn.Open();
                    string query = "INSERT INTO intelreports (reporter_id,target_id,text)" +
                    "VALUES (@reporter_id,@target_id,@text)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@reporter_id", reportId);
                        cmd.Parameters.AddWithValue("@target_id", targetId);
                        cmd.Parameters.AddWithValue("@text", Text);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public int GetAvgTextLen(int targetId)
        {
            string query = $"SELECT AVG(LENGTH(text)) AS avg FROM intelreports WHERE target_id = @id"; ;
            try
            {
                using (var conn = new MySqlConnection(connStr))
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", targetId);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int idNum = reader.GetInt32("avg");
                                return idNum;
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error : {ex.Message}");
            }
            return 0;
        }
    }
}
