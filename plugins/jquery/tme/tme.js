var gldatetimeadjust=0;
function digitalclock(pdatetimeadjust,pControlID){
    //hrcConsole_log('digitalclock-start-' + pdatetimeadjust);
    gldatetimeadjust= pdatetimeadjust;
    var newDate = new Date() ;
    newDate.setSeconds(newDate.getSeconds() + gldatetimeadjust);
   
    setInterval( function() {
        var newDate = new Date();
                newDate.setSeconds(newDate.getSeconds() + gldatetimeadjust);
        //hrcConsole_log(newDate);
        var auxValue;
        var auxString = '';
        
        auxString += newDate.getDate() + '/' + (newDate.getMonth() +1) + '/' + newDate.getFullYear() + ' ';
        auxString += newDate.getHours() + ':' + newDate.getMinutes() + ':'+ newDate.getSeconds() ;
        $('#' + pControlID + '_date').html(auxString);
	},1000);
	
  	

}