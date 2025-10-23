using System;
using System.Collections.Generic;

namespace EmpujeComunitario.Graphql.Common.Model
{
    public class EventParticipationSummary
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public List<MonthlyEventDetail> Events { get; set; } = new List<MonthlyEventDetail>();
    }
    public class MonthlyEventDetail
    {
        public int Day { get; set; }
        public string EventName { get; set; }
        public string Description { get; set; }
        public List<EventDonationDto> Donations { get; set; } = new List<EventDonationDto>();

        public List<EventParticipantDto> Participants { get; set; } = new List<EventParticipantDto>();
    }

    public class EventParticipantDto
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
    }

    public class EventDonationDto
    {
        public string DonationId { get; set; }
        public int Quantity { get; set; }
    }
}