using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestChallenge_WoowUp.src;

namespace Challenge_WoowUp_Gaston_Campos
{
    [TestClass]
    public class SistemaAlertasTests
    {
        [TestMethod]
        public void RegistrarUsuario()
        {
            // Arrange
            var sistemaAlertas = new SistemaAlertas();

            // Act
            sistemaAlertas.RegistrarUsuario("Usuario1");

            // Assert
            Assert.AreEqual(1, sistemaAlertas.usuarios.Count);
            Assert.AreEqual("Usuario1", sistemaAlertas.usuarios[0].Nombre);
        }

        [TestMethod]
        public void RegistrarUsuarioYaRegistrado()
        {
            // Arrange
            var sistemaAlertas = new SistemaAlertas();

            // Act
            sistemaAlertas.RegistrarUsuario("Usuario1");
            sistemaAlertas.RegistrarUsuario("Usuario1");

            // Assert
            Assert.AreEqual(1, sistemaAlertas.usuarios.Count); //Solo debe exisitir 1 usuario
        }

        [TestMethod]
        public void RegistrarTema()
        {
            // Arrange
            var sistemaAlertas = new SistemaAlertas();

            // Act
            sistemaAlertas.RegistrarTema("Tema01");

            // Assert
            Assert.AreEqual("Tema01", sistemaAlertas.temas[0]);
        }

        [TestMethod]
        public void AsignarTemaUsuario()
        {
            // Arrange
            var sistemaAlertas = new SistemaAlertas();

            // Act
            sistemaAlertas.RegistrarTema("Tema01");
            sistemaAlertas.RegistrarUsuario("Usuario1");

            sistemaAlertas.AsignarTemaAUsuario("Usuario1", "Tema01");

            // Assert
            Assert.IsTrue(sistemaAlertas.usuarios[0].TemaSubscrito("Tema01"));
        }

        [TestMethod]
        public void EnviarAlertaSobreMismoTema()
        {
            // Arrange
            var sistemaAlertas = new SistemaAlertas();

            // Act
            InitSistema(ref sistemaAlertas);

            sistemaAlertas.EnviarAlerta("Esto es una alerta", TipoAlerta.Urgente, "Tema01");

            // Assert
            Assert.AreEqual(1, sistemaAlertas.usuarios[0].ObtenerAlertas().Count);
            Assert.AreEqual(1, sistemaAlertas.usuarios[1].ObtenerAlertas().Count);
        }

        [TestMethod]
        public void EnviarAlertaUsuarioEspecifico()
        {
            // Arrange
            var sistemaAlertas = new SistemaAlertas();
            InitSistema(ref sistemaAlertas);

            // Act
            sistemaAlertas.EnviarAlerta("Esto es una alerta", TipoAlerta.Urgente, "Tema01", nombreUser: "Usuario1");

            // Assert
            Assert.AreEqual(1, sistemaAlertas.usuarios[0].ObtenerAlertas().Count);
            Assert.AreEqual(0, sistemaAlertas.usuarios[1].ObtenerAlertas().Count);
        }

        [TestMethod]
        public void EnviarAlertaFechaExpiracion()
        {
            // Arrange
            var sistemaAlertas = new SistemaAlertas();
            InitSistema(ref sistemaAlertas);

            // Act
            DateTime date = new DateTime(2025, 1, 15, 12, 30, 0);

            sistemaAlertas.EnviarAlerta("Esto es una alerta", TipoAlerta.Urgente, "Tema01", fechaExpiracion: date);

            // Assert
            Assert.AreEqual(date.ToString(), sistemaAlertas.usuarios[0].ObtenerAlertas()[0].FechaExpiracion.ToString());
        }

        [TestMethod]
        public void ObtenerAlertasNoLeidas()
        {
            // Arrange
            var sistemaAlertas = new SistemaAlertas();
            InitSistema(ref sistemaAlertas);
            EnviarAlertas(ref sistemaAlertas);

            // Act
            Usuario usuario = sistemaAlertas.usuarios[0];
            usuario.MarcarAlertaLeida("I1");
            usuario.MarcarAlertaLeida("U1");

            // Assert
            Assert.AreEqual(4, sistemaAlertas.usuarios[0].ObtenerAlertasNoLeidas().Count); //4 porque el usuario 1 ya leyo 2 alerta
            Assert.AreEqual(6, sistemaAlertas.usuarios[1].ObtenerAlertasNoLeidas().Count);
        }

        [TestMethod]
        public void ObtenerAlertasNoLeidasOrdenadas()
        {
            // Arrange
            var sistemaAlertas = new SistemaAlertas();
            InitSistema(ref sistemaAlertas);

            // Act
            EnviarAlertas(ref sistemaAlertas);

            // Assert
            var alertasNoLeidas = sistemaAlertas.usuarios[0].ObtenerAlertasNoLeidas();
            List<string> alertasMensaje = alertasNoLeidas.Select(alert => alert.Mensaje).ToList();

            string[] secuenciaBuscada = { "U2", "U1", "I1", "I2", "I3", "I4" }; //Secuencia esperada

            Assert.IsTrue(alertasMensaje.SequenceEqual(secuenciaBuscada));
        }

        private void EnviarAlertas(ref SistemaAlertas sistemaAlertas)
        {
            sistemaAlertas.EnviarAlerta("I1", TipoAlerta.Informativa, "Tema01", fechaExpiracion: new DateTime(2024, 2, 15, 5, 10, 0));
            sistemaAlertas.EnviarAlerta("I2", TipoAlerta.Informativa, "Tema01", fechaExpiracion: new DateTime(2024, 2, 15, 5, 10, 0));
            sistemaAlertas.EnviarAlerta("U1", TipoAlerta.Urgente, "Tema01", fechaExpiracion: new DateTime(2024, 2, 15, 5, 10, 0));
            sistemaAlertas.EnviarAlerta("I3", TipoAlerta.Informativa, "Tema01", fechaExpiracion: new DateTime(2024, 2, 15, 5, 10, 0));
            sistemaAlertas.EnviarAlerta("U2", TipoAlerta.Urgente, "Tema01", fechaExpiracion: new DateTime(2024, 2, 15, 5, 10, 0));
            sistemaAlertas.EnviarAlerta("I4", TipoAlerta.Informativa, "Tema01", fechaExpiracion: new DateTime(2024, 2, 15, 5, 10, 0));
        }

        private void InitSistema(ref SistemaAlertas sistemaAlertas)
        {
            sistemaAlertas.RegistrarTema("Tema01");
            sistemaAlertas.RegistrarUsuario("Usuario1");
            sistemaAlertas.RegistrarUsuario("Usuario2");

            sistemaAlertas.AsignarTemaAUsuario("Usuario1", "Tema01");
            sistemaAlertas.AsignarTemaAUsuario("Usuario2", "Tema01");
        }

    }
}
