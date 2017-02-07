/*
 * Crée par SharpDevelop.
 * Utilisateur: Quentin
 * Date: 03/02/2017
 * Heure: 12:45
 * 
 * Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
 */
using System;
using WComp.Beans;

namespace WComp.Beans
{

	[Bean(Category="Epine")]
	public class UpDateValeurBoolCapteur
	{
		
		public void upDate(bool val) {
			setValServeurAlgo(val);
			verificationStatusJardin();
		}
		
		//////////////////////// Gestion valeurs capteurs ////////////////////////

		public delegate void setboolValueEventHandlerServeur(bool val);
	

			//////////////////////// Gestion luminosite ////////////////////////
		
		public event setboolValueEventHandlerServeur setValServeur;

		private void setValServeurAlgo(bool i) {
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
