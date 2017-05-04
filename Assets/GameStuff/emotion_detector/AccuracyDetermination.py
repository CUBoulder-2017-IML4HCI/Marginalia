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
    from sklearn.model_selection import cross_val_score
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
# train_emotion
def Train_Emotion(classifier, pca_matrix, y_train):

        # Train the classifier
        classifier.fit(pca_matrix, y_train)

        # Perform 10-fold cross-validation
        scores = cross_val_score(classifier, pca_matrix, y_train, cv=5)
        print("Accuracy: %0.2f (+/- %0.2f)" % (scores.mean(), scores.std() * 2))


def main():

    # define files for training data
    X_file = './faces_X_train.txt'
    y_file = './faces_y_train.txt'

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

    # Perform principal component analysis
    pca_model = decomposition.PCA(n_components=min(len(X_train), len(X_train[0])))
    pca_model.fit(feature_matrix)
    pca_matrix = pca_model.transform(feature_matrix)

    # Determine accuracy for SVM with polynomial kernel
    for ii in range(1, 10, 1):
        print "degree = " + str(ii)
        classifier = SVC(kernel='poly', degree=ii)
        Train_Emotion(classifier, pca_matrix, y_train)


if __name__ == "__main__":
    main()