$(document).ready(function ()
{
    listarCampañas(window.cookie.getCookie()["sedeId"]);
    getMax(getcurrentDate());
});

function getMax(today)
{
    //$("#fecha").attr("max", today);
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
                var str = "<option value='0'>--SELECCIONAR--</option>";
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

function traerReporte(elem)
{
    var data = {};
    data.campaniaId = $("#campania").val() * 1;
    data.tipo = $("#tipo").val() * 1;
    data.fechaBase = $("#fecha").val();
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
            //console.log(response);
            //crearExcel(response);
        }
    });
}

/*function crearExcel(data)
{
    var uri = 'data:application/vnd.ms-excel;base64,'
        , tmplWorkbookXML = '<?xml version="1.0"?><?mso-application progid="Excel.Sheet"?><Workbook xmlns="urn:schemas-microsoft-com:office:spreadsheet" xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet">'
            + '<DocumentProperties xmlns="urn:schemas-microsoft-com:office:office"><Author>Axel Richter</Author><Created>{created}</Created></DocumentProperties>'
            + '<Styles>'
            + '<Style ss:ID="Currency"><NumberFormat ss:Format="Currency"></NumberFormat></Style>'
            + '<Style ss:ID="s1"><Interior ss:Color="#F0CA46" ss:Pattern="Solid"/><Alignment ss:Vertical="Center"/><Borders> <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"/><Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/><Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/><Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"/></Borders></Style>'
            + '<Style ss:ID="s2"><Alignment ss:Vertical="Center"/><Borders> <Border ss:Position="Bottom" ss:LineStyle="Continuous" ss:Weight="1"/><Border ss:Position="Left" ss:LineStyle="Continuous" ss:Weight="1"/><Border ss:Position="Right" ss:LineStyle="Continuous" ss:Weight="1"/><Border ss:Position="Top" ss:LineStyle="Continuous" ss:Weight="1"/></Borders></Style>'
            + '<Style ss:ID="Date"><NumberFormat ss:Format="Medium Date"></NumberFormat></Style>'
            + '</Styles>'
            + '{worksheets}</Workbook>'
        , tmplWorksheetXML = '<Worksheet ss:Name="{nameWS}"><Table>{rows}</Table></Worksheet>'
        , tmplCellXML = '<Cell{attributeStyleID}><Data ss:Type="{nameType}">{data}</Data></Cell>'
        , base64 = function (s) { return window.btoa(unescape(encodeURIComponent(s))) }
        , format = function (s, c) { return s.replace(/{(\w+)}/g, function (m, p) { return c[p]; }) }

    var ctx = "";
    var workbookXML = "";
    var worksheetsXML = "";
    var rowsXML = "";

    var keysData = Object.keys(data);
    var keysFila = null;
    var filas = null;
    var valor = null;
    debugger;
    for (var i = 0; i < keysData.length; i++)
    {
        filas = data[keysData[i]];
        keysFila = Object.keys(filas[0]);
        for (var j = 0; j < filas.length; j++)
        {
            if (j == 0)
            {
                rowsXML += '<Row><Cell ss:MergeAcross="3"><Data ss:Type="String">' + keysData[i]+'</Data></Cell></Row>';
                rowsXML += '<Row><Cell></Cell>';
                for (var m = 0; m < keysFila.length; m++)
                {
                    ctx = {
                        attributeStyleID: ' ss:StyleID="s1"'
                        , nameType: 'String'
                        , data: keysFila[m]
                    };
                    rowsXML += format(tmplCellXML, ctx);
                }
                rowsXML += '</Row>';
            }

            rowsXML += '<Row><Cell></Cell>';
            valor = filas[j];
            for (var z = 0; z < keysFila.length; z++)
            {
                ctx = {
                    attributeStyleID:' ss:StyleID="s2"'
                    , nameType: 'String'
                    , data: valor[keysFila[z]]
                };
                rowsXML += format(tmplCellXML, ctx);
            }
            rowsXML += '</Row>'
        }
        ctx = { rows: rowsXML, nameWS: keysData[i] || 'Sheet' + i };
        worksheetsXML += format(tmplWorksheetXML, ctx);
        rowsXML = "";
    }
    debugger;
    ctx = { created: (new Date()).getTime(), worksheets: worksheetsXML };
    workbookXML = format(tmplWorkbookXML, ctx);

    //console.log(workbookXML);

    var link = document.createElement("A");
    link.href = uri + base64(workbookXML);
    link.download = 'Reporte_cruce_datos.xls' || 'Workbook.xls';
    document.body.appendChild(link);
    link.click();
    document.body.removeChild(link);
}*/