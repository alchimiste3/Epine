# pression.py

import grovepi
import smbus
import RPi.GPIO as GPIO
from grove_i2c_barometic_sensor_BMP180 import BMP085

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

bmp = BMP085(0x77, 1)
rev = GPIO.RPI_REVISION
if rev == 2 or rev == 3:
	bus = smbus.SMBus(1)
else:
	bus = smbus.SMBus(0)

def read_pression():
	try:
		temp = bmp.readTemperature()
		pressure = bmp.readPressure()
		altitude = bmp.readAltitude(101560)
		#donnees = ("temperature = %.2f C pression = %.2f hPa altitude = %.2f m" %(temp, pressure, altitude))
		donnees = (pressure)
		return donnees

	except IOError:
		return "Error Pression"
		# print ("Error")

class PressionService(Service):
	version = (1, 0)
	serviceType = "urn:schemas-upnp-org:service:PressionService:1"
	serviceId = "urn:upnp-org:serviceId:PressionService"

	actions = {
		'GetPression': [
			ServiceActionArgument('Pression','out','Pression')
		]
	}
	
	stateVariables = [
		# Variables
		ServiceStateVariable('Pression','string',sendEvents=True)	,	
		ServiceStateVariable('ListeningPression','boolean',sendEvents=True)

	]
		
	state=EventProperty('ListeningPression')
	pression=EventProperty('Pression')

	@register_action('GetPression')
	def getState(self):
		return {
			'Pression' : str(read_pression())
		}
		
	def listen_to_pression_sensor(self, s):
		print "Listening for pression sensor values"
		while True:
			try:
				print "J'entre dans le listen de pression\n"
				self.pression = read_pression()
				time.sleep(3)
			except IOError as e:
				print "I/O error({0}): {1}".format(e.errno, e.strerror)
				time.sleep(0.5)
		return
		
		
	def startListening(self):
		self.state=True
		
		self.thread = threading.Thread(target=PressionService.listen_to_pression_sensor, args = (self,0))
		self.thread.daemon = True
		self.thread.start();
		return {
			'ListeningPression':True
		}
