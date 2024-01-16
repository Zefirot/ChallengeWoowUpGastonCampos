using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestChallenge_WoowUp.src
{
    public enum TipoAlerta
    {
        Informativa,
        Urgente
    }

    public class Alerta
    {
        public string Mensaje { get; set; }
        public TipoAlerta Tipo { get; set; }
        public string Tema { get; set; }
        public string Destinatario { get; set; }
        public DateTime? FechaExpiracion { get; set; }
        public bool Leida { get; set; }

        public Alerta(string mensaje, TipoAlerta tipo, string tema, string destinatario = null, DateTime? fechaExpiracion = null)
        {
            Mensaje = mensaje;
            Tipo = tipo;
            Tema = tema;
            Destinatario = destinatario;
            FechaExpiracion = fechaExpiracion;
            Leida = false;
        }
    }
}
