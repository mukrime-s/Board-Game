/**
* @file logInScreen.cs
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
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using System.Data.SqlClient;

namespace BoardGame
{
    public partial class logInScreen : Form
    {
        string outputFile;

        /**
         * @brief Baska formlarda kullanabilmek icin statik degisken tanimlandi.
         */
        public static string userName;

        public logInScreen()
        {
            InitializeComponent();

            outputFile = "../../../user.txt";//dosyayı okumak için
            FileStream fileStream = new FileStream(outputFile, FileMode.OpenOrCreate, FileAccess.Read);

            /**
             * @brief Son giris yapan kullanicinin hatirlanmasi saglandi.
             */
            using (StreamReader reader = new StreamReader(fileStream))
            {
                string satir = reader.ReadLine();
                if (satir != "")
                {
                    txtUsername.Text = satir;
                }
                reader.Close();
            }

            fileStream.Close();
        }

        /**
         * @brief Veri tabani icin gerekli degiskenler tanimlandi.
         */
        SqlConnection baglanti;
        SqlCommand komut;
        SqlDataReader dr;

        /**
         * @brief Parolanin gizlenmesi saglandi.
         */
        private void logInScreen_Load(object sender, EventArgs e)
        {
            txtPassword.UseSystemPasswordChar = true;
            baglanti = new SqlConnection("server=.; Initial Catalog=userinfo;Integrated Security=SSPI");
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            lblError.Text = "";
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            lblError.Text = "";
        }

        /**
         * @brief Butona basildiginda veri tabanina kayit yapar, kullanici adı ve parola dogruysa giris yapilir.
         */
        private void btn_log_in_Click(object sender, EventArgs e)
        {
            string ad = txtUsername.Text;
            string parola = txtPassword.Text;
            komut = new SqlCommand();
            baglanti.Open();
            komut.Connection = baglanti;
            komut.CommandText= "SELECT *FROM userinfo WHERE Username='"+ad+"' AND Passwords='" + parola + "'";
            dr = komut.ExecuteReader();
            if (dr.Read())
            {
                Form2 settingsScreen = new Form2();
                settingsScreen.Show();
                this.Hide();
            }
            else
            {
                lblError.Text = "Invalid username or password!";
            }
            baglanti.Close();

            /**
             * @brief Eger user veya admin ile giris yapilirsa bu dogru bir giristir.
             */
            if ((txtUsername.Text == "user" || txtUsername.Text == "USER") &&
                (txtPassword.Text == "user" || txtPassword.Text == "USER"))
            {
                userName = txtUsername.Text;
                Form2 settingsScreen = new Form2();
                settingsScreen.Show();
                this.Hide();
            }
            else if ((txtUsername.Text == "admin" || txtUsername.Text == "ADMIN") &&
                (txtPassword.Text == "admin" || txtPassword.Text == "ADMIN")) {
                
                userName = txtUsername.Text;

                Form2 settingsScreen = new Form2();
                settingsScreen.Show();
                this.Hide();
            }
            else {
                lblError.Text = "Invalid username or password!";
            }

            /**
             * @brief Son kullanici adi saklandi.
             */
            using (StreamWriter writer = new StreamWriter(outputFile))
            {
                writer.WriteLine(txtUsername.Text);
            }
        }

        /**
         * @brief Kullanici kayit olmamıssa register formu acilacak.
         */
        private void signup_Click(object sender, EventArgs e)
        {
            register rg = new register();
            rg.Show();
            this.Hide();
        }

        /**
         * @brief Kullanici adina sadece harf girilebilir.
         */
        private void txtUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsLetter(e.KeyChar) && !char.IsControl(e.KeyChar) && !char.IsSeparator(e.KeyChar);
        }
        
        /**
         * @brief Tiki olursa eger parola gozukur, yoksa gozukmez.
         */
        private void chkBoxPassword_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBoxPassword.Checked)
            {
                txtPassword.UseSystemPasswordChar = false;
            }
            else
            {
                txtPassword.UseSystemPasswordChar = true;
            }
        }
    }
}
