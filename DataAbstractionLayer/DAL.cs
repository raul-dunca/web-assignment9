using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Asp_Lab.Models;
using MySql.Data.MySqlClient;

namespace Asp_Lab.DataAbstractionLayer
{
    public class DAL
    {
        public List<Document> GetAllDocuments(string type=null, string format=null)
        {
            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;

            myConnectionString = "server=localhost;uid=root;pwd=;database=db_documents;";
            List<Document> document_list = new List<Document>();

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;

                if (!string.IsNullOrEmpty(type) && !string.IsNullOrEmpty(format))
                {
                    cmd.CommandText = "select * from documents WHERE type LIKE '%' '"+type+ "' '%' AND format LIKE '%' '" + format + "' '%'";
                }
                else
                {
                    if (string.IsNullOrEmpty(type) && !string.IsNullOrEmpty(format))
                    {
                        cmd.CommandText = "select * from documents WHERE format LIKE '%' '" + format + "' '%'";
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(type) && string.IsNullOrEmpty(format))
                        {
                            cmd.CommandText = "select * from documents WHERE type LIKE '%' '" + type + "' '%'";
                        }
                        else
                        {
                            cmd.CommandText = "select * from documents";
                        }
                    }
                }

                
                MySqlDataReader myreader = cmd.ExecuteReader();

                while (myreader.Read())
                {
                    Document document = new Document();
                    document.Id = myreader.GetInt32("id");
                    document.Author = myreader.GetString("author");
                    document.Title = myreader.GetString("title");
                    document.NrOfPages = myreader.GetInt32("nr_of_pages");
                    document.Type = myreader.GetString("type");
                    document.Format = myreader.GetString("format");
                    document_list.Add(document);
                }
                myreader.Close();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.Write(ex.Message);
            }
            return document_list;
        }


        public void AddDocument(Document document)
        {
            


            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;

            myConnectionString = "server=localhost;uid=root;pwd=;database=db_documents;";
            
            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO documents (author, title, nr_of_pages, type, format) VALUES (@Author, @Title, @NrOfPages, @Type, @Format)";

                // Add parameters to the command
                cmd.Parameters.AddWithValue("@Author", document.Author);
                cmd.Parameters.AddWithValue("@Title", document.Title);
                cmd.Parameters.AddWithValue("@NrOfPages", document.NrOfPages);
                cmd.Parameters.AddWithValue("@Type", document.Type);
                cmd.Parameters.AddWithValue("@Format", document.Format);

                cmd.ExecuteNonQuery();

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.Write(ex.Message);
            }
            
        }



        public void UpdateDocument(int id,string new_author, string new_title, int new_nr_of_pages, string new_type, string new_format)
        {



            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;

            myConnectionString = "server=localhost;uid=root;pwd=;database=db_documents;";

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "UPDATE documents SET author=@Author, title=@Title, nr_of_pages=@NrOfPages, type=@Type, format=@Format WHERE id=@Id";

                // Add parameters to the command
                cmd.Parameters.AddWithValue("@Author", new_author);
                cmd.Parameters.AddWithValue("@Title", new_title);
                cmd.Parameters.AddWithValue("@NrOfPages", new_nr_of_pages);
                cmd.Parameters.AddWithValue("@Type", new_type);
                cmd.Parameters.AddWithValue("@Format", new_format);
                cmd.Parameters.AddWithValue("@Id", id);

                cmd.ExecuteNonQuery();

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.Write(ex.Message);
            }

        }

        public void DeleteDocument(int id)
        {



            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;

            myConnectionString = "server=localhost;uid=root;pwd=;database=db_documents;";

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "DELETE FROM documents WHERE id=@Id";

                cmd.Parameters.AddWithValue("@Id", id);

                cmd.ExecuteNonQuery();

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.Write(ex.Message);
            }

        }


        public bool Login(string username , string password)
        {



            MySql.Data.MySqlClient.MySqlConnection conn;
            string myConnectionString;

            myConnectionString = "server=localhost;uid=root;pwd=;database=db_users;";

            try
            {
                conn = new MySql.Data.MySqlClient.MySqlConnection();
                conn.ConnectionString = myConnectionString;
                conn.Open();

                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM users WHERE username=@Username AND password=@Pass";

                cmd.Parameters.AddWithValue("@Username", username);
                cmd.Parameters.AddWithValue("@Pass", password);

                MySqlDataReader myreader = cmd.ExecuteReader();
                if (!myreader.HasRows)
                {
                    return false;
                }
                
                
                return true;
                
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Console.Write(ex.Message);
                return false;
            }

        }
    }
}