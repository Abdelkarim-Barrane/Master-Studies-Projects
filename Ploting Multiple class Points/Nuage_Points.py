# -*- coding: utf-8 -*-


"""
Created on Sat Jul  6 15:34:44 2019

@author: karim
"""
import matplotlib.pyplot as plt
import numpy as np   
import random  

class Nuage() :
    
    def __init__(self, classes=2):
        """ La classe Nuage :
            
            *classes = le nombre de classes dans le nuage
            *M= la liste que l'on peut utiliser qui contients que les x et y
            *e_click= pour activer la fonction de l evenement du clic
            *e_press= pour activer la fonction qui utilise l événement de la touche clavier
            *color[] = est pour assigner à chaque classe une couleur d'une façon aléatoire
            *now_color= pour préserver la couleur utilisé à chaque clic sur un numéro d'une classe
            *now_classe= pour préserver la classe utilisé à chaque clic sur un numéro d'une classe """

        self.classes= classes
        self.M=[]
        self.e_click, self.e_press, self.now_color ,self.now_class = [0,0,0,0]       
        #assigner à chaque un couleur alléatoire
        self.color = ["#"+''.join([random.choice('00112233445566778899AABBCCDDEEFF') for j in range(6)]) for i in range(classes)]                      
        self.fig = plt.figure()
        self.ax = self.fig.gca()    
        #dessiner X et Y en titres
        self.ax.set_xlabel('X_value')
        self.ax.set_ylabel('Y_value')
        #Limiter les valeurs de 0 à 100
        self.ax.set_xlim([0, 100])
        self.ax.set_ylim([0, 100])
        
        #création du nouveau fichier csv et écrire ensuite les titres classe x_data et y_data
        with open('monfichier.csv', 'w') as fp:
            fp.write('classe,x_data,y_data')
        
        #connection des événement de touche clavier à la figure de plot
        self.e_press = self.fig.canvas.mpl_connect('key_press_event', self._onpress)
        plt.show()

        
    def _onpress(self,event):
        #fonction de touche clavier 
        
        #stockage de la valeur du clic et transformer en int    
        i= int(event.key)   
        
        if i in range(1 , (int(self.classes) + 1)) :
        #stockage du numéro de class et de couleur touché à l'instant et connection de l'événement du clic
            self.now_class= i
            self.now_color=self.color[self.now_class - 1]        
            self.e_click = self.fig.canvas.mpl_connect('button_press_event', self._onclick)
        #si on touche une autre touche que le numéro de classe rien ne se passe 
        else :
            self.fig.canvas.mpl_disconnect(self.e_click)

    def _onclick(self,event):
        #fonction de lévénement du clic
        
        #écriture dans le fichier csv ouvert de chaque point
        with open('monfichier.csv', 'a+') as fp:
            fp.write('\n'+str(event.xdata)+','+str(event.ydata)+','+str(self.now_class))

        #enregistrement des données du point cliqué dans la liste M
        a=np.array([event.xdata, event.ydata])
        self.M.append(a)
        
        #affichage des points dans le diagramme en cliquant et dans la console 
        print('classe= %i , xdata=%f, ydata=%f' %(self.now_class,event.xdata, event.ydata))
        plt.plot(event.xdata, event.ydata, marker='o', color=self.now_color)
        self.fig.canvas.draw()


""" 

                                            Fonctionnement:
                                            
    1) On saisie une valeur des classes entre 1 et 9 
    2) touche shift+le numéro de la classe ou bien tout simplement un nombre parmis le nombre de classes
    (ne pas faire attention aux erreurs de shift) voulu et il n y a pas de problème en changeant le numéro de la 
    classe et aussi on peut cliquer autant qu'on veut sans changer de numéro de classe.
    3) les couleurs changent avec les numéros de classes et les données sont stockées dans le monfichier.csv
    
    
"""
print("donner le nombre de classes voulu entre 1 et 9 : \n")

c=int(input())

while not (c > 0 and c <= 9 ):
    print("donner un nombre entre 1 et 9")
    c=int(input())

l=Nuage(c)
s=l.M







 