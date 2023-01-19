//using Microsoft.Extensions.Hosting;
//using MockBookingSystem.ServiceLayer.Contracts;
//using MockBookingSystem.ServiceLayer.Wrappers;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace MockBookingSystem.ServiceLayer
//{
    
//    public class DestinationOptionSynchronizerBackgroundService : BackgroundService
//    {
//        private readonly ITripxClient _tripxClient;
//        private readonly IDAL _dal;

//        public DestinationOptionSynchronizerBackgroundService(ITripxClient tripxClient, IDAL dal)
//        {
//            _tripxClient = tripxClient;
//            _dal = dal;
//        }

//        //Sync every 10 minutes db with api
//        private readonly PeriodicTimer _timer = new PeriodicTimer(TimeSpan.FromMinutes(10));
//        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//        {
//            while (await _timer.WaitForNextTickAsync(stoppingToken) && !stoppingToken.IsCancellationRequested)
//            {
//                //There should have been the update
//            }
//        }
//    }
//}
