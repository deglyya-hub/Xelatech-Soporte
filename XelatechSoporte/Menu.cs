
using System;
using System.Collections.Generic;

public class Menu
{
    static GestorXelaTech gestor = new GestorXelaTech();

    public static void Iniciar()
    {
        
        Console.Title = "XelaTech Soporte";

        bool salir = false;

        while (!salir)
        {
            Console.Clear();
            Console.WriteLine("==================================");
            Console.WriteLine("       XELATECH SOPORTE");
            Console.WriteLine("==================================");
            Console.WriteLine("1. Registrar clientes");
            Console.WriteLine("2. Registrar técnicos");
            Console.WriteLine("3. Registrar equipos");
            Console.WriteLine("4. Registrar repuestos");
            Console.WriteLine("5. Registrar solicitudes");
            Console.WriteLine("6. Gestionar solicitudes");
            Console.WriteLine("7. Consultar registros");
            Console.WriteLine("8. Ordenamientos");
            Console.WriteLine("9. Búsquedas");
            Console.WriteLine("10. Reportes");
            Console.WriteLine("11. Cargar datos de prueba");
            Console.WriteLine("0. Salir");
            Console.WriteLine("==================================");

            int opcion = LeerEntero("Seleccione una opción: ");

            try
            {
                switch (opcion)
                {
                    case 1:
                        RegistrarCliente();
                        break;
                    case 2:
                        RegistrarTecnico();
                        break;
                    case 3:
                        RegistrarEquipo();
                        break;
                    case 4:
                        RegistrarRepuesto();
                        break;
                    case 5:
                        RegistrarSolicitud();
                        break;
                    case 6:
                        MenuSolicitudes();
                        break;
                    case 7:
                        MenuConsultas();
                        break;
                    case 8:
                        MenuOrdenamientos();
                        break;
                    case 9:
                        MenuBusquedas();
                        break;
                    case 10:
                        MenuReportes();
                        break;
                    case 11:
                        GeneradorDatos.CargarDatosPrueba(gestor);
                        break;
                    case 0:
                        salir = true;
                        Console.WriteLine("Gracias por utilizar XelaTech Soporte.");
                        break;
                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine("Error: " + ex.Message);
            }

            if (!salir)
                Pausar();
        }
    }

    static string LeerTexto(string mensaje)
    {
        while (true)
        {
            Console.Write(mensaje);
            string texto = Console.ReadLine() ?? "";

            if (!string.IsNullOrWhiteSpace(texto))
                return texto.Trim();

            Console.WriteLine("Este campo no puede estar vacío.");
        }
    }

    static int LeerEntero(string mensaje)
    {
        while (true)
        {
            Console.Write(mensaje);

            if (int.TryParse(Console.ReadLine(), out int numero))
                return numero;

            Console.WriteLine("Ingrese un número entero válido.");
        }
    }

    static double LeerDecimal(string mensaje)
    {
        while (true)
        {
            Console.Write(mensaje);

            if (double.TryParse(Console.ReadLine(), out double numero) &&
                !double.IsNaN(numero) &&
                !double.IsInfinity(numero))
            {
                return numero;
            }

            Console.WriteLine("Ingrese un número válido.");
        }
    }

    static int LeerOpcion(string mensaje, int minimo, int maximo)
    {
        while (true)
        {
            int numero = LeerEntero(mensaje);

            if (numero >= minimo && numero <= maximo)
                return numero;

            Console.WriteLine(
                "Ingrese un número entre " + minimo + " y " + maximo + "."
            );
        }
    }

    static void Pausar()
    {
        Console.WriteLine();
        Console.WriteLine("Presione una tecla para continuar...");
        Console.ReadKey(true);
    }

    static void RegistrarCliente()
    {
        Console.WriteLine("=== REGISTRAR CLIENTE ===");

        string codigo = LeerTexto("Código: ");
        string nombre = LeerTexto("Nombre completo: ");
        string telefono = LeerTexto("Teléfono: ");
        string correo = LeerTexto("Correo electrónico: ");

        Console.WriteLine("1. Estudiante");
        Console.WriteLine("2. Profesional");
        Console.WriteLine("3. Negocio");
        Console.WriteLine("4. Institución");

        int opcion = LeerOpcion("Tipo de cliente: ", 1, 4);
        TipoCliente tipo = (TipoCliente)(opcion - 1);

        Cliente cliente = new Cliente(
            codigo, nombre, telefono, correo, tipo
        );

        gestor.RegistrarCliente(cliente);

        Console.WriteLine("Cliente registrado correctamente.");
    }

    static void RegistrarTecnico()
    {
        Console.WriteLine("=== REGISTRAR TÉCNICO ===");

        string codigo = LeerTexto("Código: ");
        string nombre = LeerTexto("Nombre completo: ");
        string telefono = LeerTexto("Teléfono: ");

        Console.WriteLine("1. Laptop");
        Console.WriteLine("2. Computadora de escritorio");
        Console.WriteLine("3. Impresora");

        int opcion = LeerOpcion("Especialidad: ", 1, 3);
        Especialidad especialidad = (Especialidad)(opcion - 1);

        Tecnico tecnico = new Tecnico(
            codigo, nombre, telefono, especialidad
        );

        gestor.RegistrarTecnico(tecnico);

        Console.WriteLine("Técnico registrado correctamente.");
    }

    static void RegistrarEquipo()
    {
        Console.WriteLine("=== REGISTRAR EQUIPO ===");

        string codigoCliente = LeerTexto("Código del propietario: ");
        Cliente? cliente = gestor.BuscarCliente(codigoCliente);

        if (cliente == null)
        {
            Console.WriteLine("El cliente no existe.");
            return;
        }

        Console.WriteLine("1. Laptop");
        Console.WriteLine("2. Computadora de escritorio");
        Console.WriteLine("3. Impresora");

        int tipo = LeerOpcion("Tipo de equipo: ", 1, 3);

        string codigo = LeerTexto("Código interno: ");
        string serie = LeerTexto("Número de serie: ");
        string marca = LeerTexto("Marca: ");
        string modelo = LeerTexto("Modelo: ");
        string estado = LeerTexto("Estado físico: ");
        string accesorios = LeerTexto("Accesorios entregados: ");

        Equipo equipo;

        switch (tipo)
        {
            case 1:
                equipo = new Laptop(
                    codigo, serie, marca, modelo,
                    cliente, estado, accesorios
                );
                break;

            case 2:
                equipo = new CompuEscritorio(
                    codigo, serie, marca, modelo,
                    cliente, estado, accesorios
                );
                break;

            default:
                equipo = new Impresora(
                    codigo, serie, marca, modelo,
                    cliente, estado, accesorios
                );
                break;
        }

        gestor.RegistrarEquipo(equipo);

        Console.WriteLine("Equipo registrado correctamente.");
    }

    static void RegistrarRepuesto()
    {
        Console.WriteLine("=== REGISTRAR REPUESTO ===");

        string codigo = LeerTexto("Código: ");
        string nombre = LeerTexto("Nombre: ");
        string categoria = LeerTexto("Categoría: ");

        int existencia = LeerEntero("Existencia actual: ");
        int minima = LeerEntero("Existencia mínima: ");

        double costo = LeerDecimal("Costo unitario: Q");
        double precio = LeerDecimal("Precio de venta: Q");

        Repuesto repuesto = new Repuesto(
            codigo, nombre, categoria,
            existencia, minima, costo, precio
        );

        gestor.RegistrarRepuesto(repuesto);

        Console.WriteLine("Repuesto registrado correctamente.");
    }

    static void RegistrarSolicitud()
    {
        Console.WriteLine("=== REGISTRAR SOLICITUD ===");

        string codigoCliente = LeerTexto("Código del cliente: ");
        Cliente? cliente = gestor.BuscarCliente(codigoCliente);

        if (cliente == null)
        {
            Console.WriteLine("Cliente no encontrado.");
            return;
        }

        string codigoEquipo = LeerTexto("Código del equipo: ");
        Equipo? equipo = gestor.BuscarEquipo(codigoEquipo);

        if (equipo == null)
        {
            Console.WriteLine("Equipo no encontrado.");
            return;
        }

        if (equipo.GetPropietario() != cliente)
        {
            Console.WriteLine("El equipo no pertenece al cliente indicado.");
            return;
        }

        string codigo = LeerTexto("Código de solicitud: ");
        string descripcion = LeerTexto("Descripción del problema: ");

        Console.WriteLine("1. Normal");
        Console.WriteLine("2. Urgente");

        int opcion = LeerOpcion("Prioridad: ", 1, 2);

        Prioridad prioridad = opcion == 2
            ? Prioridad.Urgente
            : Prioridad.Normal;

        Solicitud solicitud = new Solicitud(
            codigo, cliente, equipo, descripcion, prioridad
        );

        int horas = LeerEntero("Tiempo estimado en horas: ");
        solicitud.SetTiempoEstimado(horas);

        gestor.RegistrarSolicitud(solicitud);

        Console.WriteLine("Solicitud registrada y enviada a la cola.");
    }

    static void MenuSolicitudes()
    {
        bool volver = false;

        while (!volver)
        {
            Console.Clear();
            Console.WriteLine("=== GESTIÓN DE SOLICITUDES ===");
            Console.WriteLine("1. Mostrar colas");
            Console.WriteLine("2. Atender siguiente solicitud");
            Console.WriteLine("3. Iniciar diagnóstico");
            Console.WriteLine("4. Registrar diagnóstico");
            Console.WriteLine("5. Iniciar reparación");
            Console.WriteLine("6. Agregar repuesto");
            Console.WriteLine("7. Marcar lista para entregar");
            Console.WriteLine("8. Entregar solicitud");
            Console.WriteLine("9. Cancelar solicitud");
            Console.WriteLine("10. Ver historial");
            Console.WriteLine("11. Deshacer último cambio");
            Console.WriteLine("0. Volver");

            int opcion = LeerEntero("Opción: ");

            try
            {
                string codigo;

                switch (opcion)
                {
                    case 1:
                        gestor.MostrarColas();
                        break;

                    case 2:
                        Console.WriteLine("0. Lunes");
                        Console.WriteLine("1. Martes");
                        Console.WriteLine("2. Miércoles");
                        Console.WriteLine("3. Jueves");
                        Console.WriteLine("4. Viernes");

                        int dia = LeerOpcion("Día: ", 0, 4);
                        Solicitud asignada = gestor.AtenderSiguiente(dia);

                        Console.WriteLine(
                            "Solicitud asignada: " + asignada.GetCodigo()
                        );
                        Console.WriteLine(
                            "Técnico: " +
                            asignada.GetTecnicoAsignado()?.GetNombre()
                        );
                        break;

                    case 3:
                        codigo = LeerTexto("Código de solicitud: ");
                        gestor.IniciarDiagnostico(codigo);
                        Console.WriteLine("Diagnóstico iniciado.");
                        break;

                    case 4:
                        codigo = LeerTexto("Código de solicitud: ");

                        string diagnostico = LeerTexto("Diagnóstico: ");
                        string acciones = LeerTexto("Acciones realizadas: ");
                        double costo = LeerDecimal("Mano de obra: Q");

                        gestor.RegistrarDiagnostico(
                            codigo, diagnostico, acciones, costo
                        );

                        Console.WriteLine("Diagnóstico registrado.");
                        break;

                    case 5:
                        codigo = LeerTexto("Código de solicitud: ");
                        gestor.IniciarReparacion(codigo);
                        Console.WriteLine("Reparación iniciada.");
                        break;

                    case 6:
                        codigo = LeerTexto("Código de solicitud: ");

                        string repuesto = LeerTexto("Código del repuesto: ");
                        int cantidad = LeerEntero("Cantidad: ");

                        gestor.AgregarRepuestoSolicitud(
                            codigo, repuesto, cantidad
                        );

                        Console.WriteLine("Repuesto agregado.");
                        break;

                    case 7:
                        codigo = LeerTexto("Código de solicitud: ");
                        gestor.MarcarListaParaEntregar(codigo);
                        Console.WriteLine("Solicitud lista para entregar.");
                        break;

                    case 8:
                        codigo = LeerTexto("Código de solicitud: ");

                        double total = gestor.EntregarSolicitud(codigo);

                        Console.WriteLine("Solicitud entregada.");
                        Console.WriteLine("Total: Q" + total.ToString("0.00"));
                        break;

                    case 9:
                        codigo = LeerTexto("Código de solicitud: ");
                        gestor.CancelarSolicitud(codigo);
                        Console.WriteLine("Solicitud cancelada.");
                        break;

                    case 10:
                        codigo = LeerTexto("Código de solicitud: ");
                        Solicitud? historial = gestor.BuscarSolicitud(codigo);

                        if (historial == null)
                            Console.WriteLine("Solicitud no encontrada.");
                        else
                            historial.MostrarHistorial();
                        break;

                    case 11:
                        codigo = LeerTexto("Código de solicitud: ");
                        Solicitud? solicitud = gestor.BuscarSolicitud(codigo);

                        if (solicitud == null)
                        {
                            Console.WriteLine("Solicitud no encontrada.");
                        }
                        else if (
                            solicitud.GetEstado() == EstadoSolicitud.Asignada ||
                            solicitud.GetEstado() == EstadoSolicitud.EnEspera)
                        {
                            Console.WriteLine(
                                "No se permite deshacer desde Asignada " +
                                "o EnEspera para proteger las colas."
                            );
                        }
                        else
                        {
                            solicitud.DeshacerUltimoCambio();
                            Console.WriteLine("Último cambio deshecho.");
                        }
                        break;

                    case 0:
                        volver = true;
                        break;

                    default:
                        Console.WriteLine("Opción no válida.");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            if (!volver)
                Pausar();
        }
    }

    static void MenuConsultas()
    {
        Console.Clear();
        Console.WriteLine("=== CONSULTAR REGISTROS ===");
        Console.WriteLine("1. Clientes");
        Console.WriteLine("2. Técnicos");
        Console.WriteLine("3. Equipos");
        Console.WriteLine("4. Repuestos");
        Console.WriteLine("5. Solicitudes");

        int opcion = LeerOpcion("Opción: ", 1, 5);

        switch (opcion)
        {
            case 1:
                foreach (Cliente cliente in gestor.GetClientes())
                {
                    cliente.MostrarInformacion();
                    Console.WriteLine("----------------------");
                }
                break;

            case 2:
                foreach (Tecnico tecnico in gestor.GetTecnicos())
                {
                    tecnico.MostrarInformacion();
                    Console.WriteLine("----------------------");
                }
                break;

            case 3:
                foreach (Equipo equipo in gestor.GetEquipos())
                {
                    equipo.MostrarInformacion();
                    Console.WriteLine("----------------------");
                }
                break;

            case 4:
                foreach (Repuesto repuesto in gestor.GetRepuestos())
                {
                    repuesto.MostrarInformacion();
                    Console.WriteLine("----------------------");
                }
                break;

            case 5:
                foreach (Solicitud solicitud in gestor.GetSolicitudes())
                {
                    solicitud.MostrarInformacion();
                    Console.WriteLine("----------------------");
                }
                break;
        }
    }

    static void MenuOrdenamientos()
    {
        Console.Clear();
        Console.WriteLine("=== ORDENAMIENTOS ===");
        Console.WriteLine("1. Burbuja - Solicitudes por costo");
        Console.WriteLine("2. Selección - Técnicos por servicios");
        Console.WriteLine("3. Selección - Repuestos por existencia");
        Console.WriteLine("4. Burbuja - Solicitudes por fecha");

        int opcion = LeerOpcion("Opción: ", 1, 4);

        switch (opcion)
        {
            case 1:
                Ordenamientos.MostrarSolicitudesPorCosto(
                    gestor.GetSolicitudes()
                );
                break;

            case 2:
                Ordenamientos.MostrarTecnicosPorServicios(
                    gestor.GetTecnicos()
                );
                break;

            case 3:
                Ordenamientos.MostrarRepuestosPorExistencia(
                    gestor.GetRepuestos()
                );
                break;

            case 4:
                Ordenamientos.MostrarSolicitudesPorFecha(
                    gestor.GetSolicitudes()
                );
                break;
        }
    }

    static void MenuBusquedas()
    {
        Console.Clear();
        Console.WriteLine("=== BÚSQUEDAS ===");
        Console.WriteLine("1. Clientes por nombre");
        Console.WriteLine("2. Solicitudes por estado");
        Console.WriteLine("3. Equipos por marca");
        Console.WriteLine("4. Solicitud por código - Binaria");

        int opcion = LeerOpcion("Opción: ", 1, 4);

        switch (opcion)
        {
            case 1:
                Busquedas.MostrarClientesPorNombre(
                    gestor.GetClientes(),
                    LeerTexto("Nombre: ")
                );
                break;

            case 2:
                MostrarEstados();

                int estado = LeerOpcion("Estado: ", 0, 7);

                Busquedas.MostrarSolicitudesPorEstado(
                    gestor.GetSolicitudes(),
                    (EstadoSolicitud)estado
                );
                break;

            case 3:
                Busquedas.MostrarEquiposPorMarca(
                    gestor.GetEquipos(),
                    LeerTexto("Marca: ")
                );
                break;

            case 4:
                Busquedas.MostrarBusquedaBinaria(
                    gestor.GetSolicitudes(),
                    LeerTexto("Código: ")
                );
                break;
        }
    }

    static void MostrarEstados()
    {
        foreach (EstadoSolicitud estado in Enum.GetValues<EstadoSolicitud>())
        {
            Console.WriteLine((int)estado + ". " + estado);
        }
    }

    static void MenuReportes()
    {
        bool volver = false;

        while (!volver)
        {
            Console.Clear();
            Reportes.MostrarMenuReportes();
            Console.WriteLine("0. Volver");

            int opcion = LeerOpcion("Seleccione un reporte: ", 0, 12);

            try
            {
                switch (opcion)
                {
                    case 1:
                        Reportes.PendientesPorPrioridad(gestor);
                        break;

                    case 2:
                        Reportes.SolicitudesPorEstado(gestor);
                        break;

                    case 3:
                        Reportes.DisponibilidadTecnicos(gestor);
                        break;

                    case 4:
                        Reportes.TecnicoConMasServicios(gestor);
                        break;

                    case 5:
                        Reportes.EquiposPorTipo(gestor);
                        break;

                    case 6:
                        Reportes.SolicitudesPorCosto(gestor);
                        break;

                    case 7:
                        Reportes.SolicitudesPorFecha(gestor);
                        break;

                    case 8:
                        Reportes.RepuestosPorExistencia(gestor);
                        break;

                    case 9:
                        Reportes.AlertasDeInventario(gestor);
                        break;

                    case 10:
                        Reportes.IngresosPorServicios(gestor);
                        break;

                    case 11:
                        Console.WriteLine("1. Clientes por nombre");
                        Console.WriteLine("2. Solicitudes por estado");
                        Console.WriteLine("3. Equipos por marca");
                        Console.WriteLine("4. Solicitud por código");

                        int tipo = LeerOpcion("Tipo: ", 1, 4);

                        if (tipo == 2)
                            MostrarEstados();

                        string criterio = LeerTexto("Criterio: ");

                        Reportes.ResultadosDeBusqueda(
                            gestor, tipo, criterio
                        );
                        break;

                    case 12:
                        Reportes.MatrizCargaSemanal(gestor);
                        break;

                    case 0:
                        volver = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            if (!volver)
                Pausar();
        }
    }
}