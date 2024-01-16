using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestChallenge_WoowUp.src;

namespace UnitTestChallenge_WoowUp.Tests
{
    [TestClass]
    public class UsuarioUnitTest
    {
        [TestMethod]
        public void AgregarAlerta()
        {
            // Arrange
            var usuario = new Usuario("Usuario1");
            var alerta = new Alerta("Mensaje de prueba", TipoAlerta.Informativa, "Tema1");

            // Act
            usuario.RecibirAlerta(alerta);

            // Assert
            CollectionAssert.Contains(usuario.ObtenerAlertas(), alerta);
        }

        [TestMethod]
        public void CambiarEstadoAlertaALeida()
        {
            // Arrange
            var usuario = new Usuario("Usuario1");
            var alerta = new Alerta("Mensaje de prueba", TipoAlerta.Informativa, "Tema1");

            // Act
            usuario.RecibirAlerta(alerta);
            usuario.MarcarAlertaLeida("Mensaje de prueba");

            // Assert
            Assert.IsTrue(alerta.Leida);
        }
    }
}
