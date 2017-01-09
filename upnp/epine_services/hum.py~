# hum.py

import grovepi

import logging
from threading import Thread
import time
import threading
from twisted.internet import reactor
from pyupnp.event import EventProperty
from pyupnp.device import Device, DeviceIcon
from pyupnp.logr import Logr
from pyupnp.services import register_action, Service, ServiceActionArgument, ServiceStateVariable
from pyupnp.ssdp import SSDP
from pyupnp.upnp import UPnP

import os
import glob
import time

os.system('modprobe w1-gpio')
os.system('modprobe w1-therm')


sensor = 1


def read_hum():
	try:
		return (grovepi.analogRead(sensor))
		time.sleep(.5)

	except IOError:
		return ("Error Hum")

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
		while True:
			try:
				self.hum = read_hum()
				time.sleep(3)
			except IOError as e:
				print "I/O error({0}): {1}".format(e.errno, e.strerror)
				time.sleep(0.5)
		return
		
		
	def startListening(self):
		self.state=True
		
		self.thread = threading.Thread(target=HumService.listen_to_hum_sensor, args = (self,0))
		self.thread.daemon = True
		self.thread.start();
		return {
			'ListeningHum':True
		}
