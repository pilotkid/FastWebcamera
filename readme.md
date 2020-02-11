# FastWebcam

FastWebcam is just a quick wrapper for opencv to take snapshots from a webcamera. This works with WinForms apps in the .net framework. I am not sure its compatibility as it is just a quick project that I am using for an application. 

     Camera cam = null;

     public Form1()
     {
        InitializeComponent();
        cam = new Camera();
     }
     
     private timer1_Tick(object sender, EventArgs e)
     {
         pictureBox1.Image = cam.GetBitmap();
     }
     
or if you want to manually intialize the camera

    Camera cam = new Camera(false);//START THE CAMERA BUT DO NOT INITALIZE AT CLASS CONSTRUCTION
    cam.Initalize();//START THE CAMERA STREAM
    pictureBox1.Image = cam.GetBitmap();//GET IMAGE
    cam.Deinitalize();//STOP THE CAMERA STREAM
    
    
    cam.Dispose();//Dispose class
