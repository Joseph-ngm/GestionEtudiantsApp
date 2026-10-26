using System.ComponentModel;

namespace UI
{
    partial class FrmMatieres
    {
        private System.ComponentModel.IContainer components = null;

        private DataGridView dgvMatieres;
        private TextBox txtNomMatiere;
        private NumericUpDown numCoefficient;
        private TextBox txtRecherche;
        private Button btnAjouter;
        private Button btnModifier;
        private Button btnSupprimer;
        private Button btnNouveau;
        private Button btnRechercher;
        private Label lblNomMatiere;
        private Label lblCoefficient;
        private Label lblRecherche;
        private Label lblTotalMatieres;
        private GroupBox groupBox1;
        private GroupBox groupBox2;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            dgvMatieres = new DataGridView();
            txtNomMatiere = new TextBox();
            numCoefficient = new NumericUpDown();
            txtRecherche = new TextBox();
            btnAjouter = new Button();
            btnModifier = new Button();
            btnSupprimer = new Button();
            btnNouveau = new Button();
            btnRechercher = new Button();
            lblNomMatiere = new Label();
            lblCoefficient = new Label();
            lblRecherche = new Label();
            lblTotalMatieres = new Label();
            groupBox1 = new GroupBox();
            groupBox2 = new GroupBox();

            ((ISupportInitialize)dgvMatieres).BeginInit();
            ((ISupportInitialize)numCoefficient).BeginInit();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            SuspendLayout();

            // ==== DataGrid ====
            dgvMatieres.Location = new Point(20, 70);
            dgvMatieres.Size = new Size(620, 430);
            dgvMatieres.ReadOnly = true;
            dgvMatieres.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvMatieres.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvMatieres.SelectionChanged += dgvMatieres_SelectionChanged;

            // ==== Fields ====
            lblNomMatiere.Text = "Nom Matière:";
            lblNomMatiere.Location = new Point(20, 40);

            txtNomMatiere.Location = new Point(20, 60);
            txtNomMatiere.Width = 260;

            lblCoefficient.Text = "Coefficient:";
            lblCoefficient.Location = new Point(20, 100);

            numCoefficient.Location = new Point(20, 120);
            numCoefficient.Minimum = 1;
            numCoefficient.Maximum = 10;

            // ==== Buttons ====
            btnAjouter.Text = "➕ Ajouter";
            btnAjouter.Location = new Point(20, 170);
            btnAjouter.Click += btnAjouter_Click;

            btnModifier.Text = "✏️ Modifier";
            btnModifier.Location = new Point(20, 215);
            btnModifier.Click += btnModifier_Click;

            btnSupprimer.Text = "🗑 Supprimer";
            btnSupprimer.Location = new Point(20, 260);
            btnSupprimer.Click += btnSupprimer_Click;

            btnNouveau.Text = "🆕 Nouveau";
            btnNouveau.Location = new Point(20, 305);
            btnNouveau.Click += btnNouveau_Click;

            // ==== Search ====
            lblRecherche.Text = "Recherche:";
            lblRecherche.Location = new Point(20, 30);

            txtRecherche.Location = new Point(100, 27);
            txtRecherche.Width = 400;

            btnRechercher.Text = "🔍 Rechercher";
            btnRechercher.Location = new Point(510, 25);
            btnRechercher.Click += btnRechercher_Click;

            lblTotalMatieres.Text = "Total : 0 matière(s)";
            lblTotalMatieres.Location = new Point(20, 515);

            // ==== GroupBoxes ====
            groupBox1.Text = "Informations Matière";
            groupBox1.Location = new Point(12, 12);
            groupBox1.Size = new Size(300, 550);
            groupBox1.Controls.Add(lblNomMatiere);
            groupBox1.Controls.Add(txtNomMatiere);
            groupBox1.Controls.Add(lblCoefficient);
            groupBox1.Controls.Add(numCoefficient);
            groupBox1.Controls.Add(btnAjouter);
            groupBox1.Controls.Add(btnModifier);
            groupBox1.Controls.Add(btnSupprimer);
            groupBox1.Controls.Add(btnNouveau);

            groupBox2.Text = "Liste des Matières";
            groupBox2.Location = new Point(320, 12);
            groupBox2.Size = new Size(660, 550);
            groupBox2.Controls.Add(lblRecherche);
            groupBox2.Controls.Add(txtRecherche);
            groupBox2.Controls.Add(btnRechercher);
            groupBox2.Controls.Add(dgvMatieres);
            groupBox2.Controls.Add(lblTotalMatieres);

            // ==== Form ====
            ClientSize = new Size(1000, 600);
            Controls.Add(groupBox1);
            Controls.Add(groupBox2);
            Text = "Gestion des Matières";
            Load += FrmMatieres_Load;

            ResumeLayout(false);
        }
    }
}
