# You should have received a copy of the GNU General Public License
# along with this program.  If not, see <http://www.gnu.org/licenses/>.
# Doc & examples : https://github.com/fuzeman/PyUPnP


import logging
from threading import Thread
import time
from twisted.internet import reactor
from pyupnp.event import EventProperty
from pyupnp.device import Device, DeviceIcon
from pyupnp.logr import Logr
from pyupnp.services import register_action, Service, ServiceActionArgument, ServiceStateVariable
from pyupnp.ssdp import SSDP
from pyupnp.upnp import UPnP
from epine_services.luminosite import LuminositeService
from epine_services.tempHum import TempHumService

import grovepi
from grovepi import *
import random

sensor = 14     # Pin 14 is A0 Port.
grovepi.pinMode(sensor,"INPUT")

class EpineDevice(Device):
    deviceType = 'urn:schemas-upnp-org:device:Manchon:1'
    friendlyName = "The smart epine"
    
    def __init__(self):
        Device.__init__(self)
        self.uuid='c9300b5a-6f40-4875-a457-7e5ab90339d6'

        self.luminositeService = LuminositeService()
        self.tempHumService = TempHumService()



        
        self.services = [
            self.luminositeService,
            self.tempHumService,
        ]

        self.luminositeService.startListening()
        self.tempHumService.startListening()

        
        self.icons = [DeviceIcon('image/png', 32, 32, 24,'./index.png')]

        
if __name__ == '__main__':
    Logr.configure(logging.DEBUG)

    device = EpineDevice()

    upnp = UPnP(device)
    ssdp = SSDP(device)

    upnp.listen()
    ssdp.listen()

    print "Epine est connecte"

    #for i in range(0,51):
        # setRGB(random.randint(0,255),random.randint(0,255),random.randint(0,255))
        #time.sleep(.1)


    reactor.run()
    
        