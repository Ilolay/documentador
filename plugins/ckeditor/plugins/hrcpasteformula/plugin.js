/**
 * @license Copyright (c) 2003-2015, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

/**
 * @fileOverview Charts for CKEditor using Chart.js.
 */

/* global alert:false, Chart:false */

'use strict';

// TODO IE8 fallback to a table maybe?
// TODO a11y http://www.w3.org/html/wg/wiki/Correct_Hidden_Attribute_Section_v4
(function () {
    CKEDITOR.plugins.add('hrcpasteformula', {
        // Required plugins
        requires: 'widget,dialog',
        // Name of the file in the "icons" folder
        icons: 'hrcpasteformula',

        // Load library that renders charts inside CKEditor, if Chart object is not already available.
        afterInit: function () {
            var plugin = this;

        },

        // Function called on initialization of every editor instance created in the page.
        init: function (editor) {
            //var plugin = this;
            CKEDITOR.dialog.add('hrcpasteformula', this.path + 'dialogs/hrcpasteformula.js');
           
            editor.widgets.add('hrcpasteformula', {
                button: 'hrcpasteformula',
                // Connect widget with a dialog defined earlier. So our toolbar button will open a dialog window.
               dialog: 'hrcpasteformula',
                // Based on this template a widget will be created automatically once user exits the dialog window.
               template: '<div class="hrcmathformula" ></div>',
                // In order to provide styles (classes) for this widget through config.stylesSet we need to explicitly define the stylable elements.           
                upcast: function (element) {
                    return element.name == 'div' && element.hasClass('hrcmathformula');
                },
                defaults: {
                    contents: '',
                },
                             

                init: function () {
                   // this.setData('contents', this.parts.image.getAttribute('src'));

                },
                data: function () {
                 //   this.parts.contents.setAttribute('src', this.data.imageSrc);

                    //// Also update data-cke-saved-src
                    //this.parts.image.setAttribute('data-cke-saved-src', this.data.imageSrc);

                    //this.parts.contents.setText(this.data.contents);

                },
            });
            editor.addCommand('hrcpasteformula', new CKEDITOR.dialogCommand('hrcpasteformula'));
            editor.ui.addButton && editor.ui.addButton('hrcpasteformula', {
                label: "Formula",
                command: 'hrcpasteformula',
                toolbar: 'insert'
            });
           
         }
    });
})();
