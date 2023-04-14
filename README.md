Analyse statistique de différents serveurs web
*Lancement du projet*

**Ouvrez la solution à l'aide de Visual Studio.**
    - Accédez au site en ouvrant votre navigateur avec l'URL : http://localhost:8080.
    - Cliquez sur un bouton pour voir les statistiques calculées correspondant aux exercices 1, 2 et 3.
    
    
**Statistiques du bouton "Voir les statistiques des serveurs"**

  - Un lancement de requêtes correspondantes au fichier urls/urls1.txt sera effectué.
  - Un tableau s'affichera, indiquant :
        * Les quatre serveurs les plus utilisés
        * Le nombre d'utilisations
        * Le pourcentage d'utilisation
Statistiques du bouton "Voir les statistiques de l'âge des pages du Wikipedia"
Un lancement de requêtes correspondantes au fichier urls/urls2.txt sera effectué. (Vous pouvez ajouter des liens, nous vous conseillons de rester sur des liens Wikipédia pour plus de cohérence.)
Un tableau s'affichera, indiquant :
Le nombre de sites appelés
La moyenne de l'âge des pages
L'écart type par rapport à l'âge
Statistiques du bouton "Voir les statistiques pertinentes"
Un lancement de requêtes correspondantes au fichier urls/urls3.txt sera effectué.
Un tableau s'affichera, indiquant :
Le nombre de sites appelés
La moyenne de la taille des pages
L'encodage le plus utilisé (il est possible que les headers ne renvoient pas cette information)
Le temps de réponse moyen
Ces statistiques sont intéressantes car il est important de savoir le nombre de sites qu'on a appelés. La moyenne de la taille des pages est utile pour avoir une idée de quelle taille viser lors de la création de nos pages. L'encodage peut servir à déterminer comment le contenu des pages est encodé. Le temps de réponse moyen est utile pour renvoyer une réponse aux utilisateurs dans cette ordre de grandeur.
