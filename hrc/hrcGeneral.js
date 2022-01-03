/**  v19 se agregó gNumber_round **/
/** v20 se agrego hrcConsole_log y fix de lostfocus
/** v21 se agregó hrcApp_TransferSec
/** v22 TextCounter con pmode numeric

/** Formularios modales **/
function hrcConsole_log(pMsg) {
    if (typeof console == "undefined") {
        this.console = {
            log: function () {
            }, info: function () {
            }, error: function () {
            }, warn: function () {
            }
        };
    }
    console.info(pMsg);
}

function hrcShowModal(pString, pwidth, pheight) {
    hrcConsole_log('modal');
	if (typeof pwidth == "undefined") {pwidth=750;};	
	if (typeof pheight == "undefined") { pheight = 500; }
	var auxWin;
	if (window.showModalDialog) {
	    auxWin = window.showModalDialog(pString + '&timestamp=' + new Date().getTime(), '', 'menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=' + pwidth + 'px;dialogHeight=' + pheight + 'px;');
	}
	else {
	    auxWin = window.open(pString + '&timestamp=' + new Date().getTime(), '', 'modal=yes;directories=no;menubar=no;resizable=yes;toolbar=no;status=no;center=yes;dialogWidth=' + pwidth + 'px;dialogHeight=' + pheight + 'px;');	    
	};
	
	return auxWin;
}


function hrcShowWindowNoModal(pString) {
   return window.open(decodeURIComponent(pString) + '&timestamp=' + new Date().getTime(), '', '');
}
function hrcShowWindowNoModal_WithDecode(pString) {
  return  window.open(decodeURIComponent(pString) + '&timestamp=' + new Date().getTime(), '', '');
}

function gNumber_round(rnum, rlength) {
    var newnumber = Math.round(rnum * Math.pow(10, rlength)) / Math.pow(10, rlength);
    return parseFloat(newnumber); // Output the result to the form field (change for your purposes)
}

function gNumber_pad(number, length) {
    var str = '' + number;
    while (str.length < length) {
        str = '0' + str;
    }
    return str;
}

function calendarShown(sender, args) {
    sender._popupBehavior._element.style.zIndex = 100005;
}
/** Contador de caracteres v3 **/
var auxdiv;
var auxtext;

function textCounter(field, maxlimit,pmode) {
    //pmode=1:= numerico
        getEvent = event.keyCode;
      switch (getEvent) {
        case 9: 
            break;
          default:
              isvalid = false;
              switch (pmode) {
                  case 1:
                      isvalid = (String.fromCharCode(getEvent).match(/^\d+$/) != null);
                      break;
                  default:
                      isvalid = (getEvent != 226);
                      break;
              }
              if (isvalid == true) {
                if (field.value.length > maxlimit) {
                    field.value = field.value.substring(0, maxlimit);
                }
                else {
                    auxtext.value = '(restan ' + (maxlimit - field.value.length) + ' de ' + maxlimit + ')';
                };
            } else {
                auxtext.value = 'No válido (' + getEvent + ')';
                event.returnValue = false;
            };
    };
   
}
    
   
function textCounter_GetFocus(field, maxlimit,pmode) {
    if (auxdiv == null) {
        auxdiv = document.createElement('DIV')
        auxtext = document.createElement('INPUT', 'txtTextCounter')
        auxtext.className = 'error';
        auxtext.style.position = 'absolute';
        auxtext.tabIndex=-1;
        auxtext.style.zIndex = 10000;
        auxdiv.appendChild(auxtext);
    }
    auxdiv.style.position = 'relative';
    textCounter(field, maxlimit,pmode);
    field.offsetParent.appendChild(auxdiv);
    //hrcConsole_log( auxdiv);

}

function textCounter_LostFocus(field) {
    try {
        if (auxdiv) {
            auxdiv.style.visibility = 'hidden';
            field.offsetParent.removeChild(auxdiv);
            auxdiv = null;
        };
        field.value = field.value.replaceAll('<', '[');
        field.value = field.value.replaceAll('>', ']');
        //field.value = field.value.replaceAll(String.fromCharCode(13),'');
    } catch (err) {
    }   
}

var hrcControl_NextFocus;
function gControl_SetNextFocus(pControl) {
    hrcControl_NextFocus = pControl;
}

function gControl_NextFocus() {    
    if (hrcControl_NextFocus != null) {      
        $('#' + hrcControl_NextFocus).focus();
        hrcControl_NextFocus = null;
    }
}

function gControl_SetError(pTitle,pControlID, perror) {
    //alert(perror);
    gControl_SetNextFocus(pControlID);
    var newDiv = $(document.createElement('div'));
    newDiv.html('<p class=error style=font-size:12px>' + perror + '<p>');
    newDiv.dialog({
        modal: true, width: 'auto', title: pTitle,
        buttons: [ { text: "Ok", click: function() { $( this ).dialog( "close" ); } } ] ,
        close: function (event, ui) { gControl_NextFocus(); }
    });
    
}


