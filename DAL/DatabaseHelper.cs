using System;
using System.Data;
using System.Data.SqlClient;

namespace GestionEtudiantsApp.DAL
{
    public class DatabaseHelper
    {

        private static string connectionString =
     @"Server=(localdb)\MSSQLLocalDB;Database=GestionEtudiantsDB;Trusted_Connection=True;";
        public static string ConnectionString
        {
            get { return connectionString; }
            set { connectionString = value; }
        }

        
        public static SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }

        
        // Tester la connexion à la base de données
        
        public static bool TestConnection()
        {
            try
            {
                using (SqlConnection conn = GetConnection())
                {
                    conn.Open();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        
        // Exécuter une requête scalaire
        public static object ExecuteScalar(string query, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);

                    conn.Open();
                    return cmd.ExecuteScalar();
                }
            }
        }

        // Exécuter une commande (INSERT, UPDATE, DELETE)
       
        public static int ExecuteNonQuery(string query, params SqlParameter[] parameters)
        {
            using (SqlConnection conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);

                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        public static DataTable ExecuteDataTable(string query, params SqlParameter[] parameters)
        {
            DataTable dt = new DataTable();

            using (SqlConnection conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null)
                        cmd.Parameters.AddRange(parameters);

                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        da.Fill(dt);
                    }
                }
            }

            return dt;
        }
    }
}