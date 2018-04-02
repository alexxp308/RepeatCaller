$(document).ready(function ()
{
    listarCampañas(window.cookie.getCookie()["sedeId"]);
    getMax(getcurrentDate());
    var formulario = document.getElementById("formReporte");
    formulario.onsubmit = function (e)
    {
        e.preventDefault();
        basesFaltantes();
    }
});

function getMax(today)
{
    //$("#fecha").attr("max", today);
    //$("#fechaFinal").attr("max", today);
}

function mostrarDiv(elem)
{
    if (elem.value == "3")
    {
        document.getElementById("divff").style.display = "block";
        document.getElementById("ini").innerHTML = " inicial";
        document.getElementById("fechaFinal").setAttribute("required", "required");
    } else
    {
        document.getElementById("divff").style.display = "none";
        document.getElementById("ini").innerHTML = "";
        document.getElementById("fechaFinal").removeAttribute("required");
    }
}

function validarFecha(elem)
{
    var ff = document.getElementById("fechaFinal");
    if (elem.value != "")
    {
        ff.removeAttribute("disabled");
        ff.setAttribute("min", elem.value);
        if (elem.value > ff.value)
        {
            ff.value = "";
        }
    } else
    {
        ff.value = "";
        ff.setAttribute("disabled", "disabled");
    }
}

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
                var str = "<option value=''>--SELECCIONAR--</option>";
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

function traerReporte()
{
    //primero busqueda de todos los reportes pasados!!!! usar un ajax dentro de otro
    var data = {};
    data.campaniaId = $("#campania").val() * 1;
    data.tipo = $("#tipo").val() * 1;
    data.fechaBase = $("#fecha").val();
    data.fechaFinal = $("#fechaFinal").val();

    $(".loader").toggle(true);
    $.ajax({
        method: "POST",
        url: "/Reporte/Reportes",
        contentType: "application/json",
        data: JSON.stringify(data),
        dataType: "text",
        success: function (response)
        {
            $(".loader").toggle(false);
            location.href = response;
        }
    });
}

function sumarDias(fecha, dias)
{
    fecha.setDate(fecha.getDate() + dias);
    return fecha;
}

function formatDate(date)
{
    var d = new Date(date),
        month = '' + (d.getMonth() + 1),
        day = '' + d.getDate(),
        year = d.getFullYear();

    if (month.length < 2) month = '0' + month;
    if (day.length < 2) day = '0' + day;

    return [year, month, day].join('-');
}

var reportes = [];
function basesFaltantes()
{
    var data = {};
    data.campaniaId = $("#campania").val() * 1;
    data.tipo = $("#tipo").val() * 1;
    data.fechaBase = $("#fecha").val();
    data.fechaFinal = $("#fechaFinal").val();
    $(".loader").toggle(true);
    $.ajax({
        method: "POST",
        url: "/Reporte/basesFaltantes",
        contentType: "application/json",
        data: JSON.stringify(data),
        dataType: "text",
        success: function (response)
        {
            $(".loader").toggle(false);
            if (response.length > 0)
            {
                var datos = response.split("#");
                var dato = null;
                var d = null;
                reportes = [];
                debugger;
                if (data.tipo == 1 || data.tipo==2)
                {
                    d = new Date(data.fechaBase + " 00:00:00");
                    for (var i = 1; i < 16; i++)
                    {
                        reportes.push({ fecha: formatDate(sumarDias(d, -1)), tipo: 'CDR' });
                        reportes.push({ fecha: formatDate(d), tipo: 'TIPI' });
                    }
                } else if (data.tipo==3)
                {
                    d = new Date(data.fechaBase + " 00:00:00");
                    var d1 = new Date(data.fechaFinal + " 00:00:00");
                    var cantDias = diferenciaDias(d, d1);
                    for (var i = 0; i <= cantDias; i++)
                    {
                        reportes.push({ fecha: formatDate(sumarDias(d1, ((i==0)?0:-1))), tipo: 'CDR' });
                        reportes.push({ fecha: formatDate(d1), tipo: 'IVR' });
                    }
                    console.log(reportes);
                }
                for (var j = 0; j < reportes.length; j++)
                {
                    for (var z = 0; z < datos.length; z++)
                    {
                        dato = datos[z].split("|");
                        if (dato[0] == reportes[j]["fecha"] && dato[1] == reportes[j]["tipo"])
                        {
                            reportes.splice(j, 1);
                        }
                    }
                }
                if (reportes.length > 0)
                {
                    llenarTable(reportes);
                    $("#nameReporte").html($('#tipo option:selected').text());
                    $("#dvBasesFaltantes").modal();
                } else
                {
                    traerReporte();
                }
            } else
            {
                alert("No se ha subido ninguna base");
            }
        }
    });
}

function llenarTable(reportes)
{
    $("#listBasesFaltantes").html("");
    var str = "";
    for (var i = 0; i < reportes.length; i++)
    {
        str += "<tr>";
        str += "<td align='center'>" + (i+1) + "</td>";
        str += "<td align='center'>" + reportes[i]["fecha"] + "</td>";
        str += "<td align='center'>" + reportes[i]["tipo"] + "</td>";
        str += "</tr>";
    }
    $("#listBasesFaltantes").html(str);
}

function diferenciaDias(fi,ff)
{
    var fechaInicio = new Date(fi).getTime();
    var fechaFin = new Date(ff).getTime();

    var diff = fechaFin - fechaInicio;

    return diff / (1000 * 60 * 60 * 24);
}