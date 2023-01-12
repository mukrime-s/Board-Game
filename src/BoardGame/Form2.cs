/**
* @file Form2.cs
* @author Mukrime_Sagiroglu_152120191034
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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        /**
         * @brief Settings butonuna basildiginda form3 formu acilacak.
         */
        private void settings_Click(object sender, EventArgs e)
        {
            Form3 f3 = new Form3();
            f3.Show();
            this.Hide();
        }

        /**
         * @brief Kullanici tipi admin olursa admin butonu gozukecek, profile butonu gorunmez olacak.
         */
        private void Form2_Load(object sender, EventArgs e)
        {
            if (logInScreen.userName == "admin")
            {
                btnAdmin.Visible = true;
                btnProfile.Visible = false;
            }
        }

        /**
         * @brief Butona basildiginda adminpanel formu acilacak.
         */
        private void btnAdmin_Click(object sender, EventArgs e)
        {
            adminpanel ap = new adminpanel();
            ap.Show();
            this.Hide();
        }

        /**
         * @brief Butona basildiginda UserProfile formu acilacak.
         */
        private void btnProfile_Click(object sender, EventArgs e)
        {
            UserProfile up = new UserProfile();
            up.Show();
            this.Hide();
        }

        /*
         * @brief Butona basildiginda about formu gözükecek ve o kapanana kadar baska yere basilamayacak.
         */
        private void btnAbout_Click(object sender, EventArgs e)
        {
            //about ab = new about();
            //ab.Show();
            //this.Hide();
            about frm = new about();
            if (frm.ShowDialog() == System.Windows.Forms.DialogResult.No)
                this.Close();
        }

        /**
         * @brief Butona basildiginda newGame formu acilacak.
         */
        private void btnGame_Click(object sender, EventArgs e)
        {
            newGame ng = new newGame();
            ng.Show();
            this.Hide();
        }

        /*
         * @brief Butona basildiginda about formu gözükecek ve o kapanana kadar baska yere basilamayacak.
         */
        private void btnHelp_Click(object sender, EventArgs e)
        {
            helpScreen h = new helpScreen();
            if (h.ShowDialog() == System.Windows.Forms.DialogResult.No)
                this.Close();
        }

        /**
         * @brief Butona basildiginda multilogin formu acilacak.
         */
        private void button1_Click(object sender, EventArgs e)
        {
            multilogin multi = new multilogin();
            multi.Show();
            this.Hide();
        }
    }
}
