/*
 * Crée par SharpDevelop.
 * Utilisateur: Mathieu
 * Date: 17/01/2017
 * Heure: 13:43
 * 
 * Pour changer ce modèle utiliser Outils | Options | Codage | Editer les en-têtes standards.
 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace WComp.Beans
{
	/// <summary>
	/// Description of Url.
	/// </summary>
	public class Url
	{
		
		// Returns JSON string 
        static string GET(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                WebResponse response = request.GetResponse();
                using (Stream responseStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream, Encoding.UTF8);
                    return reader.ReadToEnd();
                }
            }
            catch (WebException ex)
            {
                WebResponse errorResponse = ex.Response;
                using (Stream responseStream = errorResponse.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(responseStream,
        Encoding.GetEncoding("utf-8"));
                    String errorText = reader.ReadToEnd();
                    // log errorText 
                }
                throw;
            }
        }
		
		
		public static String GetUrl(string ville){
        	return GET("http://api.openweathermap.org/data/2.5/weather?q="+ville+"&appid=27621976fa5139f782910ce0c8947999");
		}
	}
}
