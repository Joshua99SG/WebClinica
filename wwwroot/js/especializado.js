/*--------------------Asigna Roles--------------------*/
function AsignarRol(urlAgregar, urlEliminar) {
    var TipoUsuarioId = document.getElementById("UserType").value;
    var BotonHabilitado = 1;
    var frmAgregar = new FormData;
    var frmEliminar = new FormData;
    frmAgregar.append("TipoUsuarioId", TipoUsuarioId);
    frmEliminar.append("TipoUsuarioId", TipoUsuarioId);
    frmAgregar.append("BotonHabilitado", BotonHabilitado);
    var checks = document.getElementsByClassName("checkbox");
    var nchecks = checks.length
    for (var i = 0; i < nchecks; i++) {
        if (checks[i].checked == true) {
            frmAgregar.append("_PaginasAgregadas[]", checks[i].id.replace("/", ""));
        } else {
            frmEliminar.append("_PaginasEliminadas[]", checks[i].id.replace("/", ""));
        }
    }
    $.ajax({
        type: "POST",
        url: urlAgregar,
        data: frmAgregar,
        contentType: false,
        processData: false,
        success: function (data) {
            if (data == "OK") {
                correcto("Se actualizó correctamente el rol segun tipo de usuario!");
                document.getElementById("frmAgregar").submit();
                document.getElementById("frmRegresar").submit();
            }
            else {
                error("Ocurrió un error, por favor verifique!");
            }
        }
        //},
        //error: alert("No se pudo procesar el registro")
    })
    $.ajax({
        type: "POST",
        url: urlEliminar,
        data: frmEliminar,
        contentType: false,
        processData: false,
        success: function (data) {
            if (data == "OK") {
                correcto("Se actualizó correctamente el rol segun tipo de usuario!");
                document.getElementById("frmEliminar").submit();
                document.getElementById("frmRegresar").submit();
            }
            else {
                error("Ocurrió un error, por favor verifique!");
            }
        }
    })
}




function ListarBotones() {
    $.get("PaginaTipoUsuario/listarBotones", function (data) {
        var contenido = "<table class='table'>";
        contenido += "<thead>";
        contenido += "<tr>";
        contenido += "<td></td>";
        contenido += "<td>Nombre Boton</td>";
        contenido += "</tr>";
        contenido += "</thead>";
        contenido += "<tbody>";
        for (var i = 0; i < data.length; i++) {
            contenido += "<tr>";
            contenido += "<td> <input type='checkbox' class='checkbox' id='chk" + data[i].iidboton + "' /> </td>";
            contenido += "<td>" + data[i].nombre + "</td>";
            contenido += "</tr>";
        }
        contenido += "</tbody>";
        contenido += "<table>";

        document.getElementById("divBotones").innerHTML = contenido;
        recuperar();
    })
}

function ListarPaginas() {
    $.get("/AsignaRol/CargarPaginas/", function (data) {
        var contenido = "<table class='table'>";
        contenido += "<thead>";
        contenido += "<tr>";
        contenido += "<th>PaginaId</th>";
        contenido += "<th>Controlador</th>";
        contenido += "<th></th>";
        contenido += "<th></th>";
        contenido += "</tr>";
        contenido += "</thead>";
        contenido += "<tbody>";
        for (var i = 0; i < data.length; i++) {
            contenido += "<tr>";
            contenido += "<td> <input type='checkbox' class='checkbox' id='"
                + data[i].paginaid + "' /> </td>";
            contenido += "</tr>";
        }
        contenido += "</tbody>";
        contenido += "<table>";
        document.getElementById("divPaginas").innerHTML = contenido;
        recuperar();
    })
}

