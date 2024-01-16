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
    public class AlertaUnitTest
    {
        [TestMethod]
        public void InicializarPropiedadesCorrectamente()
        {
            // Arrange
            var mensaje = "Mensaje de prueba";
            var tipo = TipoAlerta.Informativa;
            var tema = "Tema1";
            var destinatario = "Usuario1";
            var fechaExpiracion = DateTime.Now.AddDays(1);

            // Act
            var alerta = new Alerta(mensaje, tipo, tema, destinatario, fechaExpiracion);

            // Assert
            Assert.AreEqual(mensaje, alerta.Mensaje);
            Assert.AreEqual(tipo, alerta.Tipo);
            Assert.AreEqual(tema, alerta.Tema);
            Assert.AreEqual(destinatario, alerta.Destinatario);
            Assert.AreEqual(fechaExpiracion, alerta.FechaExpiracion);
            Assert.IsFalse(alerta.Leida);
        }
    }
}
