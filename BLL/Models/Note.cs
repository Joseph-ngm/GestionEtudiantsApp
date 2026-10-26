namespace GestionEtudiantsApp.BLL.Models
{
    public class Note
    {
        public int IdNote { get; set; }
        public int IdEtudiant { get; set; }
        public int IdMatiere { get; set; }
        public decimal Valeur { get; set; }
        public string Session { get; set; }

        // Champs liés pour affichage
        public string NomEtudiant { get; set; }
        public string NomMatiere { get; set; }
    }
}