function recuperar(tipoUsuarioid) {

    $.get("/AsignaRol/RecuperarPaginas/?tipoUsuarioId=" + tipoUsuarioid, function (data) {
        for (var i = 0; i < data.length; i++) {
            var pagid = data[i].paginaId;
            var cajita = document.getElementById(pagid);
            cajita.checked = true;
        }
    });
}
/*--------------------Asigna Roles--------------------*/
/*--------------------Login-SingIn--------------------*/
(function ($) {
    "use strict";
    /*==================================================================
    [ Show pass ]*/
    var showPass = 0;
    $('.btn-show-pass').on('click', function () {
        if (showPass == 0) {
            $(this).next('input').attr('type', 'text');
            $(this).find('i').removeClass('fa-eye');
            $(this).find('i').addClass('fa-eye-slash');
            showPass = 1;
        }
        else {
            $(this).next('input').attr('type', 'password');
            $(this).find('i').removeClass('fa-eye-slash');
            $(this).find('i').addClass('fa-eye');
            showPass = 0;
        }

    });


})(jQuery);
/*--------------------Login-SingIn--------------------*/
/*--------------------LOGIN-SingIn--------------------*/

function Login() {
    var user = document.getElementById("name").value;
    var pass = document.getElementById("password").value;
    if (user == "" && pass == "") {
        error("Debe llenar ambos campos")
        error
    } else {
        $.ajax({
            type: "GET",
            url: "/Login/_Login",
            data: { "user": user, "pass": pass },
            success: function (data) {
                if (data == "") {
                    error("Usuario o contraseña incorrecto!");
                } else {
                    correcto("Bienvenido!")
                    document.location.href = "/Home/Index"
                }
            },
            error: "Esta mierda no sirve"
        })
    }
};

function KeyUpEnterLogin() {
    var input = document.getElementById("password");

    input.addEventListener("keyup", function (event) {
        if (event.keyCode === 13) {
            event.preventDefault();

            document.getElementById("Ingresar").click();
        }
    });
};

function AbrirSingIn() {
    document.location.href = "/SingIn/Index";
}

function SingIn() {
    var IdUser = document.getElementById("Cedula").value;
    var UserName = document.getElementById("name").value;
    var Password = document.getElementById("password").value;
    var Usertype = 5;
    $.ajax({
        type: "GET",
        url: "/SingIn/_SingIn",
        data: { "UserId": IdUser, "TipoUsuario": Usertype, "NombreUsuario": UserName, "Password": Password },
        success: function (data) {
            if (data == "Ya existe el usuario") {
                error("Ya existe un usuario registrado con ese ID");
            } else {
                correcto("El usuario ha sido creado satisfactoriamente");
                document.location.href = "/Login/Index"
            }
        },
        error: "Ah sucedido un error, intenta de nuevo"
    })
};
/*--------------------LOGIN-SingIn--------------------*/
/*--------------------MODALES CREAR--------------------*/

function abrirModalCrearEnfermedad() {
    verModal('Agregar enfermedad', '¿Desea guardar la enfermedad?').then((result) => {
        if (result.value) {
            var viewAgregar = document.getElementById("viewAgregar");
            viewAgregar.submit();
            Swal.fire(
                'Agregado!',
                'La enfermedad fue agregado!.',
                'success'
            )
        }
        else if (result.dismiss === Swal.DismissReason.cancel) {
            Swal.fire(
                'Cancelado',
                'La enfermedad no fue agregada!!!:)',
                'error'
            )
        }
    })
}
function abrirModalCrearEspecialidad() {
    verModal('Agregar especialidad', '¿Desea guardar la especialidad?').then((result) => {
        if (result.value) {
            var viewAgregar = document.getElementById("viewAgregar");
            viewAgregar.submit();
            Swal.fire(
                'Agregado!',
                'La especialidad fue agregada!.',
                'success'
            )
        }
        else if (result.dismiss === Swal.DismissReason.cancel) {
            Swal.fire(
                'Cancelado',
                'La especialidad no fue agregada!!!:)',
                'error'
            )
        }
    })
}
function abrirModalCrearMedico() {
    verModal('Agregar medico', '¿Desea guardar al medico?').then((result) => {
        if (result.value) {
            var viewAgregarMedico = document.getElementById("viewAgregarMedico");
            viewAgregarMedico.submit();
            Swal.fire(
                'Agregado!',
                'El medico fue agregado!.',
                'success'
            )
        }
        else if (result.dismiss === Swal.DismissReason.cancel) {
            Swal.fire(
                'Cancelado',
                'El medico no fue agregado!!!:)',
                'error'
            )
        }
    })
}
function abrirModalCrearPaciente() {
    verModal('Agregar paciente', '¿Desea guardar al paciente?').then((result) => {
        if (result.value) {
            var viewAgregar = document.getElementById("viewAgregar");
            viewAgregar.submit();
            Swal.fire(
                'Agregado!',
                'El paciente fue agregado!.',
                'success'
            )
        }
        else if (result.dismiss === Swal.DismissReason.cancel) {
            Swal.fire(
                'Cancelado',
                'El paciente no fue agregad!!!:)',
                'error'
            )
        }
    })
}
function abrirModalCrearUsuario() {
    verModal('Agregar usuario', '¿Desea guardar al usuario?').then((result) => {
        if (result.value) {
            var viewAgregarUsuario = document.getElementById("viewAgregarUsuario");
            viewAgregarUsuario.submit();
            Swal.fire(
                'Agregado!',
                'El usuario fue agregado!.',
                'success'
            )
        }
        else if (result.dismiss === Swal.DismissReason.cancel) {
            Swal.fire(
                'Cancelado',
                'El usuario no fue agregado!!!:)',
                'error'
            )
        }
    })
}

