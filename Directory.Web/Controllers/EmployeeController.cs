namespace Directory.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Mvc;
    using Directory.BLL.Abstract.IServices;
    using Directory.Common;
    using Directory.Common.Models;
    using Directory.Web.Mappers;
    using Directory.Web.Models.Employee;
    using Directory.Web.Utils;
    using PagedList;

    public class EmployeeController : Controller
    {
        private readonly IEmployeeService employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        [HttpGet]
        public async Task<ActionResult> List(string terms, string sortField = "CreateDate", string sortDist = "Desc", int page = 1)
        {
            var tuple = await employeeService.GetList(terms, new PageSortInfo(sortField, sortDist.Convert(), page - 1, ApplicationSettings.PageSize));

            var list = tuple.Item1.Select(EmployeeMapper.Map);
            var totalCount = tuple.Item2;

            var res = new StaticPagedList<EmployeeViewModel>(list, page, ApplicationSettings.PageSize, totalCount);

            ViewBag.SortField = sortField;
            ViewBag.SortDist = sortDist;
            ViewBag.Terms = terms;

            return View(res);
        }

        [HttpGet]
        public async Task<ActionResult> Details(Guid uid)
        {
            var model = await employeeService.GetByUid(uid);

            if (model is null)
                return HttpNotFound();

            var res = EmployeeMapper.Map(model);

            return View(res);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(EmployeeViewModel request)
        {
            var model = await employeeService.GetByEmail(request.Email);

            if (model.Item1.Any())
                ModelState.AddModelError("Email", "Пользователь с такой почтой уже есть.");

            if (ModelState.IsValid)
            {
                var employee = new EmployeeModel();

                EmployeeMapper.Map(request, employee);

                await employeeService.Create(employee);

                return RedirectToAction("List");
            }

            return View(request);
        }

        [HttpGet]
        public async Task<ActionResult> Update(Guid uid)
        {
            var model = await employeeService.GetByUid(uid);

            if (model is null)
                return HttpNotFound();

            return View(EmployeeMapper.Map(model));
        }

        [HttpPost]
        public async Task<ActionResult> Update(EmployeeViewModel request)
        {
            var employee = await employeeService.GetByUid((Guid)request.Uid);

            if (employee == null)
                ModelState.AddModelError("Uid", String.Format("Сотрудника с таким идентификатором нету: {0}.", request.Uid));

            var model = await employeeService.GetByEmail(request.Email);

            if (model.Item1.Where(x => x.Uid != request.Uid).Any())
                ModelState.AddModelError("Email", "Пользователь с такой почтой уже есть.");

            if (ModelState.IsValid)
            {
                EmployeeMapper.Map(request, employee);

                await employeeService.Update(employee);

                return RedirectToAction("List");
            }

            return View(request);
        }

        [HttpGet]
        public async Task<ActionResult> Delete(Guid uid)
        {
            await employeeService.Delete(uid);

            return RedirectToAction("List");
        }
    }
}