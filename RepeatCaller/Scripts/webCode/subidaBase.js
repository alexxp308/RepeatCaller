$(document).ready(function (){
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
                    str += "<option val='"+campania[0]+"'>"+campania[1]+"</option>";
                }
                $("#campania").html(str);

            } else
            {
                alert("No se han creado campañas");
            }
        }
    });
}

function ultimoReporte()
{
    var campania = $("#campania").val();
    var tipo = $("#tipo").val();
    var fechaI = $("#fechaI").val();
    if (campania != "0" && tipo != "0" && fechaI != "")
    {
        console.log("busca el ultimo reporte!");
        document.getElementById("divUltimo").style.display = "block";
    } else
    {
        document.getElementById("divUltimo").style.display = "none";
    }
}

function cambioFile(elem)
{
    debugger;
    var extension = elem.value.substr(-3);
    
    if (extension == "txt")
    {
        var name = elem.value.substr(elem.value.lastIndexOf("\\")+1);
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
    var doc = document.getElementById("feik").value;
    var tipo = $("#tipo").val();
    var campania = $("#campania").val();
    var fecha = $("#fechaI").val()
    if (doc != "" && tipo != "0" && fecha != "")
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
                   /* $.ajax({
                        method: "POST",
                        url: "/SubidaBase/CargarTablas",
                        contentType: "application/json",
                        data: JSON.stringify(data),
                        dataType: "text",
                        success: function (response)
                        {

                        }
                    });
                    console.log(response);*/
                    console.log(response);
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