using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Services.CachedServices
{
    public class CachedAnagram
    {
        private string connectionString;

        SqlConnection cn = new SqlConnection();

        public CachedAnagram(string con)
        {
            connectionString = con;
        }

        public bool InsertCache(List<int> elements, string query)
        {
            int Id = FindId(query);

            cn.ConnectionString = connectionString;

            cn.Open();

            SqlCommand insert = new SqlCommand();
            insert.Connection = cn;

            insert.CommandType = System.Data.CommandType.Text;
            insert.CommandText = "INSERT INTO dbo.CachedWord (WordId, AnagramId) VALUES(@FN, @LN)";

            insert.Parameters
                .Add(new SqlParameter("@FN", System.Data.SqlDbType.Int, 1000000, "WordId"));

            insert.Parameters
               .Add(new SqlParameter("@lN", System.Data.SqlDbType.Int, 1000000, "AnagramId"));

            SqlDataAdapter adapter = new SqlDataAdapter("SELECT Id, Name FROM dbo.CachedWord", cn);

            adapter.InsertCommand = insert;

            DataSet ds = new DataSet();
            adapter.Fill(ds, "dbo.Word");

            foreach (var word in WordsList)
            {
                if (word == null)
                {
                    continue;
                }
                DataRow newRow = ds.Tables[0].NewRow();
                newRow["Name"] = word;
                ds.Tables[0].Rows.Add(newRow);
            }

            adapter.Update(ds.Tables[0]);
            cn.Close();
            adapter.Dispose();

            //using (adapter) //IDisposable
            //{

            //}


                return false;
        }

        private int FindId(string Name)
        {
            cn.ConnectionString = connectionString;
            cn.Open();

            SqlDataAdapter adapter = new SqlDataAdapter();

            SqlCommand command = new SqlCommand("SELECT Word.Id FROM dbo.Word WHERE Name = @FN", cn);

            command.Parameters.AddWithValue("@FN", Name);

            adapter.SelectCommand = command;

            DataSet data = new DataSet();

            adapter.Fill(data, "Word");

            cn.Close();
            adapter.Dispose();

            return Int32.Parse(data.Tables[0].
                Rows[0]["Id"].ToString());

        }
        public WordAndAnagram GetCachedData(string Name)
        {
            cn.ConnectionString = connectionString;
            cn.Open();

            SqlDataAdapter adapter = new SqlDataAdapter();

            SqlCommand command = new SqlCommand("SELECT CachedAnagram.Name FROM dbo.CachedAnagram " +    
                                                  "WHERE CachedAnagram.Name = @FN", cn);

            command.Parameters.AddWithValue("@FN", Name);

            adapter.SelectCommand = command;

            DataSet dataSet = new DataSet();

            adapter.Fill(dataSet, "Word");

            cn.Close();
            adapter.Dispose();

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                return FillSet(dataSet);
            }

            return null;
        }

        private WordAndAnagram FillSet(DataSet data)
        {
            WordAndAnagram dtr = new WordAndAnagram();

            for (var i = 0; i < data.Tables[0].Rows.Count; i++)
            {
                dtr.AnagramsId.Add(Int32.Parse(data.Tables[0].Rows[i]["Id"].ToString()));
                dtr.ListOfAanagrams.Add(data.Tables[0].Rows[i]["Name"].ToString());
            }

            return dtr;
        }
    }

    public class WordAndAnagram
    {
        public List<int> AnagramsId { get; set; }
        public HashSet<string> ListOfAanagrams { get; set;}
    }
}
