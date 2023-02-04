using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mineswepper
{
    public partial class Form1 : Form
    {

        PictureBox[][] pboard = new PictureBox[9][];
        int nr_bombe =10;
        bool[][] bombe = new bool[9][];
        int k = 0;
        bool[][] steag = new bool[9][];
        bool fail = false;
        bool win=false;

        Random r = new Random();

        System.Media.SoundPlayer Bk_music = new System.Media.SoundPlayer(Properties.Resources.Bk_music);
        public Form1()
        {
            InitializeComponent();
            Bk_music.PlayLooping();
            DateTime dateTime = DateTime.Now;
            int timeMsSinceMidnight = (int)dateTime.TimeOfDay.TotalMilliseconds;
            r.Next(timeMsSinceMidnight);
            for (int i = 1; i <= 8; i++) bombe[i] = new bool[9]; //matrici bomba
            for (int i = 1; i <= 8; i++) steag[i] = new bool[9];
            //umple matricea cu picture boxurile de pe fiecare i j
            #region init_pboard
            for (int i = 1; i <= 8; i++) pboard[i] = new PictureBox[9];
            pboard[1][1] = pictureBox2;
            pboard[1][2] = pictureBox3;
            pboard[1][3] = pictureBox4;
            pboard[1][4] = pictureBox5;
            pboard[1][5] = pictureBox6;
            pboard[1][6] = pictureBox7;
            pboard[1][7] = pictureBox8;
            pboard[1][8] = pictureBox9;
            pboard[2][1] = pictureBox10;
            pboard[2][2] = pictureBox11;
            pboard[2][3] = pictureBox12;
            pboard[2][4] = pictureBox13;
            pboard[2][5] = pictureBox14;
            pboard[2][6] = pictureBox15;
            pboard[2][7] = pictureBox16;
            pboard[2][8] = pictureBox17;
            pboard[3][1] = pictureBox18;
            pboard[3][2] = pictureBox19;
            pboard[3][3] = pictureBox20;
            pboard[3][4] = pictureBox21;
            pboard[3][5] = pictureBox22;
            pboard[3][6] = pictureBox23;
            pboard[3][7] = pictureBox24;
            pboard[3][8] = pictureBox25;
            pboard[4][1] = pictureBox26;
            pboard[4][2] = pictureBox27;
            pboard[4][3] = pictureBox28;
            pboard[4][4] = pictureBox29;
            pboard[4][5] = pictureBox30;
            pboard[4][6] = pictureBox31;
            pboard[4][7] = pictureBox32;
            pboard[4][8] = pictureBox33;
            pboard[5][1] = pictureBox34;
            pboard[5][2] = pictureBox35;
            pboard[5][3] = pictureBox36;
            pboard[5][4] = pictureBox37;
            pboard[5][5] = pictureBox38;
            pboard[5][6] = pictureBox39;
            pboard[5][7] = pictureBox40;
            pboard[5][8] = pictureBox41;
            pboard[6][1] = pictureBox42;
            pboard[6][2] = pictureBox43;
            pboard[6][3] = pictureBox44;
            pboard[6][4] = pictureBox45;
            pboard[6][5] = pictureBox46;
            pboard[6][6] = pictureBox47;
            pboard[6][7] = pictureBox48;
            pboard[6][8] = pictureBox49;
            pboard[7][1] = pictureBox50;
            pboard[7][2] = pictureBox51;
            pboard[7][3] = pictureBox52;
            pboard[7][4] = pictureBox53;
            pboard[7][5] = pictureBox54;
            pboard[7][6] = pictureBox55;
            pboard[7][7] = pictureBox56;
            pboard[7][8] = pictureBox57;
            pboard[8][1] = pictureBox58;
            pboard[8][2] = pictureBox59;
            pboard[8][3] = pictureBox60;
            pboard[8][4] = pictureBox61;
            pboard[8][5] = pictureBox62;
            pboard[8][6] = pictureBox63;
            pboard[8][7] = pictureBox64;
            pboard[8][8] = pictureBox65;
            #endregion  
            Reset();
        }
        
        private void Reset()
        {
            
            nr_bombe = 10;
            k = 0;
            fail = false;
            win = false;
            for (int i = 1; i <= 8; i++)
            {
                for (int j = 1; j <= 8; j++)
                {
                    bombe[i][j] = false;
                    steag[i][j] = false;
                    if (pboard[i][j].Image != null)
                    {
                        pboard[i][j].Image = null;
                        pboard[i][j].Refresh();
                    }
                }
                
            }
            while (k < nr_bombe)
            {
                int x, y; //coordonate
                x = r.Next(1, 8);
                y = r.Next(1, 8);
                if (!bombe[x][y])
                {
                    bombe[x][y] = true;
                    k++;
                }
            }
            Bk_music.PlayLooping();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void click(object sender, MouseEventArgs e)
        {
            if (fail || win)
                return;
            PictureBox pb = sender as PictureBox;
            bool ok = true;
            int i = 1, j = 1;
            for (i = 1; i <= 8 && ok; i++)
            {
                for (j = 1; j <= 8 && ok; j++)
                {
                    if (pboard[i][j] == pb)
                    {
                        ok = false;
                        break;
                    }

                }
                if (!ok)
                    break;
            }
            if (e.Button == MouseButtons.Right)
            {
               
                if (pboard[i][j].Image == null)
                {
                    steag[i][j] = true;
                    pboard[i][j].Image = Properties.Resources.steag;
                    pboard[i][j].Refresh();
                }
                else if (steag[i][j])
                {
                    Console.WriteLine();
                    pboard[i][j].Image = null;
                    steag[i][j] = false;
                    pboard[i][j].Refresh();
                }
                return;
            }
            alg(i, j);
            Console.WriteLine(i + " " + j);
            ok = true;
            if (!fail)
            {
                for (i = 1; i <= 8 && ok; i++)
                {
                    for (j = 1; j <= 8 && ok; j++)
                    {
                        if ((pboard[i][j].Image == null || pboard[i][j].Image == Properties.Resources.steag) && !bombe[i][j])

                            ok = false;
                    }
                }
                if (ok)
                {
                    Bk_music.Stop();
                    Victorie v = new Victorie();
                    v.ShowDialog();
                    Console.WriteLine("win");
                    win = true;
            
                }
            }
        }
        private void alg(int x, int y) //algoritm pt dezvăluirea tablei
        {
            if (pboard[x][y].Image != null)
                return;
            if (bombe[x][y])
            {
                System.Media.SoundPlayer soundBomb = new System.Media.SoundPlayer(Properties.Resources.Bomb);
                soundBomb.Play();
                fail = true;
                for (int i = 1; i <= 8; i++)
                    for (int j = 1; j <= 8; j++)
                    {
                        if (bombe[i][j])
                            afis(i, j);
                    }
                Infrangere inf= new Infrangere();
                inf.ShowDialog();
                return;
            }
            int n = 0;
            int[] dx = new int[8] { -1, -1, -1, 0, 0, 1, 1, 1 };
            int[] dy = new int[8] { -1, 0, 1, -1, 1, -1, 0, 1 };
            for (int i = 0; i < 8; i++)
            {

                int ii = x + dx[i];
                int jj = y + dy[i];
                if (ii > 0 && ii < 9 && jj > 0 && jj < 9)
                {
                    if (bombe[ii][jj])
                        n++;
                }

            }
            if (n == 0)
            {
                afis(x, y);
                for (int i = 0; i < 8; i++)
                {
                    int ii = x + dx[i];
                    int jj = y + dy[i];
                    if (ii > 0 && ii < 9 && jj > 0 && jj < 9)
                        alg(ii, jj);
                }
            }
            else
                afis(x, y);
        }
        private void afis(int x, int y)
        {
            if (pboard[x][y].Image != null)
                return;

            int n = 0;
            int[] dx = new int[8] { -1, -1, -1, 0, 0, 1, 1, 1 };
            int[] dy = new int[8] { -1, 0, 1, -1, 1, -1, 0, 1 };
            for (int i = 0; i < 8; i++)
            {

                int ii = x + dx[i];
                int jj = y + dy[i];
                if (ii > 0 && ii < 9 && jj > 0 && jj < 9)
                {
                    if (bombe[ii][jj])
                        n++;
                }

            }
            if (bombe[x][y])
            {
                pboard[x][y].Image = Properties.Resources.bomba1;
                pboard[x][y].Refresh();
                return;
            }
            switch (n)
            {
                case 0:
                    pboard[x][y].Image = Properties.Resources.blank;
                    pboard[x][y].Refresh();
                    break;
                case 1:
                    pboard[x][y].Image = Properties.Resources._1;
                    pboard[x][y].Refresh();
                    break;
                case 2:
                    pboard[x][y].Image = Properties.Resources._2;
                    pboard[x][y].Refresh();
                    break;
                case 3:
                    pboard[x][y].Image = Properties.Resources._3;
                    pboard[x][y].Refresh();
                    break;
                case 4:
                    pboard[x][y].Image = Properties.Resources._4;
                    pboard[x][y].Refresh();
                    break;
                case 5:
                    pboard[x][y].Image = Properties.Resources._5;
                    pboard[x][y].Refresh();
                    break;
                case 6:
                    pboard[x][y].Image = Properties.Resources._6;
                    pboard[x][y].Refresh();
                    break;
                case 7:
                    pboard[x][y].Image = Properties.Resources._7;
                    pboard[x][y].Refresh();
                    break;
                case 8:
                    pboard[x][y].Image = Properties.Resources._8;
                    pboard[x][y].Refresh();
                    break;


            }
        }

        
        #region PictureBox_Click
        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox5_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox6_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox7_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox9_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox10_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox11_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox12_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox13_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox14_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox15_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox16_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox17_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox18_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox19_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox20_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox21_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox22_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox23_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox24_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox25_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox26_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox27_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox28_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox29_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox30_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox31_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox32_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox33_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox34_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox35_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox36_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox37_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox38_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox39_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox40_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox41_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox42_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox43_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox44_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox45_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox46_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox47_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox48_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox49_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox50_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox51_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox52_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox53_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox54_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox55_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox56_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox57_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox58_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox59_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox60_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox61_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox62_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox63_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox64_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox65_Click(object sender, EventArgs e)
        {

        }
        private void pictureBox2_MouseDown(object sender, MouseEventArgs e)
        {
            click(sender, e);
        }
        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            click(sender, e);
        }
        private void pictureBox4_MouseDown(object sender, MouseEventArgs e)
        {
            click(sender, e);
        }
        private void pictureBox5_MouseDown(object sender, MouseEventArgs e)
        {
            click(sender, e);
        }
        private void pictureBox6_MouseDown(object sender, MouseEventArgs e)
        {
            click(sender, e);
        }
        private void pictureBox7_MouseDown(object sender, MouseEventArgs e)
        {
            click(sender, e);
        }
        private void pictureBox8_MouseDown(object sender, MouseEventArgs e)
        {
            click(sender, e);
        }
        private void pictureBox9_MouseDown(object sender, MouseEventArgs e)
        {
            click(sender, e);
        }
        private void pictureBox10_MouseDown(object sender, MouseEventArgs e)
        {
            click(sender, e);
        }
        private void pictureBox11_MouseDown(object sender, MouseEventArgs e)
        {
            click(sender, e);
        }
        private void pictureBox12_MouseDown(object sender, MouseEventArgs e)
        {
            click(sender, e);
        }
        private void pictureBox13_MouseDown(object sender, MouseEventArgs e)
        {
            click(sender, e);
        }
        private void pictureBox14_MouseDown(object sender, MouseEventArgs e)
        {
            click(sender, e);
        }
        private void pictureBox15_MouseDown(object sender, MouseEventArgs e)
        {
            click(sender, e);
        }
        private void pictureBox16_MouseDown(object sender, MouseEventArgs e)
        {
            click(sender, e);
        }
        private void pictureBox17_MouseDown(object sender, MouseEventArgs e)
        {
            click(sender, e);
        }
        private void pictureBox18_MouseDown(object sender, MouseEventArgs e)
        {
            click(sender, e);
        }
        private void pictureBox19_MouseDown(object sender, MouseEventArgs e)
        {
            click(sender, e);
        }
        private void pictureBox20_MouseDown(object sender, MouseEventArgs e)
        {
            click(sender, e);
        }
        private void pictureBox21_MouseDown(object sender, MouseEventArgs e)
        {
            click(sender, e);
        }
        private void pictureBox22_MouseDown(object sender, MouseEventArgs e)
        {
            click(sender, e);
        }
        private void pictureBox23_MouseDown(object sender, MouseEventArgs e)
        {
            click(sender, e);
        }
        private void pictureBox24_MouseDown(object sender, MouseEventArgs e)
        {
            click(sender, e);
        }
        private void pictureBox25_MouseDown(object sender, MouseEventArgs e)
        {
            click(sender, e);
        }
        private void pictureBox26_MouseDown(object sender, MouseEventArgs e)
        {
            click(sender, e);
        }
        private void pictureBox27_MouseDown(object sender, MouseEventArgs e)
        {
            click(sender, e);
        }
        private void pictureBox28_MouseDown(object sender, MouseEventArgs e)
        {
            click(sender, e);
        }
        private void pictureBox29_MouseDown(object sender, MouseEventArgs e)
        {
            click(sender, e);
        }
        private void pictureBox30_MouseDown(object sender, MouseEventArgs e)
        {
            click(sender, e);
        }
        private void pictureBox31_MouseDown(object sender, MouseEventArgs e)
        {
            click(sender, e);
        }
        private void pictureBox32_MouseDown(object sender, MouseEventArgs e)
        {
            click(sender, e);
        }
        private void pictureBox33_MouseDown(object sender, MouseEventArgs e)
        {
            click(sender, e);
        }
        private void pictureBox34_MouseDown(object sender, MouseEventArgs e)
        {
            click(sender, e);
        }
        private void pictureBox35_MouseDown(object sender, MouseEventArgs e)
        {
            click(sender, e);
        }
        private void pictureBox36_MouseDown(object sender, MouseEventArgs e)
        {
            click(sender, e);
        }
        private void pictureBox37_MouseDown(object sender, MouseEventArgs e)
        {
            click(sender, e);
        }
        private void pictureBox38_MouseDown(object sender, MouseEventArgs e)
        {
            click(sender, e);
        }
        private void pictureBox39_MouseDown(object sender, MouseEventArgs e)
        {
            click(sender, e);
        }
        private void pictureBox40_MouseDown(object sender, MouseEventArgs e)
        {
            click(sender, e);
        }
        private void pictureBox41_MouseDown(object sender, MouseEventArgs e)
        {
            click(sender, e);
        }
        private void pictureBox42_MouseDown(object sender, MouseEventArgs e)
        {
            click(sender, e);
        }
        private void pictureBox43_MouseDown(object sender, MouseEventArgs e)
        {
            click(sender, e);
        }
        private void pictureBox44_MouseDown(object sender, MouseEventArgs e)
        {
            click(sender, e);
        }
        private void pictureBox45_MouseDown(object sender, MouseEventArgs e)
        {
            click(sender, e);
        }
        private void pictureBox46_MouseDown(object sender, MouseEventArgs e)
        {
            click(sender, e);
        }
        private void pictureBox47_MouseDown(object sender, MouseEventArgs e)
        {
            click(sender, e);
        }
        private void pictureBox48_MouseDown(object sender, MouseEventArgs e)
        {
            click(sender, e);
        }
        private void pictureBox49_MouseDown(object sender, MouseEventArgs e)
        {
            click(sender, e);
        }
        private void pictureBox50_MouseDown(object sender, MouseEventArgs e)
        {
            click(sender, e);
        }
        private void pictureBox51_MouseDown(object sender, MouseEventArgs e)
        {
            click(sender, e);
        }
        private void pictureBox52_MouseDown(object sender, MouseEventArgs e)
        {
            click(sender, e);
        }
        private void pictureBox53_MouseDown(object sender, MouseEventArgs e)
        {
            click(sender, e);
        }
        private void pictureBox54_MouseDown(object sender, MouseEventArgs e)
        {
            click(sender, e);
        }
        private void pictureBox55_MouseDown(object sender, MouseEventArgs e)
        {
            click(sender, e);
        }
        private void pictureBox56_MouseDown(object sender, MouseEventArgs e)
        {
            click(sender, e);
        }
        private void pictureBox57_MouseDown(object sender, MouseEventArgs e)
        {
            click(sender, e);
        }
        private void pictureBox58_MouseDown(object sender, MouseEventArgs e)
        {
            click(sender, e);
        }
        private void pictureBox59_MouseDown(object sender, MouseEventArgs e)
        {
            click(sender, e);
        }
        private void pictureBox60_MouseDown(object sender, MouseEventArgs e)
        {
            click(sender, e);
        }
        private void pictureBox61_MouseDown(object sender, MouseEventArgs e)
        {
            click(sender, e);
        }
        private void pictureBox62_MouseDown(object sender, MouseEventArgs e)
        {
            click(sender, e);
        }
        private void pictureBox63_MouseDown(object sender, MouseEventArgs e)
        {
            click(sender, e);
        }
        private void pictureBox64_MouseDown(object sender, MouseEventArgs e)
        {
            click(sender, e);
        }
        private void pictureBox65_MouseDown(object sender, MouseEventArgs e)
        {
            click(sender, e);
        }

        #endregion


        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