function abrirModalCrearCita() {
    verModal('Agregar cita', '¿Desea guardar la cita?').then((result) => {
        if (result.value) {
            var viewAgregarCita = document.getElementById("viewAgregarCita");
            viewAgregarCita.submit();
            Swal.fire(
                'Agregado!',
                'La cita fue agregado!.',
                'success'
            )
        }
        else if (result.dismiss === Swal.DismissReason.cancel) {
            Swal.fire(
                'Cancelado',
                'La cita no fue agregada!!!:)',
                'error'
            )
        }
    })
}
function abrirModalCrearTipoUsuario() {
    verModal('Agregar tipo usuario', '¿Desea guardar el tipo de Usuario?').then((result) => {
        if (result.value) {
            var viewAgregarTipoUsuario = document.getElementById("viewAgregarTipoUsuario");
            viewAgregarTipoUsuario.submit();
            Swal.fire(
                'Agregado!',
                'El tipo de usuario fue agregado!.',
                'success'
            )
        }
        else if (result.dismiss === Swal.DismissReason.cancel) {
            Swal.fire(
                'Cancelado',
                'El tipo usuario no fue agregado!!!:)',
                'error'
            )
        }
    })
}

function abrirModalCrearPagina() {
    verModal('Agregar página ', '¿Desea guardar la página ?').then((result) => {
        if (result.value) {
            var viewAgregarPagina = document.getElementById("viewAgregarPagina");
            viewAgregarPagina.submit();
            Swal.fire(
                'Agregado!',
                'La página fue agregada!.',
                'success'
            )
        }
        else if (result.dismiss === Swal.DismissReason.cancel) {
            Swal.fire(
                'Cancelado',
                'La página no fue agregada!!!:)',
                'error'
            )
        }
    })
}

/*------------------------------------------------------*/
/*--------------------MODALES EDITAR--------------------*/

