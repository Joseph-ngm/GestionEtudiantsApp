namespace UI
{
    partial class FrmMenu
    {
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MenuStrip menuStrip1;

        private ToolStripMenuItem menuEtudiants;
        private ToolStripMenuItem menuMatieres;
        private ToolStripMenuItem menuNotes;
        private ToolStripMenuItem menuClassement;
        private ToolStripMenuItem menuBulletin;
        private ToolStripMenuItem menuQuitter;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            menuStrip1 = new MenuStrip();

            menuEtudiants = new ToolStripMenuItem();
            menuMatieres = new ToolStripMenuItem();
            menuNotes = new ToolStripMenuItem();
            menuClassement = new ToolStripMenuItem();
            menuBulletin = new ToolStripMenuItem();
            menuQuitter = new ToolStripMenuItem();

           
            menuStrip1.Items.AddRange(new ToolStripItem[]
            {
                menuEtudiants,
                menuMatieres,
                menuNotes,
                menuClassement,
                menuBulletin,
                menuQuitter
            });

            menuStrip1.Location = new System.Drawing.Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new System.Drawing.Size(1200, 24);


            menuEtudiants.Text = "Étudiants";
            menuEtudiants.Click += menuEtudiants_Click;

            menuMatieres.Text = "Matières";
            menuMatieres.Click += menuMatieres_Click;

            menuNotes.Text = "Notes";
            menuNotes.Click += menuNotes_Click;

            menuClassement.Text = "Classement";
            menuClassement.Click += menuClassement_Click;

            menuBulletin.Text = "Bulletin";
            menuBulletin.Click += menuBulletin_Click;

            menuQuitter.Text = "Quitter";
            menuQuitter.Click += menuQuitter_Click;


            this.ClientSize = new System.Drawing.Size(1200, 800);
            this.Controls.Add(menuStrip1);
            this.MainMenuStrip = menuStrip1;
            this.Name = "FrmMenu";
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Menu Principal – Gestion Étudiants";
        }
    }
}
