/**
* @file helpScreen.cs
* @author Hazar_Namdar_152120191053
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace BoardGame
{
    public partial class helpScreen : Form
    {
        public helpScreen()
        {
            InitializeComponent();
        }

        /**
         * @brief Butona basildiginda about formu acilir ve onun disinda herhangi bir yere basmaya izin verilmez.
         */
        private void btnAbout_Click(object sender, EventArgs e)
        {
            about ab = new about();
            
            if (ab.ShowDialog() == System.Windows.Forms.DialogResult.No)
                this.Close();
        }

        private void helpScreen_Load(object sender, EventArgs e)
        {

        }
    }
}
