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

/*
 SELECT 
c.ID,
c.Nombre,
c.Apellido
FROM 
Clientes c
JOIN 
Ventas v ON c.ID = v.Id_cliente
WHERE 
v.Fecha >= DATEADD(month, -12, GETDATE())  -- Obtener ventas de los últimos 12 meses
GROUP BY 
c.ID, c.Nombre, c.Apellido
HAVING 
SUM(v.Importe) > 100000
 */

