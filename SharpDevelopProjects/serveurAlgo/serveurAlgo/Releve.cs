/*
 * Crée par SharpDevelop.
 * Utilisateur: Quentin
 * Date: 13/01/2017
 * Heure: 09:00
 * 
 * Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
 */
using System;

namespace serveurAlgo.Properties
{
	/// <summary>
	/// Description of Releve.
	/// </summary>
	public class Releve
	{

		private double lum;
		private double temp;
		private double humAir;
		private double pression;

		public Releve(double lum, double temp, double humAir, double pression)
		{
			this.lum = lum;
			this.temp = lum;
			this.humAir = humAir;
			this.pression = pression;
		}
		
		public double Luminosite {
			get { return lum; }
			set {
				lum = value;
			}
		}

		public double Temperature {
			get { return temp; }
			set {
				temp = value;
			}
		}
		
		public double Pression {
			get { return pression; }
			set {
				pression = value;
			}
		}
		
		public double HumiditeAir {
			get { return humAir; }
			set {
				humAir = value;
			}
		}
		
		
	}
}
