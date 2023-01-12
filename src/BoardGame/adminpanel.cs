/**
* @file adminpanel.cs
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
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace BoardGame
{
    public partial class adminpanel : Form
    {
        public adminpanel()
        {
            InitializeComponent();
        }

        /**
         * @brief Veri tabanina baglama icin gerekli degiskenler tanimlandi.
         */
        public static SqlConnection baglanti;
        public static SqlCommand komut;
        public static SqlDataAdapter da;

        /**
         * @brief listele() fonksiyonunda bilgiler dataGridView da gosterildi ve veri tabanina kaydedildi.
         */
        void listele()
        {
            baglanti = new SqlConnection("server=.; Initial Catalog=userinfo;Integrated Security=SSPI");
            baglanti.Open();
            da = new SqlDataAdapter("SELECT *FROM userinfo", baglanti);
            DataTable tablo = new DataTable();
            da.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglanti.Close();

            //XmlDocument x = new XmlDocument();
            //DataSet ds = new DataSet();
            //XmlReader xmlfile;
            //xmlfile = XmlReader.Create(@"../../../userinfo.xml", new XmlReaderSettings());
            //ds.ReadXml(xmlfile);

            //ds.Tables[0].Columns.Remove(ds.Tables[0].Columns["Password"]);   

            //dataGridView1.DataSource = ds.Tables[0];

            //xmlfile.Close();
        }

        /**
         * @brief listele() metodu butona basildiginda cagirilacak.
         */
        private void btnListele_Click(object sender, EventArgs e)
        {
            listele();
        }

        /**
         * @brief Yeni kullanici eklenecegi zaman userAdd formu acilacak.
         */
        private void btnEkle_Click(object sender, EventArgs e)
        {

            userAdd ua = new userAdd();
            ua.Show();
            this.Hide();

        }

        /**
         * @brief Var olan kullanici, guncellenmek istendiginde userUpdate formu acilacak.
         */
        private void btnGüncelle_Click(object sender, EventArgs e)
        {
            userUpdate uu = new userUpdate();
            uu.Show();
            this.Hide();
        }

        /**
         * @brief Butona basildiginda hem veri tabanindan hem de dataGridView dan kisi silinecek.
         */
        private void btnSilme_Click(object sender, EventArgs e)
        {

            //string file = @"../../../userinfo.xml";
            //XDocument x = XDocument.Load(file);
            //XElement rootElement = x.Root;
            //foreach (XElement Users in rootElement.Elements())
            //{
            //    if (Users.Element("id").Value == txtSilme.Text) //idye göre silme işlemi yapıldı.
            //    {
            //        Users.Remove();
            //    }
            //}
            //x.Save(file);

            //foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            //{
            //    dataGridView1.Rows.RemoveAt(row.Index);

            //}
            //x.Save(@"../../../userinfo.xml");
            //listele();

            DialogResult dialogResult = MessageBox.Show("Do you want to permanently delete the user?", "Delete Title", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                string sorgu = "DELETE FROM userinfo WHERE id=@id";
                komut = new SqlCommand(sorgu, baglanti);
                komut.Parameters.AddWithValue("@id", dataGridView1.CurrentRow.Cells[0].Value);
                baglanti.Open();
                komut.ExecuteNonQuery();
                baglanti.Close();
                listele();
                MessageBox.Show("User permanently deleted");
                Form2 f2 = new Form2();
                f2.Show();
                this.Hide();
            }
            else if (dialogResult == DialogResult.No)
            {
            }
        }
    }
}
