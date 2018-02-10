using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Kingston.Demo.Ver1.Models;

namespace Kingston.Demo.Ver1.Controllers
{
    public class TblCodesController : Controller
    {
        private readonly KingStonDemoContext _context;

        public TblCodesController(KingStonDemoContext context)
        {
            _context = context;
        }

        // GET: TblCodes
        public async Task<IActionResult> Index()
        {
            return View(await _context.TblCode.ToListAsync());
        }

        //public IActionResult Index()
        //{
        //    List<TblCode> ListCDO = (from p in _context.TblCode
        //                             where p.Type.ToUpper() == "SYS"
        //                             select p).ToList();
        //    //ListCDO.Insert(0, new TblCode() { Text = "請選取", Value = "" });
        //    ViewBag.ListOfTblCode = ListCDO;
        //    return View();
        //}

        // GET: TblCodes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblCode = await _context.TblCode
                .SingleOrDefaultAsync(m => m.Id == id);
            if (tblCode == null)
            {
                return NotFound();
            }

            return base.View((object)tblCode);
        }

        // GET: TblCodes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TblCodes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type,Seq,Value,Text,UpdateDate,UpdateUser")] TblCode tblCode)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tblCode);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tblCode);
        }

        // GET: TblCodes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblCode = await _context.TblCode.SingleOrDefaultAsync(m => m.Id == id);
            if (tblCode == null)
            {
                return NotFound();
            }
            return base.View((object)tblCode);
        }

        // POST: TblCodes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type,Seq,Value,Text,UpdateDate,UpdateUser")] TblCode tblCode)
        {
            if (id != tblCode.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tblCode);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TblCodeExists(tblCode.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(tblCode);
        }

        // GET: TblCodes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tblCode = await _context.TblCode
                .SingleOrDefaultAsync(m => m.Id == id);
            if (tblCode == null)
            {
                return NotFound();
            }

            return base.View((object)tblCode);
        }

        // POST: TblCodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tblCode = await _context.TblCode.SingleOrDefaultAsync(m => m.Id == id);
            _context.TblCode.Remove(tblCode);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TblCodeExists(int id)
        {
            return _context.TblCode.Any(e => e.Id == id);
        }
    }
}
