namespace BL
{
    public class Usuario
    {
        public static ML.Result Add(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.DayanUsuarioContext context = new DL.DayanUsuarioContext())
                {
                    DL.Usuario usuarioDL = new DL.Usuario();

                    usuarioDL.Nombre = usuario.Nombre;
                    usuarioDL.ApellidoPaterno = usuario.ApellidoPaterno;
                    usuarioDL.ApellidoMaterno = usuario.ApellidoMaterno;
                    usuarioDL.Edad = usuario.Edad;

                    context.Usuarios.Add(usuarioDL);
                    context.SaveChanges();

                    result.Correct = true;

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }

        public static ML.Result Update(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.DayanUsuarioContext context = new DL.DayanUsuarioContext())
                {
                    var query = (from usuarioLQ in context.Usuarios
                                 where usuarioLQ.IdUsuario == usuario.IdUsuario
                                 select usuarioLQ).SingleOrDefault();

                    if (query != null)
                    {
                        query.Nombre = usuario.Nombre;
                        query.ApellidoPaterno = usuario.ApellidoPaterno;
                        query.ApellidoMaterno = usuario.ApellidoMaterno;
                        query.Edad = usuario.Edad;

                        context.SaveChanges();
                        result.Correct = true;
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }

        public static ML.Result Delete(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.DayanUsuarioContext context = new DL.DayanUsuarioContext())
                {
                    var query = (from usuarioLQ in context.Usuarios
                                 where usuarioLQ.IdUsuario == IdUsuario
                                 select usuarioLQ).First();

                    context.Usuarios.Remove(query);
                    context.SaveChanges();
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }

        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.DayanUsuarioContext context = new DL.DayanUsuarioContext())
                {
                    var listaUsuarios = (from usuario in context.Usuarios
                                         select new
                                         {
                                             usuario.IdUsuario,
                                             usuario.Nombre,
                                             usuario.ApellidoPaterno,
                                             usuario.ApellidoMaterno,
                                             usuario.Edad
                                         }).ToList();
                    if (listaUsuarios.Count > 0)
                    {
                        result.Objects = new List<object>();

                        foreach (var usuario in listaUsuarios)
                        {
                            ML.Usuario usuarioItem = new ML.Usuario();
                            usuarioItem.IdUsuario = usuario.IdUsuario;
                            usuarioItem.Nombre = usuario.Nombre;
                            usuarioItem.ApellidoPaterno = usuario.ApellidoPaterno;
                            usuarioItem.ApellidoMaterno = usuario.ApellidoMaterno;
                            usuarioItem.Edad = usuario.Edad;

                            result.Objects.Add(usuarioItem);
                        }
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No hay registros en la tabla";
                    }

                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
        public static ML.Result GetById(int IdUsuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.DayanUsuarioContext context = new DL.DayanUsuarioContext())
                {
                    var query = (from usuarioLQ in context.Usuarios
                                 where usuarioLQ.IdUsuario == IdUsuario
                                 select usuarioLQ).Single();

                    if (query != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();

                        usuario.IdUsuario = query.IdUsuario;
                        usuario.Nombre = query.Nombre;
                        usuario.ApellidoPaterno = query.ApellidoPaterno;
                        usuario.ApellidoMaterno = query.ApellidoMaterno;
                        usuario.Edad = query.Edad;

                        result.Object = usuario;
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontro el usuario";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }
    }
}
