/**
* @file multilogin.cs
* @author Mukrime_Sagiroglu_152120191034
* @author Hazar_Namdar_152120191053
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;


namespace BoardGame
{
    public partial class multilogin : Form
    {
        public multilogin()
        {
            InitializeComponent();
        }

        private void multilogin_Load(object sender, EventArgs e)
        {

        }
        /**
         * @brief client kismi olusturuldu.
         */
        private void button1_Click(object sender, EventArgs e)//client
        {
            multiplayer newGame = new multiplayer(false, textBox1.Text);
            Visible = false;
            if (!newGame.IsDisposed)
                newGame.ShowDialog();
            Visible = true;
        }
        /**
         * @brief host kismi olusturuldu.
         */
        private void button2_Click(object sender, EventArgs e) //host
        {

            multiplayer newGame = new multiplayer(true);
            Visible = false;
            if (!newGame.IsDisposed)
                newGame.ShowDialog();
            Visible = true;
        }

      
    }
}
