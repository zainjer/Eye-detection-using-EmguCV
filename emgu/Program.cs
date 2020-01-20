using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Drawing;
using Emgu;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;

namespace emgu
{
    class Program
    {
        static void Main(string[] args)
        {
            
            //Edit this line to change curr
            int camIndex=1;
            CascadeClassifier faceCascade = new CascadeClassifier("haarcascade_frontalface_alt.xml");
            CascadeClassifier eyeCascade = new CascadeClassifier("haarcascade_eye_tree_eyeglasses.xml");      
            VideoCapture webCamFeed = new VideoCapture(camIndex);
            if (!webCamFeed.IsOpened)
            {
                Console.WriteLine("Camera not detected [Jaani camera to lagao koi]");
            }
            Mat frame = new Mat();
            while (true)
            {
                webCamFeed.Read(frame);
                detectEyes(frame, faceCascade, eyeCascade);
                CvInvoke.Imshow("Webcam", frame);
                if (CvInvoke.WaitKey(30) >= 0)
                    break;
            }
        }
        private static void detectEyes(Mat frame, CascadeClassifier faceCascade, CascadeClassifier eyeCascade)
        {
            Mat grayscaleVersion = new Mat();
            CvInvoke.CvtColor(frame, grayscaleVersion, ColorConversion.Bgr2Gray); //Dekh jaan ye method convert maarde image ko Grayscale me
            CvInvoke.EqualizeHist(grayscaleVersion, grayscaleVersion); //ye image ki contrast bharaey ga
            Rectangle[] faces = faceCascade.DetectMultiScale(grayscaleVersion, 2.1, 0);

            if (faces.Length == 0) return;

            CvInvoke.Rectangle(frame, faces[0], new MCvScalar(0, 0, 255), 2);            
        }

        //public void RunWebCam()
        //{
        //    VideoCapture webCamFeed = new VideoCapture(0);

        //    if (!webCamFeed.IsOpened)
        //    {
        //        Console.WriteLine("Camera not detected [Jaani camera to lagao koi]");
        //    }

        //    Mat frame = new Mat();
        //    while (true)
        //    {
        //        webCamFeed.Read(frame);
        //        CvInvoke.Imshow("Webcam", frame);
        //        drawFaceRectangle(frame, faceCascade, eyeCascade);

        //        if (CvInvoke.WaitKey(30) >= 0)
        //            break;
        //    }
        //}

        public void drawFaceRectangle(Mat frame,CascadeClassifier faceCascade,CascadeClassifier eyeCascade)
        {
            Mat grayscaleVersion = new Mat();
            CvInvoke.CvtColor(frame, grayscaleVersion, ColorConversion.Bgr2Gray); //Dekh jaan ye method convert maarde image ko Grayscale me
            CvInvoke.EqualizeHist(grayscaleVersion, grayscaleVersion);
            //Rectangle

        }
    }

    
}
