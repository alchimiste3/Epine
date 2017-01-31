/*
 * Crée par SharpDevelop.
 * Utilisateur: Quentin Laborde
 * Date: 13/01/2017
 * Heure: 08:48
 * 
 */
using System;
using System.Collections;

namespace serveurAlgo.Properties
{
	/// <summary>
	/// Description of Meteo.
	/// </summary>
	public class Meteo
	{
		
		private ArrayList listReleves;
				


		
		public ArrayList ListRelevesMeteos {
			get { return listReleves; }
			set {
				listReleves = value;
			}
		}
		
		
		
		/// <summary>
		/// Constructeur
		/// </summary>
		public Meteo()
		{
			ListRelevesMeteos = new ArrayList();
			
		}
		
		
		/// <summary>
		/// Permet d'ajouter un Releve dans un objet Meteo
		/// </summary>
		/// <param name="r"></param>
		public void addReleve(Releve r){
			ListRelevesMeteos.Add(r);
		}
		
		/// <summary>
		/// Permet de recup une moyenne des releve meteo
		/// </summary>
		/// <returns></returns>
		public Releve getMoyenneReleve(){

			double lum = 0;
			double temp = 0;
			double humAir = 0;
			double pression = 0;
			
			int nbReleve = ListRelevesMeteos.Count;
			
			foreach (Releve r in ListRelevesMeteos){
				lum += r.Luminosite;
				temp += r.Temperature;
				humAir += r.HumiditeAir;
				pression += r.Pression;
			}
			
			lum = lum/(double)nbReleve;
			temp = temp/(double)nbReleve;
			humAir = humAir/(double)nbReleve;
			pression = pression/(double)nbReleve;
			
			Releve newReleve = new Releve(lum, temp, humAir, pression);
			
			return newReleve;
		}
		

		
		
	}
}
