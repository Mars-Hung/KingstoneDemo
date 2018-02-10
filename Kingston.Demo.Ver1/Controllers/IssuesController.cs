using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Kingston.Demo.Ver1.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Kingston.Demo.Ver1.ViewModels;
namespace Kingston.Demo.Ver1.Controllers
{
    public class IssuesController : Controller
    {
        private readonly KingStonDemoContext _context;

        public IssuesController(KingStonDemoContext context)
        {
            _context = context;
        }

        // GET: Issues
        public async Task<IActionResult> Index2()
        {
            return View(await _context.Issue.ToListAsync());
        }
        public async Task<IActionResult> Index(string searchString,string SysType)
        {
            IssueViewModel issueVM = new IssueViewModel();
            issueVM.TblCode_Sys = (from p in _context.TblCode
                                   where p.Type.ToUpper() == "SYS"
                                   select new SelectListItem
                                   {
                                       Value = p.Value,
                                       Text = p.Text,
                                       Selected = !String.IsNullOrEmpty(SysType) && p.Value == SysType
                                   }).ToList();
            var issue = from p in _context.Issue
                        join t in _context.TblCode.Where(a => a.Type == "Status")
                        on p.Status equals int.Parse(t.Value) into ps
                        from t in ps.DefaultIfEmpty()
                        let bSearchString = String.IsNullOrEmpty(searchString)
                        let bSysType = String.IsNullOrEmpty(SysType)
                        || p.Desc.Contains(searchString)
                        || p.Title.Contains(searchString)
                        || p.Tags.Contains(searchString)
                        || p.Type == SysType
                        where bSearchString && bSysType
                        select new Issue
                        {
                            Pid = p.Pid,
                            Desc = p.Desc,
                            Id = p.Id,
                            Type = p.Type,
                            Title = p.Title,
                            Tags = p.Tags,
                            Status = p.Status,
                            SubType = p.SubType,
                            KeyInDate = p.KeyInDate,
                            KeyInUser = p.KeyInUser,
                            StatusName = t.Text
                        };
                       
            ViewData["currentFilter"] = searchString;
            
            issueVM.IssueDatas = await issue.AsNoTracking().ToListAsync();
            return View(issueVM);
        }

        // GET: Issues/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            IssueViewModel issueVM = new IssueViewModel();
            
            var issue = await _context.Issue
                .SingleOrDefaultAsync(m => m.Id == id);
            issueVM.IssueData = issue;
            issueVM.IssueSubDatas = (from p in _context.IssueSub
                                    where p.Pid == id
                                    select p).ToList();

            issueVM.IssueFiles = from p in _context.IssueFiles
                                 where p.IssueId == id
                                 select p;
            if (issue == null)
            {
                return NotFound();
            }

            return View(issueVM);
        }

        // GET: Issues/Create
        public IActionResult Create()
        {
            var issueVM = new IssueViewModel();
            issueVM.IssueData = new Issue();

            issueVM.TblCode_Sys=
             (from p in _context.TblCode
                               where p.Type.ToUpper() == "SYS"
                               select new SelectListItem
                               {
                                   Value = p.Value,
                                   Text = p.Text
                               }).ToList();
            return View(issueVM);
            //return View();
        }

        
        // POST: Issues/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( IssueViewModel issueViewModel)
        {
            if (ModelState.IsValid)
            {
                issueViewModel.IssueData.Status = 1;
                _context.Add(issueViewModel.IssueData);
                 await _context.SaveChangesAsync();
                Issue issue = _context.Issue.Last();
                #region//存檔
                foreach (var formFile in issueViewModel.files)
                {
                    if (formFile.Length > 0)
                    {
                        var folder = Path.Combine("wwwroot","FileUploads", issue.Id.ToString());
                        //var filePathNew = Path.Combine(folder, formFile.FileName);
                        var path = Path.Combine(Directory.GetCurrentDirectory(), folder, formFile.FileName);
                        if (!Directory.Exists(folder))
                        {
                            Directory.CreateDirectory(folder);
                        }
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            await formFile.CopyToAsync(stream);
                        }
                        _context.Add(new IssueFiles()
                        {
                            FileName=formFile.FileName,
                            IssueId = issue.Id,
                        });
                        await _context.SaveChangesAsync();
                    }
                }
                #endregion
                return RedirectToAction(nameof(Index));
            }
            return View(issueViewModel.IssueData);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateIssueSub(IssueViewModel issueViewModel)
        {
            if (ModelState.IsValid)
            {
               
                var issue = await _context.Issue.FindAsync(issueViewModel.IssueSubData.Pid);
                if (issue.Status == null || issue.Status < 10)
                {
                    issue.Status = 10;
                    _context.Update(issue);
                }
                _context.Add(issueViewModel.IssueSubData);
                await _context.SaveChangesAsync();
                IssueSub issueSub = _context.IssueSub.Last();
                return RedirectToAction(nameof(Details), new { id = issueViewModel.IssueSubData.Pid });
            }
            else
            {
                var issue = await _context.Issue
                    .SingleOrDefaultAsync(m => m.Id == issueViewModel.IssueSubData.Pid);
                issueViewModel.IssueData = issue;
                return View(nameof(Details), issueViewModel);
            }
        }

        // GET: Issues/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issue = await _context.Issue.SingleOrDefaultAsync(m => m.Id == id);
            if (issue == null)
            {
                return NotFound();
            }
            return View(issue);
        }

        // POST: Issues/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Pid,Title,Type,SubType,Desc,Status,Tags,KeyInUser,KeyInDate")] Issue issue)
        {
            if (id != issue.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(issue);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IssueExists(issue.Id))
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
            return View(issue);
        }

        // GET: Issues/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var issue = await _context.Issue
                .SingleOrDefaultAsync(m => m.Id == id);
            if (issue == null)
            {
                return NotFound();
            }

            return View(issue);
        }

        // POST: Issues/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var issue = await _context.Issue.SingleOrDefaultAsync(m => m.Id == id);
            _context.Issue.Remove(issue);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool IssueExists(int id)
        {
            return _context.Issue.Any(e => e.Id == id);
        }

        [HttpPost("UploadFiles")]
        public async Task<IActionResult> UploadFiles(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            // full path to file in temp location
            var filePath = Path.GetTempFileName();


            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var fileName = Path.GetFileName(formFile.FileName);
                    var filePathNew = Path.Combine("FileUploads", fileName);
                    using (var stream = new FileStream(filePathNew, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            // process uploaded files
            // Don't rely on or trust the FileName property without validation.

            //return Ok(new { count = files.Count, size, filePath });
            return Ok(new { count = files.Count, size, filePath });
        }

    }
}
