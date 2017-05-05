# Credit: This script adapted from work by Adithya Selvaprithiviraj
# https://github.com/adithyaselv/face-expression-detect


import sys
sys.path.append('/usr/local/lib/python2.7/site-packages/')

import argparse, warnings
from datetime import datetime


try:
    from FeatureGen import*
except ImportError:
    print "FeatureGen.pyc must be in the current directory"
    exit()

try:
    import dlib
    from skimage import io
    from skimage import transform
    from skimage import util
    import numpy as np
    from matplotlib import pyplot as plt
    from sklearn.externals import joblib
    from sklearn.naive_bayes import GaussianNB
    from sklearn.svm import SVC
    from sklearn import decomposition
    from sklearn.cluster import KMeans

except ImportError:
        print "OpenCV, dLib, scikit learn and skimage libraries must be properly installed"
        exit()

# emotions = {1: "anger", 2: "contempt", 3: "disgust", 4: "fear", 5: "happy", 6: "sadness", 7: "surprise"}


#
# get_data_from_file imports training data from text files and returns a list (or list of lists)
def get_data_from_file(X_filepath, y_filepath):
    X_train = []
    y_train = []

    # get X data
    with open(X_filepath, 'r') as X_file:
        X_train = X_file.readlines()
    X_file.close()
    X_train = [x_line.strip() for x_line in X_train]
    X_train = [x_line.split(',') for x_line in X_train]

    # get y data
    with open(y_filepath, 'r') as y_file:
        y_train = y_file.readline().strip().split(',')
    y_file.close()

    return X_train, y_train


#
# pre_process_data converts the standard data format into a feature matrix
def pre_process_data(X_train):
    # generate features
    feature_matrix = []
    for x_train in X_train:
        features = generateFeatures(x_train)
        feature_matrix.append(features)
    np.asarray(feature_matrix)
    return feature_matrix


#
# get_cal_matrix fetches and formats the calibration data
def get_cal_data(detector, predictor, happy_file, sad_file, angry_file, scared_file):
    emotions = {angry_file: 1, scared_file: 4, happy_file: 5, sad_file: 6}

    # Initialize matrix
    calibration_matrix = []
    y_calibration = []

    # Get calibration data
    for filename in [happy_file, sad_file, angry_file, scared_file]:
        input_file = filename + '.jpg'
        # open image
        try:
            img = io.imread(input_file)
        except:
            print "Exception: File Not found."
            return

        # Make sure dlib can recognize base calibration image
        check_image = get_features(img, detector, predictor)
        if len(check_image) == 1:
            return np.zeros((1,1)), np.zeros((1,1))

        for angle in range(-10, 10, 2):
            img_rot = transform.rotate(img, angle, resize=False, mode='constant', cval=1.0)
            with warnings.catch_warnings():
                warnings.simplefilter("ignore")
                img2 = util.img_as_ubyte(img_rot)

            new_vector = get_features(img2, detector, predictor)
            if len(new_vector) > 1:
                calibration_matrix.append(new_vector)
                y_calibration.append(emotions[filename])

        img = np.fliplr(img)

        for angle in range(-10, 10, 2):
            img_rot = transform.rotate(img, angle, resize=False, mode='constant', cval=1.0)
            with warnings.catch_warnings():
                warnings.simplefilter("ignore")
                img2 = util.img_as_ubyte(img_rot)

            new_vector = get_features(img2, detector, predictor)
            if len(new_vector) > 1:
                calibration_matrix.append(new_vector)
                y_calibration.append(emotions[filename])

    calibration_matrix = np.asarray(calibration_matrix).astype(np.float)
    y_calibration = np.asarray(y_calibration).astype(np.float).reshape((len(y_calibration),))
    return calibration_matrix, y_calibration


#
# get_features calculates features for an image and returns the associated feature vector
def get_features(img, detector, predictor):
    dets = detector(img, 1)

    if len(dets) == 0:
        # print "Unable to find any face."
        return np.zeros((1,1))

    #print "Found face"
    for k, d in enumerate(dets):

        shape = predictor(img, d)
        landmarks = []
        for i in range(68):
            landmarks.append(shape.part(i).x)
            landmarks.append(shape.part(i).y)

    landmarks = np.array(landmarks)

    # generate features
    features = generateFeatures(landmarks)
    return np.asarray(features)

#
# train_emotion
def Train_Emotion(classifier, pca_matrix, y_train):

        # Train the classifier
        classifier.fit(pca_matrix, y_train)


def main():
    start_time = datetime.now()
    # define files for training data
    X_file = './faces_X_train.txt'
    y_file = './faces_y_train.txt'

    # define calibration files
    happy_file = 'Happy'
    sad_file = 'Sadness'
    angry_file = 'Anger'
    scared_file = 'Fear'

    # define landmark file
    landmark_path = "shape_predictor_68_face_landmarks.dat"

    # initialize Dlib face detector -- required for training on calibration photos
    detector = dlib.get_frontal_face_detector()

    # load landmark identification data -- required for training on calibration photos
    try:
        predictor = dlib.shape_predictor(landmark_path)
    except:
        print "Trained facial shape predictor missing. \nYou can download a trained facial shape predictor from: \nhttp://sourceforge.net/projects/dclib/files/dlib/v18.10/shape_predictor_68_face_landmarks.dat.bz2"
        exit()

    # import data from file
    X_train, y_train = get_data_from_file(X_file, y_file)
    X_train = np.asarray(X_train).astype(np.float)
    y_train = np.asarray(y_train).astype(np.float).reshape((327,))

    # Pre-process sample data
    feature_matrix = pre_process_data(X_train)

    # Calibrate training data with user supplied samples
    calibration_matrix, y_calibration = get_cal_data(detector, predictor, happy_file, sad_file, angry_file, scared_file)
    if len(calibration_matrix) == 1 and len(y_calibration) == 1:
        print "Calibration image bad"
        exit(1)

    feature_matrix = np.concatenate((feature_matrix, calibration_matrix), axis=0)
    y_train = np.concatenate((y_train, y_calibration))

    # Perform principal component analysis
    pca_model = decomposition.PCA(n_components=min(len(X_train) + 4, len(X_train[0])))
    pca_model.fit(feature_matrix)
    pca_matrix = pca_model.transform(feature_matrix)

    # Initialize classifier and principal component analysis models
    # classifier = GaussianNB()
    classifier = SVC(kernel='poly', degree=3)
    #pca_model = decomposition.PCA(n_components=min(len(X_train), len(X_train[0])))


    # Train classifier
    Train_Emotion(classifier, pca_matrix, y_train)


    # Dump model to file for persistence
    joblib.dump(pca_model, "pca_model_file.pkl")
    joblib.dump(classifier, "classifier_file.pkl")

    # print "Time elapsed: " + str(datetime.now() - start_time)
    print "0"

if __name__ == "__main__":
    main()