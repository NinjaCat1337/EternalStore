using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EternalStore.ApplicationLogic.StoreManagement.DTO;

namespace EternalStore.ApplicationLogic.StoreManagement.Interfaces
{
    public interface IStatisticManager
    {
        Task<IEnumerable<ProductStatisticDTO>> GetProductsStatistic(DateTime dateFrom, DateTime dateTo);
    }
}
