# Credit: This script adapted from work by Adithya Selvaprithiviraj
# https://github.com/adithyaselv/face-expression-detect


import argparse, sys, warnings


try:
    from FeatureGen import*
except ImportError:
    print "FeatureGen.pyc must be in the current directory"
    exit()

try:
    import dlib
    from skimage import io
    import numpy as np
    from sklearn.externals import joblib
    from sklearn.naive_bayes import GaussianNB
    from sklearn.svm import SVC
    from sklearn import decomposition
except ImportError:
        print "OpenCV, dLib, scikit learn and skimage libraries must be properly installed"
        exit()

emotions = {1: "Angry", 2: "Contempt", 3: "Disgust", 4: "Afraid", 5: "Happy", 6: "Sad", 7: "Surprise"}


#
# load_model_from_file imports a saved model from a pickle file
def load_model_from_file(pca_file, model_file):

    try:
        classify = joblib.load(model_file)
        pca = joblib.load(pca_file)
    except:
        print "Unable to load trained data. \nMake sure that traindata.pkl and pcadata.pkl are in the current directory"
        exit()

    return pca, classify

#
# predict_emotion
def Predict_Emotion(classifier, pca_model, detector, predictor, filename):

    # open image
    try:
        img = io.imread(filename)
    except:
        print "Exception: File Not found."
        return

    dets = detector(img, 1)

    if len(dets) == 0:
        print "Unable to find any face."
        return

    for k, d in enumerate(dets):

        shape = predictor(img, d)
        landmarks = []
        for i in range(68):
            landmarks.append(shape.part(i).x)
            landmarks.append(shape.part(i).y)

        landmarks = np.array(landmarks)

        # generate features
        features = generateFeatures(landmarks)
        features = np.asarray(features)

        # perform pca transform
        pca_features = pca_model.transform(features)

        # predict using trained model
        emo_predicts = classifier.predict(pca_features)

        sys.stdout.write(emotions[int(emo_predicts[0])])
        #print ""


def main():

    warnings.filterwarnings('ignore', category=DeprecationWarning)
    warnings.filterwarnings('ignore', category=UserWarning, append=True)

    parser = argparse.ArgumentParser()
    parser.add_argument('-i', type=str, nargs='+', help="Enter the filenames with extention of an Image")
    arg = parser.parse_args()

    
    if not len(sys.argv) > 1:
        parser.print_help()
        exit()

    # define files that contain previous model
    model_file = 'classifier_file.pkl'
    pca_file = 'pca_model_file.pkl'

    landmark_path = "shape_predictor_68_face_landmarks.dat"

    # initiale Dlib face detector
    detector = dlib.get_frontal_face_detector()

    # load landmark identification data
    try:
        predictor = dlib.shape_predictor(landmark_path)
    except:
        print "Trained facial shape predictor missing. \nYou can download a trained facial shape predictor from: \nhttp://sourceforge.net/projects/dclib/files/dlib/v18.10/shape_predictor_68_face_landmarks.dat.bz2"
        exit()

    # load previously saved models
    pca_model, classifier = load_model_from_file(pca_file, model_file)

    # classify
    for filename in arg.i:
        Predict_Emotion(classifier, pca_model, detector, predictor, filename)


if __name__ == "__main__":
    main()