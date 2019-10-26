using System.Linq;
using Microsoft.AspNetCore.Mvc;
using WebFileStorageData.Models;
using System.Collections.Generic;

namespace WebFileStorageData.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(IFileRepository repo)
        {
            repository = repo;
        }
        [HttpGet]
        public ViewResult Index()
        {
            return View(repository);
        }
        [HttpPost]
        public ViewResult Index(int i)
        {
            string newSelectedFile = Request.Form.FirstOrDefault(p => p.Key == "newSelected").Value;

            if(!string.IsNullOrEmpty(newSelectedFile))
            {
                if(repository.SelectedFile != newSelectedFile)
                {
                    repository.FileData = null;
                    repository.SelectedFile = newSelectedFile;
                }
            }
            else
            {
                var selectedFile = Request.Form.FirstOrDefault(p => p.Key == "selected").Value;
                if (!string.IsNullOrEmpty(selectedFile)) repository.SelectedFile = selectedFile;
                repository.GetFileData();
            }

            return View(repository);
        }
        [HttpPost]
        public bool Update()
        {
            var requestData = Request.Form.ToList();
            return repository.SetFileData(requestData);            
        }
        private IFileRepository repository;
    }
}
