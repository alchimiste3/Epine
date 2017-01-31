# luminosite.py

import grovepi
import logging
from threading import Thread, Lock
import time
import threading
from twisted.internet import reactor
from pyupnp.event import EventProperty
from pyupnp.device import Device, DeviceIcon
from pyupnp.logr import Logr
from pyupnp.services import register_action, Service, ServiceActionArgument, ServiceStateVariable
from pyupnp.ssdp import SSDP
from pyupnp.upnp import UPnP
from test.threadRead import threadRead

import os
import glob
import time

os.system('modprobe w1-gpio')
os.system('modprobe w1-therm')

light_sensor = 0
grovepi.pinMode(light_sensor,"INPUT")


def read_lum():
	try:
		# Get sensor value
		sensor_value = grovepi.analogRead(light_sensor)
		#sensor_value = 50

		# Calculate resistance of sensor in K
		#resistance = (float)(1023 - sensor_value) * 10 / sensor_value

		donnees = (sensor_value)
		return donnees
		time.sleep(.5)

	except IOError:
		return -1

class LuminositeService(Service):

	mutex = Lock()
	version = (1, 0)
	serviceType = "urn:schemas-upnp-org:service:LuminositeService:1"
	serviceId = "urn:upnp-org:serviceId:LuminositeService"

	actions = {
		'GetLum': [
			ServiceActionArgument('Lum','out','Lum')
		]
	}
	
	stateVariables = [
		# Variables
		ServiceStateVariable('Lum','string',sendEvents=True)	,	
		ServiceStateVariable('ListeningLum','boolean',sendEvents=True)

	]
		
	state=EventProperty('ListeningLum')
	lum=EventProperty('Lum')

	@register_action('GetLum')
	def getState(self):
		return {
			'Lum' : str(read_lum())
		}
		
	def listen_to_lum_sensor(self, s):
		print "Listening for lum sensor values"
		tmp = 10
		tmp1 = 10
		while True:
			threadRead.mutex.acquire(1)
			print("Lum a pris le mutex\n");
			try:
				tmp1 = read_lum()
				print "J'entre dans le listen de lum\n"
				if tmp1 == 0 or tmp1 > 2000 or tmp == -1:
					print "Valeur non changee erreur\n"

				if tmp1 > (tmp - 50) and tmp1 < (tmp + 50):
					print "Valeur non changee trop proche\n"

				if (tmp1 < (tmp - 50) or tmp1 > (tmp + 50)) and tmp != -1:
					self.lum = tmp1
					tmp = tmp1



				#print read_lum()
				time.sleep(5)
				threadRead.mutex.release()
				print("Lum a rendu le mutex\n");
				time.sleep(3)
			except IOError as e:
				print "Erreur\n"
				print "I/O error({0}): {1}".format(e.errno, e.strerror)
				time.sleep(0.5)
				threadRead.mutex.release()
				print("Lum a rendu le mutex\n");
		return
		
		
	def startListening(self):
		self.state=True
		
		self.thread = threading.Thread(target=LuminositeService.listen_to_lum_sensor, args = (self,0))
		self.thread.daemon = True
		self.thread.start();
		return {
			'ListeningLum':True
		}

