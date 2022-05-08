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
    public partial class GameForm : Form
    {
        public PanelPolje[ , ] arrPoljaPanela = new PanelPolje[3,3];
        public string[,] arrPoljaZnakova = new string[3, 3];
        private Label lblTrenutniIgrac = new Label();
        private Label lblVreme = new Label();
        private Label lblRezultat = new Label();
        public Igrac Igrac1;
        public Igrac Igrac2;
        public Timer Vreme;
        public Igrac TrenutniIgrac = null;
        public int Sekunde;
        public int Sekunde2;
        public int BrRundi;
        public int skor1;
        public int skor2;
        public Panel panelTabla = new Panel();
        public Button btnSlRunda = new Button();
        public GameForm(Igrac igrac1, Igrac igrac2, int vreme, int brRundi)
        {
            ClientSize = new System.Drawing.Size(600, 600);

            Igrac1 = igrac1;
            Igrac2 = igrac2;

            TrenutniIgrac = Igrac1;
            Sekunde = vreme;
            Sekunde2 = vreme;
            BrRundi = brRundi;

            Vreme = new Timer();
            Vreme.Interval = 1000;
            Vreme.Tick += Vreme_Tick;
            Vreme.Start();

            panelTabla.Size = new System.Drawing.Size(300, 300);
            panelTabla.BackColor = System.Drawing.Color.Blue;
            panelTabla.Location = new System.Drawing.Point(50, 50);
            Controls.Add(panelTabla);

            lblTrenutniIgrac.Location = new System.Drawing.Point(20, 20);
            lblTrenutniIgrac.Text = TrenutniIgrac.Ime;
            lblTrenutniIgrac.AutoSize = true;
            Controls.Add(lblTrenutniIgrac);

            lblVreme.Location = new System.Drawing.Point(480, 20);
            lblVreme.AutoSize = true;
            Controls.Add(lblVreme);

            lblRezultat.Location = new System.Drawing.Point(300, 20);
            lblRezultat.AutoSize = true;
            Controls.Add(lblRezultat);

            btnSlRunda.Text = "Sledeca runda";
            btnSlRunda.Enabled = false;
            btnSlRunda.Location = new Point(0, 0);
            btnSlRunda.Click += Restart;
            Controls.Add(btnSlRunda);

            GenerisiTablu();
            InitializeComponent();
        }

        private void Vreme_Tick(object sender, EventArgs e)
        {
            if(Sekunde == 0)
            {
                brPokusaja = 0;
                lblRezultat.Text = "isteklo vreme igracu: " + TrenutniIgrac.Ime;
                btnSlRunda.Enabled = true;
                panelTabla.Enabled = false;
                Vreme.Stop();
                Sekunde = Sekunde2;
            }
            lblVreme.Text = Sekunde--.ToString();
            
        }

        private void GenerisiNiz()
        {
            
        }

        private void GenerisiTablu()
        {
            int x = 0;
            int y = 0;
            for (int i = 1; i <= 9; i++)
            {
                
                PanelPolje p = new PanelPolje(i.ToString());
                p.Click += panelPolje_Click;
                arrPoljaPanela[(i-1)/3,(i-1)%3] = p;
                arrPoljaZnakova[(i - 1) / 3, (i - 1) % 3] = "";
                p.Location = new System.Drawing.Point(x,y);

                panelTabla.Controls.Add(p);
                x += 100;
                if(i % 3 == 0)
                {
                    x = 0;
                    y += 100;
                }
                
            }
            
        }
        
        int brPokusaja = 0;
        
        private void panelPolje_Click(object sender, EventArgs e)
        {
            Sekunde = Sekunde2;
            (sender as PanelPolje).Enabled = false;
            (sender as PanelPolje).lblZnak.Text = TrenutniIgrac.Znak;
            (sender as PanelPolje).Znak = TrenutniIgrac.Znak;
            for (int i = 0; i < arrPoljaPanela.GetLength(0); i++)
            {
                for (int j = 0; j < arrPoljaPanela.GetLength(1); j++)
                {
                    if(arrPoljaPanela[i,j] == (sender as PanelPolje))
                    {
                        arrPoljaZnakova[i, j] = TrenutniIgrac.Znak;
                    }
                }
            }
            if (Pobeda(sender as PanelPolje))
            {
                if (TrenutniIgrac == Igrac1)
                    skor1++;
                else if(TrenutniIgrac==Igrac2)
                    skor2++;    
                    
                panelTabla.Enabled = false;
                lblRezultat.Text = "pobedio je " + TrenutniIgrac.Ime;
                btnSlRunda.Enabled = true;
                Vreme.Stop();
            }
            else if(brPokusaja == 9)
            {
                panelTabla.Enabled = false;
                lblRezultat.Text = "nereseno";
                btnSlRunda.Enabled= true;
                
            }
            if (TrenutniIgrac == Igrac1)
            {
                TrenutniIgrac = Igrac2;
            }
            else if (TrenutniIgrac == Igrac2)
            {
                TrenutniIgrac = Igrac1;
            }
            lblTrenutniIgrac.Text = TrenutniIgrac.Ime;
            
        }
        private void Restart(Object sender, EventArgs e)
        {
            BrRundi--;
            if(BrRundi == 0)
            {
                if(skor1>skor2)
                    MessageBox.Show("gotovo, pobedio je:" + Igrac1.Ime);
                if (skor1 < skor2)
                    MessageBox.Show("gotovo, pobedio je:" + Igrac2.Ime);
                if (skor1 == skor2)
                {
                    MessageBox.Show("NERESENO!");
                }
            }    
            brPokusaja = 0;
            lblRezultat.Text = "";
            Vreme.Start();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    arrPoljaZnakova[i, j] = "";
                }
            }
            panelTabla.Controls.Clear();
            panelTabla.Enabled = true;
            GenerisiTablu();
            (sender as Button).Enabled = false;
        }
        private bool Pobeda(PanelPolje p)
        {
            brPokusaja++;
            if (arrPoljaZnakova[0, 0] == p.Znak && arrPoljaZnakova[0, 1] == p.Znak && arrPoljaZnakova[0, 2] == p.Znak) { return true; }
            if (arrPoljaZnakova[1, 0] == p.Znak && arrPoljaZnakova[1, 1] == p.Znak && arrPoljaZnakova[1, 2] == p.Znak) { return true; }
            if (arrPoljaZnakova[2, 0] == p.Znak && arrPoljaZnakova[2, 1] == p.Znak && arrPoljaZnakova[2, 2] == p.Znak) { return true; }

            // check columns
            if (arrPoljaZnakova[0, 0] == p.Znak && arrPoljaZnakova[1, 0] == p.Znak && arrPoljaZnakova[2, 0] == p.Znak) { return true; }
            if (arrPoljaZnakova[0, 1] == p.Znak && arrPoljaZnakova[1, 1] == p.Znak && arrPoljaZnakova[2, 1] == p.Znak) { return true; }
            if (arrPoljaZnakova[0, 2] == p.Znak && arrPoljaZnakova[1, 2] == p.Znak && arrPoljaZnakova[2, 2] == p.Znak) { return true; }

            // check diags
            if (arrPoljaZnakova[0, 0] == p.Znak && arrPoljaZnakova[1, 1] == p.Znak && arrPoljaZnakova[2, 2] == p.Znak) { return true; }
            if (arrPoljaZnakova[0, 2] == p.Znak && arrPoljaZnakova[1, 1] == p.Znak && arrPoljaZnakova[2, 0] == p.Znak) { return true; }
            return false;
        }

        private void GameForm_Load(object sender, EventArgs e)
        {

        }
    }
}
