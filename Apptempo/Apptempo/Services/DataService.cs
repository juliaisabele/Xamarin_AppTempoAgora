﻿using System;
using System.Collections.Generic;
using System.Text;

using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Apptempo.Model;

namespace Apptempo.Services
{
    class DataService
    {

        public static async Task<Tempo> GetPrevisaoDoTempo(string cidade)
        {
            string appId = "a7ae709afa9c7fb3ddcbaad411e7f8b8";

            string queryString = "http://api.openweathermap.org/data/2.5/weather?q=" + cidade + "&units=metric" + "&appId=" + appId;
            dynamic resultado = await getDataFromService(queryString).ConfigureAwait(false);

            if (resultado["weather"] != null)
            {
                Tempo previsao = new Tempo();
                previsao.Title = (string)resultado["name"];
                previsao.Temperature = (string)resultado["main"]["temp"] + "C";
                previsao.Wind = (string)resultado["wind"]["speed"] + " mph";
                previsao.Humidity = (string)resultado["main"]["humidity"] + " %";
                previsao.Visibility = (string)resultado["weather"][0]["main"];
                DateTime time = new DateTime(1970, 1, 1, 0, 0, 0, 0);
                DateTime sunrise = time.AddSeconds((double)resultado["sys"]["sunrise"]);
                DateTime sunset = time.AddSeconds((double)resultado["sys"]["sunset"]);
                previsao.Sunrise = String.Format("{0:d/MM/yyyy HH:mm:ss}", sunrise);
                previsao.Sunset = String.Format("{0:d/MM/yyyy HH:mm:ss}", sunset);
                return previsao;

            }
            else
            {
                return null;
            }
        }

        public static async Task<dynamic> getDataFromService(string queryString)
        {
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(queryString);
            dynamic data = null;
            if (response != null)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject(json);
            }
            return data;
        }
        

            public static async Task<dynamic> getDataFromServiceBycity(string city)
        {
            string appId = "a7ae709afa9c7fb3ddcbaad411e7f8b8";

            string url = string.Format("http://api.openweathermap.org/data/2.5/forecast/daily?q={0}&units=metric&cnt=1&APPID={1}", city.Trim(), appId);
            HttpClient client = new HttpClient();
            var response = await client.GetAsync(url);
            dynamic data = null;
            if (response !=null)
            {
                string json = response.Content.ReadAsStringAsync().Result;
                data = JsonConvert.DeserializeObject(json);
            }
            return data;
        }
        

        
    }
}
