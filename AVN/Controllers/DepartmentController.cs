﻿using AutoMapper.Execution;
using AVN.Automapper;
using AVN.Data;
using AVN.Data.UnitOfWorks;
using AVN.Model.Entities;
using AVN.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace AVN.Web.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly AppDbContext context;

        public DepartmentController(IUnitOfWork unitOfWork, IMapper mapper, AppDbContext context)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.context = context;
        }

        // GET: Department
        public async Task<IActionResult> Index()
        {
            var departments = await unitOfWork.DepartmentRepository.GetAllAsync();
            var mappedDepartment = mapper.Map<Department, DepartmentVM>(departments).ToList();
            return View(mappedDepartment);
        }

        // GET: Department/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var department = await unitOfWork.DepartmentRepository.GetByIdAsync(id);

            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // GET: Department/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: Department/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DepartmentName,DepartmentShortName,FacultyId")] DepartmentVM department)
        {
            if (ModelState.IsValid)
            {
                var mappedDepartment = mapper.Map<DepartmentVM,Department> (department);
                await unitOfWork.DepartmentRepository.CreateAsync(mappedDepartment);
                await unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        // GET: Department/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var department = await unitOfWork.DepartmentRepository.GetByIdAsync(id);
            if (department == null)
            {
                return NotFound();
            }
            var mappedDepartment = mapper.Map<Department, DepartmentVM> (department);
            return View(mappedDepartment);
        }

        // POST: Department/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DepartmentName,DepartmentShortName,FacultyId")] DepartmentVM department)
        {
            if (id != department.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var mappedDepartment = mapper.Map<DepartmentVM, Department> (department);
                await unitOfWork.DepartmentRepository.UpdateAsync(mappedDepartment);
                await unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        // GET: Department/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var department = await unitOfWork.DepartmentRepository.GetByIdAsync(id);
            if (department == null)
            {
                return NotFound();
            }

            return View(department);
        }

        // POST: Department/Delete/5
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var department = await context.Departments
                    .Include(f => f.Employees)
                    .Include(d => d.Subjects)
                    .Include(d => d.Directions)
                    .ThenInclude(g => g.Groups)
                    .FirstOrDefaultAsync(i => i.Id == id);

                if (department == null)
                {
                    TempData["error"] =
                        "Не удалось найти кафедру. Пожалуйста попробуйте позже, либо обратитесь к администратору.";
                    return RedirectToAction("Index", "Department");
                }

                if (false && department.Faculty != null)
                {
                    TempData["error"] = "Невозможно удалить студента, так как у него есть связанная запись.";
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    await unitOfWork.EmployeeRepository.DeleteRangeAsync(department.Employees);
                    await unitOfWork.SubjectRepository.DeleteRangeAsync(department.Subjects);
                    await unitOfWork.DirectionRepository.DeleteRangeAsync(department.Directions);
                }

                TempData["success"] = "Запись успешно удалена";
                await unitOfWork.DepartmentRepository.DeleteAsync(department);
                await unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch(Exception ex) 
            {
                TempData["error"] = $"Произошла внутренняя ошибка: {ex.Message}.  Пожалуйста попробуйте позже, либо обратитесь к администратору.";
                return RedirectToAction("Index", "Department");
            }
           
        }

        public async Task<List<SelectListItem>> GetDepartmentsByFaculty(int facultyId)
        {
            var departments = (await unitOfWork.DepartmentRepository.GetAllAsync()).Where(x => x.FacultyId == facultyId);
            var departmentList = departments.Select(f => new SelectListItem { Value = f.Id.ToString(), Text = f.DepartmentName }).ToList();
            return departmentList;
        }

        public async Task<List<SelectListItem>> GetDepartments()
        {
            var departments = await unitOfWork.DepartmentRepository.GetAllAsync();
            var departmentList = departments.Select(f => new SelectListItem { Value = f.Id.ToString(), Text = f.DepartmentName }).ToList();
            return departmentList;
        }

    }
}
