
using System;
using System.Collections.Generic;

public static class GeneradorDatos
{
    public static void CargarDatosPrueba(GestorXelaTech gestor)
    {
        if (gestor == null)
            throw new ArgumentNullException(nameof(gestor));

        if (gestor.GetClientes().Count > 0 ||
            gestor.GetTecnicos().Count > 0 ||
            gestor.GetEquipos().Count > 0 ||
            gestor.GetRepuestos().Count > 0 ||
            gestor.GetSolicitudes().Count > 0)
        {
            throw new InvalidOperationException(
                "El gestor debe estar vacío para cargar los datos de prueba."
            );
        }

        Random aleatorio = new Random();

        string[] nombres =
        {
            "Carlos López",
            "María Pérez",
            "José Ramírez",
            "Ana Morales",
            "Luis García",
            "Sofía Hernández",
            "Pedro Castillo",
            "Elena Méndez",
            "Diego Vásquez"
        };

        TipoCliente[] tiposClientes =
        {
            TipoCliente.Estudiante,
            TipoCliente.Profesional,
            TipoCliente.Negocio,
            TipoCliente.Institucion
        };

        List<Cliente> clientes = new List<Cliente>();

        for (int i = 0; i < nombres.Length; i++)
        {
            TipoCliente tipo = tiposClientes[
                aleatorio.Next(tiposClientes.Length)
            ];

            Cliente cliente = new Cliente(
                "CLI" + (i + 1).ToString("000"),
                nombres[i],
                "5555" + (1000 + i),
                "cliente" + (i + 1) + "@correo.com",
                tipo
            );

            gestor.RegistrarCliente(cliente);
            clientes.Add(cliente);
        }

        Tecnico[] tecnicos =
        {
            new Tecnico(
                "TEC001", "Mario Díaz", "55110001",
                Especialidad.Laptop
            ),

            new Tecnico(
                "TEC002", "Andrea Ruiz", "55110002",
                Especialidad.Laptop
            ),

            new Tecnico(
                "TEC003", "Roberto León", "55110003",
                Especialidad.Escritorio
            ),

            new Tecnico(
                "TEC004", "Lucía Torres", "55110004",
                Especialidad.Escritorio
            ),

            new Tecnico(
                "TEC005", "Fernando Soto", "55110005",
                Especialidad.Impresora
            ),

            new Tecnico(
                "TEC006", "Valeria Cruz", "55110006",
                Especialidad.Impresora
            )
        };

        foreach (Tecnico tecnico in tecnicos)
        {
            gestor.RegistrarTecnico(tecnico);
        }

        Equipo[] equipos =
        {
            new Laptop(
                "EQ001", "SER001", "HP", "Pavilion",
                clientes[0], "Buen estado", "Cargador"
            ),

            new Laptop(
                "EQ002", "SER002", "Dell", "Inspiron",
                clientes[1], "Uso normal", "Cargador"
            ),

            new CompuEscritorio(
                "EQ003", "SER003", "Lenovo", "ThinkCentre",
                clientes[2], "Buen estado", "Cable de poder"
            ),

            new Impresora(
                "EQ004", "SER004", "Epson", "L3250",
                clientes[3], "Uso normal", "Cable USB"
            ),

            new Laptop(
                "EQ005", "SER005", "Asus", "VivoBook",
                clientes[4], "Buen estado", "Cargador"
            ),

            new CompuEscritorio(
                "EQ006", "SER006", "HP", "ProDesk",
                clientes[5], "Uso normal", "Cable de poder"
            ),

            new Impresora(
                "EQ007", "SER007", "Canon", "G3110",
                clientes[6], "Buen estado", "Cable USB"
            ),

            new Laptop(
                "EQ008", "SER008", "Acer", "Aspire",
                clientes[7], "Uso normal", "Cargador"
            ),

            new CompuEscritorio(
                "EQ009", "SER009", "Dell", "OptiPlex",
                clientes[8], "Buen estado", "Cable de poder"
            )
        };

        foreach (Equipo equipo in equipos)
        {
            gestor.RegistrarEquipo(equipo);
        }

        Repuesto[] repuestos =
        {
            new Repuesto(
                "REP001", "Memoria RAM 8GB", "Memoria",
                12, 3, 120, 180
            ),

            new Repuesto(
                "REP002", "Disco SSD 240GB", "Almacenamiento",
                8, 2, 150, 230
            ),

            new Repuesto(
                "REP003", "Teclado de laptop", "Periféricos",
                5, 2, 80, 130
            ),

            new Repuesto(
                "REP004", "Fuente de poder", "Energía",
                4, 2, 170, 250
            ),

            new Repuesto(
                "REP005", "Cable USB", "Accesorios",
                15, 4, 15, 30
            ),

            new Repuesto(
                "REP006", "Cartucho de tinta", "Impresión",
                2, 3, 55, 90
            ),

            new Repuesto(
                "REP007", "Ventilador", "Refrigeración",
                0, 2, 45, 85
            ),

            new Repuesto(
                "REP008", "Pasta térmica", "Mantenimiento",
                10, 3, 20, 40
            )
        };

        foreach (Repuesto repuesto in repuestos)
        {
            gestor.RegistrarRepuesto(repuesto);
        }

        string[] problemas =
        {
            "La laptop no enciende.",
            "La laptop funciona lentamente.",
            "La computadora se reinicia.",
            "La impresora no imprime.",
            "La laptop se calienta demasiado.",
            "La computadora presenta fallas de energía.",
            "La impresora presenta errores.",
            "La laptop tiene problemas de teclado.",
            "La computadora funciona lentamente."
        };

        for (int i = 0; i < equipos.Length; i++)
        {
            Prioridad prioridad =
                i < 3 ? Prioridad.Urgente : Prioridad.Normal;

            Solicitud solicitud = new Solicitud(
                "SOL" + (i + 1).ToString("000"),
                clientes[i],
                equipos[i],
                problemas[i],
                prioridad
            );

            solicitud.SetTiempoEstimado(
                aleatorio.Next(2, 9)
            );

            gestor.RegistrarSolicitud(solicitud);
        }

        for (int i = 0; i < 3; i++)
        {
            Solicitud solicitud = gestor.AtenderSiguiente(i);

            gestor.IniciarDiagnostico(
                solicitud.GetCodigo()
            );

            gestor.RegistrarDiagnostico(
                solicitud.GetCodigo(),
                "Falla detectada durante la revisión técnica.",
                "Revisión y mantenimiento del equipo.",
                aleatorio.Next(100, 301)
            );

            gestor.IniciarReparacion(
                solicitud.GetCodigo()
            );

            if (i == 0)
            {
                gestor.AgregarRepuestoSolicitud(
                    solicitud.GetCodigo(),
                    "REP001",
                    1
                );
            }
            else if (i == 1)
            {
                gestor.AgregarRepuestoSolicitud(
                    solicitud.GetCodigo(),
                    "REP002",
                    1
                );
            }

            gestor.MarcarListaParaEntregar(
                solicitud.GetCodigo()
            );

            gestor.EntregarSolicitud(
                solicitud.GetCodigo()
            );
        }

        Solicitud solicitudNormal =
            gestor.AtenderSiguiente(3);

        gestor.IniciarDiagnostico(
            solicitudNormal.GetCodigo()
        );

        gestor.RegistrarDiagnostico(
            solicitudNormal.GetCodigo(),
            "Problema identificado en el equipo.",
            "Limpieza y ajuste de componentes.",
            aleatorio.Next(80, 201)
        );

        gestor.IniciarReparacion(
            solicitudNormal.GetCodigo()
        );

        Solicitud otraSolicitud =
            gestor.AtenderSiguiente(4);

        gestor.IniciarDiagnostico(
            otraSolicitud.GetCodigo()
        );

        gestor.RegistrarDiagnostico(
            otraSolicitud.GetCodigo(),
            "Revisión inicial completada.",
            "Verificación de componentes.",
            aleatorio.Next(80, 201)
        );

        gestor.CancelarSolicitud("SOL009");

        Console.WriteLine("=== DATOS DE PRUEBA CARGADOS ===");
        Console.WriteLine("Clientes: " + gestor.GetClientes().Count);
        Console.WriteLine("Técnicos: " + gestor.GetTecnicos().Count);
        Console.WriteLine("Equipos: " + gestor.GetEquipos().Count);
        Console.WriteLine("Repuestos: " + gestor.GetRepuestos().Count);
        Console.WriteLine("Solicitudes: " + gestor.GetSolicitudes().Count);
        Console.WriteLine("Datos generados correctamente.");
    }
}
