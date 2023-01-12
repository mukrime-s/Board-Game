/**
* @file userAdd.cs
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
    public partial class userAdd : Form
    {
        public userAdd()
        {
            InitializeComponent();
        }
        //string file = @"../../../counter.txt";
        private void userAdd_Load(object sender, EventArgs e)
        {

            btnBack.Visible = false;
            btnSave.Visible = true;
            txtId.ReadOnly = true;
            adminpanel.baglanti = new SqlConnection("server=.; Initial Catalog=userinfo;Integrated Security=SSPI");

            //string file = @"../../../counter.txt";
            //if (File.Exists(file) == true)
            //{
            //    string temp;
            //    int count = Convert.ToInt32(File.ReadAllText(file));
            //    count++;
            //    temp = count.ToString();
            //    File.WriteAllText(file, temp);
            //}
            //else
            //{
            //    FileInfo fi = new FileInfo(file);
            //    StreamWriter sw = new StreamWriter(file);
            //    string temp;
            //    int count = 0;
            //    temp = count.ToString();
            //    sw.Close();
            //    File.WriteAllText(file, temp);
            //}

            //string counter = File.ReadAllText(file);
            //txtId.Text = counter;
        }


        //private String hPassword;
        //public static String sha256_hash(String value)
        //{
        //    StringBuilder Sb = new StringBuilder();

        //    using (SHA256 hash = SHA256Managed.Create())
        //    {
        //        Encoding enc = Encoding.UTF8;
        //        Byte[] result = hash.ComputeHash(enc.GetBytes(value));

        //        foreach (Byte b in result)
        //            Sb.Append(b.ToString("x2"));
        //    }

        //    return Sb.ToString();
        //} // Encrypting with SHA256

        /**
         * @brief Yeni kullanici veri tabanina eklendi.
         */
        private void btnSave_Click(object sender, EventArgs e)
        {
            btnSave.Visible = false;
            btnBack.Visible = true;


           // string counter = File.ReadAllText(file);

            if ((txtUsername.Text != "") && (txtPassword.Text != "") && (txtNameSurname.Text != "") //tüm kullanıcı bilgilerini doldurması için
                && (txtPhoneNumber.Text != "") && (txtAddress.Text != "") && (txtCity.Text != "")
                && (txtCountry.Text != "") && (txtEmail.Text != ""))
            {

                //OleDbCommand cmd = new OleDbCommand("INSERT INTO userinfo ([UserType],[Username],[Password],[NameSurname],[PhoneNumber],[Address],[City],[Country],[Mail]) VALUES (?,?,?,?,?,?,?,?,?", baglanti);
                string sorgu = "INSERT INTO userinfo (UserType,Username,Passwords,NameSurname,PhoneNumber,Address,City,Country,Mail) values (@UserType,@Username,@Passwords,@NameSurname,@PhoneNumber,@Address,@City,@Country,@Mail)";
                adminpanel.komut = new SqlCommand(sorgu, adminpanel.baglanti);
                adminpanel.komut.Parameters.AddWithValue("@UserType", txtUserType.Text);
                adminpanel.komut.Parameters.AddWithValue("@Username", txtUsername.Text);
                adminpanel.komut.Parameters.AddWithValue("@Passwords", txtPassword.Text);
                adminpanel.komut.Parameters.AddWithValue("@NameSurname", txtNameSurname.Text);
                adminpanel.komut.Parameters.AddWithValue("@PhoneNumber", txtPhoneNumber.Text);
                adminpanel.komut.Parameters.AddWithValue("@Address", txtAddress.Text);
                adminpanel.komut.Parameters.AddWithValue("@City", txtCity.Text);
                adminpanel.komut.Parameters.AddWithValue("@Country", txtCountry.Text);
                adminpanel.komut.Parameters.AddWithValue("@Mail", txtEmail.Text);
                adminpanel.baglanti.Open();
                adminpanel.komut.ExecuteNonQuery();
                adminpanel.baglanti.Close();
                
                

                //XDocument x = XDocument.Load(@"../../../userinfo.xml");
                //hPassword = sha256_hash(txtPassword.Text);
                //x.Element("users").Add(
                //    new XElement("userInformation",
                //    new XElement("id", counter),
                //    new XElement("UserType", txtUserType.Text),
                //    new XElement("Username", txtUsername.Text),
                //    new XElement("Password", hPassword),
                //    new XElement("NameSurname", txtNameSurname.Text),
                //    new XElement("PhoneNumber", txtPhoneNumber.Text),
                //    new XElement("Address", txtAddress.Text),
                //    new XElement("City", txtCity.Text),
                //    new XElement("Country", txtCountry.Text),
                //    new XElement("Mail", txtEmail.Text)));
                //x.Save(@"../../../userinfo.xml");
                //label1.Text = "Saved";
            }
            else
            {
                label1.Text = "Error";
                //string temp;
                //int count = Convert.ToInt32(counter);
                //count--;
                //temp = count.ToString();
                //File.WriteAllText(file, temp);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            adminpanel ap = new adminpanel();
            ap.Show();
            this.Hide();
        }
    }
}
