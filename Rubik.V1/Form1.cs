using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using AForge.Video;
using AForge.Video.DirectShow;

using Emgu.CV;
using Emgu.CV.UI;
using Emgu.Util;
using Emgu.CV.Structure;
using System.IO;

namespace Rubik.V1
{
    public partial class Form1 : Form
    {
        public char[][] Rubik = new char[6][] { new char[] {'B','B','B','B','B','B','B','B','B' },
                                                new char[] {'P','P', 'P', 'P', 'P', 'P', 'P', 'P', 'P'},
                                                new char[] { 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W'  },
                                                new char[] { 'R', 'R', 'R', 'R', 'R', 'R', 'R', 'R', 'R' },
                                                new char[] {'G', 'G' , 'G' , 'G', 'G' , 'G' , 'G' , 'G','G' },
                                                new char[] { 'Y', 'Y', 'Y', 'Y', 'Y', 'Y', 'Y', 'Y', 'Y' } };
        // public char[][] Rubik_copy = new char[6][];
        //public string Convert_RGB_to_Face(char Rubik);
        //Notebook
        private FilterInfoCollection webcam;
        private VideoCaptureDevice cam;
        int numTake = 1;
        Image<Hsv, Byte> R_Image;
        Image<Hsv, Byte> My_Image;
        String str = "";
        int numPicture = 1;
        int numList = 1;
        int Time,Delay=0;
        //time counter = new time();
        int NumTimer = 0;
        int min, sec, ssec;

        String Result ="", Face ="" ;
        

        // Test Commit
        // รูปต้นฉบับ
        // Image<Hsv, Byte> gray_image;   // รูปที่โดนเปลี่ยนเป็นแบบ GRY
        Image<Hsv, Byte> My_image_copy; // รูปที่ใช้แก้ใข 

        public Form1()
        {
           
            InitializeComponent();
            Text_Time.Text = min + " : " + sec + " : " + ssec;
           
        }



        private void Form1_Load(object sender, EventArgs e)
        {
            serialPort1.Open();
            webcam = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            foreach (FilterInfo VideoCaptureDevice in webcam)
            {
                comboBox1.Items.Add(VideoCaptureDevice.Name);
            }
            comboBox1.SelectedIndex = 0;

        }


        void cam_NewFrame(object sender, NewFrameEventArgs eventArgs)   // Finish
        {

            Bitmap bit = (Bitmap)eventArgs.Frame.Clone();
            My_Image = new Image<Hsv, byte>(bit);
            if (numTake == 1)
            {
                Box1.Image = bit;
                R_Image = My_Image.Copy();
               // My_Image1 = new Image<Hsv, byte>(bit);
            }
            
            else if (numTake == 2)
            {
                R_Image = My_Image.Copy();
                Box5.Image = bit;
               // My_Image1 = new Image<Hsv, byte>(new Bitmap(Box1.Image));
            }
            else if (numTake == 3)
            {
                R_Image = My_Image.Copy();
                Box2.Image = bit;
              //  My_Image1 = new Image<Hsv, byte>(new Bitmap(Box1.Image));
            }
            else if (numTake == 4)
            {
                R_Image = My_Image.Copy();
                Box4.Image = bit;
                //My_Image1 = new Image<Hsv, byte>(new Bitmap(Box1.Image));
            }
            else if (numTake == 5)
            {
                R_Image = My_Image.Copy();
                Box3.Image = bit;
              //  My_Image1 = new Image<Hsv, byte>(new Bitmap(Box1.Image));
            }
            else if (numTake == 6)
            {
                R_Image = My_Image.Copy();
                Box6.Image = bit;
                //My_Image1 = new Image<Hsv, byte>(new Bitmap(Box1.Image));
            }

        }

        private void Start_Click_1(object sender, EventArgs e) // เปิดกล้องงงงงงงง // Finish
        {
            cam = new VideoCaptureDevice(webcam[comboBox1.SelectedIndex].MonikerString);
            cam.NewFrame += new NewFrameEventHandler(cam_NewFrame);
            cam.Start();
            label2.Text = "Cam_" + numTake + " Running";


        }

        private void Take_Click_1(object sender, EventArgs e)  // Finish
        {
            // My_Image = new Image<Hsv, Byte>();
            //read_color(numTake-1);
            if (numTake == 1)
            {

                //Box1.Image = R_Image.ToBitmap();
                read_color(1 - 1);
            }
            if (numTake == 2)
            {

                //Box5.Image = R_Image.ToBitmap();
                read_color(5 - 1);
            }
            if (numTake == 3)
            {

                //Box2.Image = R_Image.ToBitmap();
                read_color(2 - 1);
            }
            if (numTake == 4)
            {

                //Box4.Image = R_Image.ToBitmap();
                read_color(4 - 1);
            }
            if (numTake == 5)
            {

                //Box3.Image = R_Image.ToBitmap();
                read_color(3 - 1);
            }
            if (numTake == 6)
            {

                //Box6.Image = R_Image.ToBitmap();
                read_color(6 - 1);
            }


            numTake++;
            timer1.Enabled = true;
            //  String str = "";
            
            timer1.Start();

            label2.Text = "Cam_" + numTake + " Running";

            show_color(Rubik);

            
                 Text_face.Text = showRubik(Rubik);
            Face = showRubik(Rubik);
            //  saveFileDialog1.InitialDirectory = @"d:\picture";
            //saveFileDialog1.FileName = str+i;
            //  if (DialogResult.OK == saveFileDialog1.ShowDialog())
            // {
            //         Box1.Image.Save(saveFileDialog1.FileName);
            //  }
            // int a = numTake - 2;
            // pictureBox1.Image = R_Image.ToBitmap();

            timer1.Stop();
            //numTake++;
            if (numTake == 7)
            {
                label2.Text = " Complete";
                //timer2.Stop();
               
            }
            cam.Start();
        }


        private void Stop_Click(object sender, EventArgs e) // Finish
        {
            if (cam.IsRunning)
            {
                cam.Stop();
            }

        }













     


        public void read_color(int Face) // อ่านสี ภาพ 1 ภาพ // Finish
        {
            int[,] col = new int[9, 3];
            int i = 0, j = 0;
            int A = 0, B = 0;
            int roll, collum;
            float sumHue = 0, sumSatuation = 0, sumValue = 0;
            //ดึงค่าใส่ในตัวแปร

            for (int y = 110; y < 360; y += 120)    // แกน YYYYYYYYYYY
            {

                for (int x = 220; x < 470; x += 120)   // แกน XXXXXXXXXXXXX
                {
                    collum = y - 5;
                    roll = x - 5;
                    sumHue = 0;
                    sumSatuation = 0;
                    sumValue = 0;
                    for (A = 0; A < 10; A++)
                    {
                        for (B = 0; B < 10; B++)
                        {
                            sumHue += (float)(0.01 * R_Image[collum, roll].Hue);
                            sumSatuation += (float)(0.01 * R_Image[collum, roll].Satuation);
                            sumValue += (float)(0.01 * R_Image[collum, roll].Value);
                        }
                    }
                    // label10.Text = "sumB = " + (int)((sumB / 100) * 100);
                    // label11.Text = "sumG = " + (int)((sumG / 100) * 100);
                    // label12.Text = "sumG = " + (int)((sumR / 100) * 100);



                    for (j = 0; j < 3; j++)
                    {

                        if (j == 0)
                            col[i, j] = (int)((sumHue / 100) * 100);
                        else if (j == 1)
                            col[i, j] = (int)((sumSatuation / 100) * 100);
                        else if (j == 2)
                            col[i, j] = (int)((sumValue / 100) * 100);
                        /*
                        if (j == 0)
                            col[i, j] = My_Image[y, x].Blue;
                        else if (j == 1)
                            col[i, j] = My_Image[y, x].Green;
                        else if (j == 2)
                            col[i, j] = My_Image[y, x].Red;
                           */

                    }


                    i++;
                }
            }
            /*
            label10.Text = "sumB = " + sumB;
            label11.Text = "sumG = " + sumG;
            label12.Text = "sumG = " + sumR;
            */
            //set_label(col);
            double H, g, r;
            for (i = 0; i < 9; i++)
            {
                H = col[i, 0];
                g = col[i, 1];
                r = col[i, 2];
                if (H > 87 && H < 99)    // white
                {
                    Rubik[Face][i] = 'W';
                }

                if (H > 99 && H < 115)   //blue
                {
                    Rubik[Face][i] = 'B';
                }

                if (H > 140 && H < 160)   // ม่วง
                {
                    Rubik[Face][i] = 'P';
                }

                if (H >= 62 && H < 83)   //Green
                {
                    Rubik[Face][i] = 'G';
                }

                if (H > 28 && H < 49) //yellow
                {
                    Rubik[Face][i] = 'Y';
                }

                if ((H > 162 && H < 180) || (H >= 0 && H < 13)) // RED
                {
                    Rubik[Face][i] = 'R';
                }

            }






        }            //



       

      


        ///////////////////////////////// คำสั่งที่ใช้หมุนลูกรูบิค/////////////////////////////////////


        public void rotate_left(int arrow) //Left // Finish
        {
            if (arrow == 0)
            {
                rotate_Face(Rubik, 1, 0);

                char[] tmp = new char[3];
                tmp[0] = Rubik[0][0]; tmp[1] = Rubik[0][3]; ; tmp[2] = Rubik[0][6];
                Rubik[0][0] = Rubik[2][0]; Rubik[0][3] = Rubik[2][3]; Rubik[0][6] = Rubik[2][6];
                Rubik[2][0] = Rubik[4][0]; Rubik[2][3] = Rubik[4][3]; Rubik[2][6] = Rubik[4][6];
                Rubik[4][0] = Rubik[5][0]; Rubik[4][3] = Rubik[5][3]; Rubik[4][6] = Rubik[5][6];
                Rubik[5][0] = tmp[0]; Rubik[5][3] = tmp[1]; Rubik[5][6] = tmp[2];
            }
            else if(arrow == 1)
            {
                rotate_Face(Rubik, 1, 1);

                char[] tmp = new char[3];

                tmp[0] = Rubik[5][0]; tmp[1] = Rubik[5][3]; tmp[2] = Rubik[5][6];

                 Rubik[5][0]= Rubik[4][0] ;  Rubik[5][3] = Rubik[4][3];  Rubik[5][6] = Rubik[4][6];
              
                 Rubik[4][0] = Rubik[2][0] ;  Rubik[4][3] = Rubik[2][3]; Rubik[4][6] = Rubik[2][6];
                 Rubik[2][0] = Rubik[0][0] ; Rubik[2][3] = Rubik[0][3];  Rubik[2][6] = Rubik[0][6] ;

                Rubik[0][0] = tmp[0]; Rubik[0][3] = tmp[1]; Rubik[0][6] = tmp[2];
               

            }

        }

        public void rotate_right(int arrow) // Right // Finish
        {
            if (arrow == 0)
            {
                rotate_Face(Rubik, 3, 0);

                char[] tmp = new char[3];
                tmp[0] = Rubik[0][2]; tmp[1] = Rubik[0][5]; ; tmp[2] = Rubik[0][8];
                Rubik[0][2] = Rubik[5][2]; Rubik[0][5] = Rubik[5][5]; Rubik[0][8] = Rubik[5][8];
                Rubik[5][2] = Rubik[4][2]; Rubik[5][5] = Rubik[4][5]; Rubik[5][8] = Rubik[4][8];
                Rubik[4][2] = Rubik[2][2]; Rubik[4][5] = Rubik[2][5]; Rubik[4][8] = Rubik[2][8];
                Rubik[2][2] = tmp[0]; Rubik[2][5] = tmp[1]; Rubik[2][8] = tmp[2];
            }
            else if(arrow == 1)
            {
                rotate_Face(Rubik, 3, 1);

                char[] tmp = new char[3];
                tmp[0] = Rubik[2][2]; tmp[1] = Rubik[2][5]; tmp[2] = Rubik[2][8];

                Rubik[2][2] = Rubik[4][2]; Rubik[2][5] = Rubik[4][5]; Rubik[2][8] = Rubik[4][8];
                 Rubik[4][2] = Rubik[5][2]; Rubik[4][5] = Rubik[5][5];  Rubik[4][8] = Rubik[5][8];
                Rubik[5][2] = Rubik[0][2]; Rubik[5][5] = Rubik[0][5]; Rubik[5][8] = Rubik[0][8];

                Rubik[0][2] = tmp[0]; Rubik[0][5] = tmp[1]; ; Rubik[0][8] = tmp[2];
                

            }
        }

        public void rotate_top(int arrow) // Top or UP // //
        {
            if (arrow == 0)
            {
                rotate_Face(Rubik, 2, 0);

                char[] tmp = new char[3];
                tmp[0] = Rubik[0][6]; tmp[1] = Rubik[0][7]; ; tmp[2] = Rubik[0][8];
                Rubik[0][6] = Rubik[3][0]; Rubik[0][7] = Rubik[3][3]; Rubik[0][8] = Rubik[3][6];
                Rubik[3][0] = Rubik[4][2]; Rubik[3][3] = Rubik[4][1]; Rubik[3][6] = Rubik[4][0];
                Rubik[4][2] = Rubik[1][8]; Rubik[4][1] = Rubik[1][5]; Rubik[4][0] = Rubik[1][2];
                Rubik[1][8] = tmp[0]; Rubik[1][5] = tmp[1]; Rubik[1][2] = tmp[2];
            }
            else if(arrow == 1)
            {
                rotate_Face(Rubik, 2, 1);

                char[] tmp = new char[3];
                tmp[0] = Rubik[1][8]; tmp[1] = Rubik[1][5]; tmp[2] = Rubik[1][2];
               
                Rubik[1][8] = Rubik[4][2]; Rubik[1][5] = Rubik[4][1]; Rubik[1][2] = Rubik[4][0];
               
                 Rubik[4][2] = Rubik[3][0];  Rubik[4][1] = Rubik[3][3];  Rubik[4][0] = Rubik[3][6];
                Rubik[3][0] = Rubik[0][6]; Rubik[3][3] = Rubik[0][7]; Rubik[3][6] = Rubik[0][8];

                Rubik[0][6] = tmp[0]; Rubik[0][7] = tmp[1]; ; Rubik[0][8] = tmp[2];

            }

        }

        public void rotate_front(int arrow) // Front // Finish
        {
            if (arrow == 0)
            {
                rotate_Face(Rubik, 4, 0);
                char[] tmp = new char[3];
                tmp[0] = Rubik[2][6]; tmp[1] = Rubik[2][7]; ; tmp[2] = Rubik[2][8];

                Rubik[2][6] = Rubik[3][6]; Rubik[2][7] = Rubik[3][7]; Rubik[2][8] = Rubik[3][8];
                Rubik[3][6] = Rubik[5][2]; Rubik[3][7] = Rubik[5][1]; Rubik[3][8] = Rubik[5][0];
                Rubik[5][2] = Rubik[1][6]; Rubik[5][1] = Rubik[1][7]; Rubik[5][0] = Rubik[1][8];

                Rubik[1][6] = tmp[0]; Rubik[1][7] = tmp[1]; Rubik[1][8] = tmp[2];
            }
            else if( arrow ==1)
            {
                rotate_Face(Rubik, 4, 1);
                char[] tmp = new char[3];
                tmp[0] = Rubik[1][6]; tmp[1] = Rubik[1][7]; tmp[2] = Rubik[1][8];
                Rubik[1][6] = Rubik[5][2]; Rubik[1][7] = Rubik[5][1]; Rubik[1][8] = Rubik[5][0];
                Rubik[5][2] = Rubik[3][6]; Rubik[5][1] = Rubik[3][7]; Rubik[5][0] = Rubik[3][8];
                Rubik[3][6] = Rubik[2][6]; Rubik[3][7] = Rubik[2][7]; Rubik[3][8] = Rubik[2][8];
                Rubik[2][6] = tmp[0] ; Rubik[2][7] = tmp[1] ; ; Rubik[2][8] = tmp[2] ;
               
                
                 
                
                
            }


        }

        public void rotate_rear(int arrow) // Back // Finish
        {
            if (arrow == 0){
            rotate_Face(Rubik, 0, 0);

            char[] tmp = new char[3];

            tmp[0] = Rubik[5][6]; tmp[1] = Rubik[5][7]; ; tmp[2] = Rubik[5][8];

            Rubik[5][6] = Rubik[3][2]; Rubik[5][7] = Rubik[3][1]; Rubik[5][8] = Rubik[3][0];
            Rubik[3][2] = Rubik[2][2]; Rubik[3][1] = Rubik[2][1]; Rubik[3][0] = Rubik[2][0];
            Rubik[2][2] = Rubik[1][2]; Rubik[2][1] = Rubik[1][1]; Rubik[2][0] = Rubik[1][0];

            Rubik[1][2] = tmp[0]; Rubik[1][1] = tmp[1]; Rubik[1][0] = tmp[2];
            }
            else if(arrow == 1)
            {
                rotate_Face(Rubik, 0, 1);

                char[] tmp = new char[3];
                tmp[0] = Rubik[1][2]; tmp[1] = Rubik[1][1]; tmp[2] = Rubik[1][0];
                Rubik[1][2] = Rubik[2][2];  Rubik[1][1]= Rubik[2][1] ; Rubik[1][0] = Rubik[2][0] ;
                Rubik[2][2] = Rubik[3][2];  Rubik[2][1] = Rubik[3][1] ;  Rubik[2][0] = Rubik[3][0] ;
                Rubik[3][2] = Rubik[5][6] ;  Rubik[3][1] = Rubik[5][7] ;  Rubik[3][0] = Rubik[5][8] ;
               
              
                Rubik[5][6] = tmp[0]; Rubik[5][7] = tmp[1]; ; Rubik[5][8] = tmp[2];
            }

        }

        public void rotate_down(int arrow)
        {
            if (arrow == 0)
            {
                rotate_Face(Rubik, 5, 0);

                char[] tmp = new char[3];
                tmp[0] = Rubik[4][6]; tmp[1] = Rubik[4][7]; ; tmp[2] = Rubik[4][8];
                Rubik[4][6] = Rubik[3][8]; Rubik[4][7] = Rubik[3][5]; Rubik[4][8] = Rubik[3][2];
                Rubik[3][2] = Rubik[0][0]; Rubik[3][5] = Rubik[0][1]; Rubik[3][8] = Rubik[0][2];
                Rubik[0][0] = Rubik[1][6]; Rubik[0][1] = Rubik[1][3]; Rubik[0][2] = Rubik[1][0];
                Rubik[1][0] = tmp[0]; Rubik[1][3] = tmp[1]; Rubik[1][6] = tmp[2];
            }
            else if(arrow == 1)
            {
                rotate_Face(Rubik, 5,1);

                char[] tmp = new char[3];

                tmp[0] = Rubik[1][0]; tmp[1] = Rubik[1][3]; tmp[2] = Rubik[1][6];

                Rubik[1][6] = Rubik[0][0];  Rubik[1][3] = Rubik[0][1]; Rubik[1][0] = Rubik[0][2];
                Rubik[0][0] = Rubik[3][2]; Rubik[0][1] = Rubik[3][5];  Rubik[0][2] = Rubik[3][8];
                Rubik[3][8] = Rubik[4][6];  Rubik[3][5] = Rubik[4][7];  Rubik[3][2] = Rubik[4][8];

                Rubik[4][6] = tmp[0]; Rubik[4][7] = tmp[1]; Rubik[4][8] = tmp[2];
               
            }

        }

        


        public void rotate_Face(char[][] Rc,int F,int arrow) //ไว้หมุนหน้าทุกด้าน // Finish
        {
            if (arrow == 0)  // หมุนทวน
            {
                char M = Rc[F][1];

                Rc[F][1] = Rc[F][5];
                Rc[F][5] = Rc[F][7];
                Rc[F][7] = Rc[F][3];
                Rc[F][3] = M;

                char C = Rc[F][0];

                Rc[F][0] = Rc[F][2];
                Rc[F][2] = Rc[F][8];
                Rc[F][8] = Rc[F][6];
                Rc[F][6] = C;
            }
            else if(arrow == 1)  // หมุนตามเข็ม
            {
                char M = Rc[F][3];

                Rc[F][3] = Rc[F][7];
                Rc[F][7] = Rc[F][5];
                Rc[F][5] = Rc[F][1];
                Rc[F][1] = M;

                char C = Rc[F][6];

                Rc[F][6] = Rc[F][8];
                Rc[F][8] = Rc[F][2];
                Rc[F][2] = Rc[F][0];
                Rc[F][0] = C;
            }

           
        }


        public void rotate_Face_right(char[][] Rc, int F)
        {
            char M = Rc[F][1];

            Rc[F][1] = Rc[F][3];
            Rc[F][3] = Rc[F][7];
            Rc[F][7] = Rc[F][5];
            Rc[F][5] = M;

            char C = Rc[F][0];

            Rc[F][0] = Rc[F][6];
            Rc[F][6] = Rc[F][8];
            Rc[F][8] = Rc[F][2];
            Rc[F][2] = C;
        }





        public void show_color(char[][] rk) // แสดงค่าสีบน panel // Finish
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 9; j++)
                {

                    if (i == 0) // Zone AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
                    {
                        if (rk[i][j] == 'P')
                        {//สีม่วง
                            if (j == 0)
                                A0.BackColor = Color.Purple;
                            else if (j == 1)
                                A1.BackColor = Color.Purple;
                            else if (j == 2)
                                A2.BackColor = Color.Purple;
                            else if (j == 3)
                                A3.BackColor = Color.Purple;
                            else if (j == 4)
                                A4.BackColor = Color.Purple;
                            else if (j == 5)
                                A5.BackColor = Color.Purple;
                            else if (j == 6)
                                A6.BackColor = Color.Purple;
                            else if (j == 7)
                                A7.BackColor = Color.Purple;
                            else if (j == 8)
                                A8.BackColor = Color.Purple;
                        }


                        else if (rk[i][j] == 'B')   //สีน้ำเงิน
                        {
                            if (j == 0)
                                A0.BackColor = Color.Blue;
                            else if (j == 1)
                                A1.BackColor = Color.Blue;
                            else if (j == 2)
                                A2.BackColor = Color.Blue;
                            else if (j == 3)
                                A3.BackColor = Color.Blue;
                            else if (j == 4)
                                A4.BackColor = Color.Blue;
                            else if (j == 5)
                                A5.BackColor = Color.Blue;
                            else if (j == 6)
                                A6.BackColor = Color.Blue;
                            else if (j == 7)
                                A7.BackColor = Color.Blue;
                            else if (j == 8)
                                A8.BackColor = Color.Blue;
                        }

                        else if (rk[i][j] == 'Y')   // สีเหลือง
                        {
                            if (j == 0)
                                A0.BackColor = Color.Yellow;
                            else if (j == 1)
                                A1.BackColor = Color.Yellow;
                            else if (j == 2)
                                A2.BackColor = Color.Yellow;
                            else if (j == 3)
                                A3.BackColor = Color.Yellow;
                            else if (j == 4)
                                A4.BackColor = Color.Yellow;
                            else if (j == 5)
                                A5.BackColor = Color.Yellow;
                            else if (j == 6)
                                A6.BackColor = Color.Yellow;
                            else if (j == 7)
                                A7.BackColor = Color.Yellow;
                            else if (j == 8)
                                A8.BackColor = Color.Yellow;
                        }

                        else if (rk[i][j] == 'G')   //สีเขียว
                        {
                            if (j == 0)
                                A0.BackColor = Color.Green;
                            else if (j == 1)
                                A1.BackColor = Color.Green;
                            else if (j == 2)
                                A2.BackColor = Color.Green;
                            else if (j == 3)
                                A3.BackColor = Color.Green;
                            else if (j == 4)
                                A4.BackColor = Color.Green;
                            else if (j == 5)
                                A5.BackColor = Color.Green;
                            else if (j == 6)
                                A6.BackColor = Color.Green;
                            else if (j == 7)
                                A7.BackColor = Color.Green;
                            else if (j == 8)
                                A8.BackColor = Color.Green;
                        }

                        else if (rk[i][j] == 'R') //สีแดง
                        {
                            if (j == 0)
                                A0.BackColor = Color.Red;
                            else if (j == 1)
                                A1.BackColor = Color.Red;
                            else if (j == 2)
                                A2.BackColor = Color.Red;
                            else if (j == 3)
                                A3.BackColor = Color.Red;
                            else if (j == 4)
                                A4.BackColor = Color.Red;
                            else if (j == 5)
                                A5.BackColor = Color.Red;
                            else if (j == 6)
                                A6.BackColor = Color.Red;
                            else if (j == 7)
                                A7.BackColor = Color.Red;
                            else if (j == 8)
                                A8.BackColor = Color.Red;
                        }
                        else if (rk[i][j] == 'W') //สีขาว
                        {
                            if (j == 0)
                                A0.BackColor = Color.White;
                            else if (j == 1)
                                A1.BackColor = Color.White;
                            else if (j == 2)
                                A2.BackColor = Color.White;
                            else if (j == 3)
                                A3.BackColor = Color.White;
                            else if (j == 4)
                                A4.BackColor = Color.White;
                            else if (j == 5)
                                A5.BackColor = Color.White;
                            else if (j == 6)
                                A6.BackColor = Color.White;
                            else if (j == 7)
                                A7.BackColor = Color.White;
                            else if (j == 8)
                                A8.BackColor = Color.White;

                        }
                    }


                    if (i == 1) // Zone BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB
                    {
                        if (rk[i][j] == 'P')
                        {//สีม่วง
                            if (j == 0)
                                B0.BackColor = Color.Purple;
                            else if (j == 1)
                                B1.BackColor = Color.Purple;
                            else if (j == 2)
                                B2.BackColor = Color.Purple;
                            else if (j == 3)
                                B3.BackColor = Color.Purple;
                            else if (j == 4)
                                B4.BackColor = Color.Purple;
                            else if (j == 5)
                                B5.BackColor = Color.Purple;
                            else if (j == 6)
                                B6.BackColor = Color.Purple;
                            else if (j == 7)
                                B7.BackColor = Color.Purple;
                            else if (j == 8)
                                B8.BackColor = Color.Purple;
                        }


                        else if (rk[i][j] == 'B')   //สีน้ำเงิน
                        {
                            if (j == 0)
                                B0.BackColor = Color.Blue;
                            else if (j == 1)
                                B1.BackColor = Color.Blue;
                            else if (j == 2)
                                B2.BackColor = Color.Blue;
                            else if (j == 3)
                                B3.BackColor = Color.Blue;
                            else if (j == 4)
                                B4.BackColor = Color.Blue;
                            else if (j == 5)
                                B5.BackColor = Color.Blue;
                            else if (j == 6)
                                B6.BackColor = Color.Blue;
                            else if (j == 7)
                                B7.BackColor = Color.Blue;
                            else if (j == 8)
                                B8.BackColor = Color.Blue;
                        }

                        else if (rk[i][j] == 'Y')   // สีเหลือง
                        {
                            if (j == 0)
                                B0.BackColor = Color.Yellow;
                            else if (j == 1)
                                B1.BackColor = Color.Yellow;
                            else if (j == 2)
                                B2.BackColor = Color.Yellow;
                            else if (j == 3)
                                B3.BackColor = Color.Yellow;
                            else if (j == 4)
                                B4.BackColor = Color.Yellow;
                            else if (j == 5)
                                B5.BackColor = Color.Yellow;
                            else if (j == 6)
                                B6.BackColor = Color.Yellow;
                            else if (j == 7)
                                B7.BackColor = Color.Yellow;
                            else if (j == 8)
                                B8.BackColor = Color.Yellow;
                        }

                        else if (rk[i][j] == 'G')   //สีเขียว
                        {
                            if (j == 0)
                                B0.BackColor = Color.Green;
                            else if (j == 1)
                                B1.BackColor = Color.Green;
                            else if (j == 2)
                                B2.BackColor = Color.Green;
                            else if (j == 3)
                                B3.BackColor = Color.Green;
                            else if (j == 4)
                                B4.BackColor = Color.Green;
                            else if (j == 5)
                                B5.BackColor = Color.Green;
                            else if (j == 6)
                                B6.BackColor = Color.Green;
                            else if (j == 7)
                                B7.BackColor = Color.Green;
                            else if (j == 8)
                                B8.BackColor = Color.Green;
                        }

                        else if (rk[i][j] == 'R') //สีแดง
                        {
                            if (j == 0)
                                B0.BackColor = Color.Red;
                            else if (j == 1)
                                B1.BackColor = Color.Red;
                            else if (j == 2)
                                B2.BackColor = Color.Red;
                            else if (j == 3)
                                B3.BackColor = Color.Red;
                            else if (j == 4)
                                B4.BackColor = Color.Red;
                            else if (j == 5)
                                B5.BackColor = Color.Red;
                            else if (j == 6)
                                B6.BackColor = Color.Red;
                            else if (j == 7)
                                B7.BackColor = Color.Red;
                            else if (j == 8)
                                B8.BackColor = Color.Red;
                        }
                        else if (rk[i][j] == 'W') //สีขาว
                        {
                            if (j == 0)
                                B0.BackColor = Color.White;
                            else if (j == 1)
                                B1.BackColor = Color.White;
                            else if (j == 2)
                                B2.BackColor = Color.White;
                            else if (j == 3)
                                B3.BackColor = Color.White;
                            else if (j == 4)
                                B4.BackColor = Color.White;
                            else if (j == 5)
                                B5.BackColor = Color.White;
                            else if (j == 6)
                                B6.BackColor = Color.White;
                            else if (j == 7)
                                B7.BackColor = Color.White;
                            else if (j == 8)
                                B8.BackColor = Color.White;

                        }
                    }


                    if (i == 2) // Zone CCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCCC
                    {
                        if (rk[i][j] == 'P')
                        {//สีม่วง
                            if (j == 0)
                                C0.BackColor = Color.Purple;
                            else if (j == 1)
                                C1.BackColor = Color.Purple;
                            else if (j == 2)
                                C2.BackColor = Color.Purple;
                            else if (j == 3)
                                C3.BackColor = Color.Purple;
                            else if (j == 4)
                                C4.BackColor = Color.Purple;
                            else if (j == 5)
                                C5.BackColor = Color.Purple;
                            else if (j == 6)
                                C6.BackColor = Color.Purple;
                            else if (j == 7)
                                C7.BackColor = Color.Purple;
                            else if (j == 8)
                                C8.BackColor = Color.Purple;
                        }


                        else if (rk[i][j] == 'B')   //สีน้ำเงิน
                        {
                            if (j == 0)
                                C0.BackColor = Color.Blue;
                            else if (j == 1)
                                C1.BackColor = Color.Blue;
                            else if (j == 2)
                                C2.BackColor = Color.Blue;
                            else if (j == 3)
                                C3.BackColor = Color.Blue;
                            else if (j == 4)
                                C4.BackColor = Color.Blue;
                            else if (j == 5)
                                C5.BackColor = Color.Blue;
                            else if (j == 6)
                                C6.BackColor = Color.Blue;
                            else if (j == 7)
                                C7.BackColor = Color.Blue;
                            else if (j == 8)
                                C8.BackColor = Color.Blue;
                        }

                        else if (rk[i][j] == 'Y')   // สีเหลือง
                        {
                            if (j == 0)
                                C0.BackColor = Color.Yellow;
                            else if (j == 1)
                                C1.BackColor = Color.Yellow;
                            else if (j == 2)
                                C2.BackColor = Color.Yellow;
                            else if (j == 3)
                                C3.BackColor = Color.Yellow;
                            else if (j == 4)
                                C4.BackColor = Color.Yellow;
                            else if (j == 5)
                                C5.BackColor = Color.Yellow;
                            else if (j == 6)
                                C6.BackColor = Color.Yellow;
                            else if (j == 7)
                                C7.BackColor = Color.Yellow;
                            else if (j == 8)
                                C8.BackColor = Color.Yellow;
                        }

                        else if (rk[i][j] == 'G')   //สีเขียว
                        {
                            if (j == 0)
                                C0.BackColor = Color.Green;
                            else if (j == 1)
                                C1.BackColor = Color.Green;
                            else if (j == 2)
                                C2.BackColor = Color.Green;
                            else if (j == 3)
                                C3.BackColor = Color.Green;
                            else if (j == 4)
                                C4.BackColor = Color.Green;
                            else if (j == 5)
                                C5.BackColor = Color.Green;
                            else if (j == 6)
                                C6.BackColor = Color.Green;
                            else if (j == 7)
                                C7.BackColor = Color.Green;
                            else if (j == 8)
                                C8.BackColor = Color.Green;
                        }

                        else if (rk[i][j] == 'R') //สีแดง
                        {
                            if (j == 0)
                                C0.BackColor = Color.Red;
                            else if (j == 1)
                                C1.BackColor = Color.Red;
                            else if (j == 2)
                                C2.BackColor = Color.Red;
                            else if (j == 3)
                                C3.BackColor = Color.Red;
                            else if (j == 4)
                                C4.BackColor = Color.Red;
                            else if (j == 5)
                                C5.BackColor = Color.Red;
                            else if (j == 6)
                                C6.BackColor = Color.Red;
                            else if (j == 7)
                                C7.BackColor = Color.Red;
                            else if (j == 8)
                                C8.BackColor = Color.Red;
                        }
                        else if (rk[i][j] == 'W') //สีขาว
                        {
                            if (j == 0)
                                C0.BackColor = Color.White;
                            else if (j == 1)
                                C1.BackColor = Color.White;
                            else if (j == 2)
                                C2.BackColor = Color.White;
                            else if (j == 3)
                                C3.BackColor = Color.White;
                            else if (j == 4)
                                C4.BackColor = Color.White;
                            else if (j == 5)
                                C5.BackColor = Color.White;
                            else if (j == 6)
                                C6.BackColor = Color.White;
                            else if (j == 7)
                                C7.BackColor = Color.White;
                            else if (j == 8)
                                C8.BackColor = Color.White;

                        }
                    }


                    if (i == 3) // Zone DDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDDD
                    {
                        if (rk[i][j] == 'P')
                        {//สีม่วง
                            if (j == 0)
                                D0.BackColor = Color.Purple;
                            else if (j == 1)
                                D1.BackColor = Color.Purple;
                            else if (j == 2)
                                D2.BackColor = Color.Purple;
                            else if (j == 3)
                                D3.BackColor = Color.Purple;
                            else if (j == 4)
                                D4.BackColor = Color.Purple;
                            else if (j == 5)
                                D5.BackColor = Color.Purple;
                            else if (j == 6)
                                D6.BackColor = Color.Purple;
                            else if (j == 7)
                                D7.BackColor = Color.Purple;
                            else if (j == 8)
                                D8.BackColor = Color.Purple;
                        }


                        else if (rk[i][j] == 'B')   //สีน้ำเงิน
                        {
                            if (j == 0)
                                D0.BackColor = Color.Blue;
                            else if (j == 1)
                                D1.BackColor = Color.Blue;
                            else if (j == 2)
                                D2.BackColor = Color.Blue;
                            else if (j == 3)
                                D3.BackColor = Color.Blue;
                            else if (j == 4)
                                D4.BackColor = Color.Blue;
                            else if (j == 5)
                                D5.BackColor = Color.Blue;
                            else if (j == 6)
                                D6.BackColor = Color.Blue;
                            else if (j == 7)
                                D7.BackColor = Color.Blue;
                            else if (j == 8)
                                D8.BackColor = Color.Blue;
                        }

                        else if (rk[i][j] == 'Y')   // สีเหลือง
                        {
                            if (j == 0)
                                D0.BackColor = Color.Yellow;
                            else if (j == 1)
                                D1.BackColor = Color.Yellow;
                            else if (j == 2)
                                D2.BackColor = Color.Yellow;
                            else if (j == 3)
                                D3.BackColor = Color.Yellow;
                            else if (j == 4)
                                D4.BackColor = Color.Yellow;
                            else if (j == 5)
                                D5.BackColor = Color.Yellow;
                            else if (j == 6)
                                D6.BackColor = Color.Yellow;
                            else if (j == 7)
                                D7.BackColor = Color.Yellow;
                            else if (j == 8)
                                D8.BackColor = Color.Yellow;
                        }

                        else if (rk[i][j] == 'G')   //สีเขียว
                        {
                            if (j == 0)
                                D0.BackColor = Color.Green;
                            else if (j == 1)
                                D1.BackColor = Color.Green;
                            else if (j == 2)
                                D2.BackColor = Color.Green;
                            else if (j == 3)
                                D3.BackColor = Color.Green;
                            else if (j == 4)
                                D4.BackColor = Color.Green;
                            else if (j == 5)
                                D5.BackColor = Color.Green;
                            else if (j == 6)
                                D6.BackColor = Color.Green;
                            else if (j == 7)
                                D7.BackColor = Color.Green;
                            else if (j == 8)
                                D8.BackColor = Color.Green;
                        }

                        else if (rk[i][j] == 'R') //สีแดง
                        {
                            if (j == 0)
                                D0.BackColor = Color.Red;
                            else if (j == 1)
                                D1.BackColor = Color.Red;
                            else if (j == 2)
                                D2.BackColor = Color.Red;
                            else if (j == 3)
                                D3.BackColor = Color.Red;
                            else if (j == 4)
                                D4.BackColor = Color.Red;
                            else if (j == 5)
                                D5.BackColor = Color.Red;
                            else if (j == 6)
                                D6.BackColor = Color.Red;
                            else if (j == 7)
                                D7.BackColor = Color.Red;
                            else if (j == 8)
                                D8.BackColor = Color.Red;
                        }
                        else if (rk[i][j] == 'W') //สีขาว
                        {
                            if (j == 0)
                                D0.BackColor = Color.White;
                            else if (j == 1)
                                D1.BackColor = Color.White;
                            else if (j == 2)
                                D2.BackColor = Color.White;
                            else if (j == 3)
                                D3.BackColor = Color.White;
                            else if (j == 4)
                                D4.BackColor = Color.White;
                            else if (j == 5)
                                D5.BackColor = Color.White;
                            else if (j == 6)
                                D6.BackColor = Color.White;
                            else if (j == 7)
                                D7.BackColor = Color.White;
                            else if (j == 8)
                                D8.BackColor = Color.White;

                        }
                    }

                    if (i == 4) // Zone E
                    {
                        if (rk[i][j] == 'P')
                        {//สีม่วง
                            if (j == 0)
                                E0.BackColor = Color.Purple;
                            else if (j == 1)
                                E1.BackColor = Color.Purple;
                            else if (j == 2)
                                E2.BackColor = Color.Purple;
                            else if (j == 3)
                                E3.BackColor = Color.Purple;
                            else if (j == 4)
                                E4.BackColor = Color.Purple;
                            else if (j == 5)
                                E5.BackColor = Color.Purple;
                            else if (j == 6)
                                E6.BackColor = Color.Purple;
                            else if (j == 7)
                                E7.BackColor = Color.Purple;
                            else if (j == 8)
                                E8.BackColor = Color.Purple;
                        }


                        else if (rk[i][j] == 'B')   //สีน้ำเงิน
                        {
                            if (j == 0)
                                E0.BackColor = Color.Blue;
                            else if (j == 1)
                                E1.BackColor = Color.Blue;
                            else if (j == 2)
                                E2.BackColor = Color.Blue;
                            else if (j == 3)
                                E3.BackColor = Color.Blue;
                            else if (j == 4)
                                E4.BackColor = Color.Blue;
                            else if (j == 5)
                                E5.BackColor = Color.Blue;
                            else if (j == 6)
                                E6.BackColor = Color.Blue;
                            else if (j == 7)
                                E7.BackColor = Color.Blue;
                            else if (j == 8)
                                E8.BackColor = Color.Blue;
                        }

                        else if (rk[i][j] == 'Y')   // สีเหลือง
                        {
                            if (j == 0)
                                E0.BackColor = Color.Yellow;
                            else if (j == 1)
                                E1.BackColor = Color.Yellow;
                            else if (j == 2)
                                E2.BackColor = Color.Yellow;
                            else if (j == 3)
                                E3.BackColor = Color.Yellow;
                            else if (j == 4)
                                E4.BackColor = Color.Yellow;
                            else if (j == 5)
                                E5.BackColor = Color.Yellow;
                            else if (j == 6)
                                E6.BackColor = Color.Yellow;
                            else if (j == 7)
                                E7.BackColor = Color.Yellow;
                            else if (j == 8)
                                E8.BackColor = Color.Yellow;
                        }

                        else if (rk[i][j] == 'G')   //สีเขียว
                        {
                            if (j == 0)
                                E0.BackColor = Color.Green;
                            else if (j == 1)
                                E1.BackColor = Color.Green;
                            else if (j == 2)
                                E2.BackColor = Color.Green;
                            else if (j == 3)
                                E3.BackColor = Color.Green;
                            else if (j == 4)
                                E4.BackColor = Color.Green;
                            else if (j == 5)
                                E5.BackColor = Color.Green;
                            else if (j == 6)
                                E6.BackColor = Color.Green;
                            else if (j == 7)
                                E7.BackColor = Color.Green;
                            else if (j == 8)
                                E8.BackColor = Color.Green;
                        }

                        else if (rk[i][j] == 'R') //สีแดง
                        {
                            if (j == 0)
                                E0.BackColor = Color.Red;
                            else if (j == 1)
                                E1.BackColor = Color.Red;
                            else if (j == 2)
                                E2.BackColor = Color.Red;
                            else if (j == 3)
                                E3.BackColor = Color.Red;
                            else if (j == 4)
                                E4.BackColor = Color.Red;
                            else if (j == 5)
                                E5.BackColor = Color.Red;
                            else if (j == 6)
                                E6.BackColor = Color.Red;
                            else if (j == 7)
                                E7.BackColor = Color.Red;
                            else if (j == 8)
                                E8.BackColor = Color.Red;
                        }
                        else if (rk[i][j] == 'W') //สีขาว
                        {
                            if (j == 0)
                                E0.BackColor = Color.White;
                            else if (j == 1)
                                E1.BackColor = Color.White;
                            else if (j == 2)
                                E2.BackColor = Color.White;
                            else if (j == 3)
                                E3.BackColor = Color.White;
                            else if (j == 4)
                                E4.BackColor = Color.White;
                            else if (j == 5)
                                E5.BackColor = Color.White;
                            else if (j == 6)
                                E6.BackColor = Color.White;
                            else if (j == 7)
                                E7.BackColor = Color.White;
                            else if (j == 8)
                                E8.BackColor = Color.White;

                        }
                    }

                    if (i == 5) // Zone F
                    {
                        if (rk[i][j] == 'P')
                        {//สีม่วง
                            if (j == 0)
                                F0.BackColor = Color.Purple;
                            else if (j == 1)
                                F1.BackColor = Color.Purple;
                            else if (j == 2)
                                F2.BackColor = Color.Purple;
                            else if (j == 3)
                                F3.BackColor = Color.Purple;
                            else if (j == 4)
                                F4.BackColor = Color.Purple;
                            else if (j == 5)
                                F5.BackColor = Color.Purple;
                            else if (j == 6)
                                F6.BackColor = Color.Purple;
                            else if (j == 7)
                                F7.BackColor = Color.Purple;
                            else if (j == 8)
                                F8.BackColor = Color.Purple;
                        }


                        else if (rk[i][j] == 'B')   //สีน้ำเงิน
                        {
                            if (j == 0)
                                F0.BackColor = Color.Blue;
                            else if (j == 1)
                                F1.BackColor = Color.Blue;
                            else if (j == 2)
                                F2.BackColor = Color.Blue;
                            else if (j == 3)
                                F3.BackColor = Color.Blue;
                            else if (j == 4)
                                F4.BackColor = Color.Blue;
                            else if (j == 5)
                                F5.BackColor = Color.Blue;
                            else if (j == 6)
                                F6.BackColor = Color.Blue;
                            else if (j == 7)
                                F7.BackColor = Color.Blue;
                            else if (j == 8)
                                F8.BackColor = Color.Blue;
                        }

                        else if (rk[i][j] == 'Y')   // สีเหลือง
                        {
                            if (j == 0)
                                F0.BackColor = Color.Yellow;
                            else if (j == 1)
                                F1.BackColor = Color.Yellow;
                            else if (j == 2)
                                F2.BackColor = Color.Yellow;
                            else if (j == 3)
                                F3.BackColor = Color.Yellow;
                            else if (j == 4)
                                F4.BackColor = Color.Yellow;
                            else if (j == 5)
                                F5.BackColor = Color.Yellow;
                            else if (j == 6)
                                F6.BackColor = Color.Yellow;
                            else if (j == 7)
                                F7.BackColor = Color.Yellow;
                            else if (j == 8)
                                F8.BackColor = Color.Yellow;
                        }

                        else if (rk[i][j] == 'G')   //สีเขียว
                        {
                            if (j == 0)
                                F0.BackColor = Color.Green;
                            else if (j == 1)
                                F1.BackColor = Color.Green;
                            else if (j == 2)
                                F2.BackColor = Color.Green;
                            else if (j == 3)
                                F3.BackColor = Color.Green;
                            else if (j == 4)
                                F4.BackColor = Color.Green;
                            else if (j == 5)
                                F5.BackColor = Color.Green;
                            else if (j == 6)
                                F6.BackColor = Color.Green;
                            else if (j == 7)
                                F7.BackColor = Color.Green;
                            else if (j == 8)
                                F8.BackColor = Color.Green;
                        }

                        else if (rk[i][j] == 'R') //สีแดง
                        {
                            if (j == 0)
                                F0.BackColor = Color.Red;
                            else if (j == 1)
                                F1.BackColor = Color.Red;
                            else if (j == 2)
                                F2.BackColor = Color.Red;
                            else if (j == 3)
                                F3.BackColor = Color.Red;
                            else if (j == 4)
                                F4.BackColor = Color.Red;
                            else if (j == 5)
                                F5.BackColor = Color.Red;
                            else if (j == 6)
                                F6.BackColor = Color.Red;
                            else if (j == 7)
                                F7.BackColor = Color.Red;
                            else if (j == 8)
                                F8.BackColor = Color.Red;
                        }
                        else if (rk[i][j] == 'W') //สีขาว
                        {
                            if (j == 0)
                                F0.BackColor = Color.White;
                            else if (j == 1)
                                F1.BackColor = Color.White;
                            else if (j == 2)
                                F2.BackColor = Color.White;
                            else if (j == 3)
                                F3.BackColor = Color.White;
                            else if (j == 4)
                                F4.BackColor = Color.White;
                            else if (j == 5)
                                F5.BackColor = Color.White;
                            else if (j == 6)
                                F6.BackColor = Color.White;
                            else if (j == 7)
                                F7.BackColor = Color.White;
                            else if (j == 8)
                                F8.BackColor = Color.White;

                        }
                    } // end Zone F


                } // End for j
            } // end for i
        } // end Function show Panel




     
        private void Test_Click(object sender, EventArgs e) // Reset Click125 //Finish
        {
            char[][] RK = new char[6][] {  new char[] {'B','B','B','B','B','B','B','B','B' },
                                                new char[] {'P','P', 'P', 'P', 'P', 'P', 'P', 'P', 'P'},
                                                new char[] { 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W', 'W'  },
                                                new char[] { 'R', 'R', 'R', 'R', 'R', 'R', 'R', 'R', 'R' },
                                                new char[] {'G', 'G' , 'G' , 'G', 'G' , 'G' , 'G' , 'G','G' },
                                                new char[] { 'Y', 'Y', 'Y', 'Y', 'Y', 'Y', 'Y', 'Y', 'Y' } };

            listBox1.Items.Clear();
            numList = 0;
            Rubik = RK ;
            show_color(Rubik);

        } 



        //ส่วนควบคุมการหมุน

       



       
        

      
        private void Ck_Color_Click(object sender, EventArgs e)
        {
            Form2 fm2 = new Form2();
            fm2.Show();
        }


        private void button2_Click(object sender, EventArgs e) // Test  TakePicture
        {
            
            if (numPicture == 1)
                str = "C:/Users/BOM/Desktop/New folder/2.jpg"; // back
            else if (numPicture == 2)
                str = "C:/Users/BOM/Desktop/New folder/4.jpg"; // Front
            else if (numPicture == 3)
                str = "C:/Users/BOM/Desktop/New folder/1.jpg"; // left
            else if(numPicture == 4)
                str = "C:/Users/BOM/Desktop/New folder/5.jpg"; // Right
            else if(numPicture == 5)
                str = "C:/Users/BOM/Desktop/New folder/6.jpg"; // up
            else if(numPicture == 6)
                str = "C:/Users/BOM/Desktop/New folder/3.jpg"; // Dow


            R_Image = new Image<Hsv, Byte>(str);

            if (numPicture == 1)
            {
               
                Box1.Image = R_Image.ToBitmap();
                read_color(1 - 1);
            }
            if (numPicture == 2)
            {
               
                Box5.Image = R_Image.ToBitmap();
                read_color(5 - 1);
            }
            if (numPicture == 3)
            {
               
                Box2.Image = R_Image.ToBitmap();
                read_color(2 - 1);
            }
            if (numPicture == 4)
            {
                
                Box4.Image = R_Image.ToBitmap();
                read_color(4 - 1);
            }
            if (numPicture == 5)
            {
                
                Box3.Image = R_Image.ToBitmap();
                read_color(3 - 1);
            }
            if (numPicture == 6)
            {
                
                Box6.Image = R_Image.ToBitmap();
                read_color(6 - 1);
            }
            


           // read_color()

            show_color(Rubik);
            numPicture++;

            Text_face.Text = showRubik(Rubik);




        }
        public string showRubik(char[][] Rubik)    // Show data in Rubik
        {
            String str = "";
            for (int i = 0; i < 6; i++)
            {
                for(int j = 0; j < 9; j++)
                {
                    //if(Rubik[i][j] == 'B')
                    str = str + Convert_RGB_to_Face(Rubik[i][j]);
                }
            }
            return str;
        }


        public string Convert_RGB_to_Face(char Rubik)
        {
            String str = "";
            if (Rubik == 'B')
            {
                str = "B";
            }
            else if (Rubik == 'P')
            {
                str = "L";
            }
            else if (Rubik == 'W')
            {
                str = "U";
            }
            else if (Rubik == 'R')
            {
                str = "R";
            }
            else if (Rubik == 'G')
            {
                str = "F";
            }
            else if (Rubik == 'Y')
            {
                str = "D";
            }
            return str;
        }






        private void RotateRight_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void RotateLeft_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }



        ///////////////////////// ทิศในการหมุน    ปุ่มควบคุม  //////////////////////////////////////////////////
        private void Top_Click(object sender, EventArgs e)   // Chang Face
        {

        }




        private void Left_Click(object sender, EventArgs e)    //  L
        {
            rotate_left(1);
            show_color(Rubik);
            listBox1.Items.Add(numList + ":L");
            numList++;
            // serialPort1.Write("L");
            L();
        }

        private void Right_Click_1(object sender, EventArgs e)   //  R
        {
            rotate_right(1);
            show_color(Rubik);
            listBox1.Items.Add(numList + ": R");
            numList++;
            //  serialPort1.Write("R");
            R();
        }



        private void Front_Click(object sender, EventArgs e) // F
        {
            rotate_front(1);
            show_color(Rubik);
            listBox1.Items.Add(numList + ": F");
            numList++;
            /// Step of Work ///
            F();

            
            ///
            //serialPort1.Write("F");// Tanapon Ninket
        }

        private void Rear_Click(object sender, EventArgs e)  //  B
        {
            rotate_rear(1);
            show_color(Rubik);
            listBox1.Items.Add(numList + ":B");
            numList++;
            // serialPort1.Write("B");
            B();
        }


        private void U_Click(object sender, EventArgs e)  //  Top
        {
            rotate_top(1);
            show_color(Rubik);
            listBox1.Items.Add(numList + ": U'");
            numList++;
            // serialPort1.Write("b");
            UU();

        }

        private void D_Click(object sender, EventArgs e)   // Down
        {
            rotate_down(1);
            show_color(Rubik);
            listBox1.Items.Add(numList + ":D");
            numList++;
            // serialPort1.Write("b");
            DD();
        }

        private void Bdet_Click(object sender, EventArgs e) //B'
        {
            rotate_rear(0);
            show_color(Rubik);
            listBox1.Items.Add(numList + ": B'");
            numList++;
            // serialPort1.Write("b");
            B_det();
        }

        private void Ldet_Click(object sender, EventArgs e)  // L'
        {
            rotate_left(0);
            show_color(Rubik);
            listBox1.Items.Add(numList + ": L'");
            //textBox1.Text = "L";
            numList++;
            // serialPort1.Write("l");
            L_det();
        }

        private void Fdet_Click(object sender, EventArgs e)  // F'
        {
            rotate_front(0);
            show_color(Rubik);
            listBox1.Items.Add(numList + ": F'");
            //Text_Result.Text = "FFFFFFFFFFFFFFFFFFFFFFFF";  //
            numList++;
            F_det();
            //serialPort1.Write("f");// Tanapon Ninket
        }

        private void Rdet_Click(object sender, EventArgs e) // R'
        {
            rotate_right(0);
            show_color(Rubik);
            listBox1.Items.Add(numList + ": R'");  
            numList++;
            R_det();
            //serialPort1.Write("r");
        }

        private void Udet_Click(object sender, EventArgs e) // U' 
        {
            rotate_top(0);
            show_color(Rubik);
            listBox1.Items.Add(numList + ": U'");
            numList++;
            U_det();
            //serialPort1.Write("r");
        }

        private void Ddet_Click(object sender, EventArgs e)
        {
            rotate_down(0);
            show_color(Rubik);
            listBox1.Items.Add(numList + ":D'");
            numList++;
            D_det();
            //serialPort1.Write("r");
        }



        private void button1_Click(object sender, EventArgs e)
        {
            Form3 fm3 = new Form3();
            fm3.Show();
        }



        private void open_left_right_Click(object sender, EventArgs e) // เลื่อนออก ซ้าย---ขวา  <<LR>>
        {
            serialPort1.Write("j");
        }

        private void close_left_right_Click(object sender, EventArgs e) // เลื่อนเข้า ซ้าย---ขวา  >>LR<<
        {
            serialPort1.Write("J");
        }

        private void open_front_back_Click(object sender, EventArgs e)  // เลื่อนเข้า หน้า---หลัง   <<FB>>
        {
            serialPort1.Write("K");
        }

        private void close_front_back_Click(object sender, EventArgs e) // เลื่อนออก หน้า---หลัง   >>FB<<
        {
            serialPort1.Write("k");
        }

        private void Start_Auto_Click(object sender, EventArgs e)
        {
           
            timer2.Enabled = true;
            timer2.Start();
            /*
            time_init();
            Result = Text_Result.Text;
            Result_to_function(Result);
            */
            //if (NumTimer == 6)
            //{


            // }



            //STOP_AUTO_Click(sender, e);

            //Start_Auto_Click(sender, e);


        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            

        }

        private void STOP_AUTO_Click(object sender, EventArgs e)   /////  Button  STOP  for Auto CLick //////////////
        {

            timer2.Stop();
            timer3.Stop();
        }

        private void timer3_Tick_1(object sender, EventArgs e)   //////  Timer for Counter  //////////
        {
            ssec++;
            if (ssec % 100 == 0)
            {
                sec++;
                ssec = 0;
            }
            if (sec  == 60)
            {
                min++;
                sec = 0;
            }
            Text_Time.Text = min + " : " + sec + " : " + ssec;
          
        }

        private void timer2_Tick(object sender, EventArgs e)   //////// Timer for TakePicture  ////////////
        {
            Take_Click_1(sender, e);
            //
            NumTimer++;
            
            if (NumTimer == 6)
            {
                timer2.Stop();
                Text_face.Text = Face;

                Write_Face();
            }
            //Delay++;
        }
        public void time_init()
        {
            timer3.Enabled = true;
            timer3.Start();
         
            min = 0;
            sec = 0;
            ssec = 0;
            

        }   ////////////////  Timne init /////////////////

        private void timer_read_result_Tick(object sender, EventArgs e)
        {
            ///////////////// for wite Read File /////////////////

        }



        // ////////////////////  Function Control  Rubik Cube Robot   Begin EiEi  ///////////////////////////////


        void F()
        {
            serialPort1.Write("F");
            serialPort1.Write("K");
            serialPort1.Write("f");
            serialPort1.Write("k");
        }
        void F_det()
        {
            serialPort1.Write("f");
            serialPort1.Write("K");
            serialPort1.Write("F");
            serialPort1.Write("k");
        }
        void F_2()
        {
            serialPort1.Write("FF");
        }
        void F2_det()
        {
            serialPort1.Write("ff");
        }
        

        void B()
        {
            serialPort1.Write("B");
            serialPort1.Write("K");
            serialPort1.Write("b");
            serialPort1.Write("k");
        }
        void B_det()
        {
            serialPort1.Write("b");
            serialPort1.Write("K");
            serialPort1.Write("B");
            serialPort1.Write("k");
        }
        void B_2()
        {
            serialPort1.Write("BB");
        }
        void B2_det()
        {
            serialPort1.Write("bb");
        }
    

        void L()
        {
            serialPort1.Write("L");
            serialPort1.Write("j");
            serialPort1.Write("l");
            serialPort1.Write("J");
        }  //เสร็จ
        void L_det()
        {
            serialPort1.Write("l");
            serialPort1.Write("j");
            serialPort1.Write("L");
            serialPort1.Write("J");
        }   /// <summary>
        /// </summary>
        void L_2()
        {
            serialPort1.Write("LL");
        }
        void L2_det()
        {
            serialPort1.Write("ll");
        }


        void R()
        {
            serialPort1.Write("R");
            serialPort1.Write("j");
            serialPort1.Write("r");
            serialPort1.Write("J");
        }  //เสร็จ
        void R_det()
        {
            serialPort1.Write("r");
            serialPort1.Write("j");
            serialPort1.Write("R");
            serialPort1.Write("J");
        }  //เสร็จ
        void R_2()
        {
            serialPort1.Write("RR");
        }   //เสร็จ
        void R2_det()
        {
            serialPort1.Write("rr");
        }

    
        void UU()//เสร็จ   // U
        {
            serialPort1.Write("j");
            serialPort1.Write("D");
            serialPort1.Write("J");
            serialPort1.Write("KdkRjrdJKDk");
        }
        void U_det() // U'  //เสร็จ
        {
            serialPort1.Write("j");
            serialPort1.Write("D");
            serialPort1.Write("J");
            serialPort1.Write("KdkrjRdJKDk");
        }
        void U_2() // เสร็จ
        {
            serialPort1.Write("j");
            serialPort1.Write("D");
            serialPort1.Write("J");
            serialPort1.Write("KdkRRjdJKDk");
        }
        void U2_det() // เสร็จ
        {
            serialPort1.Write("j");
            serialPort1.Write("D");
            serialPort1.Write("J");
            serialPort1.Write("KdkRRjdJKDk");
        }

        
        void DD()  //เสร็จ
        {
            serialPort1.Write("j");
            serialPort1.Write("D");
            serialPort1.Write("J");
            serialPort1.Write("KdkLjldJKDk");
        }
        void D_det()  // เสร็จ
        {
            serialPort1.Write("j");
            serialPort1.Write("D");
            serialPort1.Write("J");
            serialPort1.Write("KdkljLdJKDk");
        }
        void D_2()   // เสร็จ
        {
            serialPort1.Write("j");
            serialPort1.Write("D");
            serialPort1.Write("J");
            serialPort1.Write("KdkLLjdJKDk");
        }
        void D2_det()  // เสร็จ
        {
            serialPort1.Write("j");
            serialPort1.Write("D");
            serialPort1.Write("J");
            serialPort1.Write("KdkLLjdJKDk");
        }


        // ////////////////////  Function Control  Rubik Cube Robot   Finish   ///////////////////////////////



        //////////////////////   Generate  Rusult //////////////////////

        public void Result_to_function(string Result)
        {
            //Face = Face.Substring(3);  // เราต้องใส่ Index เพื่อระบุส่วนที่จะเอา(ตัดส่วนอื่นทิ้ง) (X,Y)   x คือ ตำแหน่ง   Y คือ ความยาวที่เราจะเอา
            //Fcee = Face.Trim(new char[]{ '' , '*' ,'.' })  // ใน new char คือส่วนที่เราจะตัดว่ามีตัวอักษรใหนบ้าง
            //first_string.Remove(first_string.IndexOf(" test"), " test".Length);
            StringBuilder sb = new StringBuilder(Result);
            sb.Remove(Result.IndexOf("."), 3);
            Result = sb.ToString();


            //Console.WriteLine(" Result = {0}", Result);
            //Console.WriteLine(" {0}", Result.Length);
            System.Diagnostics.Debug.WriteLine("Result length = " + (Result.Length -5) );
            System.Diagnostics.Debug.WriteLine("Result = " + Result);
            int x = 1;
            
            for (int i = 0; i < Result.Length-5 ; i += 3)
            {


               

                if (Result.Substring(i, 2).Equals("L "))
                {
                    listBox1.Items.Add(x + ":L");
                    L();
                   
                    x++;
                }
                else if (Result.Substring(i, 2).Equals("L'"))
                {
                    listBox1.Items.Add(x + ":L'");
                    L_det();
                    x++;
                }
                 else if (Result.Substring(i, 2).Equals("L2"))
                {
                    listBox1.Items.Add(x + ":L2");
                    L_2();
                    x++;
                }


                 else if (Result.Substring(i, 2).Equals("B "))
                {
                    listBox1.Items.Add(x + ":B ");
                    B();
                    x++;
                }
                 else if (Result.Substring(i, 2).Equals("B'"))
                {
                    listBox1.Items.Add(x + ":B'");
                    B_det();
                    x++;
                }
                 else if (Result.Substring(i, 2).Equals("B2"))
                {
                    listBox1.Items.Add(x + ":B2");
                    B_2();
                    x++;
                }

                 else if (Result.Substring(i, 2).Equals("R "))
                {
                    listBox1.Items.Add(x + ":R ");
                    R();
                    x++;
                }
                 else if (Result.Substring(i, 2).Equals("R'"))
                {
                    listBox1.Items.Add(x + ":R'");
                    R_det();
                    x++;
                }
                 else if (Result.Substring(i, 2).Equals("R2"))
                {
                    listBox1.Items.Add(x + ":R2");
                    R_2();
                    x++;
                }

                 else if (Result.Substring(i, 2).Equals("U "))
                {
                    listBox1.Items.Add(x + ":U ");
                    UU();
                    x++;
                }
                 else if (Result.Substring(i, 2).Equals("U'"))
                {
                    listBox1.Items.Add(x + ":U'");
                    U_det();
                    x++;
                }
                 else if (Result.Substring(i, 2).Equals("U2"))
                {
                    listBox1.Items.Add(x + ":U2");
                    U_2();
                    x++;
                }

                 else if (Result.Substring(i, 2).Equals("D "))
                {
                    listBox1.Items.Add(x + ":D ");
                    DD();
                    x++;
                }
                else if (Result.Substring(i, 2).Equals("D'"))
                {
                    listBox1.Items.Add(x + ":D'");
                    D_det();
                    x++;
                }
                else if (Result.Substring(i, 2).Equals("D2"))
                {
                    listBox1.Items.Add(x + ":D2");
                    D_2();       
                    x++;
                }
                else if (Result.Substring(i, 2).Equals("F "))
                {
              
                    listBox1.Items.Add(x + ":F ");
                    F();
                    x++;
                }
                else if (Result.Substring(i, 2).Equals("F'"))
                {
                    listBox1.Items.Add(x + ":F'");
                    F_det();
                    x++;
                }
                else if (Result.Substring(i, 2).Equals("F2"))
                {
                    listBox1.Items.Add(x + ":F2");
                    F_2();
                    x++;
                }


            }
            System.Diagnostics.Debug.WriteLine("x = " + x);



        }

        ////////////////////// Generate Result Finish ////////////////


        ////////////////////////////////Read / Write File Face and Result  /////////////////////////////////////
        
        public void Read_Result()
        {
            using (StreamReader readtext = new StreamReader(@"D:\\Result.txt"))
            {
                string readMeText = readtext.ReadLine();
                Text_Result.Text = readMeText;
                //Console.WriteLine(Result);
            }
        }
        public void Write_Face()
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"D:\\Face.txt"))
            {
                Text_face.Text = Face;
                
                file.Write(Face);
                //ReadFile
            }
        }
        ////////////////////////////////  END  File IO  ////////////////////////////////////////////////////////


        //=======
        ///////////////////   สิ้นสุดการทำงานของปุ่ม
     
    } // end class 
}
