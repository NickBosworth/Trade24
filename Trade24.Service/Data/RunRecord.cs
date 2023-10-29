﻿using System.ComponentModel.DataAnnotations;

namespace Trade24.Service.Data
{
    public class RunRecord
    {
        [Key]
        public RunRecordType Type { get; set; }
        public DateTime LastRun { get; set; }
    }

    public enum RunRecordType
    {
        UpdateTickers,
        GetHistory,
        UpdatePriceData,
    }
}
