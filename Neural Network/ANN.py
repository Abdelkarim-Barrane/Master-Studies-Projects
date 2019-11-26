# -*- coding: utf-8 -*-
"""
Created on Thu Nov 14 13:43:09 2019

I don't own this program this is just a test using a tutorial on 
predictions using the Churn Modelling using the Tenserflow neural 
network Keras

@author: karim
"""

#ANN

#Part1: Data preprocessing:

#Importing the libraries:

import pandas as pd
from sklearn.preprocessing import LabelEncoder, OneHotEncoder
from sklearn.model_selection import train_test_split
#importing the dataset:
dataset=pd.read_csv('Churn_Modelling.csv')
X=dataset.iloc[:,3:13].values
Y=dataset.iloc[:,13].values

#Encoding categorical data
label_encoder_X_1=LabelEncoder()
X[:,1]= label_encoder_X_1.fit_transform(X[:,1])
label_encoder_X_2=LabelEncoder()
X[:,2]= label_encoder_X_2.fit_transform(X[:,2])
onehotencoder=OneHotEncoder(categorical_features = [1])
X=onehotencoder.fit_transform(X).toarray()
X=X[:,1:]

#Splitting the dataset into the Training set and Test set
X_train, X_test, y_train, y_test, = train_test_split(X, Y, test_size=0.2, random_state=0)

#Feature Scaling
from sklearn.preprocessing import StandardScaler
sc= StandardScaler()
X_train=sc.fit_transform(X_train)
X_test=sc.transform(X_test)

#Part2: Making the ANN Using Keras


from keras.models import Sequential
from keras.layers import Dense

classifier= Sequential()

classifier.add(Dense(output_dim=6, init= 'uniform', activation= 'relu', input_dim=11))

#classifier.add(Dense(output_dim=6, init= 'uniform', activation= 'relu'))

classifier.add(Dense(output_dim=1, init= 'uniform', activation= 'sigmoid'))

#compliling the ANN
classifier.compile(optimizer = 'adam', loss= 'binary_crossentropy', metrics = ['accuracy'])

#fitting the ANN to the trainning set:
classifier.fit(X_train, y_train,batch_size=10, nb_epoch=100)

#Part3: Making the predictions and evaluatingthe model:
y_predict= classifier.predict(X_test)
y_predict = (y_predict > 0.5)
#Making the confusion Matrix:
from sklearn.metrics import confusion_matrix
cm= confusion_matrix(y_test, y_predict)