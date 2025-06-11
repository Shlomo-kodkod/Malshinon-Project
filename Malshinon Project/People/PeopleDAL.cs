using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace Malshinon_Project
{
    internal class PeopleDAL
    {
        private string connStr = "server=localhost;user=root;password=;database=malshinon";

        internal Guid GenerateSecretCode()
        {
            return Guid.NewGuid();
        }

        internal People GetPeopleRow(int id)
        {
            string query = $"SELECT * FROM people WHERE id = @id";;
            People currPeople = new People();

            try
            {
                using (var conn = new MySqlConnection(connStr))
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@id", id);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int idNum = reader.GetInt32("id");
                                string firstName = reader.GetString("first_name");
                                string lastName = reader.GetString("last_name");
                                string secretCode = reader.GetString("secret_code");
                                string type = reader.GetString("type");
                                int numReports = reader.GetInt32("num_reports");
                                int numMentions = reader.GetInt32("num_mentions");

                                currPeople = new People(idNum, firstName, lastName, secretCode, type, numReports, numMentions);
                            }
                        }
                    }
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error : {ex.Message}");
            }
            return currPeople;
        }

        internal void AddPeople(string type, string firstName, string lastName)
        {
            try
            {
                using (var conn = new MySqlConnection(connStr))
                {
                    conn.Open();
                    string query = "INSERT INTO people (first_name,last_name,secret_code,type)" +
                    "VALUES (@first_name,@last_name,@secret_code,@type)";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@first_name", firstName);
                        cmd.Parameters.AddWithValue("@last_name", lastName);
                        cmd.Parameters.AddWithValue("@secret_code", GenerateSecretCode());
                        cmd.Parameters.AddWithValue("@type", type);
                        cmd.ExecuteNonQuery();
                    }
                }

                Console.WriteLine("New person successfully added!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        internal void UpdateType(int id, string type)
        {
            try
            {
                using (var conn = new MySqlConnection(connStr))
                {
                    conn.Open();
                    string query = "UPDATE people SET type = @newType WHERE id = @currId";

                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@newType", type);
                        cmd.Parameters.AddWithValue("@currId", id);
                        cmd.ExecuteNonQuery();
                    }
                }

                Console.WriteLine($"Status changed successfully to {type}!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        internal void UpdateReportNum(int id)
        {
            try
            {
                using (var conn = new MySqlConnection(connStr))
                {
                    conn.Open();
                    string query = "UPDATE people SET num_reports = num_reports + 1 WHERE id = @currId";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@currId", id);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        internal void UpdateReportMentions(int id)
        {
            try
            {
                using (var conn = new MySqlConnection(connStr))
                {
                    conn.Open();
                    string query = "UPDATE people SET num_mentions = num_mentions + 1 WHERE id = @currId";
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@currId", id);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
        internal int GetIdByname(string first_name, string last_name)
        {
            string query = $"SELECT id FROM people WHERE first_name = @first_name AND last_name = @last_name;";
            try
            {
                using (var conn = new MySqlConnection(connStr))
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@first_Name", first_name);
                        cmd.Parameters.AddWithValue("@last_name", last_name);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int currId = reader.GetInt32("id");
                                return currId;
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

        internal bool IsPeopleExsist(string first_name, string last_name)
        {
            return GetIdByname(first_name, last_name) != 0;
        }

        internal bool IsUniqueName(string name)
        {
            string query = "SELECT COUNT(*) AS count FROM people WHERE first_name = @first_name";
            try
            {
                using(var conn = new MySqlConnection(connStr))
                {
                    conn.Open();
                    using(var cmd = new MySqlCommand(query,conn))
                    {
                        cmd.Parameters.AddWithValue("@first_name", name);

                        using(var reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                int countName = reader.GetInt32("count");
                                return countName == 0;
                            }
                            
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Error : {ex.Message}");
            }
            return false;
        }

        internal string GetSecretCode(string first_name, string last_name)
        {
            string query = $"SELECT secret_code FROM people WHERE first_name = @first_name AND last_name = @last_name;";
            try
            {
                using (var conn = new MySqlConnection(connStr))
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@first_Name", first_name);
                        cmd.Parameters.AddWithValue("@last_name", last_name);

                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string code = reader.GetString("secret_code");
                                return code;
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error : {ex.Message}");
            }
            return "Secret code not found please try again later";
        }

        internal List<People> GetAllDangerousTargets()
        {
            List<People> result = new List<People>();
            string query = $"SELECT * FROM people WHERE num_mentions >= 20";
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
                                int idNum = reader.GetInt32("id");
                                string firstName = reader.GetString("first_name");
                                string lastName = reader.GetString("last_name");
                                string secretCode = reader.GetString("secret_code");
                                string type = reader.GetString("type");
                                int numReports = reader.GetInt32("num_reports");
                                int numMentions = reader.GetInt32("num_mentions");

                               People currPeople = new People(idNum, firstName, lastName, secretCode, type, numReports, numMentions);
                                result.Add(currPeople);
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

        internal List<People> GetAllPotentialAgents()
        {
            List<People> result = new List<People>();
            string query = $"SELECT * FROM people WHERE type = 'potential_agent'";
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
                                int idNum = reader.GetInt32("id");
                                string firstName = reader.GetString("first_name");
                                string lastName = reader.GetString("last_name");
                                string secretCode = reader.GetString("secret_code");
                                string type = reader.GetString("type");
                                int numReports = reader.GetInt32("num_reports");
                                int numMentions = reader.GetInt32("num_mentions");

                                People currPeople = new People(idNum, firstName, lastName, secretCode, type, numReports, numMentions);
                                result.Add(currPeople);
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
