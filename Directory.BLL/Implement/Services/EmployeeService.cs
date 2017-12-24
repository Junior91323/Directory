namespace Directory.BLL.Implement.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Directory.BLL.Abstract.IServices;
    using Directory.Common;
    using Directory.Common.Models;
    using Directory.DAL.Abstract.IRepositories;

    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository employeeRepository;

        private const string PhoneNumberReg = @"^((8|\+7)[\- ]?)?(\(?\d{3}\)?[\- ]?)";

        public EmployeeService(IEmployeeRepository organizationRepository)
        {
            this.employeeRepository = organizationRepository;
        }

        public async Task<EmployeeModel> GetByUid(Guid uid)
        {
            return await employeeRepository.GetByUid(uid);
        }

        public async Task<Tuple<IList<EmployeeModel>, int>> GetByEmail(string email)
        {
            return await employeeRepository.Find((x => x.Email.ToUpper() == email.ToUpper()), PageSortInfo.All);
        }

        public async Task<Tuple<IList<EmployeeModel>, int>> GetList(PageSortInfo pageSort)
        {
            return await employeeRepository.GetList(pageSort);
        }

        public async Task<Tuple<IList<EmployeeModel>, int>> GetList(string terms, PageSortInfo pageSort)
        {
            Tuple<IList<EmployeeModel>, int> list;

            if (!string.IsNullOrWhiteSpace(terms))
            {
                if (Regex.IsMatch(terms, PhoneNumberReg))
                {
                    list = await employeeRepository.Find((x => x.PhoneNumber.StartsWith(terms)), pageSort);
                }
                else
                {
                    list = await employeeRepository.Find((x => x.FullName.Contains(terms)), pageSort);
                }
            }
            else
            {
                list = await employeeRepository.GetList(pageSort);
            }
            return list;
        }

        public async Task Create(EmployeeModel model)
        {
            if (model == null)
                throw new NullReferenceException();

            if (model.Uid == null || model.Uid == Guid.Empty)
                model.Uid = Guid.NewGuid();

            model.CreateDate = DateTime.Now;
            model.UpdateDate = DateTime.Now;
            model.IsActive = true;

            employeeRepository.Create(model);
            await employeeRepository.Save();
        }

        public async Task Update(EmployeeModel model)
        {
            if (model == null)
                throw new NullReferenceException();

            model.UpdateDate = DateTime.Now;

            employeeRepository.Update(model);
            await employeeRepository.Save();
        }

        public async Task Delete(Guid uid)
        {
            var model = await employeeRepository.GetByUid(uid);

            if (model == null)
                throw new Exception(String.Format("Сотрудника с таким идентификатором нету: {0}.", model.Uid));

            model.UpdateDate = DateTime.Now;
            model.IsActive = false;

            employeeRepository.Update(model);
            await employeeRepository.Save();
        }
    }
}
