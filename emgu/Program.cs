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
            
            //Edit this line to change current camera
            int camIndex=1;
            CascadeClassifier faceCascade = new CascadeClassifier("haarcascade_frontalface_alt.xml");
            CascadeClassifier eyeCascade = new CascadeClassifier("haarcascade_eye_tree_eyeglasses.xml");      
            VideoCapture webCamFeed = new VideoCapture(camIndex);
            if (!webCamFeed.IsOpened)
            {
                Console.WriteLine("Camera not detected [Jaani camera to lagao koi]");
            }

            //Uncomment this line
            Mat frame = new Mat();

            //REMOVE THIS LINE 
            //Mat frame = new Mat(@"C:\Users\Areeb\Pictures\yellow.jpg");



            while (true)
            {
                webCamFeed.Read(frame);
                detectEyes(frame, faceCascade, eyeCascade);
                CvInvoke.Imshow("Webcam", frame);
                
                if (CvInvoke.WaitKey(30) >= 0)
                    break;
            }
            Console.ReadLine();
        }
        private static void detectEyes(Mat frame, CascadeClassifier faceCascade, CascadeClassifier eyeCascade)
        {
            Mat grayscaleVersion = new Mat();  //cv::Mat grayscale;

            CvInvoke.CvtColor(frame, grayscaleVersion, ColorConversion.Bgr2Gray); //cv::cvtColor(frame, grayscale, CV_BGR2GRAY);   //Dekh jaan ye method convert maarde image ko Grayscale me

            CvInvoke.EqualizeHist(grayscaleVersion, grayscaleVersion); //cv::equalizeHist(grayscale, grayscale);  //ye image ki contrast bharaey ga

            Rectangle[] faces; //  std::vector<cv::Rect> faces;

            faces = faceCascade.DetectMultiScale(grayscaleVersion, 1.2, 0); //faceCascade.detectMultiScale(grayscale, faces, 1.1, 2, 0 | CV_HAAR_SCALE_IMAGE, cv::Size(150, 150));           

            if (faces.Length == 0) return; //if (faces.size() == 0) return;  // none face was detected

            

            Mat face = new Mat(frame,faces[0]); //  cv::Mat face = frame(faces[0]); // crop the face

            Rectangle[] eyes; // std::vector<cv::Rect> eyes;

            eyes = eyeCascade.DetectMultiScale(face, 1.2, 0); //eyeCascade.detectMultiScale(face, eyes, 1.1, 2, 0 | CV_HAAR_SCALE_IMAGE, cv::Size(150, 150));

            CvInvoke.Rectangle(frame, faces[0], new MCvScalar(0, 0, 255), 1); //drawing a face on Frame

            if (eyes.Length>3) return;

            foreach (Rectangle eye in eyes)
            {
                CvInvoke.Rectangle(face, eye, new MCvScalar(0, 255, 255), 1);
            }
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

    }

    
}
