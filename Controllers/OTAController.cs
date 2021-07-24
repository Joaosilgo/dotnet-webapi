using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_webapi.Controllers
{
    [Route("v1/ota")]
    [ApiController]
    public class OTAController : ControllerBase
    {

        public static IWebHostEnvironment _environment;
        public OTAController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }


        [HttpGet]
        public string Get()
        {
            string text = " Web API - OTAController in execution : " + DateTime.Now.ToLongTimeString();
            text += $"\n Environment :  {_environment.EnvironmentName}";
            return text;
        }



        /// <summary>
        /// Upload a firmware .bin file.
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> SendArchive(IFormFile file)
        {
            if (file != null)
            {
                if (file.Length > 0 && CheckIfBinFile(file))
                {
                    try
                    {
                        if (!Directory.Exists(_environment.WebRootPath + "\\Firmware\\"))
                        {
                            Directory.CreateDirectory(_environment.WebRootPath + "\\Firmware\\");
                        }
                        using (FileStream filestream = System.IO.File.Create(_environment.WebRootPath + "\\Firmware\\" + file.FileName))
                        {
                            await file.CopyToAsync(filestream);
                            filestream.Flush();
                            return "\\Firmware\\" + file.FileName;


                        }
                    }
                    catch (Exception ex)
                    {
                        return ex.ToString();
                    }
                }
                else
                {
                    return "Ocorreu uma falha no envio do file...";
                }
            }
            else
            {
                return "Ocorreu uma falha no envio do file...";
            }
        }



        private bool CheckIfBinFile(IFormFile file)
        {
            var extension = "." + file.FileName.Split('.')[file.FileName.Split('.').Length - 1];
            return (extension == ".bin" || extension == ".ino"); // Change the extension based on your need
        }

    }
}