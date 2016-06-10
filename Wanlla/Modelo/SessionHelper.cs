using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using System.Configuration;

namespace Modelo
{
    public class SessionHelper
    {
        public static void CrearSesion(string id_usuario, string tipo_usuario, string nombre_usuario)
        {
            HttpContext.Current.Session["login"] = true;
            HttpContext.Current.Session["id_usuario"] = id_usuario;
            HttpContext.Current.Session["tipo_usuario"] = tipo_usuario;
            HttpContext.Current.Session["nombre_usuario"] = nombre_usuario;
        }

        public static void DestruirSesion()
        {
            HttpContext.Current.Session.RemoveAll();
        }

        public static bool ExisteSesion()
        {
            if(Convert.ToBoolean(HttpContext.Current.Session["login"]) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static T Leer<T>(string variable)
        {
            object valor = HttpContext.Current.Session[variable];

            if (valor == null)
                return default(T);
            else
                return ((T)valor);
        }

        public static void Escribir(string variable, object valor)
        {
            HttpContext.Current.Session[variable] = valor;
        }



        public static bool ExistUserInSession()
        {
            return HttpContext.Current.User.Identity.IsAuthenticated;
        }

        public static void DestroyUserSession()
        {
            FormsAuthentication.SignOut();
        }

        public static int GetUser()
        {
            int user_id = 0;
            if (HttpContext.Current.User != null && HttpContext.Current.User.Identity is FormsIdentity)
            {
                FormsAuthenticationTicket ticket = ((FormsIdentity)HttpContext.Current.User.Identity).Ticket;
                if (ticket != null)
                {
                    user_id = Convert.ToInt32(ticket.UserData);
                }
            }
            return user_id;
        }

        public static void AddUserToSession(string id)
        {
            bool persist = true;
            var cookie = FormsAuthentication.GetAuthCookie("usuario", persist);

            cookie.Name = FormsAuthentication.FormsCookieName;
            cookie.Expires = DateTime.Now.AddMonths(3);

            var ticket = FormsAuthentication.Decrypt(cookie.Value);
            var newTicket = new FormsAuthenticationTicket(ticket.Version, ticket.Name, ticket.IssueDate, 
                                                          ticket.Expiration, ticket.IsPersistent, id);

            cookie.Value = FormsAuthentication.Encrypt(newTicket);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }
}
