Lancement du code

Installer Grove Pi
-> git clone https://github.com/DexterInd/GrovePi.git

Copier le dossier upnp sur la raspberry
-> scp -rp chemin_vers_upnp/upnp pi@nom.local:~

Aller dans upnp
-> cd upnp

Lancer le service
-> sudo python grovepi.py


Branchement des capteurs

Capteur tempétature et humidité:
-> branché sur le port D4

Capteur de distance:
-> branché sur le port D3

Capteur luminosité:
-> branché sur le port A0

Capteur humidité de la terre:
-> branché sur le port A1

Capteur de pression:
-> branché sur un des ports I2C
