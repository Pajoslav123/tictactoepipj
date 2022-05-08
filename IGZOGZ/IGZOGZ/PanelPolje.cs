using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IGZOGZ
{
    public class PanelPolje : Panel
    {
        public Label lblZnak = new Label();
        public string Znak;

        

        public PanelPolje(string z)
        {
            Size = new System.Drawing.Size(100, 100);
            BackColor = System.Drawing.Color.Purple;
            Znak = z;

            lblZnak.Text = Znak;
            lblZnak.Font = new Font("Arial", 24, FontStyle.Bold);
            lblZnak.AutoSize = true;
            lblZnak.Location = new Point(10, 10);
            this.Controls.Add(lblZnak);
        }

    }
}
