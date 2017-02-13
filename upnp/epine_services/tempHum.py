# tempHum.py

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
from test.threadRead import threadRead

import os
import glob

os.system('modprobe w1-gpio')
os.system('modprobe w1-therm')

DHT_SENSOR_TYPE = 0
DHT_SENSOR_PIN = 4
MINUTES_BETWEEN_READS = 1


def isFloat(string):
	try:
		float(string)
		return True
	except ValueError:
		return False


def read_temp():
	try:
		[temp_c,hum] = grovepi.dht(DHT_SENSOR_PIN,DHT_SENSOR_TYPE)
		print "j'entre dans le read_temp"
		#temp_c = 10.0
		#hum = 10.0

		if (isFloat(temp_c) and (isFloat(hum)) and (hum >= 0)):
			donnees = (temp_c)
			if donnees is None:
				donnees = -1
			return donnees
			time.sleep(.5)

	except IOError:
		return -1

def read_hum():
	try:
		[temp_c,hum] = grovepi.dht(DHT_SENSOR_PIN,DHT_SENSOR_TYPE)

		#temp_c = 10.0
		#hum = 10.0

		if (isFloat(temp_c) and (isFloat(hum)) and (hum >= 0)):
			donnees = (hum)
			if donnees is None:
				donnees = -1
			return donnees
			time.sleep(.5)

	except IOError:
		return -1


	#time.sleep(60*MINUTES_BETWEEN_READS)


class TempHumService(Service):
	version = (1, 0)
	serviceType = "urn:schemas-upnp-org:service:TempHumService:1"
	serviceId = "urn:upnp-org:serviceId:TempHumService"

	actions = {
		'GetTemp': [
			ServiceActionArgument('Temp','out','Temp')
		],
		'GetHumi': [
			ServiceActionArgument('Humi','out','Humi')
		]
	}
	
	stateVariables = [
		# Variables
		ServiceStateVariable('Temp','string',sendEvents=True),
		ServiceStateVariable('Humi','string',sendEvents=True),
		ServiceStateVariable('ListeningTempHum','boolean',sendEvents=True)

	]
		
	state=EventProperty('ListeningTempHum')
	temp=EventProperty('Temp')
	humi=EventProperty('Humi')

	@register_action('GetTemp')
	def getState(self):
		return {
			'Temp' : str(read_temp())
		}
		
	def listen_to_tempHum_sensor(self, s):
		print "Listening for tempHum sensor values"
		tmp = 10
		tmp1 = 10
		tmp2 = 10
		while True:
			threadRead.mutex1.acquire(1)
			print("temphum a pris le mutex\n");
			try:
				tmp1 = read_hum()
				tmp2 = read_temp()
				print "J'entre dans le listen de tempHum\n"
				#if tmp1 == -1 or tmp2 == -1 or tmp1 is None or tmp2 is None:
				#	print "NONEEEEEEEEEEEEE\n"
				
				#elif tmp1 > (tmp - 50) or tmp1 < (tmp + 50) :
				#	print "Valeur non changee trop proche\n"

				#elif tmp1 < (tmp - 50) or tmp1 > (tmp + 50) :
				self.humi = tmp1
				tmp = tmp1
				self.temp = tmp2



				time.sleep(5)
				threadRead.mutex1.release()
				print("Hum a rendu le mutex\n");
				time.sleep(3)
			except IOError as e:
				print "I/O error({0}): {1}".format(e.errno, e.strerror)
				time.sleep(0.5)
				print("temphum a rendu le mutex\n");
				threadRead.mutex1.release()
		return
		
		
	def startListening(self):
		self.state=True
		
		self.thread = threading.Thread(target=TempHumService.listen_to_tempHum_sensor, args = (self,0))
		self.thread.daemon = True
		self.thread.start();
		return {
			'ListeningTempHum':True
		}

	@register_action('GetHumi')
	def getState(self):
		return {
			'Hum' : str(read_hum())
		}
