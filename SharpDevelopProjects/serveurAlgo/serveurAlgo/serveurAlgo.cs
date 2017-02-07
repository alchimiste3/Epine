/*
 * Crée par SharpDevelop.
 * Utilisateur: Quentin Laborde
 * Date: 10/01/2017
 * Heure: 15:06
 * 
 * Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
 */
using System;
using serveurAlgo.Properties;
using WComp.Beans;
using System.Collections;
using System.Collections.Generic;

namespace WComp.Beans
{
	
	[Bean(Category="Epine")]
	public class serveurAlgo
	{
		
		//Valeur retourner par le capteur de luminosite
		private double lum;
		
		//Valeur retourner par le capteur meteo
		private double temp;
		private double humAir;
		
		//Valeur retourner par le barometre
		private double pression;
		
		//Valeur retourner par le capteur d'humiditer du sol
		private double humTerre;
		private double seuilMinHumTerre = 50;
		
		//Valeur retourner par le capteur de distance
		private bool distance;


		Dictionary<string,Meteo> listMeteo;
		
		private double seuilMaxLum = 100;
		private double seuilMaxTemp = 20;
		private double seuilMinHumAir = 20;
		private double seuilMaxPression = 20;
		

		public serveurAlgo()
		{
			listMeteo = new Dictionary<string,Meteo>();
		}
		
		/// <summary>
		/// Les props
		/// </summary>
		public double Luminosite {
			get { return lum; }
			set {
				lum = value;
				AddReleverMeteo();
			}
		}

		public double Temperature {
			get { return temp; }
			set {
				temp = value;
				AddReleverMeteo();
			}
		}
		
		public double Pression {
			get { return pression; }
			set {
				pression = value;
				AddReleverMeteo();
			}
		}
		
		public double HumiditeAir {
			get { return humAir; }
			set {
				humAir = value;
				AddReleverMeteo();
			}
		}
		
		public double HumiditeTerre {
			get { return humTerre; }
			set {
				humTerre = value;
			}
		}
		
		public double SeuilMinHumTerre {
			get { return seuilMinHumTerre; }
			set {
				seuilMinHumTerre = value;
			}
		}
		
		public bool Distance {
			get { return distance; }
			set {
				distance = value;
			}
		}
		
		public double SeuilMaxLum {
			get { return seuilMaxLum; }
			set {
				seuilMaxLum = value;
			}
		}
		
		public double SeuilMaxTemp {
			get { return seuilMaxTemp; }
			set {
				seuilMaxTemp = value;
			}
		}
		
		public double SeuilMinHumAir {
			get { return seuilMinHumAir; }
			set {
				seuilMinHumAir = value;
			}
		}
		
		public double SeuilMaxPression {
			get { return seuilMaxPression; }
			set {
				seuilMaxPression = value;
			}
		}
		
		
		
		private int testVal2 = 0;
		public int TestVal2 {
			get { return testVal2; }
			set {
				testVal2 = value;
			}
		}
		
		
		/// <summary>
		/// Les Methodes 
		/// </summary>
		

		public bool getEtatArrosage() {
			return ConditionArrosage();
		}
		
		public bool getEtatTonte() {
			return ConditionTonte();
		}
		
		public double getLuminosite() {
			return Luminosite;
		}
		
		public double getTemperature() {
			return Temperature;
		}
		
		public double getPression() {
			return Pression;
		}
		
		public double getHumiditeAir() {
			return HumiditeAir;
		}
		
		public double getHumiditeTerre() {
			return HumiditeTerre;
		}
		
		public bool getDistancee() {
			return Distance;
		}
		
		

		private bool ConditionArrosage(){
			
			bool humTerreIns = false;
			// Verification humidite de la terre
			if(humTerre < seuilMinHumTerre){
				humTerreIns = true;
			}
			
			// Verification meteo
			Meteo meteo = listMeteo[getDate()];
			Releve releveMoyenne = meteo.getMoyenneReleve();
			
			// On test le "degre" de la metoe local
			switch(getTypeMeteoLocal(releveMoyenne)){
				case 2:
					testVal2 = 2;
					return humTerreIns;  // Si = 2, alors on arrose uniquement si la terre  n'est pas assez humide
				case 3:
					testVal2 = 3;
					return humTerreIns; // Si = 3, idem
				case 4:
					testVal2 = 4;
					return true; // Si = 4, alors on arrose toujours par précotion meme si la terre est assez humide
				default:
					testVal2 = -1;
					return false;
			}
			
		}
		
		private bool ConditionTonte(){
			return distance;
		}
		
		
		private void AddReleverMeteo(){
			
			string date = getDate();
				
			if(!listMeteo.ContainsKey(date)){
				listMeteo.Add(date, new Meteo());
			}
			
			listMeteo[date].addReleve(new Releve(lum, temp, humAir, pression));
		}
		
		private string getDate(){
			DateTime date = DateTime.Now.Date;			
			return date.ToString("MMMM dd, yyyy");
		}
		
	
		public int getTypeMeteoLocal(Releve releve){
			
			int res = 0;
			
			if(releve.HumiditeAir < seuilMinHumAir) res++;
			if(releve.Luminosite > seuilMaxLum) res++;
			if(releve.Pression > seuilMaxPression) res++;
			if(releve.Temperature > seuilMaxTemp) res++;
			
			return res;
		}
		
	}
}
