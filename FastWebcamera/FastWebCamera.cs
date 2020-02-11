using System;
using OpenCvSharp;
using System.Drawing;
using System.Threading;
using OpenCvSharp.Extensions;

namespace FastWebcam
{
    public class FastWebcam
    {
        /// <summary>
        /// Initalizes camera feed
        /// </summary>
        /// <param name="AutoActivate">If set to false this disables the camera from starting, you must call the Initalize method.</param>
        public FastWebcam(bool AutoActivate = true)
        {
            if (AutoActivate)
            {
                Initalize();
            }
        }

        // Create class-level accesible variables
        static VideoCapture capture;
        static Mat frame;
        static Bitmap image;
        private static Thread camera;
        private static bool isCameraRunning = false;
        private bool imagetakinginprogress = false;

        /// <summary>
        /// Starts the camera feed
        /// </summary>
        public void Initalize()
        {
            CaptureCamera();
            isCameraRunning = true;
        }

        private void CaptureCamera()
        {
            camera = new Thread(new ThreadStart(CaptureCameraCallback));
            camera.Start();
        }

        private void CaptureCameraCallback()
        {
            if (!isCameraRunning)
            {
                return;
            }
            frame = new Mat();
            capture = new VideoCapture(0);
            capture.Open(0);

            if (capture.IsOpened())
            {
                while (isCameraRunning)
                {

                    capture.Read(frame);
                    image = BitmapConverter.ToBitmap(frame);
                }
            }
        }

        /// <summary>
        /// Gets a bitmap from the camera
        /// </summary>
        /// <returns>Bitmap image from camera</returns>
        public Bitmap GetBitmap()
        {
            if (isCameraRunning)
            {
                while (imagetakinginprogress) { }
                try
                {
                    return new Bitmap(image);
                }
                catch
                {
                    return new Bitmap(image);
                }
            }
            else
                throw new Exception("Cannot take picutre if the camera is not initalized!");
        }

        /// <summary>
        /// Deinitalizes the camera. Can be reinitalized.
        /// </summary>
        public void Deinitalize()
        {
            camera.Abort();
            capture.Release();
            isCameraRunning = false;
        }

        /// <summary>
        /// Destroys the camera
        /// </summary>
        ~FastWebcam()
        {
            Deinitalize();
            capture.Dispose();
            frame.Dispose();
            image.Dispose();
        }
    }
}
