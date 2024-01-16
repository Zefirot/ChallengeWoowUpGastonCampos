using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnitTestChallenge_WoowUp.src;

public class SistemaAlertas
{
    public List<Usuario> usuarios { get; set; }
    public List<string> temas { get; set; }
    
    public SistemaAlertas()
    {
        usuarios = new List<Usuario>();
        temas = new List<string>();
    }

    public void RegistrarUsuario(string nombre)
    {
        if(usuarios.Find(user => user.Nombre == nombre) == null)
        {
            var nuevoUsuario = new Usuario(nombre);
            usuarios.Add(nuevoUsuario);
        }
    }

    public void RegistrarTema(string tema)
    {
        if (!temas.Contains(tema))
        {
            temas.Add(tema);
        }
    }

    public void AsignarTemaAUsuario(string userName, string issueName) //Se entiende que un usuario no puede estar repetido
    {
        string temaASignar = temas.Find(tema => tema == issueName);
        if(temaASignar == null)
            throw new Exception("No se encontro el tema solicitado");
        

        Usuario usuario = usuarios.Find(user => user.Nombre == userName);
        if (usuario == null)
            throw new Exception("No se encontro el usuario solicitado");

        usuario.RegistrarTema(temaASignar);
    }

    public void EnviarAlerta(string mensaje, TipoAlerta tipo, string nombreTema, DateTime? fechaExpiracion = null, string nombreUser = "")
    {
        if(nombreUser != "")//Caso 1: Se enviar alerta a usuario
        {
            Usuario usuarioEncontrado = usuarios.Find(user => user.Nombre == nombreUser);
            usuarioEncontrado.RecibirAlerta(new Alerta(mensaje, tipo, nombreTema, fechaExpiracion: fechaExpiracion));
            return;
        }

        foreach (Usuario usuario in usuarios) //Caso 2: Se envia alerta sin especificar usuario
        {
            if (usuario.TemaSubscrito(nombreTema))
            {
                usuario.RecibirAlerta(new Alerta(mensaje, tipo, nombreTema, fechaExpiracion: fechaExpiracion));
            }
        }
    }

    public List<Alerta> ObtenerAlertasNoLeidasUsuario(string nombreUser)
    {
        Usuario userFilter = usuarios.Find(user => user.Nombre == nombreUser);

        if(userFilter != null)
        {
            return userFilter.ObtenerAlertasNoLeidas();
        }

        return new List<Alerta>();        
    }

    /*
     * DISCLAIMER: Se pueden obtener todas las alertas no expiradas para un tema. Se informa para cada alerta si es para todos los usuarios o para uno específico.
     * Mi manera de comprender el usuario es que se puede buscar las alertas no expiradas por tema y se puede especificar si es en general por un usuario
     * Esto lo aclaro porque puede que mi comprension de este punto sea erronea
     */
    public List<Alerta> ObtenerAlertasNoExpiradasPorTema(string tema, string nombreUser = "")
    {
        List<Alerta> alertasObtenidas = new List<Alerta>();

        if(nombreUser != "")
        {
            Usuario userFilter = usuarios.Find(user => user.Nombre == nombreUser);

            if(userFilter != null)
            {
                alertasObtenidas = userFilter.ObtenerAlertasPorTema(tema);
            }
        }
        else
        {
            foreach(Usuario user in usuarios)
            {
                alertasObtenidas.AddRange(user.ObtenerAlertasPorTema(tema));
            }
        }

        return alertasObtenidas;
    }
}

