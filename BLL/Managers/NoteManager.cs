using GestionEtudiantsApp.DAL;
using GestionEtudiantsApp.Models;
using System.Collections.Generic;

namespace GestionEtudiantsApp.BLL.Managers
{
    public static class NoteManager
    {
        public static List<Note> ObtenirToutes()
        {
            return NoteDAL.ObtenirToutesLesNotes();
        }

        public static List<Note> Rechercher(string motCle)
        {
            return NoteDAL.RechercherNotes(motCle);
        }

        public static void Ajouter(Note n)
        {
            NoteDAL.AjouterNote(n);
        }

        public static void Modifier(Note n)
        {
            NoteDAL.ModifierNote(n);
        }

        public static void Supprimer(int id)
        {
            NoteDAL.SupprimerNote(id);
        }
    }
}
