import numpy as np
from joblib import dump, load
from sklearn import svm
from time import time

if __name__ == '__main__':
    dataset_filename = 'users_data_init_level.csv'
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
            dataset = np.genfromtxt(dataset_filename, delimiter=delimiter, skip_header=1)
            with open(lds_filename, 'r') as lds_file:
                lds = int(lds_file.read())
            if dataset.shape[0] == lds:
                print("Model is the same")
            else:
                print("Model will be fitted!")
                model = model_class_ref()
                X, y = dataset[:, 2:], dataset[:, 1]
                model.fit(X, y)
                dump(model, model_filename)
                with open(lds_filename, 'w') as lds_file:
                    lds_file.write(str(dataset.shape[0]))
