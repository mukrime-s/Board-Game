/**
* @file newGame.cs
* @author Mukrime_Sagiroglu_152120191034
* @author Hazar_Namdar_152120191053
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Media;
using System.Threading;

namespace BoardGame
{
    public partial class newGame : Form
    {
        /**
         * @brief Skor kazanilip kazanilmadigini kontrol eden bool degisken.
         */
        private bool gotScore = false;

        /**
         * @brief Skoru tutan degisken.
         */
        private int score = 0;

        /**
         * @brief fillBoard() metodu icin kullanilicak olan degiskenler
         */
        private ArrayList arrList = new ArrayList();
        private ArrayList txtToArray = new ArrayList();
        private string outputFile, row, colm;
        private int eskiKonum = 0, yeniKonum = 0, sekil, renk;
        private Random rnd = new Random();

        /**
         * @brief Zorluk seviyesini almak icin kullanilan string degisken
         */
        private string data;

        /**
         * @brief Matriste kullanilan button[] boardun global olarak kullanilmasi saglandi.
         */
        private Button[,] board;

        /**
         * @brief Butondaki sekli kaybetmemek icin olusturulan global degisken
         */
        private Image image;

        /**
         * @brief birden fazla yerde kullanabilmek icin Row ve Colm getter setterlari kullanildi.
         */
        public int Row { get; set; } 
        public int Colm { get; set; }

        public newGame()
        {
            InitializeComponent();

            outputFile = Application.StartupPath + @"../../../userinfo.txt";    //dosyayı okumak için
            FileStream fileStream = new FileStream(outputFile, FileMode.OpenOrCreate, FileAccess.Read);
            StreamReader reader = new StreamReader(fileStream);

            string satir = reader.ReadLine();
            data = satir;
            if (data == "custom")
            {
                /**
                 * @brief Custom zorluk modunda satir ve sutun girdilerini almak icin yapildi.
                 */
                row = reader.ReadLine();
                colm = reader.ReadLine();
            }

            /**
             * @brief fillBoard() metodunda kullanilmasi icin userInfo.txt deki bilgiler txtToArray e aktarildi.
             */
            while (satir != null)
            {
                txtToArray.Add(satir);
                satir = reader.ReadLine();
            }

            /**
             * @brief Dosyadan okunan zorluk seviyesine gore; tahta, Matris(int, int) fonksiyonu ile olusturulacak
             */
            if (data == "" || data == "normal") //Normal boyutu için uygun oyun tahtası olusturuldu.
            {
                Matris(9, 9);
            }
            if (data == "easy")                    //Easy boyutu için uygun oyun tahtası olusturuldu.
            {
                Matris(15, 15);
            }
            if (data == "hard")                    //Hard boyutu için uygun oyun tahtası olusturuldu.
            {
                Matris(6, 6);
            }
            if (data == "custom")                  //Custom boyutu için uygun oyun tahtası olusturuldu.
            {
                Matris(Convert.ToInt32(row), Convert.ToInt32(colm));
            }

            reader.Close();
            fileStream.Close();
        }

        /**
         * @brief Secilen zorluga gore puan aldirma fonksiyonu getScore() olusturuldu.
         */
        public void getScore(string dt)
        {
            switch (dt)
            {
                case "easy":
                    score += 1;

                    break;
                case "normal":
                    score += 3;

                    break;
                case "hard":
                    score += 5;

                    break;
                case "custom":
                    score += 2;

                    break;
            }
        }

        /**
         * @brief Dikeyde puan alma kismi icin yapilan fonksiyon
         */
        public void getPointVertical(int j)
        {
            /**
             * @brief Girilen sutun değeri(j) icin her satirda ayni sekil ve ayni renk var mi?
             * @brief Ilk eleman ve bir sonraki elemana bakar, dogruysa bir sonraki eleman bir artar
             * @brief Her esitlemeden sonra pointCondition bir artar
             */
            int pointCondition = 1, deletedImageTag = 0;
            int firstIndex = -1, lastIndex = -1;

            for (int i = 0; i < this.Row; i++)
            {
                if (i != this.Row - 1 && board[i, j].BackgroundImage != null)
                {
                    for (int k = i + 1; k <= this.Row - 1; k++)
                    {
                        if (board[k, j].BackgroundImage != null && i != k)
                        {
                            if (Convert.ToInt32(board[i, j].BackgroundImage.Tag) == Convert.ToInt32(board[k, j].BackgroundImage.Tag))
                            {
                                deletedImageTag = Convert.ToInt32(board[i, j].BackgroundImage.Tag);

                                pointCondition++;

                                if (pointCondition >= 5)
                                {
                                    firstIndex = i;
                                    lastIndex = k;
                                }
                            }
                            else if (pointCondition < 5)
                            {
                                pointCondition = 1;
                                break;
                            }
                            else
                            {
                                lastIndex = k;
                                break;
                            }
                        } else if (board[k, j].BackgroundImage == null && pointCondition < 5)
                        {
                            pointCondition = 1;
                            break;
                        }
                        else if (board[k, j].BackgroundImage == null && pointCondition >= 5)
                        {
                            lastIndex = k;
                            break;
                        }
                    }
                }

                if (pointCondition >= 5)
                    break;
                else
                    pointCondition = 1;
            }

            /**
             * @brief pointCondition 5 veya daha buyukse puan aldirma yapilir.
             */
            if (pointCondition >= 5)
            {
                gotScore = true;

                for (int i = firstIndex; i <= lastIndex; i++)
                {
                    if (board[i, j].BackgroundImage != null && Convert.ToInt32(board[i, j].BackgroundImage.Tag) == deletedImageTag)
                    {
                        board[i, j].BackgroundImage = null;
                    }
                }

                getScore(data);
            }
        }

        /**
         * @brief Yatayda puan alma kismi icin yapilan fonksiyon
         */
        public void getPointHorizontal(int i)
        {
            /**
             * @brief Girilen satir değeri(i) icin her sutunda ayni sekil ve ayni renk var mi?
             * @brief Ilk eleman ve bir sonraki elemana bakar, dogruysa bir sonraki eleman bir artar
             * @brief Her esitlemeden sonra pointCondition bir artar
             */

            int pointCondition = 1, deletedImageTag = 0;
            int firstIndex = -1, lastIndex = -1;

            for (int j = 0; j < this.Colm; j++)
            {
                if (j != this.Colm - 1 && board[i, j].BackgroundImage != null)
                {
                    for (int k = j + 1; k <= this.Colm - 1; k++)
                    {
                        if (board[i, k].BackgroundImage != null && j != k)
                        {
                            if (Convert.ToInt32(board[i, j].BackgroundImage.Tag) == Convert.ToInt32(board[i, k].BackgroundImage.Tag))
                            {
                                deletedImageTag = Convert.ToInt32(board[i, j].BackgroundImage.Tag);

                                pointCondition++;

                                if (pointCondition >= 5)
                                {
                                    firstIndex = j;
                                    lastIndex = k;
                                }
                            }
                            else if (pointCondition < 5)
                            {
                                pointCondition = 1;
                                break;
                            }
                            else
                            {
                                lastIndex = k;
                                break;
                            }
                        }
                        else if (board[i, k].BackgroundImage == null && pointCondition < 5)
                        {
                            pointCondition = 1;
                            break;
                        }
                        else if (board[i, k].BackgroundImage == null && pointCondition >= 5)
                        {
                            lastIndex = k;
                            break;
                        }
                    }
                }

                if (pointCondition >= 5)
                    break;
                else
                    pointCondition = 1;
            }

            /**
             * @brief pointCondition 5 veya daha buyukse puan aldirma yapilir.
             */
            if (pointCondition >= 5)
            {
                gotScore = true;

                for (int j = firstIndex; j <= lastIndex; j++)
                {
                    if (board[i, j].BackgroundImage != null && Convert.ToInt32(board[i, j].BackgroundImage.Tag) == deletedImageTag)
                    {
                        board[i, j].BackgroundImage = null;
                    }
                }

                getScore(data);
            }
        }

        /**
         * @brief Eger en fazla 2 buton bossa gerisi doluysa, oyun biter. Bunu saglayan fonksiyon
         */
        public bool isBoardFull()
        {
            int isFullCounter = 0;

            for (int i = 0; i < this.Row; i++)
                for (int j = 0; j < this.Colm; j++)
                    if (board[i, j].BackgroundImage != null)
                        isFullCounter++;
            if (isFullCounter == (this.Row * this.Colm) - 2)
                return true;
            else
                return false;
        }

        /**
         * @brief Butona 2 kere basildiginda bir hamle yapilmis olacak.
         */
        public void buttonCLick(object sender, EventArgs e)
        {
            /**
             * @brief basilan buton bir değisken olarak tutuldu.
             */
            Button pushed = sender as Button;
            
            /**
             * @brief Basilan buton sekilliyse asagidakiler yapilacak.
             */
            if (pushed.BackgroundImage != null)
            {
                /**
                 * @brief Basilan butonun resmi, baska bir degiskende saklandi.
                 */
                image = pushed.BackgroundImage;

                for (int i = 0; i < Row; i++)
                {
                    for (int j = 0; j < Colm; j++)
                    {
                        if (pushed.Tag == board[i, j].Tag)
                        {
                            /**
                             * @brief Basilan butonun eski konumu tutuldu.
                             */
                            eskiKonum = Convert.ToInt32(pushed.Tag);        
                        }
                    }
                }

                /**
                 * @brief Basilan butonun resmi kaldirildi.
                 */
                pushed.BackgroundImage = null;

                /**
                 * @brief Artık sekilli butonlara basilamiyor, bos olanlara basiliyor.
                 */
                for (int i = 0; i < Row; i++)
                {
                    for (int j = 0; j < Colm; j++)
                    {
                        board[i, j].Enabled = false;
                        
                        if (board[i, j].BackgroundImage == null)
                            board[i, j].Enabled = true;
                    }
                }
            }
            /**
             * @brief Basilan buton resimsizse
             */
            else
            {
                /**
                 * @brief Degiskende saklanan resim, basilan butona aktarildi.
                 */
                pushed.BackgroundImage = image;

                for (int i = 0; i < Row; i++)
                {
                    for (int j = 0; j < Colm; j++)
                    {
                        if (pushed.Tag == board[i, j].Tag)
                        {
                            /**
                             * @brief Basilan butonun yeni konumu atandi.
                             */
                            yeniKonum = Convert.ToInt32(pushed.Tag);
                        }
                    }
                }

                /**
                 * @brief Yeni konum listeye eklendi, eski konum silindi.
                 */
                arrList.Remove(eskiKonum);
                arrList.Add(yeniKonum);
                arrList.Sort();

                /**
                 * @brief Program yarim saniye bekletildi daha sonra fillBoard() metodu cagirildi.
                 */
                Thread.Sleep(500);
                fillBoard();

                /**
                 * @brief Uygun sartlar karsilanirsa puan aldirma calisacak.
                 */
                for (int j = 0; j < this.Colm; j++)
                    this.getPointVertical(j);
                for (int i = 0; i < this.Row; i++)
                    this.getPointHorizontal(i);

                /**
                 * @brief Artık sekilli butonlara basilabiliyor, bos olanlara basilamiyor.
                 */
                for (int i = 0; i < Row; i++)
                {
                    for (int j = 0; j < Colm; j++)
                    {
                        board[i, j].Enabled = true;
                        
                        if (board[i, j].BackgroundImage == null)
                            board[i, j].Enabled = false;
                    }
                }

                /**
                 * @brief Normal hamle ve puan alma kisimlari için ses olusturuldu.
                 */
                SoundPlayer moveVoice = new SoundPlayer();//hamleden sonra ses eklendi.
                string konum = Application.StartupPath + @"../../../sounds/hamle.wav";
                moveVoice.SoundLocation = konum;
                SoundPlayer pointVoice = new SoundPlayer();
                string location = Application.StartupPath + @"../../../sounds/puan.wav";
                pointVoice.SoundLocation = location;

                /**
                 * @brief Skor alinmissa ses calicak ve puan alinacak, skor alinmamissa baska bir ses calicak.
                 */
                if (gotScore)
                {
                    pointVoice.Play();

                    if (lbScore.Text != "")
                    {
                        lbScore.Text = "";
                        lbScore.Text += score.ToString();
                    }
                    else
                    {
                        lbScore.Text += score.ToString();
                    }

                    gotScore = false;
                }
                else
                {
                    moveVoice.Play();
                }

                /**
                 * @brief Oyunun bitip bitmedigini kontrol eden yer.
                 */
                if (isBoardFull())
                {
                    MessageBox.Show("GAME OVER... Your score is: " + score);
                    this.Close();
                }
            }
        }

        /**
         * @brief Tahtaya rastgele 3 tane sekil atamak icin kullanilan fonksiyon
         */
        public void fillBoard()
        {
            /**
             * @brief Sekillerin konumlarini tutmak icin degiskenler tanimlandi. Ayrıca random degisken icin sinir tanimlandi.
             */
            int SeklinKonumu_1, SeklinKonumu_2, SeklinKonumu_3, tmp = (Row * Colm) + 1;

            /**
             * @brief Random hamlenin ust uste binmesi engellendi.
             */
            while (true)
            {
                SeklinKonumu_1 = rnd.Next(1, tmp);
                SeklinKonumu_2 = rnd.Next(1, tmp);
                SeklinKonumu_3 = rnd.Next(1, tmp);

                if ((!arrList.Contains(SeklinKonumu_1)) && (!arrList.Contains(SeklinKonumu_2)) && (!arrList.Contains(SeklinKonumu_3)))
                {
                    arrList.Add(SeklinKonumu_1);
                    arrList.Add(SeklinKonumu_2);
                    arrList.Add(SeklinKonumu_3);
                    break;
                }
            }

            int counter = 0;

            for (int i = 0; i < Row; i++)
            {
                for (int j = 0; j < Colm; j++)
                {
                    counter++;

                    /**
                     * @brief txtToArray dizisinde bulunan degerleri kontrol ederek rand sayi uretimi saglandi.
                     */
                    //bu kısımda random aralıkları belirlendi.
                    if (txtToArray.Contains("Square") && txtToArray.Contains("Triangle") && !txtToArray.Contains("Round")) sekil = rnd.Next(1, 3); //1=square 2=triangle 3=round
                    if (txtToArray.Contains("Triangle") && txtToArray.Contains("Round") && !txtToArray.Contains("Square")) sekil = rnd.Next(2, 4);
                    if (txtToArray.Contains("Square") && txtToArray.Contains("Round") && !txtToArray.Contains("Triangle"))
                    {
                        /**
                         * @brief Triangle cikmasi engellendi.
                         */
                        while (true) 
                        {
                            sekil = rnd.Next(1, 4);
                            if (sekil != 2) 
                            {
                                break;
                            }
                        }
                    }
                    if (txtToArray.Contains("Square") && txtToArray.Contains("Triangle") && txtToArray.Contains("Round")) sekil = rnd.Next(1, 4);

                    /**
                     * @brief Secilebilecek renklere gore random renk atama kombinasyonu uyarlandi.
                     */
                    if (txtToArray.Contains("red") && !txtToArray.Contains("blue") && !txtToArray.Contains("pink")) renk = 1; //1=red 2=blue 3=pink
                    if (txtToArray.Contains("blue") && !txtToArray.Contains("red") && !txtToArray.Contains("pink")) renk = 2;
                    if (txtToArray.Contains("pink") && !txtToArray.Contains("red") && !txtToArray.Contains("blue")) renk = 3;
                    if (txtToArray.Contains("red") && txtToArray.Contains("blue") && !txtToArray.Contains("pink")) renk = rnd.Next(1, 3);
                    if (txtToArray.Contains("blue") && txtToArray.Contains("pink") && !txtToArray.Contains("red")) renk = rnd.Next(2, 4);
                    if (txtToArray.Contains("red") && txtToArray.Contains("pink") && !txtToArray.Contains("blue"))
                    {
                        /**
                         * @brief Mavi bastirmamak icin yapildi.
                         */
                        while (true)
                        {
                            renk = rnd.Next(1, 4);
                            if (renk != 2)
                            {
                                break;
                            }
                        }
                    }
                    if (txtToArray.Contains("red") && txtToArray.Contains("blue") && txtToArray.Contains("pink")) renk = rnd.Next(1, 4);

                    /**
                     * @brief Burada, her ihtimal icin tahtadaki rastgele deger, yukarida bulunup asagida atanmistir.
                     */
                    if ((counter == SeklinKonumu_1 || counter == SeklinKonumu_2 || counter == SeklinKonumu_3) && (sekil == 1))
                    {
                        if (txtToArray.Contains("Square") && txtToArray.Contains("red") && renk == 1)    // Square & red
                        {
                            board[i, j].BackgroundImage = Image.FromFile(Application.StartupPath + @"../../../images/kirmizikare.JPG");
                            board[i, j].BackgroundImage.Tag = 5;
                        }
                        if (txtToArray.Contains("Square") && txtToArray.Contains("blue") && renk == 2)
                        {
                            board[i, j].BackgroundImage = Image.FromFile(Application.StartupPath + @"../../../images/mavikare.JPG");
                            board[i, j].BackgroundImage.Tag = 4;
                        }
                        if (txtToArray.Contains("Square") && txtToArray.Contains("pink") && renk == 3)
                        {
                            board[i, j].BackgroundImage = Image.FromFile(Application.StartupPath + @"../../../images/pembekare.JPG");
                            board[i, j].BackgroundImage.Tag = 6;
                        }
                    }
                    if ((counter == SeklinKonumu_1 || counter == SeklinKonumu_2 || counter == SeklinKonumu_3) && sekil == 2)
                    {
                        if (txtToArray.Contains("Triangle") && txtToArray.Contains("red") && renk == 1)
                        {
                            board[i, j].BackgroundImage = Image.FromFile(Application.StartupPath + @"../../../images/kirmiziucgen.JPG");
                            board[i, j].BackgroundImage.Tag = 8;
                        }
                        if (txtToArray.Contains("Triangle") && txtToArray.Contains("blue") && renk == 2)
                        {
                            board[i, j].BackgroundImage = Image.FromFile(Application.StartupPath + @"../../../images/maviucgen.JPG");
                            board[i, j].BackgroundImage.Tag = 7;
                        }
                        if (txtToArray.Contains("Triangle") && txtToArray.Contains("pink") && renk == 3)
                        {
                            board[i, j].BackgroundImage = Image.FromFile(Application.StartupPath + @"../../../images/pembeucgen.JPG");
                            board[i, j].BackgroundImage.Tag = 9;
                        }
                    }
                    if ((counter == SeklinKonumu_1 || counter == SeklinKonumu_2 || counter == SeklinKonumu_3) && sekil == 3)
                    {
                        if (txtToArray.Contains("Round") && txtToArray.Contains("red") && renk == 1)
                        {
                            board[i, j].BackgroundImage = Image.FromFile(Application.StartupPath + @"../../../images/kirmizidaire.JPG");
                            board[i, j].BackgroundImage.Tag = 2;//boyutlar 70,70 olarak butonlara uygun şekilde ayarlandı.
                        }
                        if (txtToArray.Contains("Round") && txtToArray.Contains("blue") && renk == 2)
                        {
                            board[i, j].BackgroundImage = Image.FromFile(Application.StartupPath + @"../../../images/mavidaire.JPG");
                            board[i, j].BackgroundImage.Tag = 1;
                        }
                        if (txtToArray.Contains("Round") && txtToArray.Contains("pink") && renk == 3)
                        {
                            board[i, j].BackgroundImage = Image.FromFile(Application.StartupPath + @"../../../images/pembedaire.JPG");
                            board[i, j].BackgroundImage.Tag = 3;
                        }
                    }
                }
            }
        }

        /**
         * @brief Bu fonksiyon ile tahta olusturulur.
         */
        public void Matris(int row, int colm)
        {
            /**
             * @brief Getter setter lar burada belirlendi.
             */
            this.Row = row;
            this.Colm = colm;

            int counter = 0;
            board = new Button[row, colm];
            int top = 0;
            int left = 0;

            /**
             * @brief Butonlar olusturuldu ve hepsi bir tagCount ile isaretlendi.
             */
            int tagCount = 1; //Butonları işaretlemek için
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < colm; j++)
                {
                    counter++;
                    board[i, j] = new Button();
                    board[i, j].Width = 35;    //buton genisligi  varsayılan olarak atandi.
                    board[i, j].Height = 35;   //buton yüksekligi varsayılan olarak atandi.
                    board[i, j].Left = left;
                    board[i, j].Top = top;
                    board[i, j].Tag = tagCount++;
                    this.Controls.Add(board[i, j]);
                    left += 35;
                    board[i, j].BackColor = Color.White;//butonlara varsayılan olarak beyaz renk atandı.
                }
                top += 35;
                left = 0;
            }

            /**
             * @brief Tahtaya 3 sekil atandi.
             */
            fillBoard();

            /**
             * @brief buttonClick eventi cagirildi ve bos butonlarin basilma ozelligi kalkti.
             */
            for (int i = 0; i < this.Row; i++)
            {
                for (int j = 0; j < this.Colm; j++)
                {
                    board[i, j].Click += new EventHandler(this.buttonCLick);

                    if (board[i, j].BackgroundImage == null)
                        board[i, j].Enabled = false;
                }
            }
        }
        private void newGame_Load(object sender, EventArgs e)
        {
            arrList.Sort();
            lbScore.Text = "0";
        }
    }
}
