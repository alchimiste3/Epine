# hum.py

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


sensor = 1

def read_hum():
	try:
		donnees = grovepi.analogRead(sensor)
		if donnees == "(Empty)":
			donnees == -1
		return donnees
		time.sleep(.5)

	except IOError:
		return -1

class HumService(Service):
	version = (1, 0)
	serviceType = "urn:schemas-upnp-org:service:HumService:1"
	serviceId = "urn:upnp-org:serviceId:HumService"

	actions = {
		'GetHum': [
			ServiceActionArgument('Hum','out','Hum')
		]
	}
	
	stateVariables = [
		# Variables
		ServiceStateVariable('Hum','string',sendEvents=True)	,	
		ServiceStateVariable('ListeningHum','boolean',sendEvents=True)

	]
		
	state=EventProperty('ListeningHum')
	hum=EventProperty('Hum')

	@register_action('GetHum')
	def getState(self):
		return {
			'Hum' : str(read_hum())
		}
		
	def listen_to_hum_sensor(self, s):
		print "Listening for hum sensor values"
		tmp = 10
		tmp1 = 10
		while True:
			threadRead.mutex.acquire(1)
			print("Hum a pris le mutex\n");
			try:
				tmp1 = read_hum()
				print "J'entre dans le listen de hum\n"
				if tmp1 > 2000 or tmp1 == -1:
					print "Valeur non changee erreur\n"

				if tmp1 > (tmp - 50) and tmp1 < (tmp + 50):
					print "Valeur non changee trop proche\n"

				if (tmp1 < (tmp - 50) or tmp1 > (tmp + 50)) and tmp1 != -1:
					self.hum = tmp1
					tmp = tmp1



				#print read_lum()
				time.sleep(5)
				threadRead.mutex.release()
				print("Hum a rendu le mutex\n");
				time.sleep(3)
			except IOError as e:
				print "I/O error({0}): {1}".format(e.errno, e.strerror)
				time.sleep(0.5)
				threadRead.mutex.release()
				print("Hum a rendu le mutex\n");
		return
		
		
	def startListening(self):
		self.state=True
		self.thread = threading.Thread(target=HumService.listen_to_hum_sensor, args = (self,0))
		self.thread.daemon = True
		self.thread.start();
		return {
			'ListeningHum':True
		}
