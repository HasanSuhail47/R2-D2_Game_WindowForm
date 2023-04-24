using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace BoredAF
{
    public partial class Form1 : Form
    {
        bool left, right, jump,interaction_bottom,interaction_right,interaction_left,interaction_up;
        int playerspeed = 5;
        double ga = 0.5;
        double gs = 0;
        int jumpforce = 20;
        int counter = 0;
       
        
        PictureBox thrust = new PictureBox();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

       

        private void Keydown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                left = true;
            }
            if (e.KeyCode == Keys.Right)
            {
                right = true;
            }
            if (e.KeyCode == Keys.Space && jump==false)
            {
                jump = true;
            }
            if (e.KeyCode == Keys.X && jump == false)
            {
                player.Location = new Point(0, 0);
            }

        }

        private void Keyup(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Left)
            {
                left = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                right = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            thrust.Name = "thrust";
            thrust.Size = new Size(30, 60);
            thrust.Location = new Point(player.Location.X, player.Location.Y + (int)player.Height);
            thrust.Image = Image.FromFile(@"C:\Users\Home PC\source\repos\BoredAF\BoredAF\Pictures\thrust-01.png");
            this.Controls.Add(thrust);
            thrust.SizeMode= System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            thrust.BringToFront();
            thrust.Hide();
            this.levelbuilder();
            
            player.Image = Image.FromFile(@"C:\Users\Home PC\source\repos\BoredAF\BoredAF\Pictures\player-01.png");
            player.BackColor = Color.Transparent;
           
        }

        private void movement(string direction)
        {
            
        }

        private void Maintimer(object sender, EventArgs e)
        {
            
            foreach (Control x in this.Controls)
            {
                if (x is PictureBox && (string)x.Tag == "platform")
                {
                    if (player.Bounds.Bottom < x.Bounds.Top+5&& player.Bounds.Bottom> x.Bounds.Top - 5&&
                        player.Bounds.Left>x.Bounds.Left&& player.Bounds.Left < x.Bounds.Right||
                        player.Bounds.Bottom < x.Bounds.Top + 5 && player.Bounds.Bottom > x.Bounds.Top - 5 &&
                        player.Bounds.Right > x.Bounds.Left && player.Bounds.Right < x.Bounds.Right)
                    {
                       
                        gs = 0;
                    }
                    
                    if (player.Bounds.Left < x.Bounds.Right + 5 && player.Bounds.Left > x.Bounds.Right - 5 &&
                        player.Bounds.Bottom < x.Bounds.Bottom && player.Bounds.Bottom > x.Bounds.Top ||
                        player.Bounds.Left < x.Bounds.Right + 5 && player.Bounds.Left > x.Bounds.Right - 5 &&
                        player.Bounds.Top < x.Bounds.Bottom && player.Bounds.Top > x.Bounds.Top
                        )
                    {

                        left = false;
                    }
                    
                    if (player.Bounds.Right < x.Bounds.Left + 5 && player.Bounds.Right > x.Bounds.Left - 5 &&
                        player.Bounds.Bottom < x.Bounds.Bottom && player.Bounds.Bottom > x.Bounds.Top ||
                        player.Bounds.Right < x.Bounds.Left + 5 && player.Bounds.Right > x.Bounds.Left - 5 &&
                        player.Bounds.Top < x.Bounds.Bottom && player.Bounds.Top > x.Bounds.Top
                        )
                    {
                        right = false;
                    }
                    

                }

            }
            
            player.Location = new Point(player.Location.X, player.Location.Y + (int)gs);

            if (gs < 5)
            { 
                thrust.Hide();
                gs += ga;
            }
            else
            {
                thrust.Image = Image.FromFile(@"C:\Users\Home PC\source\repos\BoredAF\BoredAF\Pictures\thrust-02.png");
                thrust.Location = new Point(player.Location.X+15, player.Location.Y + (int)player.Height);
                thrust.Show();
            }
                
            

            if (jump == true && jumpforce >= 0)
            {
                
                player.Location = new Point(player.Location.X, player.Location.Y - jumpforce);
                jumpforce -= 1;
                interaction_bottom = false;
                thrust.Image = Image.FromFile(@"C:\Users\Home PC\source\repos\BoredAF\BoredAF\Pictures\thrust-01.png");
                thrust.Location = new Point(player.Location.X + 15, player.Location.Y + (int)player.Height);
                thrust.Show();
            }
            else
            {
                jumpforce = 20;
                jump= false;
            }

            if (left == true && player.Left > 3&& interaction_left==false  )
            {
                player.Image = Image.FromFile(@"C:\Users\Home PC\source\repos\BoredAF\BoredAF\Pictures\player-01.png");
                player.Location = new Point(player.Location.X - playerspeed, player.Location.Y);
            }
            if (right == true && player.Left < Background.Width - player.Width)
            {
                player.Image = Image.FromFile(@"C:\Users\Home PC\source\repos\BoredAF\BoredAF\Pictures\player2-01.png");
                player.Location = new Point(player.Location.X + playerspeed, player.Location.Y);
            }
        }

        private void levelbuilder()
        {
            string[] s= File.ReadAllLines(@"C:\Users\Home PC\source\repos\BoredAF\BoredAF\Pictures\Map1.txt");
            
            this.Size = new Size(Int32.Parse((s[2].Split('|'))[0]), Int32.Parse((s[2].Split('|'))[1])+40);
            Background.Name = s[0];
            Background.Image = Image.FromFile(s[1]);
            Background.Size = new Size(Int32.Parse((s[2].Split('|'))[0]), Int32.Parse((s[2].Split('|'))[1]));
            Background.Location= new Point(Int32.Parse((s[3].Split('|'))[0]),Int32.Parse((s[3].Split('|'))[1]));
            Background.Tag = s[4];
            Background.SizeMode=PictureBoxSizeMode.StretchImage;

            int i=9;
            for (; i < s.Length; i++)
            {
                int location= Int32.Parse(s[i].Split(':')[0].Split('/')[0])*50;
                for (int j = 0; j < Int32.Parse(s[i].Split(':')[1]);j++)
                {
                    PictureBox p=new PictureBox
                    {
                        Name = s[5],
                        Image = Image.FromFile(s[6]),
                        Size= new Size(Int32.Parse((s[7].Split('|'))[0]),Int32.Parse((s[7].Split('|'))[1])),
                        Tag = s[8],
                        Location = new Point(location, Int32.Parse(s[i].Split(':')[0].Split('/')[1])*50),
                        SizeMode=PictureBoxSizeMode.StretchImage
                    };
                    location += 50;
                    this.Controls.Add(p);
                    p.BringToFront();
                }

            }
        }
    }
}
