# luminosite.py

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


light_sensor = 0
grovepi.pinMode(light_sensor,"INPUT")


#base_dir = '/sys/bus/w1/devices/'
#device_folder = glob.glob(base_dir + '28*')[0]
#device_file = device_folder + '/w1_slave'


def read_lum():
	try:
		# Get sensor value
		# sensor_value = grovepi.analogRead(light_sensor)
		sensor_value = 50

		# Calculate resistance of sensor in K
		resistance = (float)(1023 - sensor_value) * 10 / sensor_value

		salut = ("sensor_value = %d resistance =%.2f" %(sensor_value,  resistance))
		return salut
		time.sleep(.5)

	except IOError:
		return "Error"
		# print ("Error")

class LuminositeService(Service):
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
		while True:
			try:
				self.lum = read_lum()
				time.sleep(3)
			except IOError as e:
				print "I/O error({0}): {1}".format(e.errno, e.strerror)
				time.sleep(0.5)
		return
		
		
	def startListening(self):
		self.state=True
		
		self.thread = threading.Thread(target=LuminositeService.listen_to_lum_sensor, args = (self,0))
		self.thread.daemon = True
		self.thread.start();
		return {
			'ListeningLum':True
		}