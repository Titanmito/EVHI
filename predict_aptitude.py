import numpy as np
from joblib import dump, load
from sklearn import svm
from time import time

if __name__ == '__main__':
    dataset_filename = 'users_data_init_level.csv'
    lar_filename = 'last_aptitude_request.csv'
    lareply_filename = 'last_aptitude_reply.txt'
    lds_filename = 'last_dataset_size.txt'
    model_filename = 'model0.joblib'
    dataset_check_progress = time()
    dataset_check_reload = 3.0
    model_class_ref = svm.SVC
    delimiter = ','

    model = load(model_filename)

    while True:
        if time() - dataset_check_progress >= dataset_check_reload:
            print("10 seconds passed")
            dataset_check_progress = time()
            model = load(model_filename)
        try:
            features_to_predict = np.genfromtxt(lar_filename, delimiter=delimiter)
        except:
            with open(lar_filename, 'r') as lar_file:
                print(lar_file.read())
        if features_to_predict.size > 0:
            print('Features received')
            print(features_to_predict)
            with open(lar_filename, 'w'):
                pass
            y = model.predict(np.array([features_to_predict]))[0]
            with open(lareply_filename, 'w') as lareply_file:
                lareply_file.write(str(int(y)))
            print('Aptitude sent')
            print(int(y))
