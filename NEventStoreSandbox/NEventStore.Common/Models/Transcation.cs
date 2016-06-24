using System;

namespace NEventStore.Common.Models
{
    public class Transcation
    {
        public Guid Id { get; set; }
        public decimal Amount { get; set; }

    }
}