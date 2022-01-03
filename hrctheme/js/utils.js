 var glToolBoxShow=0;
    function gToolBox_Toogle(pClose){
    hrcConsole_log(glToolBoxShow);
	switch (glToolBoxShow) {
	    case 0:
	        /*abre*/
	        $('#tools_body').show();
	        $('#tools_title').show();
	        glToolBoxShow = 1;
	        $('.tools').animate({
	            right: 0
	        }, 500, function () {
	            glToolBoxShow = 2;
	        });
	        
	        break;
	    case 2:
            /*cierra*/
	        glToolBoxShow = 3;
	        $('.tools').animate({
	            right: -250
	        }, 500, function () {
	            $('#tools_body').hide();
	            $('#tools_title').hide();
	            glToolBoxShow = 0;
	        });
	        break;
        };
	hrcConsole_log(glToolBoxShow + '-end');
    };
$(document).ready(function() {

    $("ul.tabs").tabs("div.tabs-document > div");

    $(".slider .navi").tabs(".images > .slide", {
        effect: 'fade',
        fadeOutSpeed: "slow",
        rotate: true
    }).slideshow();
    $('#tools_body').hide();
    $('#tools_title').hide();
    $(".polls .navi").tabs(".poll-set > .poll", {
        rotate: true
    }).slideshow();
   
    $('.tool-vertical-tab').click(function() {
	gToolBox_Toogle(false);	
    });
        $('.tools').mouseleave(function() {
	        gToolBox_Toogle(true)
        })
    /*
    cuando era con tabla
    $('.tool-vertical-tab').click(function(){
    $(this).toggleClass('opened');
    if($(this).hasClass('opened')){
    $(this).parent().animate({
    right: 0
    }).parent().animate({
    width:300
    })   
    }else{
    $(this).parent().animate({
    right: -250
    }).parent().animate({
    width:40
    });
    }

    });
    */

    $('.input-results').click(function() {
        $(this).children('.results').show()
    }).mouseleave(function() {
        $(this).children('.results').delay(200).hide();
    });


    /*  tablesorter  
    // call the tablesorter plugin 
    $("table").tablesorter({
        headers: {
            // assign the secound column (we start counting zero) 
            0: {
                sorter: false
            },
            1: {
                sorter: false
            },
            2: {
                sorter: false
            }
        }
    });
*/

    /* Overlay */
    $(".overlay-trigger[rel]").overlay({
        mask: {
            color: '#000000',
            opacity: 0.7
        },
        closeOnClick: false
    });

    //    // built-in localization
    //      $.tools.dateinput.localize("es", {
    //      months: 'Enero,Febrero,Marzo,Abril,Mayo,Junio,Julio,Agosto,Septimbre,Octubre,Noviembre,Deciembre',
    //      shortMonths:  'Ene,Feb,Mar,Abr,May,Jun,Jul,Ago,Sep,Oct,Nov,Dic',
    //      days:         'Domingo,Lunes,Martes,Miercoles,Jueves,Viernes,Sabado',
    //      shortDays:    'Dom,Lun,Mar,Mie,Jue,Vie,Sab'
    //      });

    //    $(".datepicker").dateinput({
    //        lang: 'es'
    //    });
})

$(".polls .navi").tabs(".poll-set > .poll", {


    // start from the beginning after the last tab
    rotate: true

    // use the slideshow plugin. It accepts its own configuration
}).slideshow();


//Sharepoint

(function () {
    if (typeof (_spBodyOnLoadFunctions) === 'undefined' || _spBodyOnLoadFunctions === null) {
        return;
    }
    _spBodyOnLoadFunctions.push(function () {

        if (typeof (SPClientTemplates) === 'undefined' || SPClientTemplates === null || (typeof (APD_InAssetPicker) === 'function' && APD_InAssetPicker())) {
            return;
        }

        var registerOverrideToHideSocialActions = function (id) {
            var socialactionsOverridePostRenderCtx = {};
            socialactionsOverridePostRenderCtx.BaseViewID = 'Callout';
            socialactionsOverridePostRenderCtx.ListTemplateType = id;
            socialactionsOverridePostRenderCtx.Templates = {};
            socialactionsOverridePostRenderCtx.Templates.Footer = function (renderCtx) {
                var renderECB;
                if (typeof (isSharedWithMeView) === 'undefined' || isSharedWithMeView === null) {
                    renderECB = true;
                } else {
                    var viewCtx = getViewCtxFromCalloutCtx(renderCtx);
                    renderECB = !isSharedWithMeView(viewCtx);
                }
                // By setting a null value as 2nd parameter, we do not specify additional callout actions.
                return CalloutRenderCustomFooterTemplate(renderCtx, null, renderECB);
            };
            SPClientTemplates.TemplateManager.RegisterTemplateOverrides(socialactionsOverridePostRenderCtx);
        };
        hrcConsole_log('ook...');
        // Hide actions for default Document Libraries
        registerOverrideToHideSocialActions(101);
        // Hide actions for the document library on your My Site
        registerOverrideToHideSocialActions(700);

        function CalloutRenderCustomFooterTemplate(renderCtx, calloutActionMenuPopulator, renderECB) {
            if (typeof calloutActionMenuPopulator === 'undefined' || calloutActionMenuPopulator === null) {
                calloutActionMenuPopulator = CalloutOnPostRenderCustomTemplate;
            }
            if (typeof renderECB === 'undefined' || renderECB === null) {
                renderECB = true;
            }
            var calloutID = GetCalloutElementIDFromRenderCtx(renderCtx);

            AddPostRenderCallback(renderCtx, function () {
                var calloutActionMenu = new CalloutActionMenu(calloutID + '-actions');

                calloutActionMenuPopulator(renderCtx, calloutActionMenu);
                calloutActionMenu.render();
            });
            var ecbMarkup = [];

            if (renderECB) {
                ecbMarkup.push('<span id=' + StAttrQuote(calloutID + '-ecbMenu') + ' class="js-callout-actions js-callout-ecbActionDownArrow">');
                ecbMarkup.push(RenderECBinline(renderCtx, renderCtx.CurrentItem, renderCtx.CurrentFieldSchema));
                ecbMarkup.push('</span>');
            }
            return Callout.GenerateDefaultFooter(calloutID, ecbMarkup.join(''));
        }

        function CalloutOnPostRenderCustomTemplate(renderCtx, calloutActionMenu) {
            var listItem = renderCtx.CurrentItem;
            var openText = GetCallOutOpenText(listItem, renderCtx);

            calloutActionMenu.addAction(new CalloutAction({
                text: openText,
                onClickCallback: function (calloutActionClickEvent, calloutAction) {
                    CalloutAction_Open_OnClick(calloutActionClickEvent, calloutAction, renderCtx);
                }
            }));
        }
    });
})();

function SP_ListItemsMenuIsEnabled(pItems) 
{ 
    var items = pItems;
    var auxReturn = false;
    var ci = CountDictionary(items);
    return (ci == 1);
} 

function SP_ListItems_getFirst(pItems) {
    var auxReturn = '';
    auxReturn = SP.ListOperation.Selection.getSelectedItems()[0].id;
    return auxReturn
}