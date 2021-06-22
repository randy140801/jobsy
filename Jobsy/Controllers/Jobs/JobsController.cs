﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using ENTITY_L.Models.Jobs;
using Jobsy_API.Controllers;
using Microsoft.Owin.Security;
using System.IO;

namespace Jobsy.Controllers
{
    public class JobsController : Controller
    {
        JobsAPIController job = new JobsAPIController();


        public ActionResult PostAJob()
        {
            return View();
        }
            
        [HttpPost]
        [AllowAnonymous] 
        public async Task<ActionResult> PostAJob(JobsModel model) // Este metodo se llama al enviar el formulario
        {
            try
            {
                await job.PostAJob(model); // Llama al metodo que se encuenta en la API
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);//Lanza un mensaje en la consola en caso de error
            }

            return View();//Retorna la vista
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> LoadJobsAsync() // Metodo para devolver una vista con todos los empleaos 
        {
            try
            {
                List<JobsModel> AllJobs = await job.LoadJobsAsync(); // Llama al metodo que se encuenta en la API
                ViewBag.AllJobs = AllJobs; //Guardamos el resultado del metodo en el Viewbag
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message); //Lanza un mensaje en la consola en caso de error
            }

            return View(); //Retorna la vista
        }


        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> EditJob(string id) 
        {
            try
            {
                JobsModel ajob = await job.Loadjob(id);
                ViewBag.ajob = ajob; 
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message); 
            }

            return View(); 
        }



        [HttpGet]
        [AllowAnonymous]
        public ActionResult Deletejob(string id)
        {
            try
            {
                job.Deletejob(id);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return RedirectToAction("LoadJobsAsync");
        }
    }
}