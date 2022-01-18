using QioskAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QioskAPI.Interfaces
{
    public interface ICompanyService
    {
        Task<IEnumerable<Company>> GetCompanies();
        Task<Company> GetCompany(int id);
        Task PutCompany(int id, Company Company);
        Task PostCompany(Company Company);
        Task DeleteCompany(int id);
        bool CompanyExists(int id);
    }
}
