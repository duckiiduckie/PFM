﻿using System.ComponentModel.DataAnnotations;

namespace IncomeAPI.Models.Dto
{
    public class CreateMainIncome
    {
        public string UserId { get; set; } = "";
        public string Description { get; set; } = "";
        public decimal Amount { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public string Name { get; set; } = "";
    }
}
