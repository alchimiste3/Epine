/*
 * Crée par SharpDevelop.
 * Utilisateur: user
 * Date: 10/01/2017
 * Heure: 15:06
 * 
 * Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
 */
using System;
using WComp.Beans;

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
	public class Orchestrateur
	{

		
		private string message = "non disponnible";
		private string meteo = "non disponnible";
			
			
		private bool testVal = false;
		public bool TestVal {
			get { return testVal; }
			set {
				testVal = value;
			}
		}
		
		private string testVal2 = "";
		public string TestVal2 {
			get { return testVal2; }
			set {
				testVal2 = value;
			}
		}
		
		public void upDateLum(string lum) {
			double rep = Convert.ToDouble(lum);
			setLumServeurAlgo(rep);
			checkActionsUtilisateur();
		}
		
		public void upDatePression(string pres) {
			double rep = Convert.ToDouble(pres);
			setPressionServeurAlgo(rep);
			checkActionsUtilisateur();
		}
		
		public void upDateHumTerre(string hum) {
			double rep = Convert.ToDouble(hum);
			setHumTerreServeurAlgo(rep);
			checkActionsUtilisateur();
		}
		
		public void upDateHumAir(string hum) {
			double rep = Convert.ToDouble(hum);
			setHumAirServeurAlgo(rep);
			checkActionsUtilisateur();
		}
		
		public void upDateTemp(string temp) {
			double rep = Convert.ToDouble(temp);
			setTempServeurAlgo(rep);
			checkActionsUtilisateur();
		}

		public void upDateDistance(bool dist) {
			setDistanceServeurAlgo(dist);
			checkActionsUtilisateur();
		}
		
		
		//////////////////////// Check besoin action utilisateur ////////////////////////
		
		private void checkActionsUtilisateur(){
			EtatArrosage();
			EtatTonte();
			sendUpdateAPI();
		}
		
		
		private bool EtatArrosage() {
			bool res = getEtatArrosageServeurAlgo();
			if(res){
				testVal = true;
				message = "Vous devez arroser votre jardin.";
				meteo = getMeteoAPI();
				sendMailUtilisateur("Alerte Arrosage", message + " Meteo : "+meteo);
				return true;
			}
			
			return false;
		}
		
		private bool EtatTonte() {
			testVal = false;
			bool res = getEtatTonteServeurAlgo();
			testVal = res;
			if(res){
				testVal = true;
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
				sendUpdate("{info:"+message+",meteo:"+meteo+",lum:"+getLumServeurAlgo()+",pres:"+getPressionServeurAlgo()+",humTerre:"+getHumTerreServeurAlgo()+",humAir:"+getHumAirServeurAlgo()+",temp:"+getTempServeurAlgo()+",dist:"+getDistanceServeurAlgo()+"}");
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

		public delegate void setdoubleValueEventHandlerServeur(double val);
		public delegate double getdoubleValueEventHandlerServeur();

			//////////////////////// Gestion luminosite ////////////////////////
		
		public event setdoubleValueEventHandlerServeur setLumServeur;
		public event getdoubleValueEventHandlerServeur getLumServeur;

		private void setLumServeurAlgo(double i) {
			if (setLumServeur != null){
				setLumServeur(i);
			}
		}
		
		private double getLumServeurAlgo() {
			if (getLumServeur != null){
				return getLumServeur();
			}
			
			return 0.0;
		}
		
			//////////////////////// Gestion pression ////////////////////////
		
		public event setdoubleValueEventHandlerServeur setPressionServeur;
		public event getdoubleValueEventHandlerServeur getPressionServeur;

		private void setPressionServeurAlgo(double i) {
			if (setPressionServeur != null){
				setPressionServeur(i);
			}
		}
		
		private double getPressionServeurAlgo() {
			if (getPressionServeur != null){
				return getPressionServeur();
			}
			
			return 0.0;
		}
		
			//////////////////////// Gestion humidite de la terre ////////////////////////
		
		public event setdoubleValueEventHandlerServeur setHumTerreServeur;
		public event getdoubleValueEventHandlerServeur getHumTerreServeur;

		private void setHumTerreServeurAlgo(double i) {
			if (setHumTerreServeur != null){
				setLumServeur(i);
			}
		}
		
		private double getHumTerreServeurAlgo() {
			if (getHumTerreServeur != null){
				return getHumTerreServeur();
			}
			
			return 0.0;
		}
		
			//////////////////////// Gestion humidite de l'air ////////////////////////
		
		public event setdoubleValueEventHandlerServeur setHumAirServeur;
		public event getdoubleValueEventHandlerServeur getHumAirServeur;

		private void setHumAirServeurAlgo(double i) {
			if (setHumAirServeur != null){
				setHumAirServeur(i);
			}
		}
		
		private double getHumAirServeurAlgo() {
			if (getHumAirServeur != null){
				return getHumAirServeur();
			}
			
			return 0.0;
		}
		
			//////////////////////// Gestion temperature de l'air ////////////////////////
		
		public event setdoubleValueEventHandlerServeur setTempServeur;
		public event getdoubleValueEventHandlerServeur getTempServeur;

		private void setTempServeurAlgo(double i) {
			if (setTempServeur != null){
				setTempServeur(i);
			}
		}
		
		private double getTempServeurAlgo() {
			if (getTempServeur != null){
				return getTempServeur();
			}
			
			return 0.0;
		}
		
			//////////////////////// Gestion distance herbe ////////////////////////
		
		public delegate void setboolValueEventHandlerServeur(bool val);
		
		public event setboolValueEventHandlerServeur setDistanceServeur;
		public event getboolValueEventHandlerServeur getDistanceServeur;

		private void setDistanceServeurAlgo(bool i) {
			if (setDistanceServeur != null){
				setDistanceServeur(i);
			}
		}
		
		private bool getDistanceServeurAlgo() {
			if (getDistanceServeur != null){
				return getDistanceServeur();
			}
			
			return false;
		}
		

		
	}
}
