using QioskAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QioskAPI.Interfaces
{
    public interface IKioskService
    {
        Task<IEnumerable<Kiosk>> GetKiosks();
        Task<Kiosk> GetKiosk(int id);
        Task PutKiosk(int id, Kiosk Kiosk);
        Task PostKiosk(Kiosk Kiosk);
        Task DeleteKiosk(int id);
        bool KioskExists(int id);
    }
}
