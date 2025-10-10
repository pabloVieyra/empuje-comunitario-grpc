using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.MessageFlow.Common.Constants
{
    public static class Exchanges
    {
        public const string ONG_EXCHANGE_NAME = "ong_network.exchange";

        // Donaciones
        //punto1
        public const string RoutingKeyRequestDonation = "donation.request";           
        //punto2
        public const string RoutingKeyTransferDonation = "donation.transfer.{0}";         
        //punto 3
        public const string RoutingKeyOfferDonation = "donation.offer";               
        //punto 4
        public const string RoutingKeyRequestCancel = "donation.cancel";
        // Eventos solidarios
        //punto 5
        public const string RoutingKeyEventSolidary = "event.solidary";
        //punto 
        public const string RoutingKeyEventCancel = "event.cancel";
        //punto 7
        public const string RoutingKeyEventVolunteer = "event.volunteer.{0}";


    }
}
