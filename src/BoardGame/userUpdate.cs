/**
* @file userUpdate.cs
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
    public partial class userUpdate : Form
    {
        public userUpdate()
        {
            InitializeComponent();
        }

        /**
         * @brief Veri tabanini kullanmak icin gerekli degiskenler olusturuldu.
         */
        SqlConnection baglanti;
        SqlCommand komut;
        SqlDataAdapter da;

        /**
         * @brief Degistirilebilecek ayarlar gozukur, digerleri gozukmez.
         */
        private void userUpdate_Load(object sender, EventArgs e)
        {
            lbluserinfo.Visible = false;
            lblUserName.Visible = false;
            lblId.Visible = false;
            lblUserName.Visible = false;
            lblpassword.Visible = false;
            lblNamesurname.Visible = false;
            lblPhonenumber.Visible = false;
            lbladdress.Visible = false;
            lblcity.Visible = false;
            lblCountry.Visible = false;
            lblmail.Visible = false;

            txtUserType.Visible = false;
            txtId.Visible = false;
            txtUsername.Visible = false;
            txtPassword.Visible = false;
            txtNameSurname.Visible = false;
            txtPhoneNumber.Visible = false;
            txtAddress.Visible = false;
            txtCity.Visible = false;
            txtCountry.Visible = false;
            txtEmail.Visible = false;

            lblSearch.Visible = true;
            txtSearch.Visible = true;
            btnSearch.Visible = true;

            btnBack.Visible = true;
            btnSaveUpdate.Visible = false;
            baglanti = new SqlConnection("server=.; Initial Catalog=userinfo;Integrated Security=SSPI");
        }

        private String hPassword;

        /**
         * @brief SHA256 kullanilarak parolada sifreleme yapildi.
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
        private void btnSaveUpdate_Click(object sender, EventArgs e)
        {
            //XDocument x = XDocument.Load(@"../../../userinfo.xml");
            //XElement update = x.Element("users").Elements("userInformation").FirstOrDefault(a => a.Element("id").Value.Trim() == txtId.Text);
            string sorgu = "UPDATE userinfo Set UserType=@UserType, Passwords= @Passwords, NameSurname= @NameSurname, PhoneNumber= @PhoneNumber, Address= @Address, City= @City, Country= @Country, Mail= @Mail WHERE id=@id";
            komut = new SqlCommand(sorgu, baglanti);
            
            if ((txtPassword.Text != "") && (txtNameSurname.Text != "") //tüm kullanıcı bilgilerini doldurması için
                && (txtPhoneNumber.Text != "") && (txtAddress.Text != "") && (txtCity.Text != "")
                && (txtCountry.Text != "") && (txtEmail.Text != ""))
            {
                baglanti.Open();
                da = new SqlDataAdapter("select count(*) from userinfo where id = "+txtId.Text+"",baglanti);
                DataTable dt = new DataTable();
                da.Fill(dt);
                komut = new SqlCommand("update userinfo set UserType = '"+ txtUserType.Text.ToString()+ "'," +
                    "Passwords = '" + txtPassword.Text.ToString() + "',NameSurname = '" + txtNameSurname.Text.ToString() + "',PhoneNumber = '" + txtPhoneNumber.Text.ToString() + "'," +
                    "Address = '" + txtAddress.Text.ToString() + "',City = '" + txtCity.Text.ToString() + "',Country = '" + txtCountry.Text.ToString() + "',Mail = '" + txtEmail.Text.ToString()+"'where id ="+txtId.Text+"",baglanti);
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
            
                label1.Text = "Saved";

            }
            else
            {
                label1.Text = "Error";
            }
        }

        /**
         * @brief Bu butona basildiginda bir onceki form(adminpanel) acilir, bu form(userUpdate) kapanir.
         */
        private void btnBack_Click(object sender, EventArgs e)
        {
            adminpanel ap = new adminpanel();
            ap.Show();
            this.Hide();
        }

        /**
         * @brief Arama butonuna basildiginda, tum bilgiler gozukur.
         */
        private void btnSearch_Click(object sender, EventArgs e)
        {
            lbluserinfo.Visible = true;
            lblUserName.Visible = true;
            lblId.Visible = true;
            lblUserName.Visible = true;
            lblpassword.Visible = true;
            lblNamesurname.Visible = true;
            lblPhonenumber.Visible = true;
            lbladdress.Visible = true;
            lblcity.Visible = true;
            lblCountry.Visible = true;
            lblmail.Visible = true;

            txtUserType.Visible = true;
            txtId.Visible = true;
            txtId.ReadOnly = true;
            txtUsername.Visible = true;
            txtUsername.ReadOnly = true;
            txtPassword.Visible = true;
            txtNameSurname.Visible = true;
            txtPhoneNumber.Visible = true;
            txtAddress.Visible = true;
            txtCity.Visible = true;
            txtCountry.Visible = true;
            txtEmail.Visible = true;

            btnSaveUpdate.Visible = true;

            txtId.Text = txtSearch.Text;
        }
    }
}
