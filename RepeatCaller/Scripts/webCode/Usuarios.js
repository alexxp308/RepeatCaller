$(document).ready(function ()
{
    listarUsuarios();
});

cantUsuarios = 0;
function listarUsuarios()
{
    $.ajax({
        method: "POST",
        url: "/Usuarios/listarUsuarios",
        data: { idSede: window.cookie.getCookie()["sedeId"]*1},
        dataType: "text",
        success: function (response)
        {
            if (response.length > 0)
            {
                var data = response.split("#");
                var str = "";
                var fila = null;
                for (var j = 0; j < data.length; j++)
                {
                    cantUsuarios++;
                    fila = data[j].split("|");
                    str += "<tr id='tr_" + fila[0] + "'>";
                    str += "<th scope='row' align='center'><span style='display:none;' id='col_" + fila[0] + "'></span><p>" + cantUsuarios + "</p></th>";
                    str += "<td align='center'>" + fila[1] + "</td>";
                    str += "<td align='center'>" + fila[3] + "</td>";
                    str += "<td align='center'>" + fila[2] + "</td>";
                    str += "<td align='center'><label class='switch'><input type='checkbox' id='switch_" + fila[0] + "' " + (fila[4] == "True" ? "checked" : "") + " onchange='cambioEstado(this)'/><span class='slider round'></span></label></td>";
                    str += "<td align='center'><button title='editar usuario' type='button' class='btn btn-success btn-sm' onclick='editarUsuario(" + fila[0] + "," + "\"" + fila[1] + "\"" + "," + "\"" + fila[2] + "\"" + "," + "\"" + fila[3] + "\"" + ")'><span class='glyphicon glyphicon-pencil' aria-hidden='true'></span></button>&nbsp;&nbsp;<button title='resetear contraseña' type='button' class='btn btn-default btn-sm' onclick='resetear(\"" + fila[0] + "\")'><i class='fa fa-undo'></i></button></td>";
                    str += "</tr>";
                }
                $("#listarUsuarios").html(str);
            }
        }
    });
}

function limpiar()
{
    $("#apellidos").val("");
    $("#nombre").val("");
    $("#userName").val("");
    $("#role").val("0");
}

function crearUsuario()
{
    limpiar();
    $('#dvCrearUser').modal();
    $('#verifica').html('0');
}

function editarUsuario(id, userName, roles, nombreCompleto)
{
    $("#apellidos").val(nombreCompleto.split(", ")[0]);
    $("#nombre").val(nombreCompleto.split(", ")[1]);
    $("#userName").val(userName);
    $("#role").val(roles);
    $("#verifica").html(id);
    $('#dvCrearUser').modal();
}

function guardarUsuario()
{
    if ($("#apellidos").val() != "" && $("#nombre").val() != "" && $("#userName").val() != "" && $("#role").val() != "0")
    {
        var verifica = $("#verifica").html();
        var data = {};
        data.userName = $("#userName").val();
        data.roles = $("#role").val();
        data.nombreCompleto = $("#apellidos").val() + ", " + $("#nombre").val();

        if (verifica == "0")
        {
            $.ajax({
                method: "POST",
                url: "/Usuarios/guardarUsuario",
                contentType: "application/json",
                data: JSON.stringify(data),
                dataType: "text",
                success: function (response)
                {
                    if (response.length > 0)
                    {
                        cantUsuarios++;
                        var fila = response.split("|");
                        var str = "";
                        str += "<tr id='tr_" + fila[0] + "'>";
                        str += "<th scope='row' align='center'><span style='display:none;' id='col_" + fila[0] + "'></span><p>" + cantUsuarios + "</p></th>";
                        str += "<td align='center'>" + fila[1] + "</td>";
                        str += "<td align='center'>" + fila[3] + "</td>";
                        str += "<td align='center'>" + fila[2] + "</td>";
                        str += "<td align='center'><label class='switch'><input type='checkbox' id='switch_" + fila[0] + "' " + (fila[4] == "True" ? "checked" : "") + " onchange='cambioEstado(this)'/><span class='slider round'></span></label></td>";
                        str += "<td align='center'><button title='editar usuario' type='button' class='btn btn-success btn-sm' onclick='editarUsuario(" + fila[0] + "," + "\"" + fila[1] + "\"" + "," + "\"" + fila[2] + "\"" + "," + "\"" + fila[3] + "\"" + ")'><span class='glyphicon glyphicon-pencil' aria-hidden='true'></span></button>&nbsp;&nbsp;<button title='resetear contraseña' type='button' class='btn btn-default btn-sm' onclick='resetear(\"" + fila[0] + "\")'><i class='fa fa-undo'></i></button></td>";
                        str += "</tr>";

                        $("#listarUsuarios").append(str);
                        $("#dvCrearUser").modal("hide");
                    }
                }
            });
        } else
        {
            data.userId = verifica * 1;
            $.ajax({
                method: "POST",
                url: "/Usuarios/actualizarUsuario",
                contentType: "application/json",
                data: JSON.stringify(data),
                dataType: "text",
                success: function (response)
                {
                    if (response.length > 0)
                    {
                        var datos = response.split("|");
                        var columna = document.getElementById("tr_" + datos[0]).getElementsByTagName("td");

                        columna[0].innerHTML = datos[1];
                        columna[1].innerHTML = datos[3];
                        columna[2].innerHTML = datos[2];
                        columna[4].innerHTML = "<button title='editar usuario' type='button' class='btn btn-success btn-sm' onclick='editarUsuario(" + datos[0] + "," + "\"" + datos[1] + "\"" + "," + "\"" + datos[2] + "\"" + "," + "\"" + datos[3] + "\"" + ")'><span class='glyphicon glyphicon-pencil' aria-hidden='true'></span></button>&nbsp;&nbsp;<button title='resetear contraseña' type='button' class='btn btn-default btn-sm' onclick='resetear(\"" + datos[0] + "\")'><i class='fa fa-undo'></i></button>";

                        $("#dvCrearUser").modal("hide");
                    }
                }
            });
        }
    }
}

function cambioEstado(elem)
{
    var data = {};
    data.UserId = elem.id.substring(7, elem.id.length) * 1;
    data.IsActive = elem.checked;
    $.ajax({
        method: "POST",
        url: "/Usuarios/actualizarEstado",
        contentType: "application/json",
        data: JSON.stringify(data),
        dataType: "text",
        success: function (response)
        {
            if ((response * 1) > 0)
            {
                alert("Estado actualizado correctamente");
            } else
            {
                alert("Error en actualizar el estado de la Sala de juntas");
            }
        }
    });
}

function resetear(userId)
{
    var question = confirm("¿Esta seguro que desea resetear la contraseña del usuario?");
    if (question)
    {
        var data = {};
        data.UserId = userId * 1;
        $.ajax({
            method: "POST",
            url: "/Usuarios/resetearContrasenia",
            contentType: "application/json",
            data: JSON.stringify(data),
            dataType: "text",
            success: function (response)
            {
                if ((response * 1) > 0)
                {
                    alert("se reseteo la contraseña correctamente");
                } else
                {
                    alert("Error en resetear la contraseña");
                }
            }
        });
    }

}
