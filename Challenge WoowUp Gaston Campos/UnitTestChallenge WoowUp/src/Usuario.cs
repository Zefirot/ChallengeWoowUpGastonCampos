using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestChallenge_WoowUp.src
{
    public class Usuario
    {
        public string Nombre { get; set; }
        private List<Alerta> AlertasInformativas { get; set; }
        private Stack<Alerta> AlertasUrgentes { get; set; }

        public List<string> temas { get; set; }

        public Usuario(string nombre)
        {
            this.Nombre = nombre;
            this.AlertasInformativas = new List<Alerta>();
            this.AlertasUrgentes = new Stack<Alerta>();
            this.temas = new List<string>();
        }

        public void RecibirAlerta(Alerta alerta)
        {
            if (TipoAlerta.Informativa == alerta.Tipo)
            {
                AlertasInformativas.Add(alerta);
            }
            else
            {
                AlertasUrgentes.Push(alerta);
            }
        }

        public void MarcarAlertaLeida(string mensajeAlerta)
        {
            ObtenerAlertas().Find(alert => alert.Mensaje == mensajeAlerta).Leida = true;
        }

        public void RegistrarTema(string tema)
        {
            if (!temas.Contains(tema))
            {
                temas.Add(tema);
            }
        }

        public bool TemaSubscrito(string tema)
        {
            return temas.Contains(tema);
        }

        public List<Alerta> ObtenerAlertas()
        {
            List<Alerta> alertasObtenidas = new List<Alerta>();

            alertasObtenidas.AddRange(AlertasUrgentes.ToList());
            alertasObtenidas.AddRange(AlertasInformativas);

            return alertasObtenidas;
        }

        /*
         * DISCLAIMER: Se que en C# el stack al hacerle un find all devuelve no solo los elementos sino que los devuelve en el orden FIFO debido a la clase Stack
         * Lo mismo pasa para los List solo que serian devueltos en orden de LIFO
         */

        public List<Alerta> ObtenerAlertasNoLeidas()
        {
            List<Alerta> alertasObtenidas = new List<Alerta>();

            List<Alerta> alertasUrgentes = AlertasUrgentes.ToList().FindAll(alert => !alert.Leida && alert.FechaExpiracion > DateTime.Now);

            List<Alerta> alertasInformativas = AlertasInformativas.FindAll(alert => !alert.Leida && alert.FechaExpiracion > DateTime.Now);

            alertasObtenidas.AddRange(alertasUrgentes);
            alertasObtenidas.AddRange(alertasInformativas);

            return alertasObtenidas;
        }
    }
}
