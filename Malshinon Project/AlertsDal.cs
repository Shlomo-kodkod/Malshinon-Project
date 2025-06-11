using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using static System.Net.Mime.MediaTypeNames;

namespace Malshinon_Project
{
    internal class AlertsDal
    {
        private string connStr = "server=localhost;user=root;password=;database=malshinon";

        public void AddAlert(int target_id, string reason)
        {
            try
            {
                using (var conn = new MySqlConnection(connStr))
                {
                    conn.Open();
                    string query = "INSERT INTO alerts (target_id,reason)" +
                    "VALUES (@target_id,@reason)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@target_id", target_id);
                        cmd.Parameters.AddWithValue("@reason", reason);
                        cmd.ExecuteNonQuery();
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        public List<Alerts> GetAllAlerts()
        {
            List<Alerts> result = new List<Alerts>();
            string query = $"SELECT * FROM alerts"; ;
            try
            {
                using (var conn = new MySqlConnection(connStr))
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int id = reader.GetInt32("id");
                                int target_id = reader.GetInt32("target_id");
                                DateTime creat_at = reader.GetDateTime("created_at");
                                string reason = reader.GetString("reason");

                                Alerts alert = new Alerts(id, target_id, creat_at, reason);
                                result.Add(alert);
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error : {ex.Message}");
            }
            return result;
        }

       
    
    }
    
}
