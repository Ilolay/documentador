function check(valtea,retsel,gastos,cuota) { 
    var loanamt = top.document.form1.amt.value;
    var paymnt = top.document.form1.pay.value;
	
	if(loanamt=="" || isNaN(parseFloat(loanamt)) || loanamt==0) { 
		alert("Please enter a valid loan amount.");
		top.document.form1.amt.value = "";
		top.document.form1.amt.focus();
		return false; 
	} else if(paymnt=="" || isNaN(parseFloat(paymnt)) || paymnt==0) {
		alert("Please enter a valid number of payments.");
		top.document.form1.pay.value = "";
		top.document.form1.pay.focus();
		return false;  
	} else {
		show(valtea,retsel,gastos,cuota); 
	}
}

function clearScreen() { 
    top.document.form1.amt.value = "";
    top.document.form1.pay.value = "";
	top.document.getElementById("pmt").innerHTML="";
	top.document.getElementById("det").innerHTML="";
}

function fixVal(value,numberOfCharacters,numberOfDecimals,padCharacter) { 
	var i, stringObject, stringLength, numberToPad;            // define local variables

	value=value*Math.pow(10,numberOfDecimals);                 // shift decimal point numberOfDecimals places
	value=Math.round(value);                                   //  to the right and round to nearest integer

	stringObject=new String(value);                            // convert numeric value to a String object
	stringLength=stringObject.length;                          // get length of string
	while(stringLength<numberOfDecimals) {                     // pad with leading zeroes if necessary
		stringObject="0"+stringObject;                     // add a leading zero
		stringLength=stringLength+1;                       //  and increment stringLength variable
	}

	if(numberOfDecimals>0) {			           // now insert a decimal point
		stringObject=stringObject.substring(0,stringLength-numberOfDecimals)+"."+
		stringObject.substring(stringLength-numberOfDecimals,stringLength);
	}

	if (stringObject.length<numberOfCharacters && numberOfCharacters>0) {
		numberToPad=numberOfCharacters-stringObject.length;      // number of leading characters to pad
		for (i=0; i<numberToPad; i=i+1) {
			stringObject=padCharacter+stringObject;
		}
	}

	return stringObject;                                       // return the string object
}

function show(valtea,retsel,gastos,cuota) {
    var amount = parseFloat(top.document.form1.amt.value);
	var numpay = parseInt(top.document.form1.pay.value);

	//El valor de tea 56, debe ser remplazado por la tasa efectiva anual que esta en la base
	//var tea = 56/100;
	var tea = valtea/100
	var tem = (tea / 365)*30;
	var payment=amount*(tem/(1-Math.pow((1+tem),-numpay)));
	var total=payment*numpay;
	var interest=total-amount;
	
	//El valor de retsellos 0.005, debe ser remplazado por la retencion de sellos que hay en la base
	//var retsellos=amount*0.005;
	var retsellos=amount*retsel;
	var capneto=amount-retsellos;
	
	//El valor de gastosadm 5, debe ser remplazado por el valor de gastos administrativos que hay en la base
	//var gastosadm=5;
	var gastosadm=gastos;
	//El valor de cuotasocial 5, debe ser remplazado por la cuota social que hay en la base
	//var cuotasocial=5;
	var cuotasocial=cuota;
	var output = "";
	var detail = "";
	paymentfinal = payment + cuotasocial + gastosadm;
	output += "<table align='center' style='width:90%;margin:10px'> \
			<tr><td>Capital:</td><td align='right'>$"+amount+"</td></tr><tr><td>Nro de Cuotas:</td> \
			<td align='right'>"+numpay+"</td></tr><tr><td>Retenci&oacute;n Sellos:</td><td align='right'>"+retsellos+"</td></tr> \
			<tr><td>Capital Neto:</td><td align='right'>"+capneto+"</td></tr><tr><td>Cuota:</td> \
			<td align='right'>$"+fixVal(payment,0,2,' ')+"</td></tr><tr><td>Gastos Administrativos:</td><td align='right'>$"+gastosadm+"</td></tr> \
			<tr><td>Cuota Social:</td><td align='right'>$"+cuotasocial+"</td></tr><tr><td><b>Cuota Final:</b></td><td align='right'><b>$"+fixVal(paymentfinal,0,2,' ')+"</b></td></tr></table>";

	detail += "<table border='0' align='center' cellpadding='5' cellspacing='0' width='97%' style='font-family:courier;font-size:12px'> \
			<tr><td align='center' valign='bottom' bgcolor='white'><b>Nro de Cuotas</b></td><td align='center' valign='bottom' bgcolor='white'><b>Cuota</b></td> \
			<td align='right' valign='bottom' bgcolor='white'><b>Inter&eacute;s</b></td><td align='right' valign='bottom' bgcolor='white'><b>Capital Amortizado</b></td> \
			<td align='center' valign='bottom' bgcolor='white'><b>Balance</b></td></tr><tr><td align='center' bgcolor='white'>0</td> \
			<td align='center' bgcolor='white'>&nbsp;</td><td align='center' bgcolor='white'>&nbsp;</td><td align='center' bgcolor='white'>&nbsp;</td> \
			<td align='right' bgcolor='white'>"+fixVal(amount,0,2,' ')+"</td></tr>";

	newPrincipal=amount;
	payment = payment;
	var i = 1;
	while (i <= numpay) {
		newInterest=30*newPrincipal*(tea/365);
		reduction=payment-newInterest;
		newPrincipal=newPrincipal-reduction;
		
		detail += "<tr><td align='center'>"+i+"</td><td align='right' bgcolor='white'>"+fixVal(payment,0,2,' ')+"</td> \
				<td align='right' bgcolor='white'>"+fixVal(newInterest,0,2,' ')+"</td> \
				<td align='center' bgcolor='white'>"+fixVal(reduction,0,2,' ')+"</td> \
				<td align='center' bgcolor='white'>"+fixVal(newPrincipal,0,2,' ')+"</td></tr>";

		i++;
	}

	detail += "</table>";

	document.getElementById("pmt").innerHTML = output;
	document.getElementById("det").innerHTML = detail;
}
