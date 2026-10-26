using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GestionEtudiantsApp.BLL.Managers;
using GestionEtudiantsApp.DAL;
using GestionEtudiantsApp.Models;

namespace UI
{
    public partial class FrmNotes : Form
    {
        private int idNoteSelectionnee = 0;

        public FrmNotes()
        {
            InitializeComponent();
        }

        private void FrmNotes_Load(object sender, EventArgs e)
        {
            ChargerEtudiants();
            ChargerMatieres();
            ChargerNotes();
        }

        private void ChargerEtudiants()
        {
            cmbEtudiant.DataSource = EtudiantDAL.ObtenirTousLesEtudiants();
            cmbEtudiant.DisplayMember = "NomComplet";
            cmbEtudiant.ValueMember = "IdEtudiant";
        }

        private void ChargerMatieres()
        {
            cmbMatiere.DataSource = MatiereManager.ObtenirToutes();
            cmbMatiere.DisplayMember = "NomMatiere";
            cmbMatiere.ValueMember = "IdMatiere";
        }

        private void ChargerNotes()
        {
            var notes = NoteManager.ObtenirToutes();
            dgvNotes.DataSource = notes;

            if (dgvNotes.Columns["IdNote"] != null)
                dgvNotes.Columns["IdNote"].Visible = false;

            lblTotal.Text = $"Total : {notes.Count} note(s)";
        }

        private void btnAjouter_Click(object sender, EventArgs e)
        {
            decimal valeur;
            if (!decimal.TryParse(txtNote.Text, out valeur) || valeur < 0 || valeur > 20)
            {
                MessageBox.Show("La note doit être entre 0 et 20.");
                return;
            }

            var n = new Note
            {
                IdEtudiant = (int)cmbEtudiant.SelectedValue,
                IdMatiere = (int)cmbMatiere.SelectedValue,
                Valeur = valeur
            };

            NoteManager.Ajouter(n);
            ChargerNotes();
        }

        private void btnModifier_Click(object sender, EventArgs e)
        {
            if (idNoteSelectionnee == 0)
            {
                MessageBox.Show("Sélectionnez une note.");
                return;
            }

            decimal valeur;
            if (!decimal.TryParse(txtNote.Text, out valeur) || valeur < 0 || valeur > 20)
            {
                MessageBox.Show("La note doit être entre 0 et 20.");
                return;
            }

            var n = new Note
            {
                IdNote = idNoteSelectionnee,
                Valeur = valeur
            };

            NoteManager.Modifier(n);
            ChargerNotes();
        }

        private void btnSupprimer_Click(object sender, EventArgs e)
        {
            if (idNoteSelectionnee == 0)
            {
                MessageBox.Show("Sélectionnez une note.");
                return;
            }

            NoteManager.Supprimer(idNoteSelectionnee);
            ChargerNotes();
        }

        private void dgvNotes_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvNotes.CurrentRow == null) return;

            idNoteSelectionnee = (int)dgvNotes.CurrentRow.Cells["IdNote"].Value;
            txtNote.Text = dgvNotes.CurrentRow.Cells["Valeur"].Value.ToString();
        }

        private void btnRechercher_Click(object sender, EventArgs e)
        {
            var notes = NoteManager.Rechercher(txtRecherche.Text.Trim());
            dgvNotes.DataSource = notes;
            lblTotal.Text = $"Résultats : {notes.Count} note(s)";
        }
    }
}
