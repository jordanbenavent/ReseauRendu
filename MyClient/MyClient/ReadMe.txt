
Ce projet contient une analyse statistique de différents serveurs web :

Lancement du projet : 
	
	- Ouvrir la solution à l'aide de visual studio.
	- Ouvrir votre navigateur avec l'url : http://localhost:8080
	- En cliquant sur un bouton vous verrez les statistiques calcultées correspondant aux exercice 1, 2 et 3.


Dans les statisques du bouton "Voir les statistiques des seveurs" :
	
	- Un lancement de requêtes correspondantes au fichier urls/urls1.txt sera effectué.
	- Vous verrez un tableau apparaitre indiquant : 
		* Les quatres serveurs les plus utilisés
		* Le nombre d'utilisations
		* Le pourcentage d'utilisation

Dans les statisques du bouton "Voir les statistiques de l'age des pages du Wikipedia" :

	- Un lancement de requêtes correspondantes au fichier urls/urls2.txt sera effectué. (Vous pouvez ajoutez des liens, je vous conseils de rester sur des liens wikipedia pour plus de coherence)
	- Vous verrez un tableau apparaitre indiquant : 
		* Le nombre de site appelé
		* La moyenne de l'age des pages
		* L'ecart type par rapport a l'age

Dans les statisques du bouton "Voir les statistiques pertinentes" :
	
	- Un lancement de requêtes correspondantes au fichier urls/urls3.txt sera effectué.
	- Vous verrez un tableau apparaitre indiquant : 
		* Le nombre de site appelé
		* La moyenne de la taille des pages
		* L'encoding le plus utilise (il est possible que les headers ne renvoient pas cette information)
		* Le temps de reponse moyen 

	C'est statistiques sont interessantes car il est important de savoir le nombre de site qu'on a appele. 
	La moyenne de la taille des pages est utile pour avoir une idee de quelle taille visee lors de la creation de nos pages
	L'encoding peut servir sur comment sont encoder le contenu des pages.
	Le temps de réponse moyen est utile pour renvoyer une reponse aux utilisateurs dans cette ordre de grandeur.
