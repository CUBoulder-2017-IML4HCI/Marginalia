# Credit: This script adapted from work by Adithya Selvaprithiviraj
# https://github.com/adithyaselv/face-expression-detect
import sys
sys.path.append('/usr/local/lib/python2.7/site-packages/')
#sys.path.append('/Users/mhallwie/Documents/Marginalia/Assets/emotion_detector/')

import argparse, warnings

try:
    from FeatureGen import *
except ImportError:
    print "FeatureGen.pyc file must be in the current directory"
    exit()

try:
    import dlib
    from skimage import io
    import numpy
    import cv2
    from sklearn.externals import joblib
except ImportError:
        print "OpenCV, dLib, scikit learn and skimage libraries must be properly installed"
        exit()

emotions = {1: "angry", 2: "contemptuous", 3: "disgusted", 4: "afraid", 5: "happy", 6: "sad", 7: "surprised"}

def Predict_Emotion(filename):

    # open image
    try:
        img = io.imread(filename)
    except:
        print str(filename) + "File Not found."
        return

    dets = detector(img, 1)


    if len(dets) == 0:
        print "Face not detected."
        return

    for k, d in enumerate(dets):

        shape = predictor(img, d)
        landmarks = []
        for i in range(68):
            landmarks.append(shape.part(i).x)
            landmarks.append(shape.part(i).y)
        
    
        landmarks = numpy.array(landmarks)
    
        # generate features
        features = generateFeatures(landmarks)
        features = numpy.asarray(features)

        # perform pca transform
        pca_features = pca.transform(features)

        # predict using trained model
        emo_predicts = classify.predict(pca_features)

        sys.stdout.write(emotions[int(emo_predicts[0])])


if __name__ == "__main__":

    warnings.filterwarnings('ignore', category=DeprecationWarning)
    warnings.filterwarnings('ignore', category=UserWarning, append=True)

    parser = argparse.ArgumentParser()
    parser.add_argument('-i', type=str, nargs='+', help="Enter the filenames with extention of an Image")
    arg=parser.parse_args()
    
    if not len(sys.argv) > 1:
        parser.print_help()
        exit()

    landmark_path = "shape_predictor_68_face_landmarks.dat"

    # initiale Dlib face detector
    detector = dlib.get_frontal_face_detector()

    # load landmark identification data
    try:
        predictor = dlib.shape_predictor(landmark_path)
    except:
        print "Predictor not found. \nYou can download a trained facial shape predictor from: \nhttp://sourceforge.net/projects/dclib/files/dlib/v18.10/shape_predictor_68_face_landmarks.dat.bz2"
        exit()

    # load trained data

    try:
        classify = joblib.load("traindata.pkl")
        pca = joblib.load("pcadata.pkl")
    except:
        print "Model not found. \nMake sure that traindata.pkl and pcadata.pkl are in the current directory"
        exit()


    for filename in arg.i:
        Predict_Emotion(filename)

    # filename = "/Users/askance/Google Drive/Marginalia/Marginalia/Assets/CamCaptures/p4.jpg"
    # Predict_Emotion(filename)

