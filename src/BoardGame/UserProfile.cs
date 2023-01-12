/**
* @file UserProfile.cs
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
using System.Xml;
using System.Xml.Linq;
using System.Linq;
using System.IO;
using System.Security.Cryptography;
using System.Data.SqlClient;

namespace BoardGame
{
    public partial class UserProfile : Form
    {
        string outputFile;

        static public string userName;

        public UserProfile()
        {
            InitializeComponent();

            /**
             * @brief Bir sonraki calistirmada, kaydedilen ayarlarin sistem tarafindan hatirlanması icin bir fileStream olusturuldu.
             */
            outputFile = "../../../user.txt";
            FileStream fileStream = new FileStream(outputFile, FileMode.OpenOrCreate, FileAccess.Read);
            
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
         * @brief Veri tabanini kullanabilmek icin degiskenler olusturuldu.
         */
        SqlConnection baglanti;
        SqlCommand komut;
        SqlDataAdapter da;

        private void UserProfile_Load(object sender, EventArgs e)
        {
            //using (StreamWriter sW = new StreamWriter(of))
            //{
            //    sW.WriteLine(txtUsername.Text);
            //}
            baglanti = new SqlConnection("server=.; Initial Catalog=userinfo;Integrated Security=SSPI");
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            txtPassword2.Visible = true;
            lblPassword2.Visible = true;
            btnLogin.Visible = true;
            btnSave.Visible = false;
        }
        private String hPassword;

        /**
         * @brief SHA256 kullanilarak parola sifrelendi.
         */
        public static String sha256_hash(String value)
        {
            StringBuilder Sb = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }

        /**
         * @brief Kullanici bilgileri guncellendi ve veri tabanina kaydedildi.
         */
        private void btnLogin_Click(object sender, EventArgs e)
        {
            //XDocument x = XDocument.Load(@"../../../userinfo.xml");
            //XElement update = x.Element("users").Elements("userInformation").FirstOrDefault(a => a.Element("Username").Value.Trim() == txtUsername.Text);


            if ((txtPassword.Text != "") && (txtNameSurname.Text != "") //tüm kullanıcı bilgilerini doldurması için
                && (txtPhoneNumber.Text != "") && (txtAddress.Text != "") && (txtCity.Text != "")
                && (txtCountry.Text != "") && (txtEmail.Text != ""))
            {
                string sorgu = "UPDATE userinfo Set UserType=@UserType, Passwords= @Passwords, NameSurname= @NameSurname, PhoneNumber= @PhoneNumber, Address= @Address, City= @City, Country= @Country, Mail= @Mail WHERE Username=@Username";
                komut = new SqlCommand(sorgu, baglanti);
                komut.Parameters.AddWithValue("@UserType", txtUserType.Text);
                komut.Parameters.AddWithValue("@Passwords", txtPassword.Text);
                komut.Parameters.AddWithValue("@NameSurname", txtNameSurname.Text);
                komut.Parameters.AddWithValue("@PhoneNumber", txtPhoneNumber.Text);
                komut.Parameters.AddWithValue("@Address", txtAddress.Text);
                komut.Parameters.AddWithValue("@City", txtCity.Text);
                komut.Parameters.AddWithValue("@Country", txtCountry.Text);
                komut.Parameters.AddWithValue("@Mail", txtEmail.Text);
                komut.Parameters.AddWithValue("@Username", txtUsername.Text);
                baglanti.Open();
                komut.ExecuteNonQuery();
                baglanti.Close();
                //if (update != null)
                //{
                //    update.SetElementValue("UserType", txtUserType.Text);
                //    hPassword = sha256_hash(txtPassword.Text);
                //    update.SetElementValue("Password", hPassword);
                //    update.SetElementValue("NameSurname", txtNameSurname.Text);
                //    update.SetElementValue("PhoneNumber", txtPhoneNumber.Text);
                //    update.SetElementValue("Address", txtAddress.Text);
                //    update.SetElementValue("City", txtCity.Text);
                //    update.SetElementValue("Country", txtCountry.Text);
                //    update.SetElementValue("Mail", txtEmail.Text);
                //    x.Save(@"../../../userinfo.xml");
                //}
                label1.Text = "Saved";

            }
            else
            {
                label1.Text = "Error";
            }

            //if ((txtPassword.Text == "user" || txtPassword.Text == "USER"))
            //{
            //    MessageBox.Show("Your Information Has Been Updated");
            //}
            //else
            //    MessageBox.Show("Error! Please Check Your Password");

        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            Form2 settingsScreen = new Form2();
            settingsScreen.Show();
            this.Hide();
        }
    }
}
