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
    $("#fechaI_buscar").attr("max", today);
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
                var str = "<option value='0'>--SELECCIONAR--</option>";
                var campania = null;
                for (var i = 0; i < campanias.length; i++)
                {
                    campania = campanias[i].split("|");
                    str += "<option value='" + campania[0] + "'>" + campania[1] + "</option>";
                }
                $("#campania").html(str);
                $("#campania_buscar").html(str);

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
    var pregunta = true;
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
            $(".loader").toggle(true);
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
                            $(".loader").toggle(false);
                            alert("No se subio el archivo correctamente");
                        } else
                        {
                            var data = {};
                            data.nombreArchivo = nombre;
                            data.baseId = baseId;
                            data.tipo = tipo;
                            data.campaniaId = campania;
                            data.fechaBase = fecha;
                            $.ajax({
                                method: "POST",
                                url: "/SubidaBase/CargarTabla",
                                contentType: "application/json",
                                data: JSON.stringify(data),
                                dataType: "text",
                                success: function (response)
                                {
                                    $(".loader").toggle(false);
                                    if (!isNaN(response * 1))
                                    {
                                        alert("Se guardaron " + response + " filas");
                                        $("#dvUpload").modal("hide");
                                    } else if (response == "A")
                                    {
                                        alert("El formato no es compatible, se regreso a la base anterior");
                                    } else
                                    {
                                        alert("El formato no es compatible");
                                    }
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

function abrirModalUpload()
{
    $("#dvUpload").modal("show");
    limpiar();
}

function limpiar()
{
    $("#tipo").val("0");
    $("#campania").val("0");
    $("#fechaI").val(getcurrentDate());
    $("#feik").val("");
    $("#myfile").val("");
}

function buscarBase()
{
    var fecha_buscar = $("#fechaI_buscar").val();
    if (fecha_buscar != "")
    {
        var tipo = $("#tipo_buscar").val();
        var campania = $("#campania_buscar").val();
        data = {};
        data.tipo = tipo;
        data.campaniaId = campania*1;
        data.fechaBase = fecha_buscar;
        $(".loader").toggle(true);
        $.ajax({
            method: "POST",
            url: "/SubidaBase/obtenerBases",
            contentType: "application/json",
            data: JSON.stringify(data),
            dataType: "text",
            success: function (response)
            {
                $(".loader").toggle(false);
                if (response.length > 0)
                {
                    var filas = response.split("£");
                    var campos = null;
                    var bases = {};
                    var nombreCampania = "";
                    for (var i = 0; i < filas.length; i++)
                    {
                        campos = filas[i].split("|");
                        if (jQuery.isEmptyObject(bases[campos[6]]))
                        {
                            bases[campos[6]] = {};
                            nombreCampania = campos[2].replace(/ /g, '');
                            if (jQuery.isEmptyObject(bases[campos[6]][nombreCampania]))
                            {
                                bases[campos[6]][nombreCampania] = [];
                                bases[campos[6]][nombreCampania].push(
                                    {
                                        baseId: campos[0], nombreUser: campos[1], nombreCampania: campos[2],
                                        fechaBase: campos[3], fhCreacion: campos[4], nombreArchivo: campos[5],
                                        tipo: campos[6], isActive: campos[7], isCompatible: campos[8]
                                    });
                            } else
                            {
                                bases[campos[6]][nombreCampania].push(
                                    {
                                        baseId: campos[0], nombreUser: campos[1], nombreCampania: campos[2],
                                        fechaBase: campos[3], fhCreacion: campos[4], nombreArchivo: campos[5],
                                        tipo: campos[6], isActive: campos[7], isCompatible: campos[8]
                                    });
                            }
                        } else
                        {
                            nombreCampania = campos[2].replace(/ /g, '');
                            if (jQuery.isEmptyObject(bases[campos[6]][nombreCampania]))
                            {
                                bases[campos[6]][nombreCampania] = [];
                                bases[campos[6]][nombreCampania].push(
                                    {
                                        baseId: campos[0], nombreUser: campos[1], nombreCampania: campos[2],
                                        fechaBase: campos[3], fhCreacion: campos[4], nombreArchivo: campos[5],
                                        tipo: campos[6], isActive: campos[7], isCompatible: campos[8]
                                    });
                            } else
                            {
                                bases[campos[6]][nombreCampania].push(
                                    {
                                        baseId: campos[0], nombreUser: campos[1], nombreCampania: campos[2],
                                        fechaBase: campos[3], fhCreacion: campos[4], nombreArchivo: campos[5],
                                        tipo: campos[6], isActive: campos[7], isCompatible: campos[8]
                                    });
                            }
                        }
                    }

                    console.log(bases);
                    var keysTipo = Object.keys(bases);
                    var tipo = null;
                    var campania = null;
                    var strM = "";
                    debugger;
                    for (var i = 0; i < keysTipo.length; i++)
                    {
                        tipo = bases[keysTipo[i]];
                        strM += "<div class='panel panel-default'>";
                        strM += "<div class='panel-heading'>";
                        strM += "<h5 class='panel-title'>";
                        strM += "<div class='row'>";
                        strM += "<div class='col-sm-12 col-md-6 col-lg-6' style='padding-top: 10px;'>";
                        strM += "<a data-toggle='collapse' data-parent='#result' href='#tabla" + i + "' style='color:#f0ad4e;'><span style='font-weight:bold;'>TIPO:</span> " + keysTipo[i] + "</a></div>";
                        strM += "</div>";
                        strM += "</h5>";
                        strM += "</div>";
                        strM += ' <div id="tabla' + i + '" class="panel-collapse collapse' + ((i == 0) ? ' in' : '') + '">';
                        strM += '<div class="panel-body">';
                        strM += crearPanels(tipo, keysTipo[i]);
                        debugger;
                        strM += "</div></div></div>"
                    }
                    $("#result").html(strM);
                    document.getElementById("content").style.display = "block";
                } else
                {
                    alert("No hay datos para mostrar");
                }
            }
        });
    } else
    {
        alert("Debes seleccionar al menos la fecha");
    }
}

function crearPanels(campanias,tipo)
{
    var keyCampania = Object.keys(campanias);
    var str = "";
    var campos = "";
    var bases = null;
    str += "<div class='panel- group' id='panel_" + tipo + "'>";
    
    for (var n = 0; n < keyCampania.length; n++)
    {
        bases = campanias[keyCampania[n]];
        campos = "";
        for (var m = 0; m < bases.length; m++)
        {
            campos += "<tr id='tr_" + bases[m].baseId + "'>";
            campos += "<th scope='row' align='center'><span style='display:none;' id='col_" + bases[m].baseId + "'></span><p>" + (m + 1) + "</p></th>";
            campos += "<td align='center'>" + bases[m].nombreUser + "</td>";
            //campos += "<td align='center'>" + bases[m].fechaBase + "</td>";
            campos += "<td align='center'>" + bases[m].fhCreacion + "</td>";
            campos += "<td align='center'>" + bases[m].nombreArchivo + "</td>";
            campos += "<td align='center'><a href='/Doc/" + bases[m].nombreArchivo+"' download><button type='button' class='btn btn-default' title='" + bases[m].nombreArchivo +"'><span class='glyphicon glyphicon-download-alt' aria-hidden='true'></span></button></a></td>";
            campos += "<td align='center'><div class='esfera " + ((bases[m].isActive == "True") ? "on' title='base actual'" : "off' title='base pasada'") + "></div></td>";
            //campos += "<td align='center'><span class='"+((bases[m].isCompatible == "True") ? "glyphicon glyphicon-ok' title='correcto'" :"glyphicon glyphicon-remove' title='incorrecto'")+" aria-hidden='true' style='font-size:20px;'></span></td>";
            campos += "</tr>";
        }
        str += "<div class='panel panel-default'>";
        str += "<div class='panel-heading'>";
        str += "<h5 class='panel-title'>";
        str += "<div class='row'>";
        str += "<div class='col-sm-12 col-md-6 col-lg-6' style='padding-top: 10px;'>";
        str += "<a data-toggle='collapse' data-parent='#panel_" + tipo + "' href='#tabla_" + tipo + n + "' style='color:#5cb85c;'><span style='font-weight:bold'>CAMPAÑA:</span> " + keyCampania[n] + "</a></div>";
        str += "</div>";
        str += "</h5>";
        str += "</div>";
        str += ' <div id="tabla_' + tipo + n + '" class="panel-collapse collapse' + ((n == 0) ? ' in' : '') + '">';
        str += '<div class="panel-body">';
        str += createTable(campos);
        str += "</div></div></div>"
    }

    str += "</div>";

    return str;
}


function createTable(cadena)
{
    var my = "";
    my += '<div class="table-responsive">';
    my += '<table class="table table-bordered">';
    my += '<thead class="blue-grey lighten-4">';
    my += '<tr class="success">';
    my += '<th align="center">Nro</th>';
    my += '<th align="center">Usuario</th>';
    my += '<th align="center">Fecha creación</th>';
    my += '<th align="center">Nombre archivo</th>';
    my += '<th align="center">Archivo</th>';
    my += '<th align="center">Estado</th>';
    my += '</tr>';
    my += '</thead>';
    my += '<tbody>';
    my += cadena;
    my += '</tbody>';
    my += '</table>';
    my += '</div>';
    return my;
}

function verStatus()
{
    var fecha_buscar = $("#fechaI_buscar").val();
    var campania = $("#campania_buscar").val();
    if (fecha_buscar != "" && campania != "")
    {
        var data = {};
        data.campaniaId = campania * 1;
        data.fechaBase = fecha_buscar;
        $(".loader").toggle(true);
        $.ajax({
            method: "POST",
            url: "/SubidaBase/verStatus",
            contentType: "application/json",
            data: JSON.stringify(data),
            dataType: "text",
            success: function (response)
            {
                $(".loader").toggle(false);
                if (response.length > 0)
                {
                    var datos = response.split("£");
                    var campos = null;
                    var str = "";
                    var busc = "";
                    for (var i = 0; i < datos.length; i++)
                    {
                        campos = datos[i].split("|");
                        str += "<tr>";
                        str += "<td align='center'>" + campos[2] + "</td>";
                        str += "<td align='center'>" + campos[0] + "</td>";
                        str += "<td align='center'>" + campos[1] + "</td>";
                        str += "</tr>";
                        busc += campos[2] + ";";
                    }
                    //mostrarFaltantes(busc.substr(-1));
                    $("#nameCampFecha").html($("#campania_buscar option:selected").text() + "-" + fecha_buscar);
                    $("#listBases").html(str);
                }
                $("#dvStatus").modal("show");
            }
        });
    } else
    {
        alert("Debes escoger la fecha y la campaña");
    }
}

/*function mostrarFaltantes(cadena)
{
    var alt = "";
    if (cadena.indexOf("CDR") == -1)
    {
        alt += "Falta la base CDR,";
    } else if (cadena.indexOf("TIPI") == -1)
    {
        alt += "Falta la base TIPI,";
    } else if (cadena.indexOf("IVR") == -1)
    {
        alt += "Falta la base IVR,";
    }

    return (alt == "") ? alt : alt.substr(-1);
}*/