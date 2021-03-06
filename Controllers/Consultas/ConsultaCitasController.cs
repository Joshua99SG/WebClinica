using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clinica.Models;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Mvc;
using WebClinica.Filter;
using WebClinica.Models;
using WebClinica.Models.ViewModel;

namespace Clinica.Controllers
{
    [ServiceFilter(typeof(Seguridad))]
    public class ConsultaCitasController : Controller
    {
        private readonly DBClinicaAcmeContext _db;
        List<CitaMedica> listaCitas = new List<CitaMedica>();
        static List<CitaMedica> lista = new List<CitaMedica>();

        public ConsultaCitasController(DBClinicaAcmeContext db)
        {
            _db = db;
        }
        public IActionResult Index(DateTime FechaInicio, DateTime FechaFinal)
        {
           String _FechaInicio =  FechaInicio.ToString("dd-MM-yyyy");
           String _FechaFinal = FechaFinal.ToString("dd-MM-yyyy");


            if ((_FechaInicio == "01-01-0001") || (_FechaFinal == "01-01-0001"))
            {
                listaCitas = (from citas in _db.Citas

                              join pacientes in _db.Paciente
                              on citas.PacienteId equals
                              pacientes.PacienteId

                              join medico in _db.Medico
                              on citas.MedicoId equals
                              medico.MedicoId

                              join especialidad in _db.Especialidad
                              on citas.EspecialidadId equals
                              especialidad.EspecialidadId
                              select new CitaMedica
                              {
                                  CitaId = citas.CitaId,
                                  NombrePaciente = pacientes.Nombre +
                                              " " + pacientes.Apellidos,
                                  MedicoId = medico.MedicoId,
                                  NombreMedico = medico.Nombre + " " + medico.Apellidos,
                                  NombreEspecialidad = especialidad.Nombre,
                                  FechaCita = citas.FechaCita
                              }).ToList();
                ViewBag.Especialidad = "";
            }
            else
            {
                listaCitas = (from citas in _db.Citas

                              join pacientes in _db.Paciente
                              on citas.PacienteId equals
                              pacientes.PacienteId

                              join medico in _db.Medico
                              on citas.MedicoId equals
                              medico.MedicoId

                              join especialidad in _db.Especialidad
                              on citas.EspecialidadId equals
                              especialidad.EspecialidadId

                              where (citas.FechaCita >= FechaInicio 
                                  && citas.FechaCita <= FechaFinal)

                              select new CitaMedica
                              {
                                  CitaId = citas.CitaId,
                                  NombrePaciente = pacientes.Nombre +
                                              " " + pacientes.Apellidos,
                                  MedicoId = medico.MedicoId,
                                  NombreMedico = medico.Nombre + " " + medico.Apellidos,
                                  NombreEspecialidad = especialidad.Nombre,
                                  FechaCita = citas.FechaCita
                              }).ToList();
                ViewBag.fechaInicio = FechaInicio.ToString("yyyy-MM-dd");
                ViewBag.fechaFinal = FechaFinal.ToString("yyyy-MM-dd");
            }
            lista = listaCitas;
            return View(listaCitas);
        }
        //metodo que descarga el archivo excel
        public FileResult exportarExcel()
        {
            Utilitarios util = new Utilitarios();
            string[] cabeceras = {"Paciente", "Medico","Especialidad", "Fecha Cita" };
            string[] nombrePropiedades = {"NombrePaciente", "NombreMedico", "NombreEspecialidad","FechaCita"};
            byte[] buffer = util.generarExcel(cabeceras, nombrePropiedades, lista);
            //content type mime xlsx google
            return File(buffer, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
        }
        public FileResult exportarPDF()
        {
            Utilitarios util = new Utilitarios();
            string[] cabeceras = {"Paciente", "Medico", "Especialidad", "Fecha Cita" };

            string[] nombrePropiedades = {"NombrePaciente", "NombreMedico","NombreEspecialidad","FechaCita"};
            string titulo = "Reporte de Citas Médicas";
            byte[] buffer = util.ExportarPDFDatos(nombrePropiedades, lista, titulo);
            return File(buffer, "application/pdf");
        }
    }
}
