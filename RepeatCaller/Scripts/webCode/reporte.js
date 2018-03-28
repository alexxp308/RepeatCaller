$(document).ready(function ()
{
    listarCampañas(window.cookie.getCookie()["sedeId"]);
    getMax(getcurrentDate());
    var formulario = document.getElementById("formReporte");
    formulario.onsubmit = function (e)
    {
        e.preventDefault();
        traerReporte();
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
            debugger;
            $(".loader").toggle(false);
            location.href = response;
        }
    });
}