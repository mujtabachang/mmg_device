using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using NAudio;
using NAudio.Wave;
using NAudio.CoreAudioApi;
using System.Diagnostics;
using NDtw;

using Testing_ResizingForm;
namespace AhmedCode_Myo
{
    public partial class Form1 : Form
    {
        public int SamplingRate=400;
        public int SampleSeconds = 1;
        public WaveIn waveSource = null;
        public WaveFileWriter waveFile = null;
        public CircularBuffer<double> queue;
        public double[] saved1,saved2,saved3;
        public List<double[]> falseSaved= new List<double[]>();
        bool isSaved1,isSaved2,isSaved3;
        double thresh1=1.8, thresh2=1.2, thresh3=1.2, thresh0=3;
        int loop = 0;

        frmArrowing ArrowForm = new frmArrowing();
        frmSerialInterface SerialForm = new frmSerialInterface();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            queue = new CircularBuffer<double>(SamplingRate * SampleSeconds );
            //queue.Limit = 400 * 3;

            //Load Other forms
            ArrowForm.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            SerialForm.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            ArrowForm.Show();
            SerialForm.Show();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            StartBtn.Enabled = false;
            StopBtn.Enabled = true;

            waveSource = new WaveIn();
            waveSource.WaveFormat = new WaveFormat(SamplingRate,  1 );

            waveSource.DataAvailable += new EventHandler<WaveInEventArgs>(waveSource_DataAvailable);
            waveSource.RecordingStopped += new EventHandler<StoppedEventArgs>(waveSource_RecordingStopped);

            //waveFile = new WaveFileWriter(@"C:\Temp\Test0001.wav", waveSource.WaveFormat);

            waveSource.StartRecording();
        }

        float map(float x, float in_min, float in_max, float out_min, float out_max)
        {
            return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_max;
        }

        long lastDetect = DateTime.Now.Ticks/TimeSpan.TicksPerSecond;
        int lastGesture = 0;
        void waveSource_DataAvailable(object sender, WaveInEventArgs e)
        {
            //int max = 0;
            
            

            byte[] buffer = e.Buffer;
            int bytesRecorded = e.BytesRecorded;
            //WriteToFile(buffer, bytesRecorded);

            for (int index = 0; index < e.BytesRecorded; index += 2)
            {
                short sample = (short)((buffer[index + 1] << 8) |
                                        buffer[index + 0]);
                float sample32 = (sample / 32768f);

                
                //int finalSample =(sample32 * 255);
                //if (max < finalSample) max = finalSample;
                //sampleAggregator.Add(sample32);
                queue.Enqueue(sample32);
                
                

                //queue.print();
            }

            long curTime = DateTime.Now.Ticks / TimeSpan.TicksPerSecond;
            
            

           
            if (lastDetect + 2 > curTime)
            {
                Console.WriteLine("LAST GEST" + lastDetect + 5 + "  " + curTime);
                return;
            }

            //check if sample matches
            int gest = 0;
            double lastcost = 1000;
            if (isSaved1 /*&& (loop++%3==0)*/){

                long curTimeMs1 = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond; //Time Calc

                var cost = new Dtw (  saved1 , queue.ToArray2()).GetCost();

                long curTimeMs2 = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond; //Time Calc

                if (cost < lastcost && cost < thresh1) { gest = 1; lastcost = cost; Console.WriteLine("G1: " + cost); }
                //Debug.WriteLineIf( cost<thresh1, "1:"+cost);

                Console.WriteLine("Time DTW Took for Gesture 1 in ms: " + ( curTimeMs2 - curTimeMs1));
                //Console.WriteLine("MS 2: " + curTimeMs2);

            }
            if (isSaved2 /*&& (loop++%3==0)*/)
            {
                var cost = new Dtw(saved2, queue.ToArray2()).GetCost();
                if (cost < lastcost && cost < thresh2)
                {
                    gest = 2;
                    lastcost = cost;
                    Console.WriteLine("G2: " + cost);
                }
                //Debug.WriteLineIf(cost < thresh2, "2:"+cost);
            }
            if (isSaved3 /*&& (loop++%3==0)*/)
            {
                var cost = new Dtw(saved3, queue.ToArray2()).GetCost();
                if (cost < lastcost && cost < thresh3) { gest = 3; lastcost = cost; Console.WriteLine("G3: "+cost); }
                //Debug.WriteLineIf(cost < thresh3, "3:"+cost);
            }
            foreach (double[] s in falseSaved)
            {
                var cost = new Dtw(s, queue.ToArray2()).GetCost();
                if (cost < lastcost && cost < thresh0) { gest = 0; lastcost = cost; }
            }

            thresh1 = Convert.ToDouble("0" + txtThres1.Text);

            thresh2 = Convert.ToDouble("0"+txtThres2.Text );
            thresh3 = Convert.ToDouble("0" + txtThres3.Text);


            if (gest > 0)
            {
                lastDetect = DateTime.Now.Ticks / TimeSpan.TicksPerSecond;
                lastGesture = gest;
                Debug.WriteLine("DETECTED " + gest + " " + lastcost);
            }
            
            if (gest==1)
            {
                lbl1.Visible = true;
                lbl2.Visible = false;
                lbl3.Visible = false;

                SerialForm.SendLeft();
                ArrowForm.ShowLeft();
            }
                else if (gest==2) 
            {
                lbl1.Visible = false;
                lbl2.Visible = true;
                lbl3.Visible = false;

                SerialForm.SendCenter();
                ArrowForm.ShowCenter();
            }
            else if(gest==3)
            {
                lbl1.Visible = false;
                lbl2.Visible = false;
                lbl3.Visible = true;

                SerialForm.SendRight();
                ArrowForm.ShowRight();
            }
            else
            {
                lbl1.Visible = false;
                lbl2.Visible = false;
                lbl3.Visible = false;
                ArrowForm.ShowNone();
            }
            //else Debug.WriteLine("NONE");
            //Debug.WriteLine(max.ToString());
        }

        void waveSource_RecordingStopped(object sender, StoppedEventArgs e)
        {
            if (waveSource != null)
            {
                waveSource.Dispose();
                waveSource = null;
            }

            if (waveFile != null)
            {
                waveFile.Dispose();
                waveFile = null;
            }

            StartBtn.Enabled = true;
        }

        private void StopBtn_Click(object sender, EventArgs e)
        {
            StopBtn.Enabled = false;

            waveSource.StopRecording();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            saved1 = queue.ToArray2( );
            isSaved1 = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            saved2 = queue.ToArray2();
            isSaved2 = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            saved3 = queue.ToArray2();
            isSaved3 = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            falseSaved.Add(queue.ToArray2());
        }

        private void txtThres1_TextChanged(object sender, EventArgs e)
        {

        }
    }


}
