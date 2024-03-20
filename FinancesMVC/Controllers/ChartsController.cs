﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace FinancesMVC.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartsController : ControllerBase
    {

        private record CountByDayResponseItem(string Day, string Month, decimal MoneySpent);

        private readonly Db1Context db1Context;

        public ChartsController(Db1Context db1Context)
        {
            this.db1Context = db1Context;
        }

        [HttpGet("spentByDay")]
        public async Task<JsonResult> GetSpentByDayAsync()
        {
            CultureInfo culture = new CultureInfo("en-US");
            var responseItems = await db1Context
                .Transactions
                .GroupBy(transaction => transaction.Date.Date)
                .Select(group => new CountByDayResponseItem(group.Key.Date.ToString("dd"), 
                group.Key.Date.ToString("MMMM", culture), group.Sum(t => t.MoneySpent)))
                .ToListAsync();

            return new JsonResult(responseItems);
        }

    }
}