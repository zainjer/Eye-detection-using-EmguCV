using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu;
using Emgu.CV;
using Emgu.CV.Structure;

namespace emgu
{
    class Program
    {
        static void Main(string[] args)
        {
                        
//**************************************************************************************
            VideoCapture webCamFeed = new VideoCapture(0);

            if (!webCamFeed.IsOpened)
            {
                Console.WriteLine("Camera not detected [Jaani camera to lagao koi]");
            }

            Mat frame = new Mat();
            while (true)
            {
               webCamFeed.Read(frame);
               CvInvoke.Imshow("Webcam", frame);
                if (CvInvoke.WaitKey(30) >= 0)
                    break;
            }                                                 
        }

        VideoCapture GetCamFeed(int CamIndex)
        {
            VideoCapture webCamFeed = new VideoCapture(0);

            if (!webCamFeed.IsOpened)
            {
                Console.WriteLine("Camera not detected [Jaani camera to lagao koi]");
            }

            return webCamFeed;
        }

        Mat SetCamfeedToMat(VideoCapture vc)
        {
            Mat frame = new Mat();
            vc.Read(frame);
        }
    }
}