function abrirModalEditarCita() {
    verModal('Modificar cita', '¿Desea modificar la cita?').then((result) => {
        if (result.value) {
            var viewEditarCita = document.getElementById("viewEditarCita");
            viewEditarCita.submit();
            Swal.fire(
                'Modificado!',
                'La cita fue modificado!.',
                'success'
            )
        }
        else if (result.dismiss === Swal.DismissReason.cancel) {
            Swal.fire(
                'Cancelado',
                'La cita no fue modificado!!!:)',
                'error'
            )
        }
    })
}
function abrirModalEditarEnfermedad() {
    verModal('Modificar enfermedad', '¿Desea modificar la enfermedad?').then((result) => {
        if (result.value) {
            var viewEditarEnfermedad = document.getElementById("viewEditarEnfermedad");
            viewEditarEnfermedad.submit();
            Swal.fire(
                'Modificada!',
                'La enfermedad fue modificada!.',
                'success'
            )
        }
        else if (result.dismiss === Swal.DismissReason.cancel) {
            Swal.fire(
                'Cancelado',
                'La enfermedad no fue modificada!!!:)',
                'error'
            )
        }
    })
}
function abrirModalEditarEspecialidad() {
    verModal('Modificar especialidad', '¿Desea modificar la especialidad?').then((result) => {
        if (result.value) {
            var viewEditarEspecialidad = document.getElementById("viewEditarEspecialidad");
            viewEditarEspecialidad.submit();
            Swal.fire(
                'Modificado!',
                'La especialidad fue modificada!.',
                'success'
            )
        }
        else if (result.dismiss === Swal.DismissReason.cancel) {
            Swal.fire(
                'Cancelado',
                'La especialidad no fue modificada!!!:)',
                'error'
            )
        }
    })
}
function abrirModalEditarMedico() {
    verModal('Modificar medico', '¿Desea modificar el medico?').then((result) => {
        if (result.value) {
            var viewEditarMedico = document.getElementById("viewEditarMedico");
            viewEditarMedico.submit();
            Swal.fire(
                'Modificado!',
                'El medico fue modificado!.',
                'success'
            )
        }
        else if (result.dismiss === Swal.DismissReason.cancel) {
            Swal.fire(
                'Cancelado',
                'El medico no fue modificado!!!:)',
                'error'
            )
        }
    })
}
function abrirModalEditarPaciente() {
    verModal('Modificar paciente', '¿Desea modificar el paciente?').then((result) => {
        if (result.value) {
            var viewEditarPaciente = document.getElementById("viewEditarPaciente");
            viewEditarPaciente.submit();
            Swal.fire(
                'Modificado!',
                'El paciente fue modificado!.',
                'success'
            )
        }
        else if (result.dismiss === Swal.DismissReason.cancel) {
            Swal.fire(
                'Cancelado',
                'El paciente no fue modificado!!!:)',
                'error'
            )
        }
    })
}
function abrirModalEditarUsuario() {
    verModal('Modificar usuario', '¿Desea modificar el usuario?').then((result) => {
        if (result.value) {
            var viewEditarUsuario = document.getElementById("viewEditarUsuario");
            viewEditarUsuario.submit();
            Swal.fire(
                'Modificado!',
                'El usuario fue modificado!.',
                'success'
            )
        }
        else if (result.dismiss === Swal.DismissReason.cancel) {
            Swal.fire(
                'Cancelado',
                'El usuario no fue modificado!!!:)',
                'error'
            )
        }
    })
}
function abrirModalEditarTipoUusuario() {
    verModal('Modificar tipo Usuario', '¿Desea modificar el tipo Usuario?').then((result) => {
        if (result.value) {
            var viewEditarTipoUsuario = document.getElementById("viewEditarTipoUsuario");
            viewEditarTipoUsuario.submit();
            Swal.fire(
                'Modificado!',
                'El tipo usuario fue modificado!.',
                'success'
            )
        }
        else if (result.dismiss === Swal.DismissReason.cancel) {
            Swal.fire(
                'Cancelado',
                'El tipo usuario no fue modificado!!!:)',
                'error'
            )
        }
    })
}

function abrirModalEditarPagina() {
    verModal('Modificar página', '¿Desea modificar el página?').then((result) => {
        if (result.value) {
            var viewEditarPagina = document.getElementById("viewEditarPagina");
            viewEditarPagina.submit();
            Swal.fire(
                'Modificado!',
                'La página fue modificada!.',
                'success'
            )
        }
        else if (result.dismiss === Swal.DismissReason.cancel) {
            Swal.fire(
                'Cancelado',
                'La página no fue modificada!!!:)',
                'error'
            )
        }
    })
}

