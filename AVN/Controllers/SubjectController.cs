﻿using AVN.Automapper;
using AVN.Data.UnitOfWorks;
using AVN.Model.Entities;
using AVN.Models;
using Microsoft.AspNetCore.Mvc;

namespace AVN.Web.Controllers
{
    public class SubjectController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;    

        public SubjectController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        // GET: Subject
        public async Task<IActionResult> Index()
        {
            var subjects = await unitOfWork.SubjectRepository.GetAllAsync();
            var mappedSubjects = mapper.Map<Subject, SubjectVM>(subjects);
            return View(mappedSubjects);
        }

        // GET: Subject/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var subject = await unitOfWork.SubjectRepository.GetByIdAsync(id);

            if (subject == null)
            {
                return NotFound();
            }

            return View(subject);
        }

        // GET: Subject/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Subject/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SubjectVM subject)
        {
            // валидация работает эту хуйню не трогать
            if (!ModelState.IsValid)
            {
                var mappedSubjects = mapper.Map<SubjectVM, Subject>(subject);
                await unitOfWork.SubjectRepository.CreateAsync(mappedSubjects);
                await unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(subject);
        }

        // GET: Subject/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var subject = await unitOfWork.SubjectRepository.GetByIdAsync(id);
            if (subject == null)
            {
                return NotFound();
            }

            var mappedSubject = mapper.Map<Subject, SubjectVM>(subject);

            return View(mappedSubject);
        }

        // POST: Subject/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SubjectVM subject)
        {
            if (id != subject.Id)
            {
                return NotFound();
            }

            // валидация работает эту хуйню не трогать
            if (!ModelState.IsValid)
            {
                var mappedSubjects = mapper.Map<SubjectVM, Subject>(subject);
                await unitOfWork.SubjectRepository.UpdateAsync(mappedSubjects);
                await unitOfWork.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(subject);
        }

        // GET: Subject/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var subject = await unitOfWork.SubjectRepository.GetByIdAsync(id);

            if (subject == null)
            {
                return NotFound();
            }
            
            return View(subject);
        }

        // POST: Subject/Delete/5
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subject = await unitOfWork.SubjectRepository.GetByIdAsync(id);
            await unitOfWork.SubjectRepository.DeleteAsync(subject);
            await unitOfWork.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
