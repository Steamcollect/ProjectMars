# ProjectMars
 
IMPORTANT : Mettre l'onglet game en "Simulator" puis choisir l'appareil de votre choix

Mouvement du joueur avec un swip du click
Quand il bouge il call "RSE_OnEntityEnter" & "RSE_OnEntityExit"

Le world manager contient les coordonnées de chaque tuile
Un SSO qui contient le type de chaque tuile ou props
Contient aussi la prefabs a faire spawn

Le world manager contient une liste de props ainsi que leur nombre respectif, qui seront positionné UNIQUEMENT sur les tiles "WALKABLE"
La prefab peu contenir un script enfant de "Interactible" pour détecter le "RSE_OnEntityEnter" & "RSE_OnEntityExit"

Time manager contenant le temps maximum possible pour chaquex niveaux