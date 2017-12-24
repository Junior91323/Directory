namespace Directory.Web.Mappers
{
    using System;
    using Directory.Common.Models;
    using Directory.Web.Models.Employee;

    internal static class EmployeeMapper
    {

        internal static void Map(EmployeeViewModel request, EmployeeModel model)
        {
            model.Uid = request.Uid ?? Guid.Empty;
            model.Email = request.Email;
            model.FullName = request.FullName;
            model.PhoneNumber = request.PhoneNumber;
        }

        internal static EmployeeViewModel Map(EmployeeModel model)
        {
            return new EmployeeViewModel
            {
                Uid = model.Uid,
                FullName = model.FullName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                CreateDate = model.CreateDate,
                UpdateDate = model.UpdateDate,
                IsActive = model.IsActive
            };
        }
    }
}