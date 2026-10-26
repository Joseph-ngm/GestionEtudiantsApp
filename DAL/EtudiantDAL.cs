using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using GestionEtudiantsApp.Models;

namespace GestionEtudiantsApp.DAL
{
    public class EtudiantDAL
    {
         
        /// Ajouter un nouvel étudiant
        ///  
        public static int AjouterEtudiant(Etudiant etudiant)
        {
            string query = @"INSERT INTO Etudiants (Nom, Prenom, DateNaissance, Groupe) 
                           VALUES (@Nom, @Prenom, @DateNaissance, @Groupe);
                           SELECT CAST(SCOPE_IDENTITY() AS INT);";

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Nom", etudiant.Nom);
                    cmd.Parameters.AddWithValue("@Prenom", etudiant.Prenom);
                    cmd.Parameters.AddWithValue("@DateNaissance", etudiant.DateNaissance);
                    cmd.Parameters.AddWithValue("@Groupe", etudiant.Groupe ?? (object)DBNull.Value);

                    conn.Open();
                    return (int)cmd.ExecuteScalar();
                }
            }
        }

         
        /// Modifier un étudiant existant
        ///  
        public static bool ModifierEtudiant(Etudiant etudiant)
        {
            string query = @"UPDATE Etudiants 
                           SET Nom = @Nom, 
                               Prenom = @Prenom, 
                               DateNaissance = @DateNaissance, 
                               Groupe = @Groupe
                           WHERE IdEtudiant = @IdEtudiant";

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IdEtudiant", etudiant.IdEtudiant);
                    cmd.Parameters.AddWithValue("@Nom", etudiant.Nom);
                    cmd.Parameters.AddWithValue("@Prenom", etudiant.Prenom);
                    cmd.Parameters.AddWithValue("@DateNaissance", etudiant.DateNaissance);
                    cmd.Parameters.AddWithValue("@Groupe", etudiant.Groupe ?? (object)DBNull.Value);

                    conn.Open();
                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

         
        /// Supprimer un étudiant
        ///  
        public static bool SupprimerEtudiant(int idEtudiant)
        {
            // Vérifier d'abord s'il y a des notes associées
            string checkQuery = "SELECT COUNT(*) FROM Notes WHERE IdEtudiant = @IdEtudiant";
            int noteCount = (int)DatabaseHelper.ExecuteScalar(checkQuery,
                new SqlParameter("@IdEtudiant", idEtudiant));

            if (noteCount > 0)
            {
                throw new InvalidOperationException(
                    "Impossible de supprimer cet étudiant. Il possède des notes enregistrées.");
            }

            string query = "DELETE FROM Etudiants WHERE IdEtudiant = @IdEtudiant";
            return DatabaseHelper.ExecuteNonQuery(query,
                new SqlParameter("@IdEtudiant", idEtudiant)) > 0;
        }

         
        /// Obtenir tous les étudiants
        ///  
        public static List<Etudiant> ObtenirTousLesEtudiants()
        {
            List<Etudiant> liste = new List<Etudiant>();
            string query = "SELECT * FROM Etudiants ORDER BY Nom, Prenom";

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    conn.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            liste.Add(new Etudiant
                            {
                                IdEtudiant = (int)reader["IdEtudiant"],
                                Nom = reader["Nom"].ToString(),
                                Prenom = reader["Prenom"].ToString(),
                                DateNaissance = (DateTime)reader["DateNaissance"],
                                Groupe = reader["Groupe"] != DBNull.Value ?
                                         reader["Groupe"].ToString() : null
                            });
                        }
                    }
                }
            }
            return liste;
        }

         
        /// Obtenir un étudiant par ID
        ///  
        public static Etudiant ObtenirEtudiantParId(int idEtudiant)
        {
            string query = "SELECT * FROM Etudiants WHERE IdEtudiant = @IdEtudiant";

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@IdEtudiant", idEtudiant);
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return new Etudiant
                            {
                                IdEtudiant = (int)reader["IdEtudiant"],
                                Nom = reader["Nom"].ToString(),
                                Prenom = reader["Prenom"].ToString(),
                                DateNaissance = (DateTime)reader["DateNaissance"],
                                Groupe = reader["Groupe"] != DBNull.Value ?
                                         reader["Groupe"].ToString() : null
                            };
                        }
                    }
                }
            }
            return null;
        }

         
        /// Rechercher des étudiants par nom ou prénom
        ///  
        public static List<Etudiant> RechercherEtudiants(string recherche)
        {
            List<Etudiant> liste = new List<Etudiant>();
            string query = @"SELECT * FROM Etudiants 
                           WHERE Nom LIKE @Recherche OR Prenom LIKE @Recherche
                           ORDER BY Nom, Prenom";

            using (SqlConnection conn = DatabaseHelper.GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@Recherche", "%" + recherche + "%");
                    conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            liste.Add(new Etudiant
                            {
                                IdEtudiant = (int)reader["IdEtudiant"],
                                Nom = reader["Nom"].ToString(),
                                Prenom = reader["Prenom"].ToString(),
                                DateNaissance = (DateTime)reader["DateNaissance"],
                                Groupe = reader["Groupe"] != DBNull.Value ?
                                         reader["Groupe"].ToString() : null
                            });
                        }
                    }
                }
            }
            return liste;
        }
    }
}