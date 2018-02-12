using Services.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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

        public bool InsertCache(HashSet<string> elements, string query)
        {

            cn.ConnectionString = connectionString;

            cn.Open();

            SqlCommand insert = new SqlCommand();
            insert.Connection = cn;

            insert.CommandType = System.Data.CommandType.Text;
            insert.CommandText = "INSERT INTO dbo.CacheMaps (SortedWord) VALUES(@FN)";

            insert.Parameters
                .Add(new SqlParameter("@FN", System.Data.SqlDbType.NVarChar, 255, "SortedWord"));


            SqlDataAdapter adapter = new SqlDataAdapter("SELECT SortedWord FROM dbo.CacheMaps", cn);

            adapter.InsertCommand = insert;

            DataSet ds = new DataSet();
            adapter.Fill(ds, "dbo.CacheMaps");

            var sortedName = query.ToLower().GetWithoutWhiteSpace();
            var str = String.Concat(sortedName.OrderBy(c => c)); // SORTED AND LOWERED

            foreach (var word in elements)
            {
                if (word == null)
                {
                    continue;
                }
                DataRow newRow = ds.Tables[0].NewRow();
                newRow["SortedWord"] = str;
                ds.Tables[0].Rows.Add(newRow);
            }

            adapter.Update(ds.Tables[0]);
            cn.Close();
            adapter.Dispose();

            List<int> Ids = GetIds(str);
            InsertUnConcatedWords(Ids, elements);
            return false;
        }

        public List<int> GetIds (string sortedName)
        {

            cn.ConnectionString = connectionString;
            cn.Open();

            SqlDataAdapter adapter = new SqlDataAdapter();

            SqlCommand command = new SqlCommand("SELECT Id FROM dbo.CacheMaps " +
                                                  "WHERE SortedWord = @FN", cn);

            command.Parameters.AddWithValue("@FN", sortedName);

            adapter.SelectCommand = command;

            DataSet dataSet = new DataSet();

            adapter.Fill(dataSet, "dbo.CacheMaps");

            cn.Close();
            adapter.Dispose();

            List<int> Ids = new List<int>();
            for (var j = 0; j < dataSet.Tables[0].Rows.Count; j++)
            {
                Ids.Add(Int32.Parse(dataSet.Tables[0].Rows[j]["Id"].ToString()));
            }

            return Ids;
        }

        public HashSet<string> GetCachedData(string Name)
        {
            cn.ConnectionString = connectionString;
            cn.Open();

            SqlDataAdapter adapter = new SqlDataAdapter();

            var sortedName = Name.ToLower().GetWithoutWhiteSpace(); 
            var str = String.Concat(sortedName.OrderBy(c => c)); // SORTED AND LOWERED

            SqlCommand command = new SqlCommand("SELECT Id FROM dbo.CacheMaps " +    
                                                  "WHERE SortedWord = @FN", cn);

            command.Parameters.AddWithValue("@FN", str);

            adapter.SelectCommand = command;

            DataSet dataSet = new DataSet();

            adapter.Fill(dataSet, "dbo.CacheMaps");

            cn.Close();
            adapter.Dispose();

            if (dataSet.Tables[0].Rows.Count > 0)
            {
                return FillSet(dataSet);
            }

            return null;
        }

        private HashSet<string> FillSet(DataSet data)
        {
            HashSet<string> dtr = new HashSet<string>();

            for (var i = 0; i < data.Tables[0].Rows.Count; i++)
            {
                cn.ConnectionString = connectionString;
                cn.Open();

                SqlDataAdapter adapter = new SqlDataAdapter();


                SqlCommand command = new SqlCommand("SELECT Id FROM dbo.CacheMaps " +
                                                      "WHERE SortedWord = @FN", cn);

                command.Parameters.AddWithValue("@FN", data.Tables[0].Rows[i]["Id"]); // Assign ID

                adapter.SelectCommand = command;

                DataSet dataSet = new DataSet();

                adapter.Fill(dataSet, "dbo.CacheAnagram");

                var concatedWord = "";
                for (var j = 0; j< dataSet.Tables[0].Rows.Count; j++)
                {
                    concatedWord += (dataSet.Tables[0].Rows[j]["Name"] + " ");
                }
                dtr.Add(concatedWord);
                 cn.Close();
                adapter.Dispose();
            }

            return dtr;
        }

        private bool InsertUnConcatedWords(List<int> Ids, HashSet<string> concatedWords)
        {
            cn.ConnectionString = connectionString;
            cn.Open();


            var numbersAndWords = Ids.Zip(concatedWords, (n, w) => new { Number = n, Word = w });

            SqlCommand insert = new SqlCommand();
            insert.Connection = cn;

            insert.CommandType = System.Data.CommandType.Text;
            insert.CommandText = "INSERT INTO dbo.CacheAnagram (Id, WordId) VALUES(@FN, @LN)";

            insert.Parameters
                .Add(new SqlParameter("@FN", System.Data.SqlDbType.Int, 10000, "Id"));


            insert.Parameters
                .Add(new SqlParameter("@LN", System.Data.SqlDbType.Int, 10000, "WordId"));


            SqlDataAdapter adapter = new SqlDataAdapter("SELECT Id, WordId FROM dbo.CacheAnagram", cn);

            adapter.InsertCommand = insert;

            DataSet ds = new DataSet();
            adapter.Fill(ds, "dbo.CacheAnagram");

            //var pair in numbersAndWords
            foreach (var pair in numbersAndWords)
            {
                foreach (var ids in GetIdsOfWord(GetString(pair.Word)))
                {
                    DataRow newRow = ds.Tables[0].NewRow();
                    newRow["Id"] = pair.Number;
                    newRow["WordId"] = ids;
                    ds.Tables[0].Rows.Add(newRow);
                }
            }

            adapter.Update(ds.Tables[0]);
            cn.Close();
            adapter.Dispose();
            return true;
        }

        private List<int> GetIdsOfWord(List<string> list)
        {
            List<int> Ids = new List<int>();
            foreach (var name in list)
            {

                SqlDataAdapter adapter = new SqlDataAdapter();

                SqlCommand command = new SqlCommand("SELECT Id FROM dbo.Word " +
                                                      "WHERE Name = @FN", cn);

                command.Parameters.AddWithValue("@FN", name);

                adapter.SelectCommand = command;

                DataSet dataSet = new DataSet();

                adapter.Fill(dataSet, "dbo.Word");

                cn.Close();
                adapter.Dispose();

                for (var j = 0; j < dataSet.Tables[0].Rows.Count; j++)
                {
                    Ids.Add(Int32.Parse(dataSet.Tables[0].Rows[j]["Id"].ToString()));
                }
            }

            return Ids;
        }

        private List<string> GetString(string Name)
        {
            List<string> list = new List<string>();
            string newStr = "";
            foreach(var str in Name)
            {
                if(str == ' ')
                {
                    if(newStr.Length > 1)
                    {
                        list.Add(newStr);
                        newStr = "";
                        continue;
                    }
                    break;
                }
                newStr += str;
            }
            list.Add(newStr);
            return list;
        }
    }

    public class WordAndAnagram
    {
        public List<int> AnagramsId { get; set; }
        public HashSet<string> ListOfAanagrams { get; set;}
    }
}
