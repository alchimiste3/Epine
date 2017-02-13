Installer Grove Pi
-> git clone https://github.com/DexterInd/GrovePi.git

Copier le dossier upnp sur la raspberry
-> scp -rp chemin_vers_upnp/upnp pi@nom.local:~

Aller dans upnp
-> cd upnp

Lancer le service
-> sudo python grovepi.py