/*--------------------MODALES EDITAR-----------------------*/
/*--------------------METODOS ELIMINAR--------------------*/
function EliminarCita(CitaId) {
    document.getElementById("txtCitaId").value = CitaId;
    verModal('Eliminar cita',
        '¿Desea eliminar la cita con el código '
        + CitaId + '?').then((result) => {
            if (result.value) {
                var viewEliminarCita = document.getElementById("viewEliminarCita");
                viewEliminarCita.submit();
                Swal.fire(
                    'Eliminación!',
                    'La cita' + CitaId + 'fue eliminada!.',
                    'success'
                )
            }
            else if (result.dismiss === Swal.DismissReason.cancel) {
                Swal.fire(
                    'Cancelado',
                    'La cita no fue eliminada!!!:)',
                    'error'
                )
            }
        })
}

function EliminarEnfermedad(EnfermedadId) {
    document.getElementById("txtEnfermedadId").value = EnfermedadId;
    verModal('Eliminar enfermedad',
        '¿Desea eliminar la enfermedad con el código '
        + EnfermedadId + '?').then((result) => {
            if (result.value) {
                var viewEliminarEnfermedad = document.getElementById("viewEliminarEnfermedad");
                viewEliminarEnfermedad.submit();
                Swal.fire(
                    'Eliminación!',
                    'La enfermedad' + EnfermedadId + 'fue eliminada!.',
                    'success'
                )
            }
            else if (result.dismiss === Swal.DismissReason.cancel) {
                Swal.fire(
                    'Cancelado',
                    'La enfermedad no fue eliminada!!!:)',
                    'error'
                )
            }
        })
}
function EliminarEspecialidad(EspecialidadId) {
    document.getElementById("txtEspecialidadId").value = EspecialidadId;
    verModal('Eliminar especialidad',
        '¿Desea eliminar la especialidad código '
        + EspecialidadId + '?').then((result) => {
            if (result.value) {
                var viewEliminarEspecialidad = document.getElementById("viewEliminarEspecialidad");
                viewEliminarEspecialidad.submit();
                Swal.fire(
                    'Eliminación!',
                    'La especialidad' + EspecialidadId + 'fue eliminada!.',
                    'success'
                )
            }
            else if (result.dismiss === Swal.DismissReason.cancel) {
                Swal.fire(
                    'Cancelado',
                    'La especialidad no fue eliminada!!!:)',
                    'error'
                )
            }
        })
}
function EliminarMedico(MedicoId) {
    document.getElementById("txtMedicoId").value = MedicoId;
    verModal('Eliminar medico',
        '¿Desea eliminar el medico de código  '
        + ' ' + MedicoId + ' ?').then((result) => {
            if (result.value) {
                var viewEliminarMedico = document.getElementById("viewEliminarMedico");
                viewEliminarMedico.submit();
                Swal.fire(
                    'Eliminación!',
                    'El medico' + MedicoId + 'fue eliminado!.',
                    'success'
                )
            }
            else if (result.dismiss === Swal.DismissReason.cancel) {
                Swal.fire(
                    'Cancelado',
                    'El medico no fue eliminado!!!:)',
                    'error'
                )
            }
        })
}
function EliminarPaciente(PacienteId) {
    document.getElementById("txtPacienteId").value = PacienteId;
    verModal('Eliminar paciente',
        '¿Desea eliminar el paciente código '
        + PacienteId + '?').then((result) => {
            if (result.value) {
                var viewEliminarPaciente = document.getElementById("viewEliminarPaciente");
                viewEliminarPaciente.submit();
                Swal.fire(
                    'Eliminación!',
                    'El paciente' + PacienteId + 'fue eliminado!.',
                    'success'
                )
            }
            else if (result.dismiss === Swal.DismissReason.cancel) {
                Swal.fire(
                    'Cancelado',
                    'El paciente no fue eliminada!!!:)',
                    'error'
                )
            }
        })
}
function EliminarUsuario(UsuarioId) {
    document.getElementById("txtUsuarioId").value = UsuarioId;
    verModal('Eliminar paciente',
        '¿Desea eliminar el usuario código '
        + UsuarioId + '?').then((result) => {
            if (result.value) {
                var viewEliminarUsuario = document.getElementById("viewEliminarUsuario");
                viewEliminarUsuario.submit();
                Swal.fire(
                    'Eliminación!',
                    'El usuario' + UsuarioId + 'fue eliminado!.',
                    'success'
                )
            }
            else if (result.dismiss === Swal.DismissReason.cancel) {
                Swal.fire(
                    'Cancelado',
                    'El usuario no fue eliminada!!!:)',
                    'error'
                )
            }
        })
}
function EliminarCita(CitaId) {
    document.getElementById("txtCitaId").value = CitaId;
    verModal('Eliminar cita',
        '¿Desea eliminar la cita con el código '
        + CitaId + '?').then((result) => {
            if (result.value) {
                var viewEliminarCita = document.getElementById("viewEliminarCita");
                viewEliminarCita.submit();
                Swal.fire(
                    'Eliminación!',
                    'La cita ' + CitaId + ' fue eliminada!.',
                    'success'
                )
            }
            else if (result.dismiss === Swal.DismissReason.cancel) {
                Swal.fire(
                    'Cancelado',
                    'La cita no fue eliminada!!!:)',
                    'error'
                )
            }
        })
}
function EliminarTipoUsuario(TipoUsuarioId) {
    document.getElementById("txtTipoUsuarioId").value = TipoUsuarioId;
    verModal('Eliminar tipo de usuario',
        '¿Desea eliminar el tipo de usuario con el código '
        + TipoUsuarioId + '?').then((result) => {
            if (result.value) {
                var viewEliminarTipoUsuario = document.getElementById("viewEliminarTipoUsuario");
                viewEliminarTipoUsuario.submit();
                Swal.fire(
                    'Eliminación!',
                    'La el tipo de usuario ' + TipoUsuarioId + ' fue eliminado!.',
                    'success'
                )
            }
            else if (result.dismiss === Swal.DismissReason.cancel) {
                Swal.fire(
                    'Cancelado',
                    'El tipo de usuario no fue eliminado!!!:)',
                    'error'
                )
            }
        })
}

