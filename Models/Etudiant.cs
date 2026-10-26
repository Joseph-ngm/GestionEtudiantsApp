using System;

namespace GestionEtudiantsApp.Models
{
    public class Etudiant
    {
        public int IdEtudiant { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public DateTime DateNaissance { get; set; }
        public string Groupe { get; set; }
        public string NomComplet => $"{Prenom} {Nom}";
    }
}