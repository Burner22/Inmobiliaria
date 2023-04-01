namespace Inmobiliaria2.Models
{
    using System;

    public class Usuario
    {
        private int idUsuario;
        private string email;
        private string pass;
        private string alias;
        private string rol;

        public Usuario(int idUsuario, string email, string pass, string alias, string rol)
        {
            this.idUsuario = idUsuario;
            this.email = email;
            this.pass = pass;
            this.alias = alias;
            this.rol = rol;
        }

        public Usuario(string email, string pass, string alias, string rol)
        {
            this.email = email;
            this.pass = pass;
            this.alias = alias;
            this.rol = rol;
        }

        public int IdUsuario
        {
            get { return idUsuario; }
            set { idUsuario = value; }
        }

        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        public string Pass
        {
            get { return pass; }
            set { pass = value; }
        }

        public string Alias
        {
            get { return alias; }
            set { alias = value; }
        }

        public string Rol
        {
            get { return rol; }
            set { rol = value; }
        }
    }

}