function EliminarPagina(PaginaId) {
    document.getElementById("txtPaginaId").value = PaginaId;
    verModal('Eliminar página ',
        '¿Desea eliminar la página con el código '
        + PaginaId + '?').then((result) => {
            if (result.value) {
                var viewEliminarPagina = document.getElementById("viewEliminarPagina");
                viewEliminarPagina.submit();
                Swal.fire(
                    'Eliminación!',
                    'La página ' + PaginaId + ' fue eliminada!.',
                    'success'
                )
            }
            else if (result.dismiss === Swal.DismissReason.cancel) {
                Swal.fire(
                    'Cancelado',
                    'El tipo de usuario no fue eliminada!!!:)',
                    'error'
                )
            }
        })
}

/*--------------------METODOS ELIMINAR--------------------*/
/*---------------------METODOS BUSCAR---------------------*/
function BuscarEnfermedad() {
    var nombre = document.getElementById("nombre").value;
    var url = "ConsultaEnfermedad/BuscarEnfermedad/?nombreEnfermedad=" + nombre;
    var tbody = document.getElementById("tbDatos");
    var campos = new Array("nombre", "descripcion");
    pintarPantallaConsulta(url, campos, tbody);
}
function BuscarEspecialidad() {
    var nombre = document.getElementById("nombre").value;
    var url = "ConsultaEspecialidad/BuscarEspecialidad/?nombreEspecialidad=" + nombre;
    var tbody = document.getElementsByClassName("tbDatos");
    var campos = new Array("nombre", "descripcion");
    pintarPantallaConsulta(url, campos, tbody);
}
function BuscarMedico() {
    var nombre = document.getElementById("nombre").value;
    var url = "ConsultaMedico/BuscarMedico/?nombreMedico=" + nombre;
    var tbody = document.getElementsByClassName("tbDatos");
    var campos = new Array("Nombre", "Apellidos", "Direccion", "Telefono Fijo", "Telefono Celular", "Foto", "especialidadId");
    pintarPantallaConsulta(url, campos, tbody);
}
/*---------------------METODOS BUSCAR---------------------*/
/*----------------METODOS GLOBALES MODALES----------------*/
function Agregar() {
    var continuar = true;
    let titulo = document.title;
    while (continuar) {
        switch (titulo) {
            case "Agregar especialidad":
                abrirModalCrearEspecialidad();
                continuar = false;
                break;

            case "Agregar medico":
                abrirModalCrearMedico();
                continuar = false;
                break;

            case "Agregar paciente":
                abrirModalCrearPaciente();
                continuar = false;
                break;

            case "Agregar enfermedad":
                abrirModalCrearEnfermedad();
                continuar = false;
                break;

            case "Agregar usuario":
                abrirModalCrearUsuario();
                continuar = false;
                break;

            case "Agregar citas":
                abrirModalCrearCita();
                continuar = false;
                break;

            case "Agregar tipo Usuario":
                abrirModalCrearTipoUsuario();
                continuar = false;
                break;

            case "Agregar pagina":
                abrirModalCrearPagina();
                continuar = false;
                break;
        }
    }
}

        function Editar() {
            let titulo = document.title;
            if (titulo == "Editar especialidad") {
                abrirModalEditarEspecialidad();
            } else {
                if (titulo == "Editar medico") {
                    abrirModalEditarMedico();
                } else {
                    if (titulo == "Editar paciente") {
                        abrirModalEditarPaciente();
                    } else {
                        if (titulo == "Editar enfermedad") {
                            abrirModalEditarEnfermedad();
                        } else {
                            if (titulo == "Editar usuario") {
                                abrirModalEditarUsuario();
                            } else {
                                if (titulo == "Editar cita") {
                                    abrirModalEditarCita();
                                } else {
                                    if (titulo == "Editar Tipo de Usuario") {
                                        abrirModalEditarTipoUusuario();
                                    } else {
                                        if (titulo == "Lista de páginas") {
                                            abrirModalEditarPagina();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

function Eliminar(id) {
    let titulo = document.title;
    if (titulo == "Especialidad") {
        EliminarEspecialidad(id);
    } else {
        if (titulo == "Medico") {
            EliminarMedico(id);
        } else {
            if (titulo == "Paciente") {
                EliminarPaciente(id);
            } else {
                if (titulo == "Enfermedad") {
                    EliminarEnfermedad(id);
                } else {
                    if (titulo == "Citas") {
                        EliminarCita(id);
                    } else {
                        if (titulo == "Usuario") {
                            EliminarUsuario(id);
                        } else {
                            if (titulo == "Tipos de Usuarios") {
                                EliminarTipoUsuario(id);
                            } else {
                                if (titulo == "Asigna roles") {
                                    EliminarTipoUsuario(id);
                                } else {
                                    if (titulo == "Lista de páginas") {
                                        EliminarPagina(id);
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}

function Buscar() {
    let titulo = document.title;
    if (titulo == "Consulta Enfermedad") {
        BuscarEnfermedad();
    } else {
        if (titulo == "Consulta Especialidad") {
            BuscarEspecialidad();
        } else {
            if (titulo == "Consulta Medico") {
                BuscarMedico();
            } else {
                if (titulo == "") {
                   
                }
            }
        }
    }
}

/*----------------METODOS GLOBALES MODALES----------------*/
/*---------------METODOS CONTROLLER USUARIO---------------*/

/*---------------METODOS CONTROLLER USUARIO---------------*/

function Resetear() {
    document.getElementById("nombre").value = "";
    Buscar();
}
function ExportarPDF() {
    var aux = document.getElementById("viewExportarPDF");
    aux.submit();
}
function ExportarExcel() {
    var aux = document.getElementById("viewExportarExcel");
    aux.submit();
}

$(document).ready(function () {
    $('#TbEspecial').DataTable(
        {
            "language": {
                "url": "//cdn.datatables.net/plug-ins/1.10.15/i18n/Spanish.json"
            }
        });
})

function verificar() {
    var value = $('.dataTables_filter input').val();
    alert(value);
}









