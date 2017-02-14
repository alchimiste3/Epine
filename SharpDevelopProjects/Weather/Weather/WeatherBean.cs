/*
 * Crée par SharpDevelop.
 * Utilisateur: Mathieu
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
	/// <summary>
	/// This is a sample bean, which has an integer evented property and a method.
	/// 
	/// Notes: for beans creating threads, the IThreadCreator interface should be implemented,
	/// 	providing a cleanup method should be implemented and named `Stop()'.
	/// For proxy beans, the IProxyBean interface should  be implemented,
	/// 	providing the IsConnected property, allowing the connection status to be drawn in
	/// 	the AddIn's graphical designer.
	/// 
	/// Several classes can be defined or used by a Bean, but only the class with the
	/// [Bean] attribute will be available in WComp. Its ports will be all public methods,
	/// events and properties definied in that class.
	/// </summary>
	[Bean(Category="Epine")]
	public class WeatherBean
	{
		private String weather;

		
		public string MyProperty {
			get { return launchWeather(); }
			set {
				weather = value;
				FireWeather(weather);		// event will be fired for every property set.
			}
		}
		
		
		public string launchWeather(){			
			var json = Url.GetUrl("Biot");
			var data = JsonConvert.DeserializeObject<WeatherData>(json);
			
			string result = (data.weather[0].main).ToString();
			
			FireWeather(result);
			return result;
		}




		//----- Déclaration de l'event
		public delegate void LaunchData(string val);

		public event LaunchData Weather;
		
		private void FireWeather(string i) {
			if (Weather != null)
				Weather(i);
		}
		
	}
}
