namespace Directory.BLL.Abstract.IServices
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Directory.Common;
    using Directory.Common.Models;

    public interface IEmployeeService : IBaseService<EmployeeModel>
    {
        Task<Tuple<IList<EmployeeModel>, int>> GetList(string terms, PageSortInfo pageSort);

        Task<Tuple<IList<EmployeeModel>, int>> GetByEmail(string email);
    }
}
