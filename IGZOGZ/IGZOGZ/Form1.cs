using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGZOGZ
{
    public partial class Form1 : Form
    {
        private Label lblIgrac1 = new Label();
        private Label lblIgrac2 = new Label();
        private Label lblVreme = new Label();
        private Label lblRundi = new Label();
        private NumericUpDown numRundi = new NumericUpDown();
        private NumericUpDown numVreme = new NumericUpDown();
        private TextBox txtIgrac1 = new TextBox();
        private TextBox txtIgrac2 = new TextBox();
        private Button btnPokreni = new Button();

        public Form1()
        {
            ClientSize = new System.Drawing.Size(800, 600);

            lblIgrac1.Location = new System.Drawing.Point(50, 50);
            lblIgrac1.Text = "Igrac 1: ";
            lblIgrac1.AutoSize = true;
            Controls.Add(lblIgrac1);

            lblIgrac2.Location = new Point(50, 90);
            lblIgrac2.Text = "Igrac 2: ";
            lblIgrac2.AutoSize = true;
            Controls.Add(lblIgrac2);

            txtIgrac1.Location = new System.Drawing.Point(100, 50);
            Controls.Add(txtIgrac1);

            txtIgrac2.Location = new Point(100, 90);
            Controls.Add(txtIgrac2);

            lblVreme.Location = new Point(50, 140);
            lblVreme.Text = "Vreme po rundi: ";
            lblVreme.AutoSize = true;
            Controls.Add(lblVreme);

            lblRundi.Location = new Point(50, 190);
            lblRundi.Text = "Broj rundi: ";
            lblRundi.AutoSize = true;
            Controls.Add(lblRundi);

            numRundi.Location = new System.Drawing.Point(150,190);
            Controls.Add(numRundi);

            numVreme.Location = new System.Drawing.Point(150, 140);
            Controls.Add(numVreme);

            btnPokreni.Location = new System.Drawing.Point(50, 250);
            btnPokreni.Text = "POKRENI";
            btnPokreni.Click += btnPokreni_Click;
            Controls.Add(btnPokreni);

            InitializeComponent();
        }

        private bool Validacija()
        {
            return txtIgrac1.Text != String.Empty && txtIgrac2.Text != String.Empty && (int)numVreme.Value > 0 && (int)numRundi.Value > 0;
        }
        
        private void btnPokreni_Click(object sender,EventArgs e)
        {
            if (Validacija())
            {

                GameForm gf = new GameForm(new Igrac(txtIgrac1.Text, "X"), new Igrac(txtIgrac2.Text, "O"),(int)numVreme.Value,(int)numRundi.Value);
                gf.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Niste uneli sve informacije!");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
