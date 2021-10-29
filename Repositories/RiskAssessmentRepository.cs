using RiskAssessmentMicroservice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RiskAssessmentMicroservice.DBHandler;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http;
using Azure.Core;

namespace RiskAssessmentMicroservice.Repositories
{
    public class RiskAssessmentRepository : IRiskAssessmentRepository
    {
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(RiskAssessmentRepository));
        public DetailCashDeposit GetCollateralCashDeposit(int loanId)
        {
            _log4net.Info("GetCollateralCashDeposit method under RiskAssessmentRepository accessed for processing loanId=" + loanId);
            HttpClientHandler clientHandler = new HttpClientHandler();
            //var correlationId = HttpContext.Request.Headers["x-correlation-id"].ToString()
           
            using (HttpClient client = new HttpClient(clientHandler))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer");
                client.BaseAddress = new Uri("https://collateralloanapi.azurewebsites.net/api/");
                HttpResponseMessage response = client.GetAsync("CollateralManagement/getCollateralsCashDeposit?loanId=" + loanId).Result;
                if (response.IsSuccessStatusCode)
                {
                    string stringData = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<DetailCashDeposit>(stringData);
                }
                else 
                    return null;
            }
        }

        public DetailRealEstate GetCollateralRealEstate(int loanId)
        {
            _log4net.Info("GetCollateralRealEstate method under RiskAssessmentRepository accessed for processing loanId=" + loanId);
            HttpClientHandler clientHandler = new HttpClientHandler();
            using (HttpClient client = new HttpClient(clientHandler))
            {
            
                //client.BaseAddress = new Uri("http://localhost:53733/api/");
                client.BaseAddress = new Uri("https://collateralloanapi.azurewebsites.net/api/");

                HttpResponseMessage response = client.GetAsync("CollateralManagement/getCollateralsRealEstate?loanId=" + loanId).Result;
                if (response.IsSuccessStatusCode)
                {
                    string stringData = response.Content.ReadAsStringAsync().Result;
                    return JsonConvert.DeserializeObject<DetailRealEstate>(stringData);
                }
                else
                    return null;
            }
        }
    }
}
