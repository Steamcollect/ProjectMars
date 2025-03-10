# ProjectMars
 
Mouvement du joueur avec ZQSD
Quand il bouge il call "RSE_OnEntityEnter" & "RSE_OnEntityExit"

Le world manager contient les coordonnées de chaque tuile et les props
Un SSO qui contient le type de chaque tuile ou props
Contient aussi la prefabs a faire spawn

La prefab peu contenir un script enfant de "Interactible" pour détecter le "RSE_OnEntityEnter" & "RSE_OnEntityExit"