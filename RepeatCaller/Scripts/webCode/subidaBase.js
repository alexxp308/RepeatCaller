$(document).ready(function ()
{
    listarCampañas(window.cookie.getCookie()["sedeId"]);
    getMax(getcurrentDate());
});

function getcurrentDate()
{
    var midate = new Date();
    var dd = midate.getDate();
    var mm = midate.getMonth() + 1;
    var yyyy = midate.getFullYear();
    dd = (dd < 10) ? '0' + dd : dd;
    mm = (mm < 10) ? '0' + mm : mm;
    return yyyy + '-' + mm + '-' + dd;
}

function getMax(today)
{
    $("#fechaI").val(today);
    $("#fechaI").attr("max", today);
}

function listarCampañas(sedeId)
{
    var data = {};
    data.idSede = sedeId * 1;
    $.ajax({
        method: "POST",
        url: "/Campanias/ListarCampanias",
        contentType: "application/json",
        data: JSON.stringify(data),
        dataType: "text",
        success: function (response)
        {
            if (response.length > 0)
            {
                var campanias = response.split("#");
                var str = "<option val='0'>--SELECCIONAR--</option>";
                var campania = null;
                for (var i = 0; i < campanias.length; i++)
                {
                    campania = campanias[i].split("|");
                    str += "<option value='" + campania[0] + "'>" + campania[1] + "</option>";
                }
                $("#campania").html(str);

            } else
            {
                alert("No se han creado campañas");
            }
        }
    });
}

var estaCargado = 0;
function ultimoReporte()
{
    var div = document.getElementById("divAlert");
    div.style.opacity = "0";
    var campania = $("#campania").val();
    var tipo = $("#tipo").val();
    var fechaI = $("#fechaI").val();
    if (campania != "0" && tipo != "0" && fechaI != "")
    {
        var data = {};
        data.campaniaId = campania * 1;
        data.tipo = tipo;
        data.fechaBase = fechaI;
        $.ajax({
            method: "POST",
            url: "/SubidaBase/getBaseCargada",
            contentType: "application/json",
            data: JSON.stringify(data),
            dataType: "text",
            success: function (response)
            {
                if (response.length > 0)
                {
                    mostrarAlerta(response);
                    estaCargado = 1;
                } else
                {
                    estaCargado = 0;
                }
            }
        });
    }
}

function mostrarAlerta(fhCreacion)
{
    var div = document.getElementById("divAlert");
    if (div != null)
    {
        div.style.display = "block";
        div.style.opacity = "0.00";
        document.getElementById("fhCreacion").innerHTML = fhCreacion;
        var my = setInterval(function ()
        {
            if (div.style.opacity == "1")
            {
                clearInterval(my);
            } else
            {
                div.style.opacity = div.style.opacity * 1 + 0.05;
            }
        }, 15);
    }
}

function quitarAlerta(elem)
{
    var div = elem.parentElement;
    div.style.opacity = "0.00";
    setTimeout(function () { div.style.display = "none"; }, 300);
}

function cambioFile(elem)
{
    var extension = elem.value.substr(-3);

    if (extension == "txt")
    {
        var name = elem.value.substr(elem.value.lastIndexOf("\\") + 1);
        document.getElementById("feik").value = name;
    }
    else
    {
        document.getElementById("feik").value = "";
        elem.value = "";
        alert("Debes escoger un archivo con extension txt");
    }
}

function uploadFile()
{
    debugger;
    var pregunta = false;
    if (estaCargado == 1)
    {
        pregunta = confirm("Ya existe una base,¿Desea cambiarla?");
    }
    if (pregunta)
    {
        var doc = document.getElementById("feik").value;
        var tipo = $("#tipo").val();
        var campania = $("#campania").val();
        var fecha = $("#fechaI").val();
        if (doc != "" && tipo != "0" && fecha != "" && campania != "0")
        {
            file = document.getElementById("myfile").files[0];
            var formData = new FormData();
            formData.append("file", file);
            formData.append("tipo", tipo);
            formData.append("campania", campania);
            formData.append("fecha", fecha);

            $.ajax({
                url: "api/SubidaBase/Upload",
                type: "POST",
                data: formData,
                dataType: "json",
                cache: false,
                contentType: false,
                processData: false,
                success: function (response)
                {
                    if (response.length > 0)
                    {
                        var datos = response.split("|");
                        var nombre = datos[0];
                        var baseId = datos[1];
                        if (nombre == "" || baseId == "0")
                        {
                            alert("No se subio el archivo correctamente");
                        } else
                        {
                            var data = {};
                            data.nombreArchivo = nombre;
                            data.baseId = baseId;
                            data.tipo = tipo;
                            $.ajax({
                                method: "POST",
                                url: "/SubidaBase/CargarTabla",
                                contentType: "application/json",
                                data: JSON.stringify(data),
                                dataType: "text",
                                success: function (response)
                                {
                                    alert("Se guardaron " + response + " filas");
                                }
                            });
                        }
                    } else
                    {
                        alert("No se pudo subir este archivo");
                    }
                }
            });
        } else
        {
            alert("Falta completar campos");
        }
    }
}