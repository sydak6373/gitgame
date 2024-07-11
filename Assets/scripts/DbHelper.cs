using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data.SqlClient;
using System;

public static class DbHelper
{
    private static string connection = GetConnectionString();
    static private string GetConnectionString()
    {
        //local
        //return @"Data Source = (local)\SQLEXPRESS;Initial Catalog = SpaceDataBase; Integrated Security = True; TrustServerCertificate = True";
        //server
        return @"Data Source=DESKTOP-9T0H6F4\SQLEXPRESS;Initial Catalog=SevguPractice;Integrated Security=True;Encrypt=True;TrustServerCertificate=True";
    }
    public static void ExecuteWithoutAnswer(string str)
    {
        using (SqlConnection conn = new SqlConnection(connection))
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand(str, conn);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            conn.Close();
        }
    }
    public static List<object> ExecuteWithAnswer(string str)
    {
        using (SqlConnection conn = new SqlConnection(connection))
        {
            conn.Open();
            List<object> result = new List<object>();
            SqlCommand cmd = new SqlCommand(str, conn);
            try
            {
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        result.Add(reader.GetValue(0));
                        result.Add(reader.GetValue(1));
                        result.Add(reader.GetValue(2));
                        result.Add(reader.GetValue(3));
                        result.Add(reader.GetValue(4));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            conn.Close();
            return result;
        }
    }
    public static string ExecuteQueryWithAnswer(string query)
    {
        using (SqlConnection conn = new SqlConnection(connection))
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            var answer = cmd.ExecuteScalar();
            conn.Close();

            if (answer != null) return answer.ToString();
            else return null;
        }
    }
}