//Alerts v2
function gAlerts_StartScreenMonitor(pLevel, pCant) {
    gAlerts_SetStatus(pLevel, pCant);
    setInterval(function() { gAlerts_GetStatus(); }, 5000);
}


function gAlerts_SetStatus(pLevel, pCant) {
    if (pCant != null) {
        var auxContent = '';
        var auxFadeEffect = false;
        auxContent += '<img id=hrcAlert_img width=10px border=0 style=margin-left:5px src=imagenes/';
        switch (pLevel) {
            case '1': auxContent += 'semaphore_green.png'; break;
            case '2': auxContent += 'semaphore_yellow.png'; auxFadeEffect = true; break;
            case '3': auxContent += 'semaphore_red.png'; auxFadeEffect = true; break;
            default: auxContent += 'semaphore_blue.png';
        };
        auxContent += ' />';
        auxContent += pCant;
        $('#hrcAlert_alequemsg').html(auxContent);
        if (auxFadeEffect == true) {
            //$('#hrcAlert_alequemsg').html(auxContent + '<div id=hrcAlert_alert style=position:absolute;top:10px;left:400px;height:20px;padding:5px;vertical-align:center;background-color:#cccccc;border-width:1px;border-style:solid;border-color:#333333; >Nuevas alertas</div>');
            $('#hrcAlert_img').fadeOut(250).fadeIn(50);
            //$('#hrcAlert_alert').fadeOut(250).fadeIn(50);
            //$('#hrcAlert_alequemsg').fadeOut(100).fadeIn(100);
        }
    };
}
//Conexión de datos
function gAlerts_GetStatus() {
    $.ajax({
        type: 'POST',
        url: "hrcAlerts.ashx",
        data: {
            action: 'alerts_getstatus',
            top: 0
        },
        success: function(pdata) {
        try {
            if (pdata.length != 0) {
                gAlerts_SetStatus(pdata[0].ALEQUELEVEL, pdata[0].ALEQUEMSGCANT)
            };
        } catch (err) {
        }
        
        },
        error: function() {
            gerror_show('Error agregando datos!');
        }
    });
}

//Ajax
function gajax_start(pcontrol) {
    $('#' + pcontrol).html('<img src=imagenes/ajaxupdate.gif border=0 />');
}
function gajax_stop(pcontrol) {
    $('#' + pcontrol).html('');
}

String.prototype.replaceAll = function(pcFrom, pcTo) {
    var i = this.indexOf(pcFrom);
    var c = this;

    while (i > -1) {
        c = c.replace(pcFrom, pcTo);
        i = c.indexOf(pcFrom);
    }
    return c;
}


function gerror_show(perror) {
    $('#lblerror').html(perror);
}


function hrcgrddata_collapseNode(pNode, pObject) {
    var auxgrddataid = $('#' + pObject.id).attr('grddataid');
    var auxgrddataurl = $('#' + pObject.id).attr('grddataurl');
    $.ajax({
        type: 'POST',
        url: auxgrddataurl,
        data: {
            action: 'grddata_collapsenode',
            grddataid: auxgrddataid,
            grddatacod: pNode
        }
    });
}
function hrcgrddata_expandNode(pNode, pObject) {
    var auxgrddataid = $('#' + pObject.id).attr('grddataid');
    var auxgrddataurl = $('#' + pObject.id).attr('grddataurl');
    $.ajax({
        type: 'POST',
        url: auxgrddataurl,
        data: {
            action: 'grddata_expandnode',
            grddataid: auxgrddataid,
            grddatacod: pNode
        }
    });
}


/* Panel Modal */
function hrcPanelModal_Show(pID) {
    var bgdiv = $('<div>').attr({
    className: 'bgmodalpopup',
        id: 'bgmodalpopup'
    });
    $('body').append(bgdiv);
    var wscr = $(window).width();
    var hscr = $(window).height();
    $('#bgmodalpopup').css("width", wscr);
    $('#bgmodalpopup').css("height", hscr);
    var auxID = 'bgmodal_' + pID;
    var moddiv = $('<div>').attr({
        className: 'bgmodal',
        id: auxID
    });

    $('body').append(moddiv);

    var contenidoHTML = $('#' + pID).html();
    $('#' + pID).Show();
    /*contenidoHTML.show();*/
    $('#' + auxID).append(contenidoHTML);
    $(window).resize();
}

function hrcPanelModal_Hide(pID) {
    // removemos divs creados
    var auxID = 'bgmodal_' + pID;
    $('#' + auxID).remove();
    $('#bgmodalpopup').remove();
}

function hrcShowWindowNewTab(pUrl) {
    //window.location.href = pUrl;
    //var auxWindow = window.open(pUrl, "aa");
    getMainWindow().open(pUrl, "_blank");
}


