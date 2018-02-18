﻿using Anagrams.EFCF.Core;
using Anagrams.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Anagrams_Repositories
{
    public class DatabaseInit
    {
        private readonly IWordRepository<string> _repo;
        public HashSet<string> WordsList { get; private set; }
        private SqlConnection cn = new SqlConnection();
      
        private string connString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=Anagrams.EFCF.Core.ManagerContext;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public DatabaseInit(IWordRepository<string> repo)
        {
            _repo = repo;
            GetList();
        }


        public bool TransferToDataBase()
        {
            cn.ConnectionString = connString;

            cn.Open();

            SqlCommand insert = new SqlCommand();
            insert.Connection = cn;

            insert.CommandType = System.Data.CommandType.Text;
            insert.CommandText = "INSERT INTO WORDS (Name) VALUES(@FN)";

            insert.Parameters
                .Add(new SqlParameter("@FN", System.Data.SqlDbType.NVarChar, 255, "Name"));

            SqlDataAdapter adapter = new SqlDataAdapter("SELECT Id, Name FROM Words", cn);

            adapter.InsertCommand = insert;

            DataSet ds = new DataSet();
            adapter.Fill(ds, "Words");

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
        private bool GetList()
        {
            WordsList = _repo.GetData("");

            if (WordsList == null)
            {
                return false;
            }

            return true;
        }
    }
}
