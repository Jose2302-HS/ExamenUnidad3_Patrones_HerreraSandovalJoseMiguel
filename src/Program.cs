using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatronesU3Examenv1
{
    class Program
    {
        static List<Usuario> Usuarios = new List<Usuario>();
        static List<Curso> Cursos = new List<Curso>();
        static List<ProgresoCurso> Progresos = new List<ProgresoCurso>();

        static int ultimoIdUsuario = 0;
        static int ultimoIdCurso = 0;

        static void Main(string[] args)
        {
            InicializarDatosDemo();
            MenuPrincipal();
        }

        static void InicializarDatosDemo()
        {
            Usuarios.Add(new Usuario
            {
                Id = ++ultimoIdUsuario,
                NombreUsuario = "admin",
                Contraseña = "admin123",
                EsAdministrador = true
            });

            Cursos.Add(new Curso
            {
                Id = ++ultimoIdCurso,
                Nombre = "Fundamentos de Programación en Python",
                Instructor = "Ing. Gonzalez"
            });

            Cursos.Add(new Curso
            {
                Id = ++ultimoIdCurso,
                Nombre = "Introducción a Redes de Computadoras",
                Instructor = "Mtro. Hinojoza"
            });

            Cursos.Add(new Curso
            {
                Id = ++ultimoIdCurso,
                Nombre = "Bases de Datos MySQL",
                Instructor = "Lic. Ramirez"
            });
        }

        static void MenuPrincipal()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== PLATAFORMA DE CURSOS EN TI ===");
                Console.WriteLine("1. Registrarse");
                Console.WriteLine("2. Iniciar sesión");
                Console.WriteLine("3. Salir");
                Console.Write("Selecciona una opción: ");
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        RegistrarUsuario();
                        break;
                    case "2":
                        IniciarSesion();
                        break;
                    case "3":
                        Console.WriteLine("Saliendo del sistema.");
                        return;
                    default:
                        Console.WriteLine("Opción no válida. Presiona una tecla para continuar.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void RegistrarUsuario()
        {
            Console.Clear();
            Console.WriteLine("=== REGISTRO DE USUARIO ===");
            Console.Write("Nombre de usuario: ");
            string nombre = Console.ReadLine();

            if (Usuarios.Any(u => u.NombreUsuario.Equals(nombre, StringComparison.OrdinalIgnoreCase)))
            {
                Console.WriteLine("Ese nombre de usuario ya existe.");
                Console.ReadKey();
                return;
            }

            Console.Write("Contraseña: ");
            string contraseña = Console.ReadLine();

            var nuevo = new Usuario
            {
                Id = ++ultimoIdUsuario,
                NombreUsuario = nombre,
                Contraseña = contraseña,
                EsAdministrador = false
            };

            Usuarios.Add(nuevo);
            Console.WriteLine("Usuario registrado con éxito.");
            Console.ReadKey();
        }

        static void IniciarSesion()
        {
            Console.Clear();
            Console.WriteLine("=== INICIO DE SESIÓN ===");
            Console.Write("Nombre de usuario: ");
            string nombre = Console.ReadLine();
            Console.Write("Contraseña: ");
            string contraseña = Console.ReadLine();

            var usuario = Usuarios.FirstOrDefault(
                u => u.NombreUsuario.Equals(nombre, StringComparison.OrdinalIgnoreCase)
                     && u.Contraseña == contraseña);

            if (usuario == null)
            {
                Console.WriteLine("Credenciales incorrectas.");
                Console.WriteLine("Presiona una tecla para continuar.");
                Console.ReadKey();
                return;
            }

            if (usuario.EsAdministrador)
                MenuAdministrador(usuario);
            else
                MenuAlumno(usuario);
        }


        static void MenuAlumno(Usuario usuario)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"=== BIENVENIDO ({usuario.NombreUsuario}) ===");
                Console.WriteLine("1. Ver cursos disponibles");
                Console.WriteLine("2. Inscribirse a un curso");
                Console.WriteLine("3. Ver cursos inscritos");
                Console.WriteLine("4. Marcar curso como terminado");
                Console.WriteLine("5. Generar certificado");
                Console.WriteLine("6. Cerrar sesión");
                Console.Write("Opción: ");
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        VerCursos();
                        break;
                    case "2":
                        InscribirseCurso(usuario);
                        break;
                    case "3":
                        VerCursosInscritos(usuario);
                        break;
                    case "4":
                        MarcarCursoTerminado(usuario);
                        break;
                    case "5":
                        GenerarCertificado(usuario);
                        break;
                    case "6":
                        return;
                    default:
                        Console.WriteLine("Opción no válida.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void VerCursos()
        {
            Console.Clear();
            Console.WriteLine("=== CURSOS DISPONIBLES ===");
            foreach (var c in Cursos)
            {
                Console.WriteLine($"{c.Id}. {c.Nombre} (Instructor: {c.Instructor})");
            }
            Console.WriteLine("\nPresiona una tecla para continuar.");
            Console.ReadKey();
        }

        static void InscribirseCurso(Usuario usuario)
        {
            VerCursos();
            Console.Write("Id del curso al que deseas inscribirte: ");
            if (!int.TryParse(Console.ReadLine(), out int idCurso))
            {
                Console.WriteLine("Id no válido.");
                Console.ReadKey();
                return;
            }

            var curso = Cursos.FirstOrDefault(c => c.Id == idCurso);
            if (curso == null)
            {
                Console.WriteLine("Curso no encontrado.");
                Console.ReadKey();
                return;
            }

            if (usuario.CursosInscritos.Contains(idCurso))
            {
                Console.WriteLine("Ya estás inscrito en ese curso.");
                Console.ReadKey();
                return;
            }

            usuario.CursosInscritos.Add(idCurso);
            Console.WriteLine("Inscripción realizada con éxito.");
            Console.ReadKey();
        }

        static void VerCursosInscritos(Usuario usuario)
        {
            Console.Clear();
            Console.WriteLine("=== TUS CURSOS INSCRITOS ===");
            if (!usuario.CursosInscritos.Any())
            {
                Console.WriteLine("No estás inscrito en ningún curso.");
            }
            else
            {
                foreach (var id in usuario.CursosInscritos)
                {
                    var c = Cursos.FirstOrDefault(x => x.Id == id);
                    if (c != null)
                    {
                        bool completado = Progresos.Any(p => p.UsuarioId == usuario.Id && p.CursoId == c.Id && p.Completado);
                        string estado = completado ? "Terminado" : "En progreso";
                        Console.WriteLine($"{c.Id}. {c.Nombre} (Estado: {estado})");
                    }
                }
            }
            Console.WriteLine("\nPresiona una tecla para continuar.");
            Console.ReadKey();
        }

        static void MarcarCursoTerminado(Usuario usuario)
        {
            VerCursosInscritos(usuario);
            Console.Write("\nId del curso que deseas marcar como terminado: ");
            if (!int.TryParse(Console.ReadLine(), out int idCurso))
            {
                Console.WriteLine("Id no válido.");
                Console.ReadKey();
                return;
            }

            if (!usuario.CursosInscritos.Contains(idCurso))
            {
                Console.WriteLine("No estás inscrito en ese curso.");
                Console.ReadKey();
                return;
            }

            var progreso = Progresos.FirstOrDefault(p => p.UsuarioId == usuario.Id && p.CursoId == idCurso);
            if (progreso == null)
            {
                progreso = new ProgresoCurso
                {
                    UsuarioId = usuario.Id,
                    CursoId = idCurso,
                    Completado = true,
                    FechaFinalizacion = DateTime.Now
                };
                Progresos.Add(progreso);
            }
            else
            {
                progreso.Completado = true;
                progreso.FechaFinalizacion = DateTime.Now;
            }

            Console.WriteLine("Curso marcado como terminado.");
            Console.ReadKey();
        }

        static void GenerarCertificado(Usuario usuario)
        {
            var completados = Progresos
                .Where(p => p.UsuarioId == usuario.Id && p.Completado)
                .ToList();

            if (!completados.Any())
            {
                Console.WriteLine("No tienes cursos terminados para generar certificado.");
                Console.ReadKey();
                return;
            }

            Console.Clear();
            Console.WriteLine("=== CURSOS TERMINADOS ===");
            foreach (var p in completados)
            {
                var c = Cursos.FirstOrDefault(x => x.Id == p.CursoId);
                if (c != null)
                {
                    Console.WriteLine($"{c.Id}. {c.Nombre} (Finalizado: {p.FechaFinalizacion:dd/MM/yyyy})");
                }
            }

            Console.Write("\nIngresa el Id del curso para generar certificado: ");
            if (!int.TryParse(Console.ReadLine(), out int idCurso))
            {
                Console.WriteLine("Id no válido.");
                Console.ReadKey();
                return;
            }

            var prog = completados.FirstOrDefault(p => p.CursoId == idCurso);
            var curso = Cursos.FirstOrDefault(c => c.Id == idCurso);

            if (prog == null || curso == null)
            {
                Console.WriteLine("Curso no encontrado o no está terminado.");
                Console.ReadKey();
                return;
            }

            ICertificado certificado = new CertificadoBase(usuario, curso, prog.FechaFinalizacion ?? DateTime.Now);

            certificado = new CertificadoConFirma(certificado, curso.Instructor);
            certificado = new CertificadoConCodigo(certificado, $"CUR-{curso.Id}-USR-{usuario.Id}");

            Console.Clear();
            Console.WriteLine("Elige estilo de certificado:");
            Console.WriteLine("1. Estilo minimalista");
            Console.WriteLine("2. Estilo con marco ASCII");
            Console.Write("Opción: ");
            string opEstilo = Console.ReadLine();

            IEstiloCertificado estilo;
            if (opEstilo == "2")
                estilo = new EstiloAscii();
            else
                estilo = new EstiloMinimalista();

            var impresor = new ImprimirCertificado(certificado, estilo);
            impresor.Imprimir();

            Console.WriteLine("Certificado generado.");
            Console.ReadKey();
        }


        static void MenuAdministrador(Usuario admin)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"=== MENÚ ADMINISTRADOR ===");
                Console.WriteLine("1. Ver cursos");
                Console.WriteLine("2. Agregar curso");
                Console.WriteLine("3. Modificar curso");
                Console.WriteLine("4. Ver usuarios inscritos");
                Console.WriteLine("5. Cerrar sesión");
                Console.Write("Opción: ");
                string opcion = Console.ReadLine();

                switch (opcion)
                {
                    case "1":
                        VerCursos();
                        break;
                    case "2":
                        AgregarCurso();
                        break;
                    case "3":
                        ModificarCurso();
                        break;
                    case "4":
                        VerUsuariosInscritos();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Opción no válida.");
                        Console.ReadKey();
                        break;
                }
            }
        }

        static void AgregarCurso()
        {
            Console.Clear();
            Console.WriteLine("=== AGREGAR CURSO ===");
            Console.Write("Nombre del curso: ");
            string nombre = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                Console.WriteLine("El nombre no puede estar vacío. Regresando al menú...");
                Console.ReadKey();
                return;
            }

            Console.Write("Nombre del instructor: ");
            string instructor = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(instructor))
            {
                Console.WriteLine("El instructor no puede estar vacío. Regresando al menú...");
                Console.ReadKey();
                return;
            }

            var curso = new Curso
            {
                Id = ++ultimoIdCurso,
                Nombre = nombre,
                Instructor = instructor
            };

            Cursos.Add(curso);
            Console.WriteLine("Curso agregado con éxito.");
            Console.ReadKey();
        }

        static void ModificarCurso()
        {
            VerCursos();
            Console.Write("\nIngresa el Id del curso a modificar: ");
            if (!int.TryParse(Console.ReadLine(), out int idCurso))
            {
                Console.WriteLine("Id no válido.");
                Console.ReadKey();
                return;
            }

            var curso = Cursos.FirstOrDefault(c => c.Id == idCurso);
            if (curso == null)
            {
                Console.WriteLine("Curso no encontrado.");
                Console.ReadKey();
                return;
            }

            Console.WriteLine($"Curso actual: {curso.Nombre} (Instructor: {curso.Instructor})");
            Console.Write("Nuevo nombre (deja vacío para conservar): ");
            string nuevoNombre = Console.ReadLine();
            Console.Write("Nuevo instructor (deja vacío para conservar): ");
            string nuevoInstructor = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(nuevoNombre))
                curso.Nombre = nuevoNombre;
            if (!string.IsNullOrWhiteSpace(nuevoInstructor))
                curso.Instructor = nuevoInstructor;

            Console.WriteLine("Curso modificado.");
            Console.ReadKey();
        }

        static void VerUsuariosInscritos()
        {
            Console.Clear();
            Console.WriteLine("=== USUARIOS E INSCRIPCIONES ===");
            foreach (var usuario in Usuarios)
            {
                if (usuario.EsAdministrador) continue;

                Console.WriteLine($"Usuario: {usuario.NombreUsuario}");
                if (!usuario.CursosInscritos.Any())
                {
                    Console.WriteLine("  Sin cursos inscritos.");
                }
                else
                {
                    foreach (var idCurso in usuario.CursosInscritos)
                    {
                        var curso = Cursos.FirstOrDefault(c => c.Id == idCurso);
                        if (curso != null)
                        {
                            bool completado = Progresos.Any(p => p.UsuarioId == usuario.Id && p.CursoId == curso.Id && p.Completado);
                            string estado = completado ? "Terminado" : "En progreso";
                            Console.WriteLine($"  - {curso.Nombre} ({estado})");
                        }
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine("Presiona una tecla para continuar.");
            Console.ReadKey();
        }
    }
}
