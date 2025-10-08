using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpujeComunitario.MessageFlow.Common.Constants
{
    public static class Exchanges
    {
        public static string ExchangeRequestDonation = "donation.request.created";
        public static string ExchangeOffersDonations = "donation.offer.created";
        public static string ExchangeEventsSolidary = "event.solidary.created";
        public static string ExchangeEventsVolunteer = "event.volunteer.adhered";
        public static string ExchangeTransfersConfirm = "donation.transfer.confirmed";
        public static string ExchangeRequestsCancel = "donation.request.cancelled";
        public static string ExchangeEventsCancel = "event.solidary.cancelled";

    }
}
