CKEDITOR.dialog.add('hrcpasteformula', function (editor) {
    return {
        title: 'Formula',
        minWidth: 400,
        minHeight: 200,
        contents:
        [
            {
                id: 'general',
                label: 'Settings',
                elements:
                [
                   {
                       type: 'textarea',
                       id: 'contents',
                       label: 'Formula',
                       validate: CKEDITOR.dialog.validate.notEmpty('Ingrese un contenido'),
                       required: true,
                       setup: function (widget) {
                           var auxvalue = widget.wrapper.$.innerText;
                           widget.setData('contents', auxvalue);
                           var auxValue = $('<div>').html(widget.data.contents).text()
                          // hrcConsole_log(auxValue + widget.data);
                           this.setValue(auxvalue);
                       },
                       commit: function (data) {
                         //  hrcConsole_log(widget);
                           data.contents = this.getValue();
                           //if (widget != null) {
                           //  //  widget.setData('contents', this.getValue());
                           //} else {
                           //    data.contents = this.getValue();
                           //}
                           
                       }
                   }
                ]
            }
        ],
        onShow: function () {
            var widget = editor.widgets.focused;
            if (!widget) {
                //  this.setValueOf('contents', 'contents');
                
            } else {

            };
            //alert(this.getValueOf('contents'));
            
        }
        ,onOk: function () {
           var widget = this.widget,
                values = [], value;
            var dialog = this,
            data = {};
            this.commitContent(data);
            var auxvalue = data.contents;
            auxvalue = $('<div/>').text(auxvalue);
            var auxDiv = editor.document.createElement('div');
            auxDiv.setAttribute('class', 'hrcmathformula');
            // auxDiv.setAttribute('style', 'border:dashed;padding:5px;border-color:gray;border-width:1px;');
            auxvalue = auxvalue.html();
            auxDiv.setHtml(auxvalue);
            // editor.insertElement(auxDiv);
            if (widget != undefined) {

            } else {
                var element = editor.document.createElement('div');
                editor.insertElement(auxDiv);
                var widget = editor.widgets.initOn(auxDiv, 'hrcpasteformula');
            };
            widget.setData('contents', auxvalue);
        }
    };
})