<%@ Page Language="VB" AutoEventWireup="false" CodeFile="hrcAlerts.aspx.vb" Inherits="hrcAlerts"  %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Alertas</title>
    <base target="_self" />   
    <style type="text/css" >
        .alemsg-dsc {
         font-family:Verdana;
         font-size:9px;
         color: #202020;
    }
    .alemsg-obs {
         font-family:Verdana;
         font-size:9px;
         color: #202020;
    }
    .alemsg-timegenerated {
         font-family:Verdana;
         font-size:8px;
         color: #808080;
    }
    </style>
    <script src="plugins/jquery/jquery.min.js" type="text/javascript"></script>
    <script type="text/javascript" src="hrc/hrcGeneral.js" language="javascript" >
    </script>   
<script language="javascript" type="text/javascript" src="hrcWinPopup.js"  ></script>
<script language="javascript" type="text/javascript" src="DataType_Validate.js"  ></script>

<script src='plugins/jquery/1.5/jquery-1.5.min.js' type='text/javascript'></script>
<script src='plugins/jquery/jquery-ui-1.8.9.custom.min.js' type='text/javascript'></script>
<script src='plugins/dateformat/date-es-AR.js' type='text/javascript'></script>
<script language="javascript" type="text/javascript"  >
    function gerror_show(perror) {
        $('#lblstatus').show();
        $('#lblstatus').html('<span class=error >' + perror + '</span>');
        //alert(perror);
    }
    function gstatus_hide() {
        $('#lblstatus').hide();
    }
    function gstatus_show(plevel, pmsg) {
        var auxContent = '';
        auxContent += '<img width=12px border=0 style=margin-right:5px src=imagenes/';
        switch (plevel) {
            case 1: auxContent += 'semaphore_green.png'; break;
            case 2: auxContent += 'semaphore_yellow.png'; break;
            case 3: auxContent += 'semaphore_red.png'; break;
            default: auxContent += 'semaphore_blue.png';
        };
        auxContent += ' />' + pmsg;
        $('#lblstatus').show();
        $('#lblstatus').html(auxContent);
        //alert(perror);
    }   
   
   
//    function gAlerts_send(palemsgdsc, palemsgobs, plevel, pcatcod, palequecod) {
//    if (palemsgdsc == '' && palemsgobs == '') {
//        alert('Ingrese un mensaje');
//    } else {        
//        gajax_start('ajax_update');
//        $('#pnlALEMSG').hide();
//        $('#pnlALE').show();
//        $.ajax({
//            type: 'POST',
//            url: "hrcAlerts.ashx",
//            data: {
//                action: 'alerts_send',
//                alemsgdsc: palemsgdsc,
//                alemsgobs: palemsgobs,
//                catcod: pcatcod,
//                level: plevel,
//                alequecod: palequecod
//            },
//            success: function(pdata) {
//                gajax_stop('ajax_update');
//            },
//            error: function() {
//                gajax_stop('ajax_update');
//                gerror_show('Error enviando datos!');
//            }
//        });
//        };
//      return false;
//    }
//    function gAlerts_delete(pReaders, palemsgcod, palequecod) {
//        gajax_start('ajax_update');
//        $.ajax({
//            type: 'POST',
//            url: "hrcAlerts.ashx",
//            data: {
//                action: 'alerts_delete',
//                readers: pReaders,
//                param1: palemsgcod,
//                alequecod: palequecod
//            },
//            success: function(pdata) {
//                var auxRowID = 'ale_' + palemsgcod + '_' + palequecod;
//                $('#' + auxRowID).remove();
//                gajax_stop('ajax_update');
//            },
//            error: function() {
//                gerror_show('Error borrando datos!');
//                gajax_stop('ajax_update');
//            }
//        });
//        return false;
//    }

//    function gAlerts_get(pReaders, pTop) {
//        gajax_start('ajax_update');
//        $('#pnlALEMSG').hide();
//        $.ajax({
//            type: 'POST',
//            url: "hrcAlerts.ashx",
//            data: {
//                action: 'alerts_get',
//                readers: pReaders,
//                top: pTop
//            },
//            success: function(pdata) {
//                var auxContent = '';
//                auxContent += '<table cellspacing="0" cellpadding="2" >';
//                $('#pnlALE').show();
//                if (pdata.length == 0) {
//                    $('#grdAlerts').html('');
//                } else {
//                    for (var auxi = 0; auxi < pdata.length; auxi++) {
//                        auxContent += '<tr id=ale_' + pdata[auxi].alemsgcod + '_' + pdata[auxi].alequecod + ' >';
//                        auxContent += '<td class=alemsg-dsc >';
//                        auxContent += '<a onclick=gAlerts_delete(' + pReaders + ',' + pdata[auxi].alemsgcod + ',' + pdata[auxi].alequecod + ');return false; >';
//                        auxContent += '<img src=imagenes/actdel.png width=12px border=0 alt=Eliminar style=cursor:pointer;cursor:hand;margin-right:5px />';
//                        auxContent += '</a>';
//                        //auxContent += 'ale_' + pdata[auxi].alemsgcod + '_' + pdata[auxi].alequecod;
//                        auxContent += '<img width=12px border=0 alt=' + pdata[auxi].alemsgcod + ' style=margin-right:5px src=imagenes/';
//                        switch (pdata[auxi].alemsglevel) {
//                            case '1': auxContent += 'semaphore_green.png'; break;
//                            case '2': auxContent += 'semaphore_yellow.png'; break;
//                            case '3': auxContent += 'semaphore_red.png'; break;
//                            default: auxContent += 'semaphore_blue.png';
//                        };
//                        auxContent += ' />';
//                        auxContent += '<u>' + pdata[auxi].alemsgdsc + '</u>';
//                        auxContent += '</br>';
//                        var auxdecoded = $('<div >' + pdata[auxi].alemsgobs + '</div>').text();
//                        auxContent += auxdecoded + '<br />';
//                        auxContent += '<span class=alemsg-timegenerated >' + pdata[auxi].alemsgtimegenerated + '</span>';
//                        auxContent += '<hr />' + '</td>';
//                        auxContent += '</tr>';
//                    }
//                };
//                auxContent += '</table>';
//                $('#grdAlerts').html(auxContent);
//                gajax_stop('ajax_update');
//            },
//            error: function() {
//                gajax_stop('ajax_update');
//                gerror_show('Error agregando datos!');
//            }
//        });
//    }

/*    $(document).ready(function() { */ 
//        setTimeout(function() { gAlerts_get(0,0); ; }, 500);          
//    });

</script>
<link href="estilos.css" rel="stylesheet" type="text/css" />   
</head>
<body  >
<form id="form1" runat="server"   >
  <div runat="server" id="fmeAlerts" ></div>                 
</form>
</body>
</html>