// Call stack code
function showCallStack() {
    var f = showCallStack, result = "Call stack:\n";

    while ((f = f.caller) !== null) {
        var sFunctionName ="->>" + f.toString().match(/^function (\w+)\(/)
        sFunctionName = (sFunctionName) ? sFunctionName[1] : 'anonymous function';
        result += sFunctionName;
        result += getArguments(f.toString(), f.arguments);
        result += "\n";

    }
    alert(result);
}


function getArguments(sFunction, a) {
    var i = sFunction.indexOf(' ');
    var ii = sFunction.indexOf('(');
    var iii = sFunction.indexOf(')');
    var aArgs = sFunction.substr(ii + 1, iii - ii - 1).split(',')
    var sArgs = '';
    for (var i = 0; i < a.length; i++) {
        var q = ('string' == typeof a[i]) ? '"' : '';
        sArgs += ((i > 0) ? ', ' : '') + (typeof a[i]) + ' ' + aArgs[i] + ':' + q + a[i] + q + '';
    }
    return '(' + sArgs + ')';
}



/* Fixes z-index issues - Version 1.2
Update v1.2 - fixed an issue when this is being called on an element that is not in the DOM tree.

This plugin fixes z-index issues typically occur in IE. It traverses up the DOM to make sure that all the ancestors of the target element have a high z-index value.

This fixes a common z-index issue that an element with high z-index is showing underneath its ancestor's sibling that has the same explicit or implicit (e.g. IE's implied zero z-index when position: relative is applied) z-index as the corresponding ancestor.
For IE 6's z-index issues with input boxes, see the jQuery bgiframe plugin.

www.davidtong.me/z-index-misconceptions-bugs-fixes/

param obj:
recursive: boolean (default: true) - set to false to reduce the number of loops and if it still works, go for false
exclude: string - a list of class names to be excluded in the fix
msieOnly: boolean (default: false) - set to true to only apply the fix to IE browsers
zIndex: string (default: '9999') - a big number that should just be high enough to make the element-to-be-fixed stay on top of other elements
*/
(function($) {
    $.fn.fixZIndex = function (params) {
        hrcConsole_log('zindex' + params);
        params = params || {};
        if (params.msieOnly && !$.browser.msie) return this;
        var num_of_jobj = this.length;
        for (var i = num_of_jobj; i--; ) {
            var curr_element = this[i];
            var config_recursive = params.recursive || true;
            var config_exclude = params.exclude || null;
            while (curr_element != document.body) {
                if (!$(curr_element).hasClass(config_exclude) && ($(curr_element).css('position') == 'relative' || $(curr_element).css('position') == 'absolute')) {
                    if ($.data(curr_element, 'zIndex') == undefined) {
                        $.data(curr_element, 'zIndex', curr_element.style.zIndex || '-1');
                    }
                    curr_element.style.zIndex = params.zIndex || '9999';
                }
                curr_element = curr_element.parentNode;
                if (!config_recursive) break;
            }
        }
        return this;
    };

    // optional function to restore z-index if needed
    $.fn.restoreZIndex = function(params) {
        params = params || {};
        if (params.msieOnly && !$.browser.msie) return this;
        var num_of_jobj = this.length;
        for (var i = num_of_jobj; i--; ) {
            var curr_element = this[i];
            var config_exclude = params.exclude || null;
            while (curr_element && curr_element != document.body) {
                var currZIndex = $.data(curr_element, 'zIndex');
                if (currZIndex > -1 && !$(curr_element).hasClass(config_exclude)) {
                    curr_element.style.zIndex = currZIndex;
                    $.removeData(curr_element, 'zIndex');
                }
                else if (currZIndex == -1) {
                    curr_element.style.zIndex = '';
                }
                curr_element = curr_element.parentNode;
            }
        }
        return this;
    };
})(jQuery);


function getQuerystring() {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('?') + 1).split('&');
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars;
}

// jgQgrid
$(function() {
    try {
        if ($.fn.jqGrid != "undefined") {
            if ($.fn.jqGrid.expandNode != "undefined") {
                var orgExpandNode = $.fn.jqGrid.expandNode, orgCollapseNode = $.fn.jqGrid.collapseNode;
                $.jgrid.extend({
                    expandNode: function(pdata) {
                        //alert('before expandNode: rowid="' + rc._id_ + '", name="' + rc.name + '"');
                        this.trigger('expandNode', pdata);
                        orgExpandNode.call(this, pdata);

                    },
                    collapseNode: function(pdata) {
                        //alert('before collapseNode: rowid="' + rc._id_ + '", name="' + rc.name + '"');
                        orgCollapseNode.call(this, pdata);
                        this.trigger('collapseNode', pdata);
                    }
                })
            };
        }
    } catch (err) {

    }
})
