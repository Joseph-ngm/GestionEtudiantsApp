namespace UI
{
    partial class FrmNotes
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            dgvNotes = new System.Windows.Forms.DataGridView();
            cmbEtudiant = new System.Windows.Forms.ComboBox();
            cmbMatiere = new System.Windows.Forms.ComboBox();
            txtNote = new System.Windows.Forms.TextBox();
            txtRecherche = new System.Windows.Forms.TextBox();

            btnAjouter = new System.Windows.Forms.Button();
            btnModifier = new System.Windows.Forms.Button();
            btnSupprimer = new System.Windows.Forms.Button();
            btnNouveau = new System.Windows.Forms.Button();
            btnRechercher = new System.Windows.Forms.Button();

            lblEtudiant = new System.Windows.Forms.Label();
            lblMatiere = new System.Windows.Forms.Label();
            lblNote = new System.Windows.Forms.Label();
            lblRecherche = new System.Windows.Forms.Label();
            lblTotal = new System.Windows.Forms.Label();

            groupBox1 = new System.Windows.Forms.GroupBox();
            groupBox2 = new System.Windows.Forms.GroupBox();


            // DATAGRIDVIEW NOTES

            dgvNotes.AllowUserToAddRows = false;
            dgvNotes.AllowUserToDeleteRows = false;
            dgvNotes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dgvNotes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            dgvNotes.ReadOnly = true;
            dgvNotes.Location = new System.Drawing.Point(20, 65);
            dgvNotes.Name = "dgvNotes";
            dgvNotes.Size = new System.Drawing.Size(620, 440);
            dgvNotes.SelectionChanged += dgvNotes_SelectionChanged;


            // COMBOBOX ETUDIANT

            lblEtudiant.AutoSize = true;
            lblEtudiant.Location = new System.Drawing.Point(20, 40);
            lblEtudiant.Text = "Étudiant :";

            cmbEtudiant.Location = new System.Drawing.Point(20, 60);
            cmbEtudiant.Size = new System.Drawing.Size(260, 23);


            // COMBOBOX MATIÈRE

            lblMatiere.AutoSize = true;
            lblMatiere.Location = new System.Drawing.Point(20, 100);
            lblMatiere.Text = "Matière :";

            cmbMatiere.Location = new System.Drawing.Point(20, 120);
            cmbMatiere.Size = new System.Drawing.Size(260, 23);


            // NOTE (textbox)

            lblNote.AutoSize = true;
            lblNote.Location = new System.Drawing.Point(20, 160);
            lblNote.Text = "Note (0 à 20) :";

            txtNote.Location = new System.Drawing.Point(20, 180);
            txtNote.Size = new System.Drawing.Size(260, 23);


            // BOUTONS CRUD

            btnAjouter.BackColor = System.Drawing.Color.LightGreen;
            btnAjouter.Location = new System.Drawing.Point(20, 230);
            btnAjouter.Size = new System.Drawing.Size(260, 40);
            btnAjouter.Text = "➕ Ajouter";
            btnAjouter.Click += btnAjouter_Click;

            btnModifier.BackColor = System.Drawing.Color.LightBlue;
            btnModifier.Location = new System.Drawing.Point(20, 280);
            btnModifier.Size = new System.Drawing.Size(260, 40);
            btnModifier.Text = "✏️ Modifier";
            btnModifier.Click += btnModifier_Click;

            btnSupprimer.BackColor = System.Drawing.Color.LightCoral;
            btnSupprimer.Location = new System.Drawing.Point(20, 330);
            btnSupprimer.Size = new System.Drawing.Size(260, 40);
            btnSupprimer.Text = "🗑️ Supprimer";
            btnSupprimer.Click += btnSupprimer_Click;

            btnNouveau.Location = new System.Drawing.Point(20, 380);
            btnNouveau.Size = new System.Drawing.Size(260, 40);
            btnNouveau.Text = "🆕 Nouveau";
            //btnNouveau.Click += btnNouveau_Click;


            // RECHERCHE

            lblRecherche.AutoSize = true;
            lblRecherche.Location = new System.Drawing.Point(20, 30);
            lblRecherche.Text = "Recherche :";

            txtRecherche.Location = new System.Drawing.Point(100, 27);
            txtRecherche.Size = new System.Drawing.Size(400, 23);

            btnRechercher.Location = new System.Drawing.Point(510, 25);
            btnRechercher.Size = new System.Drawing.Size(130, 25);
            btnRechercher.Text = "🔍 Rechercher";
            btnRechercher.Click += btnRechercher_Click;


            // TOTAL

            lblTotal.AutoSize = true;
            lblTotal.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            lblTotal.Location = new System.Drawing.Point(20, 515);
            lblTotal.Text = "Total : 0 note(s)";


            // GROUPBOX GAUCHE (FORM)

            groupBox1.Controls.Add(lblEtudiant);
            groupBox1.Controls.Add(cmbEtudiant);
            groupBox1.Controls.Add(lblMatiere);
            groupBox1.Controls.Add(cmbMatiere);
            groupBox1.Controls.Add(lblNote);
            groupBox1.Controls.Add(txtNote);
            groupBox1.Controls.Add(btnAjouter);
            groupBox1.Controls.Add(btnModifier);
            groupBox1.Controls.Add(btnSupprimer);
            groupBox1.Controls.Add(btnNouveau);

            groupBox1.Text = "Saisie / Modification";
            groupBox1.Location = new System.Drawing.Point(12, 12);
            groupBox1.Size = new System.Drawing.Size(300, 550);


            // GROUPBOX DROITE (TABLE)

            groupBox2.Controls.Add(lblRecherche);
            groupBox2.Controls.Add(txtRecherche);
            groupBox2.Controls.Add(btnRechercher);
            groupBox2.Controls.Add(dgvNotes);
            groupBox2.Controls.Add(lblTotal);

            groupBox2.Text = "Liste des Notes";
            groupBox2.Location = new System.Drawing.Point(320, 12);
            groupBox2.Size = new System.Drawing.Size(660, 550);


            // FORM PRINCIPAL

            this.ClientSize = new System.Drawing.Size(1000, 600);
            this.Controls.Add(groupBox1);
            this.Controls.Add(groupBox2);
            this.Name = "FrmNotes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Gestion des Notes";
            this.Load += FrmNotes_Load;
        }

        #endregion

        private System.Windows.Forms.DataGridView dgvNotes;
        private System.Windows.Forms.ComboBox cmbEtudiant;
        private System.Windows.Forms.ComboBox cmbMatiere;
        private System.Windows.Forms.TextBox txtNote;

        private System.Windows.Forms.TextBox txtRecherche;
        private System.Windows.Forms.Button btnAjouter;
        private System.Windows.Forms.Button btnModifier;
        private System.Windows.Forms.Button btnSupprimer;
        private System.Windows.Forms.Button btnNouveau;
        private System.Windows.Forms.Button btnRechercher;

        private System.Windows.Forms.Label lblEtudiant;
        private System.Windows.Forms.Label lblMatiere;
        private System.Windows.Forms.Label lblNote;
        private System.Windows.Forms.Label lblRecherche;
        private System.Windows.Forms.Label lblTotal;

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
    }
}
