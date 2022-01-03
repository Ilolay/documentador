/*
_intelimedia version 28 - 14/7/2015
    
    
* AutoSuggest
* Copyright 2009-2010 Drew Wilson
* www.drewwilson.com
* code.drewwilson.com/entry/autosuggest-jquery-plugin
*
* Version 1.4   -   Updated: Mar. 23, 2010
*
* This Plug-In will auto-complete or auto-suggest completed search queries
* for you as you type. You can add multiple selections and remove them on
* the fly. It supports keybord navigation (UP + DOWN + RETURN), as well
* as multiple AutoSuggest fields on the same page.
*
* Inspied by the Autocomplete plugin by: Jšrn Zaefferer
* and the Facelist plugin by: Ian Tearle (iantearle.com)
*
* This AutoSuggest jQuery plug-in is dual licensed under the MIT and GPL licenses:
*   http://www.opensource.org/licenses/mit-license.php
*   http://www.gnu.org/licenses/gpl.html
*/


(function ($) {
    $.fn.autoSuggest = function (data, options) {
        

        var values_input = null;
        var opts = null;
        var org_li = null;
        var input_focus = false;
        var autosuggest_current_tmpid = '';
        var input = null;
        var raw_data = null;
        var selections_holder = null;
        var defaults = {
            additem_enabled: true,
            asHtmlID: false,
            startText: "Enter Name Here",
            onlydeleteText: "",
            emptyText: "No Results Found",
            preFill: {},
            limitText: "No More Selections Are Allowed",
            selectedItemProp: "value", //name of object property
            selectedValuesProp: "value", //name of object property
            searchObjProps: "value", //comma separated list of object property names
            queryParam: "q",
            retrieveLimit: false, //number for 'limit' param on ajax request
            extraParams: "",
            matchCase: false,
            controlid: '',
            minChars: 1,
            keyDelay: 400,
            readonly: false,
            resultsHighlight: true,
            neverSubmit: false,
            selectionLimit: false,
            showResultList: true,
            start: function () { },
            selectionClick: function (elem) { },
            formatList: false, //callback function
            beforeRetrieve: function (string) { return string; },
            retrieveComplete: function (data) { return data; },
            resultClick: function (data) { },
            resultsComplete: function () { }
        };
      
        var auxcontrolid = options.asHtmlID.replace("_autosuggest", "");
        options.controlid = auxcontrolid;
       
        opts = $.extend(defaults, options);

        var d_type = "object";
        var d_count = 0;
        if (typeof data == "string") {
            d_type = "string";
            var req_string = data;
        } else {
            var org_data = data;
            for (k in data) if (data.hasOwnProperty(k)) d_count++;
        }
      
        if ((d_type == "object" && d_count > 0) || d_type == "string") {
            return this.each(function (x) {
                if (!opts.asHtmlID) {
                    x = x + "" + Math.floor(Math.random() * 100); //this ensures there will be unique IDs on the page if autoSuggest() is called multiple times
                    var x_id = "as-input-" + x;
                } else {
                    x = opts.asHtmlID;
                    var x_id = x;
                }

                opts.start.call(this);
                //input = $(this);
                input = $(opts.controlid);
                if (opts.readonly == false) {
                    //if (opts.selectionLimit && $("li.as-selection-item", selections_holder).length > opts.selectionLimit) {
                    //    gInput_Hide($(this), auxcontrolid, opts.onlydeleteText);
                    //    gInput_Show($(this), auxcontrolid, opts.startText);
                    //} else {
                        
                    //};
                    gInput_Show($(this), auxcontrolid, opts.startText);
                    gInput_SetWriteMode(auxcontrolid);
                } else {
                    gInput_Hide($(this), auxcontrolid, opts.onlydeleteText);
                    startText = "";
                    gInput_SetReadMode(auxcontrolid);
                };
                // Setup basic elements and render them to the DOM
                //input.wrap('<ul class="as-selections" id="as-selections-' + x + '"></ul>').wrap('<li class="as-original" id="as-original-' + x + '"></li>');
                input.wrap('<ul class="as-selections" id="as-selections-' + x + '"></ul>').wrap('<li class="as-original" id="as-original-' + x + '"></li>');
                selections_holder = $("#as-selections-" + x);
                org_li = $("#as-original-" + x);
                /*hrcConsole_log('b' + x + org_li.html());*/
                var results_holder = $('<div class="as-results" id="as-results-' + x + '"></div>').hide();
                var results_ul = $('<ul class="as-list" id="as-list-' + x + '" ></ul>');
                //var results_ul = $('<ul></ul>');
                values_input = $('<input type="hidden" class="as-values" name="as_values_' + x + '" id="as-values-' + x + '" />');
                var prefill_value = "";
                if (typeof opts.preFill == "string") {
                    var vals = opts.preFill.split(",");
                    for (var i = 0; i < vals.length; i++) {
                        var v_data = {};
                        v_data[opts.selectedValuesProp] = vals[i];
                        if (vals[i] != "") {
                            /*$.autoSuggest.add_selected_item(input, values_input,opts, v_data, "000" + i);*/
                            //hrcConsole_log('Autosugest-adding2');
                            add_selected_item(v_data, "000" + i);
                        }
                    }
                    prefill_value = opts.preFill;
                } else {
                    prefill_value = "";
                    var prefill_count = 0;
                    for (k in opts.preFill) if (opts.preFill.hasOwnProperty(k)) prefill_count++;
                    if (prefill_count > 0) {
                        for (var i = 0; i < prefill_count; i++) {
                            var new_v = opts.preFill[i][opts.selectedValuesProp];
                            if (new_v == undefined) { new_v = ""; }
                            prefill_value = prefill_value + new_v + ",";
                            if (new_v != "") {
                                /*$.autoSuggest.add_selected_item(input, values_input, opts, opts.preFill[i], "000" + i);*/
                                //hrcConsole_log('Autosugest-adding3');
                                add_selected_item(opts.preFill[i], "000" + i);
                            }
                        }
                    }
                }
                if (prefill_value != "") {
                    input.val("");
                    var lastChar = prefill_value.substring(prefill_value.length - 1);
                    if (lastChar != ",") { prefill_value = prefill_value + ","; }
                    values_input.val("," + prefill_value);
                    $("li.as-selection-item", selections_holder).addClass("blur").removeClass("selected");
                }
                input.after(values_input);
                //Va al final porque tiene que estar creadoe el input
                gInput_SetLimit(auxcontrolid, opts.selectionLimit);
                selections_holder.click(function () {
                    input_focus = true
                }).mousedown(function () { input_focus = false; }).after(results_holder);

                var timeout = null;
                var prev = "";
                var totalSelections = 0;
                var tab_press = false;
                input.constructor.prototype.add_selected_item = function (pdata, pnum) {
                    //hrcConsole_log('Autosugest-adding4');
                    var auxControlid = opts.controlid;
                    add_selected_item(pdata, pnum)
                };
                input.constructor.prototype.items_clear = function (pdata, pnum) {
                    var auxControlid = 'as-selections-' + pdata['OBJID'] + '_autosuggest';
                    $("#" + auxControlid + " li.as-selection-item").each(function () {
                        $(this).remove();
                        var auxRemoved = {};
                       // auxRemoved[0] = $(this).attr("HRCTYPE");
                        //auxRemoved[2] = $(this).attr("HRCCOD");
                        auxRemoved[0] = $(this);
                        auxRemoved[1] = pdata['OBJID'];
                        auxRemoved[2] = pdata['OBJID'];
                        var auxcontrolid = pdata['OBJID'] + '_events';
                        $('#' + auxcontrolid).trigger('eventselectionRemoved', auxRemoved);
                    });
                    //var auxreadonly = false;
                    //if ($('#' + pdata['OBJID']).attr("hrcreadonly") == "true") {
                    //    auxreadonly = true;
                    //};
                    //if (auxreadonly==false){
                    gInput_Show($(this), pdata['OBJID'], opts.startText);
                    //};
                };
                input.constructor.prototype.set_readonly = function (pdata, pnum) {
                    var auxControlid = pdata['OBJID'];//+ '_autosuggest';
                    opts.readonly = true;
                    gInput_SetReadMode(pdata['OBJID']);
                    gInput_Hide($(this), auxControlid, '');
                };
                input.constructor.prototype.set_readwrite = function (pdata, pnum) {
                    var auxControlid = 'as-selections-' + pdata['OBJID'] + '_autosuggest';
                    opts.readonly = false;
                    gInput_SetWriteMode(pdata['OBJID']);
                    gInput_Show($(this), pdata['OBJID'], '');
                };
                // Handle input field events
                input.focus(function () {
                    var auxControlid = $(this).attr('id');
                    input = $('#' + auxControlid);
                    org_li = $('#as-original-' + auxControlid);
                    values_input = $('#as-values-' + auxControlid);
                    $(this).val("");
                    if ($(this).val() == opts.startText && values_input.val() == "") {
                        // $(this).val("");
                    } else if (input_focus) {
                        $("li.as-selection-item", selections_holder).removeClass("blur");
                        if ($(this).val() != "") {
                            results_ul.css("width", selections_holder.outerWidth());
                            results_holder.show();
                            results_ul.show(); /**/
                        }
                    }
                    input_focus = true;
                    return true;
                }).blur(function () {
                    if (opts.selectionLimit && $("li.as-selection-item", selections_holder).length >= opts.selectionLimit) {
                        /*$(this).val(opts.onlydeleteText);
                        input.attr("autocomplete", "off").css("border-bottom-width", "0px").css("height", "0px");*/
                        gInput_Hide($(this), $(this).attr('id'), opts.onlydeleteText);
                    } else {
                        /*input.attr("autocomplete", "on").removeAttr('style');
                        $(this).val(opts.startText);*/
                        gInput_Show($(this), $(this).attr('id'), opts.startText);
                    };
                    if ($(this).val() == "" && values_input.val() == "" && prefill_value == "") {
                        //$(this).val(opts.startText);
                    } else if (input_focus) {
                        $("li.as-selection-item", selections_holder).addClass("blur").removeClass("selected");
                        results_holder.hide();
                    }
                }).keydown(function (e) {
                    // track last key pressed
                    lastKeyPressCode = e.keyCode;
                    first_focus = false;
                    switch (e.keyCode) {
                        case 38: // up
                            e.preventDefault();
                            moveSelection("up");
                            break;
                        case 40: // down
                            e.preventDefault();
                            moveSelection("down");
                            break;
                        case 8:  // delete
                            if (input.val() == "") {
                                var last = values_input.val().split(",");
                                last = last[last.length - 2];
                                selections_holder.children().not(org_li.prev()).removeClass("selected");
                                if (org_li.prev().hasClass("selected")) {
                                    values_input.val(values_input.val().replace("," + last + ",", ","));
                                    var auxRemoved = {};
                                    auxRemoved[0] = org_li.prev();
                                    auxRemoved[2] = values_input.attr('id');
                                    var auxcontrolid = values_input.attr('id').substring(10, 46) + '_events';
                                    $('#' + auxcontrolid).trigger('eventselectionRemoved', auxRemoved);
                                    input.attr("autocomplete", "on").removeAttr('style');
                                } else {
                                    opts.selectionClick.call(this, org_li.prev());
                                    org_li.prev().addClass("selected");
                                }
                            }
                            if (input.val().length == 1) {
                                results_holder.hide();
                                prev = "";
                            }
                            if ($(":visible", results_holder).length > 0) {
                                if (timeout) { clearTimeout(timeout); }
                                timeout = setTimeout(function () { keyChange(); }, opts.keyDelay);
                            }
                            break;
                        case 9: case 188:  // tab or comma
                            tab_press = true;
                            var i_input = input.val().replace(/(,)/g, "");
                            if (i_input != "" && values_input.val().search("," + i_input + ",") < 0 && i_input.length >= opts.minChars) {
                                e.preventDefault();
                                var n_data = {};
                                n_data[opts.selectedItemProp] = i_input;
                                n_data[opts.selectedValuesProp] = i_input;
                                var lis = $("li", selections_holder).length;
                                /*$.autoSuggest.add_selected_item(n_data, "00" + (lis + 1));*/
                                input.val("");
                            }
                        case 13: // return
                            tab_press = false;
                            var active = $("li.active:first", results_holder);
                            if (active.length > 0) {
                                active.click();
                                results_holder.hide();
                            }
                            if (opts.neverSubmit || active.length > 0) {
                                e.preventDefault();
                            }
                            break;
                        default:
                            if (opts.showResultList) {
                                if (opts.selectionLimit && $("li.as-selection-item", selections_holder).length >= opts.selectionLimit) {
                                    results_ul.html('<li class="as-message">' + opts.limitText + '</li>');
                                    results_holder.show();
                                    results_ul.show(); /**/
                                } else {
                                    if (timeout) { clearTimeout(timeout); }
                                    timeout = setTimeout(function () { keyChange(); }, opts.keyDelay);
                                }
                            }
                            break;
                    }
                });

                function keyChange() {
                    // ignore if the following keys are pressed: [del] [shift] [capslock]
                    if (lastKeyPressCode == 46 || (lastKeyPressCode > 8 && lastKeyPressCode < 32)) { return results_holder.hide(); }
                    input = $('#' + opts.controlid);
                    var string = input.val().replace(/[\\]+|[\/]+/g, "");
                    if (string == prev) return;
                    prev = string;
                    if (string.length >= opts.minChars) {
                        selections_holder.addClass("loading");
                        if (d_type == "string") {
                            var limit = "";
                            if (opts.retrieveLimit) {
                                limit = "&limit=" + encodeURIComponent(opts.retrieveLimit);
                            }
                            if (opts.beforeRetrieve) {
                                string = opts.beforeRetrieve.call(this, string);
                            }
                            $.getJSON(req_string + "?" + opts.queryParam + "=" + encodeURIComponent(string) + limit + opts.extraParams, function (data) {
                                d_count = 0;
                                var new_data = opts.retrieveComplete.call(this, data);
                                for (k in new_data) if (new_data.hasOwnProperty(k)) d_count++;
                                processData(new_data, string);
                            });
                        } else {
                            if (opts.beforeRetrieve) {
                                string = opts.beforeRetrieve.call(this, string);
                            }
                            processData(org_data, string);
                        }
                    } else {
                        selections_holder.removeClass("loading");
                        results_holder.hide();
                    }
                }
                var num_count = 0;
                function processData(data, query) {
                    if (!opts.matchCase) { query = query.toLowerCase(); }
                    var matchCount = 0;
                    results_holder.html(results_ul.html("")).hide();
                    //cuando esta oculto para evitar que se muestre cuando se mueve
                    results_ul.hide();                    
                    results_ul.css("padding-right", "10px");
                    for (var i = 0; i < d_count; i++) {
                        var num = i;
                        num_count++;
                        //cambio. Siempre muestra lo que devuelve JSON
                        var forward = true;
                        if (forward) {
                            var formatted = $('<li class="as-result-item" id="as-result-item-' + num + '"></li>').click(function () {
                                raw_data = $(this).data("data");
                                var number = raw_data.num;
                                if ($("#as-selection-" + number, selections_holder).length <= 0 && !tab_press) {
                                    var pdata = raw_data.attributes;
                                    var auxcontrolid = x.replace("_autosuggest", "");
                                    pdata['OBJID'] = auxcontrolid;
                                    prev = "";
                                    /*$.autoSuggest.add_selected_item(input, values_input, opts, data, number);*/
                                    add_selected_item(pdata, number);
                                    opts.resultClick.call(this, raw_data);
                                    results_holder.hide();
                                }
                                tab_press = false;
                            }).mousedown(function () { input_focus = false; }).mouseover(function () {
                                $("li", results_ul).removeClass("active");
                                $(this).addClass("active");
                            }).data("data", { attributes: data[num], num: num_count });
                            var this_data = $.extend({}, data[num]);
                            if (!opts.matchCase) {
                                var regx = new RegExp("(?![^&;]+;)(?!<[^<>]*)(" + query + ")(?![^<>]*>)(?![^&;]+;)", "gi");
                            } else {
                                var regx = new RegExp("(?![^&;]+;)(?!<[^<>]*)(" + query + ")(?![^<>]*>)(?![^&;]+;)", "g");
                            }

                            if (opts.resultsHighlight) {
                                this_data[opts.selectedItemProp] = this_data[opts.selectedItemProp].replace(regx, "<em>$1</em>");
                            }
                            if (!opts.formatList) {
                                formatted = formatted.html(this_data[opts.selectedItemProp]);
                            } else {
                                formatted = opts.formatList.call(this, this_data, formatted);
                            }
                            results_ul.append(formatted);
                            delete this_data;
                            matchCount++;
                            if (opts.retrieveLimit && opts.retrieveLimit == matchCount) { break; }
                        }
                    }
                    selections_holder.removeClass("loading");
                    if (matchCount <= 0) {
                        results_ul.html('<li class="as-message">' + opts.emptyText + '</li>');
                    };
                    hrcConsole_log(selections_holder.outerWidth());
                    //  results_ul.css("overflow-y", "scroll"); //no utilizar- se utiliza enscroll
                    results_ul.css("width", selections_holder.outerWidth() + 15);                                    
                    results_holder.show();
                    results_ul.show();
                    $('#as-list-' + x).enscroll({
                        showOnHover: false,
                        verticalTrackClass: 'hrcobjectexplorer_scrollbar_track',
                        verticalHandleClass: 'hrcobjectexplorer_scrollbar_handle',
                        zIndex: 2001,
                        easingDuration: 0
                    });
                    $('.as-list').fixZIndex({ zIndex: 999 });
                    $('#as-list-' + x).fixZIndex({ zIndex: 2000 });                    
                    opts.resultsComplete.call(this);
                }

                function moveSelection(direction) {
                    if ($(":visible", results_holder).length > 0) {
                        var lis = $("li", results_holder);
                        if (direction == "down") {
                            var start = lis.eq(0);
                        } else {
                            var start = lis.filter(":last");
                        }
                        var active = $("li.active:first", results_holder);
                        if (active.length > 0) {
                            if (direction == "down") {
                                start = active.next();
                            } else {
                                start = active.prev();
                            }
                        }
                        lis.removeClass("active");
                        start.addClass("active");
                    }
                }

                function add_selected_item(pdata, num) {
                    if (opts.additem_enabled == false) {
                    } else {
                        var auxControlid = pdata['OBJID'];
                        selections_holder = $("#as-selections-" + auxControlid + '_autosuggest');
                        input = $('#' + auxControlid + '_autosuggest');
                        values_input = $('#as-values-' + auxControlid + '_autosuggest');
                        org_li = $('#as-original-' + auxControlid + '_autosuggest');
                        $('#as-list-' + auxControlid + '_autosuggest').restoreZIndex();
                        var auxCod = pdata['HRCCOD'];
                        var auxType = pdata['HRCTYPE'];
                        if (num < 0) {
                            num = data['HRCTYPE'] * 1000000 + pdata['HRCCOD'];
                        }
                        values_input.val(values_input.val() + pdata[opts.selectedValuesProp] + ",");
                        values_input.restoreZIndex();
                        var auxShowAutosuggest = false;
                        var auxreadonly = false;
                        if ($('#' + auxControlid).attr("hrcreadonly") == "true") {
                            auxreadonly = true;
                        };
                        var auxItemsLimit = $('#' + auxControlid).attr("hrcselectionlimit");
                        var auxItemsCount = ($("li.as-selection-item", selections_holder).length + 1);
                        if (auxreadonly == false) {
                            //calcula +1                        
                            if (opts.selectionLimit == false || (auxItemsCount < auxItemsLimit)) {
                                auxShowAutosuggest = true;
                            }
                        } else {
                        };
                        /*hrcConsole_log('Autosugest-adding:' + auxControlid + '-' + auxreadonly + '-' + auxItemsLimit + '-' + auxItemsCount + '-' + auxShowAutosuggest.toString());*/
                        //'var auxreadonly = ~input.is(":visible");
                        var item = $('<li class="form-control-autosuggest-item as-selection-item " HRCTYPE="' + auxType + '" HRCCOD="' + auxCod + '" id="as-selection-' + num + '"></li>').click(function () {
                            opts.selectionClick.call(this, $(this));
                            selections_holder.children().removeClass("selected");
                            $(this).addClass("selected");
                        }).mousedown(function () { input_focus = false; });
                        if (auxreadonly == false) {
                            //var auxcontrolid = values_input.attr('id').substring(10, 46);
                            //var auxcontrolid = x.replace("_autosuggest", "");
                            //alert(auxreadonly);
                            var close = $('<a class="as-close" TMPID=' + auxControlid + ' >&times;</a>').click(function () {
                                var auxNodeID = pdata[opts.selectedValuesProp];
                                var auxControlid = $(this).attr('TMPID');
                                values_input = $('#as-values-' + auxControlid + '_autosuggest');
                                values_input.val(values_input.val().replace(auxNodeID + ",", ""));
                                gInput_Show($(this), auxControlid, opts.startText);
                                input_focus = true;
                                input.focus();
                                var auxRemoved = {};
                                auxRemoved[0] = item;
                                auxRemoved[1] = auxControlid;
                                auxRemoved[2] = auxControlid;
                                var auxcontrolid_events = auxControlid + '_events';
                                $('#' + auxcontrolid_events).trigger('eventselectionRemoved', auxRemoved);
                                return false;
                            });
                        } else {
                            var close = $('');
                        };
                        auxItem = '<a href=#>';
                        if (auxType > 0) {
                            auxItem += '<img src=imagenes/icon' + gNumber_pad(auxType, 8) + '.png border=0 width=12px >';
                        };                          
                        auxItem += pdata[opts.selectedItemProp] + '</a>';
                        org_li.before(item.html(auxItem).prepend(close));
                        selections_holder.restoreZIndex();
                        /*hrcConsole_log('aca' + auxShowAutosuggest);*/
                        if (auxShowAutosuggest) {
                            gInput_Show($(this), pdata['OBJID'], opts.startText);
                            input.val("").focus();
                        } else {
                            gInput_Hide($(this), auxControlid, '');
                        };
                    }
                    if (pdata['INITIAL'] != '1') {
                        var auxAdded = {};
                        auxAdded[0] = item;
                        auxAdded[1] = pdata[opts.selectedItemProp];
                        auxAdded[2] = auxControlid;
                        $('#' + auxControlid + '_events').trigger('eventselectionAdded', auxAdded);
                    }
                }

                function gInput_SetLimit(pcontrolID, pLimit) {
                    /*hrcConsole_log("Autosuggest:" + pcontrolID + "-Limit:" + pLimit);*/
                    input = $('#' + pcontrolID);
                    input.attr("hrcselectionlimit", pLimit);
                }

                function gInput_SetReadMode(pcontrolID) {
                    input = $('#' + pcontrolID);
                    input.attr("hrcreadonly", "true");
                }

                function gInput_SetWriteMode(pcontrolID) {
                    input = $('#' + pcontrolID);
                    input.attr("hrcreadonly", "false");
                }

                function gInput_Hide(pControl, pcontrolID, pMessage) {
                    var auxControlid = pcontrolID;
                    input = $('#' + auxControlid);
                    input.val('');
                    input.hide();
                    //input.attr("hrcreadonly", "true");
                }

                function gInput_Show(pControl, pcontrolID, pMessage) {
                    var auxControlid = pcontrolID;
                    input = $('#' + auxControlid);
                    input.show();
                    input.val(pMessage);
                    //input.attr("hrcreadonly", "false");

                    /*                    input.focus();
                    input.attr("autocomplete", "off").addClass("as-input").attr("id", x_id).val(opts.startText);
                    input.attr("autocomplete", "on").removeAttr('style');*/
                }
            });
        }
    };


})(jQuery);

