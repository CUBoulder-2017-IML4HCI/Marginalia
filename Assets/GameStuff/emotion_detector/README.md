# Marginalia ML Component

###Training a model
python EmoTrain.py

###Predict emotion 
python EmoDetect.py -i <image.jpg>

###Landmarks Detector Data
[https://github.com/tzutalin/dlib-android/blob/master/data/shape_predictor_68_face_landmarks.dat](https://github.com/tzutalin/dlib-android/blob/master/data/shape_predictor_68_face_landmarks.dat)

Download this and place it in the project folder.

###Dependencies

* [Scikit learn]()
* [dlib with python support]()
* [numpy]()
* [skimage]()

###Dataset used for training

[http://www.consortium.ri.cmu.edu/ckagree/](http://www.consortium.ri.cmu.edu/ckagree/)

###Acknowledgements
This project is heavily based on [face-expression-detect](http://adithyaselv.com/)