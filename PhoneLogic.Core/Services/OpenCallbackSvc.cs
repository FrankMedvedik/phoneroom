﻿using System;
using System.Globalization;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Lync.Model;
using Newtonsoft.Json;
using System.Collections.Generic;
using PhoneLogic.Model;
using PhoneLogic.Model.Models;


namespace PhoneLogic.Core.Services
{
    public static class OpenCallbackSvc
    {
        public static async Task<List<myCallback>> GetOpenJobCallbacks(string jobNum, string taskId)
        {
            var client = new WebClient();
            var data = await client.DownloadStringTaskAsync(new Uri(ConditionalConfiguration.apiUrl 
                + "OpenCallbacks" 
                + "?jobNum="+jobNum
                + "&taskId="+taskId
                ));
            return JsonConvert.DeserializeObject<List<myCallback>>(data);
        }

        public static async Task<List<CallbackRpt>> GetOpenCallbackRpt()
        {
            var client = new WebClient();
            var data = await client.DownloadStringTaskAsync(new Uri(ConditionalConfiguration.apiUrl
                + "OpenCallbacks"));
            return JsonConvert.DeserializeObject<List<CallbackRpt>>(data);
        }

      
    }
}