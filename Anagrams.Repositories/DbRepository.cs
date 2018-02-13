using Anagrams.Interfaces;
using Anagrams.Interfaces.Models;
using Services.Helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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
        public List<TimeResultModel> ReturnIPSearches(string IP)
        {
            List<TimeResultModel> vw = new List<TimeResultModel>();

            var data = GetTimeAndStrings(IP);
            foreach (var str in data)
            {
                var newWord = new TimeResultModel();
                var result = GetCachedData(str.Key);

                if (result != null)
                {
                    foreach (var anagram in result)
                    {
                        newWord.Anagrams.Add(anagram);
                    }
                }
                newWord.Time = str.Value;
                vw.Add(newWord);
            }

            return vw;
        }


        public void InsertLogUser(long TIME, string ip, string query)
        {
            var sortedName = query.ToLower().GetWithoutWhiteSpace();
            var str = String.Concat(sortedName.OrderBy(c => c)); // SORTED AND LOWERED

            cn.ConnectionString = connectionString;

            cn.Open();

            SqlCommand insert = new SqlCommand();
            insert.Connection = cn;

            insert.CommandType = System.Data.CommandType.Text;
            insert.CommandText = "INSERT INTO dbo.LogIPUser (IP, Time, SortedWord) VALUES(@FN, @LN, @MN)";

            insert.Parameters
                .Add(new SqlParameter("@FN", System.Data.SqlDbType.NVarChar, 50, "IP"));

            insert.Parameters
                .Add(new SqlParameter("@LN", SqlDbType.Int, 10000, "Time"));

            insert.Parameters
                .Add(new SqlParameter("@MN", System.Data.SqlDbType.NVarChar, 50, "SortedWord"));


            SqlDataAdapter adapter = new SqlDataAdapter("SELECT IP, Time, SortedWord FROM dbo.LogIPUser", cn);

            adapter.InsertCommand = insert;

            DataSet ds = new DataSet();
            adapter.Fill(ds, "dbo.LogIPUser");

            DataRow newRow = ds.Tables[0].NewRow();
            newRow["IP"] = ip;
            newRow["SortedWord"] = str;
            newRow["Time"] = TIME;
            ds.Tables[0].Rows.Add(newRow);

            adapter.Update(ds.Tables[0]);
            cn.Close();
            adapter.Dispose();
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

        private List<int> GetIds(string sortedName)
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

            SqlCommand command = new SqlCommand("SELECT STUFF(( SELECT N', ' + dbo.Word.Name FROM dbo.CacheAnagram AS P2," +
                " dbo.Word WHERE P.Id = P2.Id" +
                "  AND Word.Id = P2.WordId " +
                " AND  P2.Id = p.Id " +
                    "FOR XML PATH(N'')), 1, 2, N'') AS result " +
                       " from (select DISTINCT dbo.CacheMaps.Id from  dbo.CacheMaps" +
                        " where dbo.CacheMaps.SortedWord = @FN) AS P " +
                        "GROUP BY P.Id", cn);

            command.Parameters.AddWithValue("@FN", str);

            adapter.SelectCommand = command;

            DataSet dataSet = new DataSet();

            adapter.Fill(dataSet, "dbo.CacheAnagram");

            cn.Close();
            adapter.Dispose();
            HashSet<string> result = new HashSet<string>();
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                for (var i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    result.Add(dataSet.Tables[0].Rows[i]["RESULT"].ToString());
                }

                return result;
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


                for (var j = 0; j < dataSet.Tables[0].Rows.Count; j++)
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
        private Dictionary<string, int> GetTimeAndStrings(string IP)
        {
            cn.ConnectionString = connectionString;
            cn.Open();

            SqlDataAdapter adapter = new SqlDataAdapter();

            SqlCommand command = new SqlCommand("SELECT TIME, SortedWord FROM dbo.LogIPUser " +
                                                    "WHERE IP = @FN", cn);

            command.Parameters.AddWithValue("@FN", IP);

            adapter.SelectCommand = command;

            DataSet dataSet = new DataSet();

            adapter.Fill(dataSet, "dbo.LogIPUser");

            cn.Close();
            adapter.Dispose();
            Dictionary<string, int> result = new Dictionary<string, int>();
            if (dataSet.Tables[0].Rows.Count > 0)
            {
                for (var i = 0; i < dataSet.Tables[0].Rows.Count; i++)
                {
                    result.Add(dataSet.Tables[0].Rows[i]["SortedWord"].ToString(),
                        Int32.Parse(dataSet.Tables[0].Rows[i]["Time"].ToString()));
                }

                return result;
            }
            return null;
        }

        private List<string> GetString(string Name)
        {
            List<string> list = new List<string>();
            string newStr = "";
            for (var i = 0; i < Name.Length; i++)
            {
                if (Name[i] == ' ')
                {
                    if (newStr.Length > 1 && i != Name.Length - 1)
                    {
                        list.Add(newStr);
                        newStr = "";
                        continue;
                    }
                    break;
                }
                newStr += Name[i];
            }
            list.Add(newStr);
            return list;
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
