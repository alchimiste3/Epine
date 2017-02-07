/*
 * Crée par SharpDevelop.
 * Utilisateur: Quentin
 * Date: 03/02/2017
 * Heure: 12:35
 * 
 * Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
 */
using System;
using WComp.Beans;

namespace WComp.Beans
{
	
	[Bean(Category="Epine")]
	public class UpDateValeurDoubleCapteur
	{
		
		public void upDate(string val) {
			double rep = Convert.ToDouble(val);
			setValServeurAlgo(rep);
			verificationStatusJardin();
		}
		
		//////////////////////// Gestion valeurs capteurs ////////////////////////

		public delegate void setdoubleValueEventHandlerServeur(double val);
		public delegate double getdoubleValueEventHandlerServeur();

			//////////////////////// Gestion luminosite ////////////////////////
		
		public event setdoubleValueEventHandlerServeur setValServeur;

		private void setValServeurAlgo(double i) {
			if (setValServeur != null){
				setValServeur(i);
			}
		}

		//////////////////////// Verification status Jardin ////////////////////////
		
		public delegate void sendVerifHandlerServeur();
		public event sendVerifHandlerServeur verificationJardin;

		private void verificationStatusJardin() {
			if (verificationJardin != null){
				verificationJardin();
			}
		}
	}
		
}
