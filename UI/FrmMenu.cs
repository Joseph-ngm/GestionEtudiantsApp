using System;
using System.Linq;
using System.Windows.Forms;

namespace UI
{
    public partial class FrmMenu : Form
    {
        public FrmMenu()
        {
            InitializeComponent();
            IsMdiContainer = true;  // Permet d'afficher les autres formulaires à l’intérieur
        }

        // ✨ Ouvre un formulaire MDI sans duplication
        private void OpenChildForm(Form frm)
        {
            // Vérifie si déjà ouvert
            var opened = MdiChildren.FirstOrDefault(f => f.GetType() == frm.GetType());

            if (opened != null)
            {
                opened.Activate();
                return;
            }

            // Sinon → l’ouvrir
            frm.MdiParent = this;
            frm.WindowState = FormWindowState.Maximized;
            frm.Show();
        }

        private void menuEtudiants_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmEtudiants());
        }

        private void menuMatieres_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmMatieres());
        }

        private void menuNotes_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmNotes());
        }

        private void menuClassement_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmClassement());
        }

        private void menuBulletin_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FrmBulletin());
        }

        private void menuQuitter_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Quitter", "Sortie", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes) {
                Application.Exit(); }
        }
    }
}
