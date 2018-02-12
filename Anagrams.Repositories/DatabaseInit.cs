using Anagrams.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Anagrams.Repositories
{
    public class DatabaseInit
    {
        private readonly IWordRepository<string> _repo;
        public HashSet<string> WordsList { get; private set; }
        // without set in not allowed to attech

        SqlConnection cn = new SqlConnection();
        private string connectionString = @"Data Source=(localdb)\MSSQLLocalDB
                                    ;Initial Catalog=ConnectionDb2018;Integrated Security=True;
                                            Connect Timeout=30;Encrypt=False;
                                             TrustServerCertificate=True;
                                                    ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public DatabaseInit(IWordRepository<string> repo)
        {
            _repo = repo;
            GetList();
        }

        private bool GetList()
        {
            WordsList = _repo.GetData("");

            if (WordsList == null)
            {
                return false;
            }

            return true;
        }

        public bool TransferToDataBase()
        {
            cn.ConnectionString = connectionString;

            cn.Open();

            SqlCommand insert = new SqlCommand();
            insert.Connection = cn;

            insert.CommandType = System.Data.CommandType.Text;
            insert.CommandText = "INSERT INTO dbo.WORD (Name) VALUES(@FN)";

            insert.Parameters
                .Add(new SqlParameter("@FN", System.Data.SqlDbType.NVarChar, 255, "Name"));

            SqlDataAdapter adapter = new SqlDataAdapter("SELECT Id, Name FROM dbo.Word", cn);

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

            return true;
        }
    }
}
