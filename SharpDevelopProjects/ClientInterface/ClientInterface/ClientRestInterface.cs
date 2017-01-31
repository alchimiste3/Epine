/*
 * Crée par SharpDevelop.
 * Utilisateur: Quentin Laborde
 * Date: 17/01/2017
 * Heure: 13:41
 * 
 * Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
 */
using System;
using WComp.Beans;
using Newtonsoft.Json;

namespace WComp.Beans
{

	[Bean(Category="Epine")]
	public class ClientRestInterface
	{

		private string port = "8181"; 
		private string ip = "10.0.0.3";
		
		public string Port {
			get { return port; }
			set {
				port = value;
			}
		}
		
		public string IP {
			get { return ip; }
			set {
				ip = value;
			}
		}
		
		public void sendUpdate(string content){	
					
			string url = "http://"+ip+":"+port+"/cxf/ocs/interface";
			
			// string content = "{info:\"info\",meteo:\"meteo\",lum:10,pres:10,humTerre:10,humAir:10,temp:10,dist:10}";
			
			Url.post(url,content);

		}
		
		


	}
}
