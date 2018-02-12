using Anagrams.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
namespace Anagrams.Repositories
{
    public class DbRepository : IWordRepository<string>
    {
        private string connectionString;

        SqlConnection cn = new SqlConnection();

        public DbRepository(string path)
        {
            connectionString = path;
        }

        public HashSet<string> Contains(string Name)
        {
            cn.ConnectionString = connectionString;
            cn.Open();

            SqlDataAdapter adapter = new SqlDataAdapter();

            SqlCommand command = new SqlCommand("SELECT Name FROM dbo.Word WHERE Name LIKE @FN", cn);

            command.Parameters.AddWithValue("@FN", "%" + Name + "%");

            adapter.SelectCommand = command;

            DataSet dataSet = new DataSet();


            adapter.Fill(dataSet, "Word");

            cn.Close();
            adapter.Dispose();

            return FillHashSet(dataSet);
        }

        public HashSet<string> GetData(string Name = null)
        {
            cn.ConnectionString = connectionString;
            cn.Open();

            SqlDataAdapter adapter = new SqlDataAdapter();

            SqlCommand command = new SqlCommand("SELECT Name FROM dbo.Word", cn);

            adapter.SelectCommand = command;

            DataSet dataSet = new DataSet();


            adapter.Fill(dataSet, "Word");

            cn.Close();
            adapter.Dispose();

           return FillHashSet(dataSet);
        }

        private HashSet<string> FillHashSet(DataSet data)
        {
            HashSet<string> dtr = new HashSet<string>();
            for(var i = 0; i < data.Tables[0].Rows.Count; i++)
            {
                dtr.Add(data.Tables[0].Rows[i]["Name"].ToString());
            }

            return dtr;
        }

        public bool InsertNewWord(string Name)
        {
            throw new NotImplementedException();
        }

        public string ReturnFilePath()
        {
            return connectionString;
        }
    }
}
