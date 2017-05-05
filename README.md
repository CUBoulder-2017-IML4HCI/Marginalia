# Marginalia
https://github.com/CUBoulder-2017-IML4HCI/Marginalia

### Required Libraries

* Python 2.7
* Unity 3D v 5.5.2f1
* Python: [Scikit learn]()
* Python: [dlib with python support]()
* Python: [numpy]()
* Python: [skimage]()
* Unity: [Vuforia]()
* Others?
* Landmarks Detector Data: [https://github.com/tzutalin/dlib-android/blob/master/data/shape_predictor_68_face_landmarks.dat](https://github.com/tzutalin/dlib-android/blob/master/data/shape_predictor_68_face_landmarks.dat)

### Build Instructions
* NOTE: This build requires an external web-cam running via USB
* NOTE: This build requires a MacOS operating system
* Install Unity
* Install Python 2.7
* Install required python libraries (we suggest using the pip utility)
* Place the Landmarks Detector data file in the marginalia_ml folder within the Unity project.
* Download and install Vuforia plugin for Unity [https://developer.vuforia.com/downloads/sdk](https://developer.vuforia.com/downloads/sdk)
* Follow Vuforia instructions here (NOTE: you will need to set up your own license key and image targets database): https://library.vuforia.com/articles/Solution/Getting-Started-with-Vuforia-for-Unity-Development
* Clone this github repo [https://github.com/CUBoulder-2017-IML4HCI/Marginalia](https://github.com/CUBoulder-2017-IML4HCI/Marginalia)
* Ensure the correct settings for running Vuforia (open Vuforia config on AR camera object, ensure your license key and image target database are loaded, ensure the correct camera is chosen for AR, should be the USB Webcam)
* Run the application in Unity 3D v 5.5.2f1

### Acknowledgements

* Base training data: [http://www.consortium.ri.cmu.edu/ckagree/](http://www.consortium.ri.cmu.edu/ckagree/)
* ML component based on [https://github.com/adithyaselv/face-expression-detect](https://github.com/adithyaselv/face-expression-detect)
* Emotion Elicitation videos based on [http://nemo.psp.ucl.ac.be/FilmStim/] (http://nemo.psp.ucl.ac.be/FilmStim/)
