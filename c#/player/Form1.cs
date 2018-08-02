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
using System.Runtime.InteropServices;
using System.Threading;

namespace player
{
    public partial class Form1 : Form
    {
        const int MAX_PROGRESS_BAR = 100;
        
        string directory = Directory.GetCurrentDirectory();
        string[] playlist;
        WMPLib.WindowsMediaPlayer current;
        int currLength;
        int playIndex = 0;
        float playTime = 0;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            progressBar1.Maximum = MAX_PROGRESS_BAR;
            timer1.Enabled = false;
            
            
            Point loc = label1.Location;
            string[] files;

            string[] args = Environment.GetCommandLineArgs();
            int count = args.Length;
            if (count > 1)
            {
                //get files from command line args
                files = new string[count - 1];
                for(int i = 0; i < count - 1; i++)
                {
                    files[i] = args[i + 1];
                }
            }
            else
            {
                //gets files from directory
                files = Directory.GetFiles(directory);
            }
            foreach (string file in files)
            {
                if (file.Split(".".ToCharArray()).Length > 1 && file.Split(".".ToCharArray())[1]=="wav")
                {
                    Label l = new Label();
                    string[] parts = file.Split("\\".ToCharArray());
                    l.Text = parts[parts.Length - 1];
                    l.Location = loc;
                    l.Click += L_Click;
                    this.Controls.Add(l);
                    loc.Y += 35;
                    if(loc.Y>this.Size.Height)
                    {
                        loc.Y = label1.Location.Y;
                        loc.X = label1.Location.X + 40;
                    }
                }
            }
        }

        private void L_Click(object sender, EventArgs e)
        {
            timer2.Enabled = false;
            if(this.current!=null)
            {
                this.current.controls.stop();
            }
            string file = ((Control)sender).Text;
            try
            {
                WMPLib.WindowsMediaPlayer s = new WMPLib.WindowsMediaPlayer();
                s.URL = file;
                s.controls.play();
               
                timer1.Enabled = true;
                //sets the global song playing varaibles
                this.current = s;
                this.currLength = SoundInfo.GetSoundLength(file) / 1000;
                playTime = 0;
            }
            catch(Exception exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (current == null)
                return;
            label2.Text = "currently playing:" + current.URL.Split("\\".ToCharArray())[current.URL.Split("\\".ToCharArray()).Length - 1].Split(".".ToCharArray())[0];
            //increasing current play time
            playTime += 0.1f;

            progressBar1.Value = (int)((playTime / (float)currLength) * MAX_PROGRESS_BAR);

            //sets progress bar text to the matching values
            //progressLabel.Text = TimeFormat((int)playTime) + "/" + TimeFormat(currLength);
            progressLabel.Text = ((int)playTime).ToString() + "/" + currLength.ToString();
            if(playTime > currLength)
            {
                current = null;
                currLength = 0;
                playTime = 0;
                timer1.Enabled = false;
                progressLabel.Text = "";
                playIndex++;
            }
        }
        private static string TimeFormat(int seconds)
        {
            DateTime d = new DateTime(0);
            d = d.AddSeconds(seconds);
            return d.ToLongTimeString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(((Control)sender).Text == "Pause")
            {
                current.controls.pause();
                timer1.Enabled = false;
                ((Control)sender).Text = "UnPause";
            }
            else
            {
                //current.LoadTimeout = (int)playTime;
                current.controls.play();
                timer1.Enabled = true;
                ((Control)sender).Text = "Pause";
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (this.current != null)
                this.current.controls.stop();
            List<string> files = new List<string>();
            foreach(Control item in Controls)
            {
                if(item is Label)
                {
                    if(((Label)item).Text.Split(".".ToCharArray()).Length > 1)
                    {
                        files.Add(((Label)item).Text);
                    }
                }
            }
            files = random.Shuffle<string>(files);
           
            WMPLib.WindowsMediaPlayer s = new WMPLib.WindowsMediaPlayer();
            this.playlist = files.ToArray();
            this.current = s;
            timer2.Enabled = true;
            //Initializes file times
            this.playTime = 0;
            this.currLength = SoundInfo.GetSoundLength(this.playlist[0]) / 1000;
            timer1.Enabled = true;
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            string play;
            if (this.playIndex < this.playlist.Length)
                play = this.playlist[this.playIndex];
            else
            {
                this.current.controls.stop();
                label2.Text = "Not Playing";
                timer1.Enabled = false;
                timer2.Enabled = false;
                return;
            }
            //TODO : FIX THIS FOR IT WILL WORK WITHOUT SUCH EXCEPTIONS (CURRENTL WILL NOT WORK IF ONE FILE CONTAINS THE OTHER ONE)
            if (this.current.URL.Contains(play))
                return;
            this.current.URL = play;
            this.current.controls.play();
            //Initializes file times
            this.currLength = SoundInfo.GetSoundLength(play) / 1000;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.playTime = 0;
            this.playIndex++;
        }
    }
    public static class SoundInfo
    {
        [DllImport("winmm.dll")]
        private static extern uint mciSendString(
            string command,
            StringBuilder returnValue,
            int returnLength,
            IntPtr winHandle);

        public static int GetSoundLength(string fileName)
        {
            StringBuilder lengthBuf = new StringBuilder(32);

            mciSendString(string.Format("open \"{0}\" type waveaudio alias wave", fileName), null, 0, IntPtr.Zero);
            mciSendString("status wave length", lengthBuf, lengthBuf.Capacity, IntPtr.Zero);
            mciSendString("close wave", null, 0, IntPtr.Zero);

            int length = 0;
            int.TryParse(lengthBuf.ToString(), out length);

            return length;
        }
    }
    public class random
    {
        public static List<T> Shuffle<T>(List<T> list)
        {
            List<T> newList = new List<T>(list);
            Random r = new Random();
            int n = list.Count;
            while(n > 1)
            {
                n--;
                int i = r.Next(n + 1);
                T value = newList[i];
                newList[i] = newList[n];
                newList[n] = value;
            }
            return newList;
        }
    }
}
