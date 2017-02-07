/*
 * Crée par SharpDevelop.
 * Utilisateur: Quentin
 * Date: 03/02/2017
 * Heure: 12:21
 * 
 * Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
 */
using System;
using WComp.Beans;

namespace WComp.Beans
{

	[Bean(Category="Epine")]
	public class VerificationStatusJardin
	{
		
		private string message = "non disponnible";
		private string meteo = "non disponnible";
		
		
		//////////////////////// Check besoin action utilisateur ////////////////////////
		
		public void verificationJardin(){
			EtatArrosage();
			EtatTonte();
			sendUpdateAPI();
		}
		
		
		private bool EtatArrosage() {
			bool res = getEtatArrosageServeurAlgo();
			if(res){
				message = "Vous devez arroser votre jardin.";
				meteo = getMeteoAPI();
				sendMailUtilisateur("Alerte Arrosage", message + " Meteo : "+meteo);
				return true;
			}
			
			return false;
		}
		
		private bool EtatTonte() {
			bool res = getEtatTonteServeurAlgo();
			if(res){
				message = "Vous devez tondre votre jardin.";
				meteo = getMeteoAPI();
				sendMailUtilisateur("Alerte Tonte",message + " Meteo : "+meteo);
				return true;
			}
			
			return false;
		}
		
		
		//////////////////////// Gestion mail ////////////////////////
		
		public delegate bool sendMailEventHandlerServeur(string title, string message);
		public event sendMailEventHandlerServeur sendMail;

		private bool sendMailUtilisateur(string title, string message) {
			if (sendMail != null){
				return sendMail(title, message);
			}
			return false;
		}
		
		//////////////////////// Gestion API meteo ////////////////////////
		
		public delegate string getMeteoEventHandlerServeur();
		public event getMeteoEventHandlerServeur getMeteo;

		private string getMeteoAPI() {
			if (getMeteo != null){
				return getMeteo();
			}
			return "Meteo Indisponnible";
		}

	
		//////////////////////// Gestion API interface Web ////////////////////////
		
		public delegate void sendUpdateEventHandlerServeur(string contentJson);
		public event sendUpdateEventHandlerServeur sendUpdate;

		private void sendUpdateAPI() {
			if (sendUpdate != null){
				
				
				//sendUpdate("{info:\"info\",meteo:\"meteo\",lum:10,pres:10,humTerre:10,humAir:10,temp:10,dist:10}");
				//testVal2 = "{info:"+message+",meteo:"+meteo+",lum:"+getLumServeurAlgo()+",pres:"+getPressionServeurAlgo()+",humTerre:"+getHumTerreServeurAlgo()+",humAir:"+getHumAirServeurAlgo()+",temp:"+getTempServeurAlgo()+",dist:"+getDistanceServeurAlgo()+"}";
				sendUpdate("{\"info\":\""+message+"\",\"meteo\":\""+meteo+"\",\"lum\":\""+getLumServeurAlgo()+"\",\"pres\":\""+getPressionServeurAlgo()+"\",\"humTerre\":\""+getHumTerreServeurAlgo()+"\",\"humAir\":\""+getHumAirServeurAlgo()+"\",\"temp\":\""+getTempServeurAlgo()+"\",\"dist\":\""+getDistanceServeurAlgo()+"\"}");
			}
			
		}
		
		
		//////////////////////// Gestion Etat arrosage et tonte ////////////////////////
		
		public delegate bool getboolValueEventHandlerServeur();
		public event getboolValueEventHandlerServeur getEtatArrosageServeur;
		public event getboolValueEventHandlerServeur getEtatTonteServeur;
		
		private bool getEtatArrosageServeurAlgo() {
			if (getEtatArrosageServeur != null){
				return getEtatArrosageServeur();
			}
			return false;
		}
		
		private bool getEtatTonteServeurAlgo() {
			if (getEtatTonteServeur != null){
				return getEtatTonteServeur();
			}
			return false;
		}
		
		
		//////////////////////// Gestion valeurs capteurs ////////////////////////

		public delegate double getdoubleValueEventHandlerServeur();

			//////////////////////// Gestion luminosite ////////////////////////
	
		public event getdoubleValueEventHandlerServeur getLumServeur;


		
		private double getLumServeurAlgo() {
			if (getLumServeur != null){
				return getLumServeur();
			}
			
			return 0.0;
		}
		
			//////////////////////// Gestion pression ////////////////////////
		
		public event getdoubleValueEventHandlerServeur getPressionServeur;
		
		private double getPressionServeurAlgo() {
			if (getPressionServeur != null){
				return getPressionServeur();
			}
			
			return 0.0;
		}
		
			//////////////////////// Gestion humidite de la terre ////////////////////////
		

		public event getdoubleValueEventHandlerServeur getHumTerreServeur;
		
		private double getHumTerreServeurAlgo() {
			if (getHumTerreServeur != null){
				return getHumTerreServeur();
			}
			
			return 0.0;
		}
		
			//////////////////////// Gestion humidite de l'air ////////////////////////

		public event getdoubleValueEventHandlerServeur getHumAirServeur;
		
		private double getHumAirServeurAlgo() {
			if (getHumAirServeur != null){
				return getHumAirServeur();
			}
			
			return 0.0;
		}
		
			//////////////////////// Gestion temperature de l'air ////////////////////////

		public event getdoubleValueEventHandlerServeur getTempServeur;
		
		private double getTempServeurAlgo() {
			if (getTempServeur != null){
				return getTempServeur();
			}
			
			return 0.0;
		}
		
			//////////////////////// Gestion distance herbe ////////////////////////

		public event getboolValueEventHandlerServeur getDistanceServeur;

		private bool getDistanceServeurAlgo() {
			if (getDistanceServeur != null){
				return getDistanceServeur();
			}
			
			return false;
		}
		
		

		
	}
}
